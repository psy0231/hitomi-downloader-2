/* Copyright (C) 2018. Hitomi Parser Developers */

using System;
using System.Collections.Generic;
using System.Linq;

namespace hitomi.Parser
{
    /// <summary>
    /// Includes tags, artists, series, and characters
    /// </summary>
    public class HitomiSearchElement
    {
        Dictionary<string, int> elements;

        public HitomiSearchElement(List<Tuple<string,int>> list)
        {
            list.Sort((x, y) => x.Item1.CompareTo(y.Item2));
            elements = list.ToDictionary(x => x.Item1, x => x.Item2);
        }

        public bool Exists(string elem)
        {
            return elements.ContainsKey(elem);
        }

        public int this[string key]
        {
            get { return elements[key]; }
        }
        
        public List<KeyValuePair<string, int>> GetElements()
        {
            return new List<KeyValuePair<string, int>> (elements.ToArray());
        }

        public List<KeyValuePair<string,int>> GetElementsCountSorted()
        {
            List<KeyValuePair<string, int>> result = GetElements();
            result.Sort((x, y) => x.Value.CompareTo(y.Value));
            return result;
        }

        public List<KeyValuePair<string,int>> GetElementsStartsWith(string elem)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            int start = 0;

            for (; start < elements.Count; start++)
                if (elements.ElementAt(start).Key.StartsWith(elem))
                    break;

            if (start == elements.Count) return result;

            for (; start < elements.Count; start++)
            {
                if (!elements.ElementAt(start).Key.StartsWith(elem))
                    break;
                result.Add(elements.ElementAt(start));
            }

            return result;
        }

        public List<KeyValuePair<string,int>> GetElementsContains(string elem)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            foreach (var pair in elements)
                if (pair.Key.Contains(elem))
                    result.Add(pair);
            return result;
        }

    }
}
