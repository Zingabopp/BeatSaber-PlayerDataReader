using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BeatSaber_PlayerDataReader;

namespace PlayerDataReader_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConversionHashMapTests();
            ConvertFavoritesTest();
            //ConvertIDTest();


            //PlayerDataTests();

            //SongHashTests();
        }

        public static void PlayerDataTests()
        {
            var playerData = new PlayerDataModel();
            playerData.Initialize();
            //playerData.WriteFile();
            var files = playerData.GetBackupFiles();
        }

        public static void SongHashTests()
        {
            var songHashes = new SongHashDataModel();
            songHashes.Initialize();
        }

        public static void ConversionHashMapTests()
        {
            var hashMap = ConversionHashMapData.HashMap;
        }

        public static void ConvertFavoritesTest()
        {
            var songHashes = new SongHashDataModel();
            songHashes.Initialize();
            string path = Path.Combine(PlayerDataModel.DEFAULT_FOLDER, "favoriteSongs.cfg");
            List<LevelStatsData> favorites = new List<LevelStatsData>();
            using (var stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    favorites.Add(new LevelStatsData() { levelId = stream.ReadLine() });
                }
            }
            var converted = IDConverter.ConvertIDs(favorites, songHashes);
            using (var writer = new StreamWriter(Path.Combine(PlayerDataModel.DEFAULT_FOLDER, "newfavoriteSongs.cfg")))
            {
                foreach (var item in favorites)
                {
                    writer.WriteLine(item.ToString());
                }
            }
        }

        public static void ConvertIDTest()
        {
            var playerData = new PlayerDataModel();
            playerData.Initialize();
            var songHashes = new SongHashDataModel();
            songHashes.Initialize();
            var bad = playerData.Data.Where(s => !string.IsNullOrEmpty(s.songName)).ToList();
            var converted = IDConverter.ConvertIDs(playerData.Data, songHashes);
            playerData.WriteFile();
        }
    }
}
