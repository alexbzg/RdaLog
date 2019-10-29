using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringIndexNS
{
    class StringIndex
    {
        private Dictionary<string, HashSet<string>> index = new Dictionary<string, HashSet<string>>();

        public void add(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 2) 
                for (int co=0; co < value.Length - 3; co++)
                {
                    string key = value.Substring(co, 3);
                    if (!index.ContainsKey(key))
                        index[key] = new HashSet<string>();
                    index[key].Add(value);
                }
        }

        public void remove(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 2)
                for (int co = 0; co < value.Length - 3; co++)
                {
                    string key = value.Substring(co, 3);
                    if (index.ContainsKey(key) && index[key].Contains(value))
                        index[key].Remove(value);
                }
        }

        public void clear()
        {
            index.Clear();
        }

        public List<string> search(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > 2)
            {
                string key = value.Substring(0, 3);
                if (index.ContainsKey(key))
                    return index[key].Where(item => item.Contains(value)).ToList();
            }
            return null;
        }

    }
}
