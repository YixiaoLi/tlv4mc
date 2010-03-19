using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base.ParserCombinators
{
    // 速度を重視したため、重複コードを許しています。
    abstract class CharParsers : ParserCombinators
    {
        protected List<T> Char(char c)
        {
            var x = _input.Peek();

            if (x.HasValue && x.Value == c)
            {
                return _input.Read();
            }
            else
            {
                throw new Exception();
            }
        }


        protected List<T> Alpha()
        {
            var x = _input.Peek();

            if (x.HasValue && (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')))
            {
                return _input.Read();
            }
            else
            {
                throw new Exception();
            }
        }


        protected List<T> Num()
        {
            var x = _input.Peek();

            if (x.HasValue && ('0' <= c && c <= '9'))
            {
                return _input.Read();
            }
            else
            {
                throw new Exception();
            }
        }


        protected List<T> AlphaNum()
        {
            if (x.HasValue && (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')
                || ('0' <= c && c <= '9')))
            {
                return _input.Read();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
