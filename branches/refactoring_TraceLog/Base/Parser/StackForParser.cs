﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class StackForParser
    {
        /// <summary>
        /// スタック配列にて扱うデータ
        /// </summary>
        public class Memory
        {
            /// <summary>
            /// ある段階でのパース結果
            /// </summary>
            public StringBuilder Result{get;set;}

            /// <summary>
            /// ある段階での文字位置
            /// </summary>
            public int InputIndex{get;set;}
            public Memory()
            {
                this.Result = new StringBuilder();
                this.InputIndex = 0;
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

        /// <summary>
        /// スタックの最上位要素を取得する。
        /// </summary>
        /// <returns>スタックの最上位要素のMemory</returns>
        public Memory Peek()
        {
            return _memory[_index];
        }


        /// <summary>
        /// スタックのPop処理。
        /// <para>　</para>
        /// <para>例外：IndexOutOfRangeException</para>
        /// </summary>
        /// <returns>スタックの最上位要素のMemory</returns>
        public Memory Pop()
        {
            return _memory[_index--];
        }


        /// <summary>
        /// スタックのPush処理。
        /// <para>　</para>
        /// <para>例外：IndexOutOfRangeException</para>
        /// </summary>
        /// <param name="index">InputStreamForParser._indexを想定している</param>
        public void Push(int index)
        {
            ++_index;
            _memory[_index].Result.Length = 0;
            _memory[_index].InputIndex = index;
        }


        // スタックのオーバフロー、アンダーフローが起きることを想定していないため、
        // IndexOutOfRangeExceptionのthrowによって検出するようにしている。
        //public bool IsEmpty()
        //{
        //    return _index == 0;
        //}
    }
}