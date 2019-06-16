using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaber_PlayerDataReader;

namespace PlayerDataReader_Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ConversionHashMapTests();
            PlayerDataTests();

            SongHashTests();
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
    }
}
