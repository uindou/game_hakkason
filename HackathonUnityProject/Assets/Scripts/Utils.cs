using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

namespace Hackathon
{
    public class CsvReader
    {
        public char delimiter = ',';
        public List<string[]> ReadFile(string path)
        {
            TextAsset csvFile = Resources.Load(path) as TextAsset;
            List<string[]> data = new List<string[]>();
            StringReader sr = new StringReader(csvFile.text);
            while (sr.Peek() > -1)
            {
                string line = sr.ReadLine();
                data.Add(line.Split(delimiter));
            }
            return data;
        }
    }

    static class StringExtension
    {
        public static Color ToColor(this string self)
        {
            var color = default(Color);
            if (!ColorUtility.TryParseHtmlString(self, out color))
            {
                Debug.LogWarning("Unknown color code... " + self);
            }
            return color;
        }
    }

    static class IEnumerableExtensions
    {
        public static IEnumerable<X> Map<T, X>(this IEnumerable<T> e, Func<T, X> f)
        {
            foreach (var element in e)
            {
                yield return f(element);
            }
        }
    }
}