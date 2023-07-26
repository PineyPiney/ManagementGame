using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace PineyPiney.Util
{
    public static class Funcs
    {
        public static Color32 ColourFromHex(uint hex)
        {
            return new Color32((byte)((hex >> 16) & 255), (byte)((hex >> 8) & 255), (byte)(hex & 255), (byte)((hex >> 24) & 255));
        }

        public static Color32 ColourFromHexString(string hex)
        {
            string trim = hex.Trim().TrimStart('#');
            uint num = uint.Parse(trim, System.Globalization.NumberStyles.HexNumber);
            if (trim.Length < 7) num |= 0xff000000; // If the string is less than 7 characters long then make the alpha channel 255
            return ColourFromHex(num);
        }
        public static string HexStringFromColour(Color32 colour)
        {
            return colour.r.ToString("X2") + colour.g.ToString("X2") + colour.b.ToString("X2");
        }

        public static List<T> ReadMenuFile<T>(string fileDirectory, Func<Dictionary<string, string>, int, T> predicate)
        {
            StreamReader reader = new StreamReader(fileDirectory);
            var list = new List<T>();
            int i = 0;
            string[] lines = reader.ReadToEnd().Split('\n');
            foreach(string line in lines)
            {
                var dict = new Dictionary<string, string>();
                string[] parts = line.Split(':');
                foreach(string part in parts)
                {
                    int index = part.IndexOf('[') + 1;
                    int indey = part.IndexOf(']');
                    string d = part.Substring(index, indey - index);
                    string f = part.Substring(indey + 1).Trim();
                    dict[d] = f;
                }
                list.Add(predicate(dict, i++));
            }
            reader.Close();
            return list;
        }

        public static Dictionary<K, V> ReadMenuFileDict<K, V>(string fileDirectory, Func<Dictionary<string, string>, int, (K, V)> predicate)
        {
            var list = ReadMenuFile<(K, V)>(fileDirectory, predicate);
            return list.ToDictionary(l => l.Item1, l => l.Item2);
        }
    }
}
