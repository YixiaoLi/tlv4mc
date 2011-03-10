/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base.Parser
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
