using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace
{
    public class ElementTable<T>
        where T : IElement
    {
        protected Dictionary<string, T> table;

        public ElementTable()
        {
            table = new Dictionary<string, T>();
        }

        public T this[string name] { get { return table[name]; } }
        public int Count { get { return table.Count; } }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(T element in table.Values)
            {
                yield return element;
            }
        }

        public virtual void Add(T element)
        {
            table.Add(element.Name, element);
        }

        public bool Contains(string name)
        {
            return table.ContainsKey(name);
        }
    }
}
