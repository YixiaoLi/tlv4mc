using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// パース対象を扱う。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputStreamForParser<T> where T : struct
    {
        private T[] _xs;
        private int _index;

        public InputStreamForParser() {
            this._xs = null;
            this._index = 0;
        }

        /// <summary>
        /// パース対象をセットする。
        /// </summary>
        /// <param name="xs">パース対象</param>
        public void Write(T[] xs)
        {
            this._xs = xs;
            this._index = 0;
        }

        /// <summary>
        /// 現在のパース位置を取得して、外部で記憶するときに使用する。
        /// </summary>
        /// <returns>現在のパース位置</returns>
        public int Save() {
            return this._index;
        }


        /// <summary>
        /// 現在のパース位置を変更します。
        /// 主に、パースに失敗して失敗前のパース位置に戻すときに使用。
        /// <para>注意：Save()で取得した値以外をセットしたときは動作保証しかねます。</para>
        /// </summary>
        /// <param name="s">パース位置</param>
        public void Restore(int s) {
            this._index = s;
        }

        /// <summary>
        /// 現在のパース位置にある要素を取り出す。
        /// </summary>
        /// <returns>現在のパース位置にある要素</returns>
        public T Read() {
            return this._xs[this._index++];
        }

        /// <summary>
        /// 現在のパース位置にある要素を取得する。パース位置を変更せず行う。
        /// </summary>
        /// <returns></returns>
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
