using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
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
