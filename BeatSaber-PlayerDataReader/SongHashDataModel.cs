﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeatSaber_PlayerDataReader
{
    [Serializable]
    public class SongHashDataModel
    {
        public FileInfo CurrentFile;
        public static readonly string DEFAULT_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low", @"Hyperbolic Magnetism\Beat Saber");
        public const string DEFAULT_FILE_NAME = "SongHashData.dat";
        public Dictionary<string, HashData> Data;
        public SongHashDataModel()
        {
            Data = new Dictionary<string, HashData>();
        }

        public void Initialize(string filePath = "")
        {
            var test = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (string.IsNullOrEmpty(filePath))
                filePath = Path.Combine(DEFAULT_FOLDER, DEFAULT_FILE_NAME);
            if (File.Exists(filePath))
            {
                var str = File.ReadAllText(filePath);
                JsonConvert.PopulateObject(str, Data);
                CurrentFile = new FileInfo(filePath);
            }
        }

    }

    [Serializable]
    public class HashData
    {
        [JsonProperty("directoryHash")]
        string directoryHash { get; set; }
        [JsonProperty("songHash")]
        string songHash { get; set; }
    }
}
