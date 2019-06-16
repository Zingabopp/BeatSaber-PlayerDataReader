using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace BeatSaber_PlayerDataReader
{
    /// <summary>
    /// Conversion dictionary for old song hashes to new song hashes.
    /// HashMap provided by lolPants.
    /// </summary>
    public static class ConversionHashMapData
    {
        private static Dictionary<string, string> _hashMap;

        /// <summary>
        /// Dictionary Key = Old Hash, Value = New Hash
        /// </summary>
        public static Dictionary<string, string> HashMap
        {
            get
            {
                if(_hashMap == null)
                {
                    _hashMap = new Dictionary<string, string>();
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BeatSaber_PlayerDataReader.hashmap.tsv"))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string[] hashes;
                        while (!reader.EndOfStream)
                        {
                            hashes = reader.ReadLine().Split(null);
                            if(!_hashMap.ContainsKey(hashes[0]))
                                _hashMap.Add(hashes[0], hashes[1]);
                        }
                    }
                }

                return _hashMap;
            }
        }

    }
}
