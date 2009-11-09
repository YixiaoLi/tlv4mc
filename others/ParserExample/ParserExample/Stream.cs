using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserExample 
{
    public class Stream<T> where T : struct
    {
        private readonly T[] xs;
        private int index;

        public Stream(T[] xs) {
            this.xs = xs;
            this.index = 0;
        }

        public struct State {
            internal int index;
            internal State(int index) { this.index = index; }
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
        
        public T? Peek() {
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
