using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
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
        protected INullObjectOfParser _nullObject;


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
    }
}
