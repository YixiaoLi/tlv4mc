using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class Stream<T> where T : struct
    {
        private T[] _xs;
        private int _index;

        public Stream() {
            this._xs = null;
            this._index = 0;
        }

        public void Write(T[] xs)
        {
            this._xs = xs;
            this._index = 0;
        }

        public int Save() {
            return this._index;
        }
        public void Restore(int s) {
            this._index = s;
        }

        
        public T Read() {
            return this._xs[this._index++];
        }

        public T? Peek() {
            if (IsEmpty())
            {
                return null;
            }
            else 
            {
                return this._xs[this._index];
            }
        }

        public bool IsEmpty() {
            return this._index >= this._xs.Length; 
        }
    }
}
