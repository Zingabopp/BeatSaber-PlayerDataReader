using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeatSaber_PlayerDataReader
{
    public class PlayerDataModel
    {
        public static readonly string DEFAULT_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", @"Hyperbolic Magnetism\Beat Saber");
        public static readonly string BACKUP_FOLDER = Path.Combine(DEFAULT_FOLDER, "PlayerDataBackups");
        public const string DEFAULT_FILE_NAME = "PlayerData.dat";

        public static readonly Regex BackupFilePattern = new Regex(@"^(\d{8})-(\d{6}).(.+)", RegexOptions.Compiled);
        [JsonProperty("version")]
        public string version;
        [JsonProperty("localPlayers")]
        public List<PlayerData> localPlayers;
        [JsonProperty("guestPlayers")]
        public List<GuestPlayer> guestPlayers;
        [JsonProperty("lastSelectedBeatmapDifficulty")]
        public int lastSelectedBeatmapDifficulty;

        [JsonIgnore]
        public FileInfo CurrentFile;
        [JsonIgnore]
        public List<LevelStatsData> Data { get { return localPlayers?.FirstOrDefault()?.levelsStatsData; } }

        public PlayerDataModel()
        {

        }

        public FileInfo[] GetBackupFiles(string directory = "")
        {
            if (string.IsNullOrEmpty(directory))
                directory = BACKUP_FOLDER;
            var dir = new DirectoryInfo(directory);
            var matchingFiles = dir.EnumerateFiles().Where(f => f.Name.ToLower().EndsWith(CurrentFile.Name.ToLower()));
            List<FileInfo> backups = new List<FileInfo>();
            foreach (var item in matchingFiles)
            {
                Match match = BackupFilePattern.Match(item.Name);
                if(match.Success)
                {
                    backups.Add(item);
                }
            }
            return backups.ToArray();
        }

        public void Initialize(string filePath = "")
        {
            if (string.IsNullOrEmpty(filePath))
                filePath = Path.Combine(DEFAULT_FOLDER, DEFAULT_FILE_NAME);
            if (File.Exists(filePath))
            {
                var str = File.ReadAllText(filePath);
                JsonConvert.PopulateObject(str, this);
                CurrentFile = new FileInfo(filePath);
            }
        }

        public void WriteFile(string filePath = "")
        {
            FileInfo fileToWrite = CurrentFile;
            if (!string.IsNullOrEmpty(filePath))
                fileToWrite = new FileInfo(filePath);
            fileToWrite.Refresh();
            if (fileToWrite.Exists)
            {
                string copyPath = Path.Combine(BACKUP_FOLDER, $"{DateTime.Now.ToString("yyyyMMdd")}-{DateTime.Now.ToString("HHmmss")} {fileToWrite.Name}");
                if (!Directory.Exists(BACKUP_FOLDER))
                    Directory.CreateDirectory(BACKUP_FOLDER);
                fileToWrite.CopyTo(copyPath);
            }
            File.WriteAllText(fileToWrite.FullName, JsonConvert.SerializeObject(this));
        }

        public void UpdateHashes()
        {
            Dictionary<string, string> hashMap = new Dictionary<string, string>();
            string[] hashes;
            using (var stream = new StreamReader("hashmap.tsv"))
            {
                while (!stream.EndOfStream)
                {
                    hashes = stream.ReadLine().Split(null);
                    hashMap.AddOrUpdate(hashes[0], hashes[1]);
                }
            }
            foreach (var song in Data)
            {

            }
        }

    }

    [Serializable]
    public class PlayerData
    {
        public string playerId;
        public string playerName;
        public bool shouldShowTutorialPrompt;
        public bool agreedToEula;
        public PlayerGameplayModifiers gameplayModifiers;
        public PlayerSpecificSettings playerSpecificSettings;
        public Dictionary<string, PlayModeOverallStatsData> playerAllOverallStatsData;
        public List<LevelStatsData> levelsStatsData;
        public List<MissionStatsData> missionStatsData;
        public List<string> showedMissionHelpIds;
        public AchievementsData achievementsData;

    }
    [Serializable]
    public class LevelStatsData
    {
        [JsonIgnore]
        private string hash;
        [JsonIgnore]
        private string songName;
        [JsonIgnore]
        private string authorName;
        [JsonIgnore]
        private string _levelId; 
        public string levelId
        {
            get
            {
                return _levelId;
            }
            set
            {
                _levelId = value;
                string[] parts = value.Split('∎');
                if (parts.Count() > 3)
                {
                    hash = parts[0];
                    songName = parts[1];
                    authorName = parts[3];
                }

            }
        }
        public int difficulty;
        public string beatmapCharacteristicName;
        public int highScore;
        public int maxCombo;
        public bool fullCombo;
        public int maxRank;
        public bool validScore;
        public int playCount;
    }
    [Serializable]
    public class PlayerGameplayModifiers
    {
        public bool energyType;
        public bool noFail;
        public bool instaFail;
        public bool failOnSaberClash;
        public int enabledObstacleType;
        public bool fastNotes;
        public bool strictAngles;
        public bool disappearingArrows;
        public bool ghostNotes;
        public bool noBombs;
        public int songSpeed;
    }
    [Serializable]
    public class PlayerSpecificSettings
    {
        public bool staticLights;
        public bool leftHanded;
        public bool swapColors;
        public double playerHeight;
        public bool disableSFX;
        public bool reduceDebris;
        public bool noTextsAndHuds;
        public bool advancedHud;
    }

    [Serializable]
    public class PlayModeOverallStatsData
    {
        public int goodCutsCount;
        public int badCutsCount;
        public int missedCutsCount;
        public long totalScore;
        public int playedLevelsCount;
        public int cleardLevelsCount;
        public int failedLevelsCount;
        public int fullComboCount;
        public float timePlayed;
        public int handDistanceTravelled;
        public long cummulativeCutScoreWithoutMultiplier;
    }
    [Serializable]
    public class MissionStatsData
    {
        public string missionId;
        public bool cleared;
    }
    [Serializable]
    public class AchievementsData
    {
        public List<string> unlockedAchievements;

        public List<string> unlockedAchievementsToUpload;
    }
    [Serializable]
    public class GuestPlayer
    {
        public string playerName;
        public PlayerSpecificSettings playerSpecificSettings;
    }


}
