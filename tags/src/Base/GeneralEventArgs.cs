using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    [SerializableAttribute]
    public delegate TResult EventHandler<TResult, TEventArgs>(Object sender, TEventArgs e) where TEventArgs : EventArgs;

    public class GeneralEventArgs<T> : EventArgs
    {
        public T Arg { get; private set; }

        public GeneralEventArgs(T arg)
        {
            Arg = arg;
        }
    }

    public class GeneralChangedEventArgs<T> : EventArgs
    {
        public bool IsAutonomic { get; set; }
        public T New { get; private set; }
        public T Old { get; private set; }

        public GeneralChangedEventArgs(T o, T n)
        {
            Old = o;
            New = n;
            IsAutonomic = true;
        }
    }
}
