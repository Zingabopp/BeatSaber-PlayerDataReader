using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BeatSaber_PlayerDataReader.ConversionHashMapData;

namespace BeatSaber_PlayerDataReader
{
    public static class IDConverter
    {
        public static Dictionary<string, string> ConvertIDs(List<LevelStatsData> levelData, SongHashDataModel hashCache)
        {
            var returnDict = new Dictionary<string, string>();
            if (levelData == null)
                throw new ArgumentNullException("Argument PlayerDataModel playerData cannot be null.");
            if (hashCache == null)
                throw new ArgumentNullException("Argument SongHashDataModel hashCache cannot be null.");

            int convertedCount = 0;
            int skippedCount = 0;
            var ToRemove = new List<LevelStatsData>();

            // Convert newer levels that have the directory name in the ID
            foreach (var level in levelData.Where(s => !string.IsNullOrEmpty(s.directory)))
            {
                if (!string.IsNullOrEmpty(level.hash))
                {
                    var newMatch = levelData.SingleOrDefault(s => !string.IsNullOrEmpty(s.hash) && s.hash.ToUpper() == level.hash &&
                        s.difficulty == level.difficulty && string.IsNullOrEmpty(s.directory));
                    if(newMatch != null)
                    {
                        newMatch.UpdateLevelData(level);
                        convertedCount++;
                        ToRemove.Add(level);
                    }
                    else
                    {
                        level.levelId = $"custom_level_{level.hash}";
                        convertedCount++;
                    }
                }
                else
                    skippedCount++;
            }
            foreach (var item in ToRemove)
            {
                levelData.Remove(item);
            }

            foreach (var level in levelData.Where(s => !string.IsNullOrEmpty(s.hash) && s.hash.Count() == 32))
            {
                string oldId = level.levelId;
                if (HashMap.ContainsKey(level.hash.ToUpper()))
                {
                    var newHash = HashMap[level.hash.ToUpper()].ToUpper();
                    var newMatch = levelData.SingleOrDefault(s => !string.IsNullOrEmpty(s.hash) &&
                        (s.hash.ToUpper() == newHash) && (s.difficulty == level.difficulty));
                    if (newMatch != null)
                    {
                        newMatch.UpdateLevelData(level);
                        ToRemove.Add(level);
                    }
                    else
                        level.levelId = $"custom_level_{HashMap[level.hash.ToUpper()].ToUpper()}";
                    returnDict.AddOrUpdate(oldId, level.levelId);
                }
                else
                {
                    skippedCount++;
                }

            }



            foreach (var item in ToRemove)
            {
                levelData.Remove(item);
            }

            return returnDict;
        }

        public static void UpdateLevelData(this LevelStatsData target, LevelStatsData source)
        {
            target.highScore = Math.Max(target.highScore, source.highScore);
            target.maxCombo = Math.Max(target.maxCombo, source.maxCombo);
            target.maxRank = Math.Max(target.maxRank, source.maxRank);
            target.fullCombo = source.fullCombo ? source.fullCombo : target.fullCombo;
            target.validScore = source.validScore ? source.validScore : target.validScore;
            target.playCount = target.playCount + source.playCount;
        }
    }
}
