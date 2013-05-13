/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
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
    public abstract class NullObjectOfParser : IParser
    {
        /// <summary>
        /// 対になるパーサクラス
        /// </summary>
        protected Parser _parser;

        /// <summary>
        /// 既にパースが成功しているかどうか。
        /// 具体的には、ParserCombinatorsクラスのORメソッドを通った場合true。
        /// </summary>
        public bool Success { get; set; }
        
        
        #region IParser メンバ

        public IParser End()
        {
            if (Success)
            {
                return _parser.End();
            }
            else
            {
                _parser.Reset();
                _parser.End();
                return this;
            }
        }        


        #region 文字パーサ メンバ

        public IParser Char(char c)
        {
            return this;
        }

        public IParser Alpha()
        {
            return this;
        }

        public IParser Num()
        {
            return this;
        }

        public IParser AlphaNum()
        {
            return this;
        }

        public IParser AnyCharOtherThan(char c)
        {
            return this;
        }

        public IParser AnyCharOtherThan(char c1, char c2)
        {
            return this;
        }

        public IParser AnyCharOtherThan(char c1, char c2, char c3)
        {
            return this;
        }

        public IParser AnyCharOtherThan(char c1, char c2, char c3, char c4)
        {
            return this;
        }

        public IParser AnyCharOtherThan(char[] clist)
        {
            return this;
        }

        public IParser Epsilon()
        {
            return this;
        }
        #endregion

        #region パーサコンビネータ メンバ

        public IParser Many<TParser>(Func<TParser> f)
        {
            return this;
        }

        public IParser Many1<TParser>(Func<TParser> f)
        {
            return this;
        }

        public IParser OR()
        {
            if (Success)
            {
                return this;
            }
            else
            {
                _parser.Reset();
                return _parser;
            }
        }

        #endregion

        #endregion
    }
}
