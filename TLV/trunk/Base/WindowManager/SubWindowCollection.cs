using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class SubWindowCollection
    {
        Dictionary<string, SubWindow> collection = new Dictionary<string, SubWindow>();

        public void Add(SubWindow subWindow)
        {
            collection.Add(subWindow.Name, subWindow);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(SubWindow subWindow)
        {
            return collection.ContainsValue(subWindow);
        }

        public bool Contains(string name)
        {
            return collection.ContainsKey(name);
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool Remove(SubWindow subWindow)
        {
            return collection.Remove(subWindow.Name);
        }

        public bool Remove(string name)
        {
            return collection.Remove(name);
        }

        public SubWindow this[string key]
        {
            get { return collection[key]; }
        }

        public IEnumerator<SubWindow> GetEnumerator()
        {
            return collection.Values.GetEnumerator();
        }
    }
}
