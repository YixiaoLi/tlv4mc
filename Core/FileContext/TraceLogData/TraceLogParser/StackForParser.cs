using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class StackForParser
    {
        public class Memory
        {
            public StringBuilder result;
            public int inputIndex;
            public Memory()
            {
                this.result = new StringBuilder();
                this.inputIndex = 0;
            }
        }

        private Memory[] _memory;

        /// <summary>
        /// 現在のスタックの深さ。0は使用しない。
        /// </summary>
        private int _index;
        private readonly int MAX;

        public StackForParser(int max)
        {
            this._index = 0;
            this.MAX = max;
            this._memory = new Memory[max];
            for (int i = 0; i < max; i++)
            {
                this._memory[i] = new Memory();
            }
        }

        public Memory Peek()
        {
            return _memory[_index];
        }


        public Memory Pop()
        {
            return _memory[_index--];
        }

        public void Push(int index)
        {
            ++_index;
            _memory[_index].result.Length = 0;
            _memory[_index].inputIndex = index;
        }

        public bool IsFull()
        {
            return _index >= MAX - 1;
        }


        public bool IsEmpty()
        {
            return _index == 0;
        }
    }
}
