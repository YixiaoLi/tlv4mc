using System;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace
{

    public class ChildrenTableAddedEventArgs<T> : EventArgs
    {
        public T AddedChild { get; protected set; }
        public ChildrenTableAddedEventArgs(T child)
        {
            this.AddedChild = child;
        }
    }

    public interface ITreeStructure<T>
        where T : ITreeStructure<T>, IElement
    {
        T Parent { get; set; }
        ChildrenTable<T> Children { get; }
        void Add(T control);
    }

    public class ChildrenTable<T> : ElementTable<T>
        where T : ITreeStructure<T>, IElement
    {
        public T Holder { get; protected set; }

        public event EventHandler<ChildrenTableAddedEventArgs<T>> Added = null;

        public ChildrenTable(T holder)
        {
            this.Holder = holder;
        }

        public override void Add(T child)
        {
            base.Add(child);
            child.Parent = Holder;
            if (Added != null)
            {
                Added(this, new ChildrenTableAddedEventArgs<T>(child));
            }
        }
    }

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
