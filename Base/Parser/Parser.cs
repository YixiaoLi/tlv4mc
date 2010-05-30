/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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

namespace NU.OJL.MPRTOS.TLV.Base
{
    public abstract class Parser : IParser
    {
        /// <summary>
        /// パース対象
        /// </summary>
        protected InputStreamForParser<char> _input = new InputStreamForParser<char>();


        /// <summary>
        /// パース中の文字列を退避させるスタック
        /// 一番下の子で初期化してください。
        /// </summary>
        /// <remarks>
        /// パーサ(メソッド)は、Beginメソッドにより、自分のパース結果を保持する
        /// 領域をStackに作成し、そこにパースした文字を追加していく。
        /// 無事にパースし終えると、Endメソッドにより、自分のパース結果をpopし、
        /// pop後の先頭要素に連結させる。
        /// </remarks>
        protected StackForParser _stack;


        /// <summary>
        /// パース失敗時を示すオブジェクト。NullObjectパターン。
        /// </summary>
        protected NullObjectOfParser _nullObject;


        #region コンストラクタ
        protected Parser() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="num">スタックの大きさ</param>
        protected Parser(int num)
        {
            this._stack = new StackForParser(num);
        }
        #endregion


        /// <summary>
        /// パースする。
        /// </summary>
        /// <param name="input">パース対象</param>
        public abstract void Parse(char[] input);

        public void Parse(string input)
        {
            Parse(input.ToCharArray());
        }



        /// <summary>
        /// パーサ(メソッド)の最初の処理。End()と対で使用。
        /// </summary>
        public void Begin()
        {
            // パース結果を入れる領域生成
            _stack.Push(_input.Save());
        }


        /// <summary>
        /// パーサ(メソッド)の最後の処理。Begin()と対で使用。
        /// </summary>
        /// <returns>this</returns>
        public IParser End()
        {
            Append(_stack.Pop().Result);
            _nullObject.Success = false;
            return this;
        }


        /// <summary>
        /// スタックの先頭の文字列領域に文字・文字列を付け加える。
        /// </summary>
        /// <param name="o">付け加えたい文字・文字列の情報が入ったもの</param>
        protected void Append(Object o)
        {
            _stack.Peek().Result.Append(o);
        }

        /// <summary>
        /// パース結果を得る
        /// </summary>
        /// <returns>パース結果</returns>
        protected string Result()
        {
            return _stack.Peek().Result.ToString();
        }


        /// <summary>
        /// スタックの先頭領域にある文字列の要素を全て削除し、
        /// Beginでセットしたところまでインデックスを戻す。
        /// </summary>
        public void Reset()
        {
            _stack.Peek().Result.Length = 0;
            _input.Restore( _stack.Peek().InputIndex);
        }



        #region 各種基本パーサ
        /*
         * ここにある各種基本パーサ(パースメソッド)へ、
         * サブクラスは同名のメソッドにて委譲します。
         * そうすることで、実質、サブクラスの型にキャストが可能となり、
         * 高い保守性を実現しています。
         */

        #region パーサコンビネータ
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を複数回適用する。
        /// 正規表現の"*"に相当する。
        /// </summary>
        /// <typeparam name="TParser">サブクラス(パーサクラス)の型</typeparam>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>this</returns>
        public IParser Many<TParser>(Func<TParser> f)
        {
            // パーサfでパースできないものが来るまでループ
            while (true)
            {
                if (f() is NullObjectOfParser) break;
            }

            return this;
        }


        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を1回以上適用する。
        /// 正規表現の"+"に相当する。
        /// </summary>
        /// <typeparam name="TParser">サブクラス(パーサクラス)の型</typeparam>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>成功：this、失敗：NullObject</returns>
        public IParser Many1<TParser>(Func<TParser> f)
        {
            if (f() is NullObjectOfParser)
            {
                return _nullObject;
            }
            else
            {
                return Many(f);
            }
        }


        /// <summary>
        /// パーサ間のORをとる。
        /// ORの前までにパースが失敗した場合、ORの後のパーサで再度パースを試みる。
        /// ORの前までのパーサでパースできた場合、ORの後のパーサは無視する。
        /// </summary>
        /// <returns>これ以前のパースに成功：NullObject</returns>
        public IParser OR()
        {
            _nullObject.Success = true;
            return _nullObject;
        }
        #endregion


        #region 文字パーサ

        /// <summary>
        /// 指定の一文字をパースする
        /// </summary>
        /// <param name="c">パースしたい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser Char(char c)
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }
            else if (_input.Peek() == c)
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// アルファベットをパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser Alpha()
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();
            if ((('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// 数字をパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser Num()
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();
            if (('0' <= c && c <= '9'))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// アルファベットと数字をパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser AlphaNum()
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();
            if ((('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')
                || ('0' <= c && c <= '9')))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }


        #region AnyCharOtherThanメソッド

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser AnyCharOtherThan(char c)
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }
            else if (_input.Peek() != c)
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser AnyCharOtherThan(char c1, char c2)
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();
            if (((c != c1) && (c != c2)))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser AnyCharOtherThan(char c1, char c2, char c3)
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();
            if (((c != c1) && (c != c2) && (c != c3)))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <param name="c4">除外したい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser AnyCharOtherThan(char c1, char c2, char c3, char c4)
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();
            if (((c != c1) && (c != c2) && (c != c3) && (c != c4)))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return _nullObject;
            }
        }

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="clist">除外したい文字を集めた配列</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        public IParser AnyCharOtherThan(char[] clist)
        {
            if (_input.IsEmpty())
            {
                return _nullObject;
            }

            var c = _input.Peek();

            foreach (char n in clist)
            {
                if (n == c) return _nullObject;
            }

            Append(_input.Read());
            return this;
        }

        /// <summary>
        /// 空文字列をパースするεを表す。
        /// なのでスペースなどをパースするものではない。
        /// </summary>
        /// <returns>this</returns>
        public IParser Epsilon()
        {
            return this;
        }
        #endregion
        #endregion

        #endregion
    }
}
