using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base.ParserCombinators
{
    public class Stream<T> where T : struct
    {
        private T[] xs;
        private int index;

        public Stream() {
            this.xs = null;
            this.index = 0;
        }

        public struct State {
            internal int index;
            internal State(int index) { this.index = index; }
        }

        public void Write(T[] xs)
        {
            this.xs = xs;
            this.index = 0;
        }

        public State Save() {
            return new State(this.index);
        }
        public void Restore(State s) {
            this.index = s.index;
        }

        
        public T Read() {
            return this.xs[this.index++];
        }
        
        public T Peek() {
            if (IsEmpty())
            {
                return null;
            }
            else 
            {
                return this.xs[this.index];
            }
        }

        public bool IsEmpty() {
            return this.index >= this.xs.Length; 
        }
    }
}
