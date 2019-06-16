using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaber_PlayerDataReader
{
    public static class DictonaryExtensions
    {
        /// <summary>
        /// Adds the given key and value to the dictionary. If they key already exists, updates the value.
        /// Returns true if the key already exists.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if the key already exists, false otherwise.</returns>
        public static bool AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return true;
            }
            dict.Add(key, value);
            return false;
        }
    }
}
