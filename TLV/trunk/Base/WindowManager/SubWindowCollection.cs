using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class SubWindowCollection
    {
        private Dictionary<string, SubWindow> _collection = new Dictionary<string, SubWindow>();

        public void Add(SubWindow subWindow)
        {
            if (_collection.ContainsKey(subWindow.Name))
                throw new ArgumentException("指定したSubWindowのNameプロパティと同じ値のNameプロパティをもつSubWindowがすでに格納されています");
            else
                _collection.Add(subWindow.Name, subWindow);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(SubWindow subWindow)
        {
            return _collection.ContainsValue(subWindow);
        }

        public bool Contains(string name)
        {
            return _collection.ContainsKey(name);
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public bool Remove(SubWindow subWindow)
        {
            return _collection.Remove(subWindow.Name);
        }

        public bool Remove(string name)
        {
            return _collection.Remove(name);
        }

        public SubWindow this[string key]
        {
            get { return _collection[key]; }
        }

        public IEnumerator<SubWindow> GetEnumerator()
        {
            return _collection.Values.GetEnumerator();
        }
    }
}
