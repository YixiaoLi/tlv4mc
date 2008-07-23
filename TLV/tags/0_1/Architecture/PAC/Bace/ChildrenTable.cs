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
}
