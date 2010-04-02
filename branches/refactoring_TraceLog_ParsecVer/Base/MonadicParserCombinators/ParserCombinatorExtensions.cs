using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /*
     * ＜参考＞
     * LukeH's WebLog : Monadic Parser Combinators using C# 3.0
     * http://blogs.msdn.com/lukeh/archive/2007/08/19/monadic-parser-combinators-using-c-3-0.aspx
     */

    public static class ParserCombinatorExtensions
    {
        public static Parser<TInput, TValue> OR<TInput, TValue>(
        this Parser<TInput, TValue> parser1,
        Parser<TInput, TValue> parser2)
        {
            return input => parser1(input) ?? parser2(input);
        }
        public static Parser<TInput, TValue2> AND<TInput, TValue1, TValue2>(
            this Parser<TInput, TValue1> parser1,
            Parser<TInput, TValue2> parser2)
        {
            return input => parser2(parser1(input).Rest);
        }

    }
}
