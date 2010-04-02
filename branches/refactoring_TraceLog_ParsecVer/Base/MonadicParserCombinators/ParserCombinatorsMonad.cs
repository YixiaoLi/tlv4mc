﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base.MonadicParserCombinators
{
    /*
     * ＜参考＞
     * LukeH's WebLog : Monadic Parser Combinators using C# 3.0
     * http://blogs.msdn.com/lukeh/archive/2007/08/19/monadic-parser-combinators-using-c-3-0.aspx
     */

    public static class ParserCombinatorsMonad
    {
        // By providing Select, Where and SelectMany methods on Parser<TInput,TValue> we make the 
        // C# Query Expression syntax available for manipulating Parsers.  
        public static Parser<TInput, TValue> Where<TInput, TValue>(
            this Parser<TInput, TValue> parser,
            Func<TValue, bool> pred)
        {
            return input =>
            {
                var res = parser(input);
                if (res == null || !pred(res.Value)) return null;
                return res;
            };
        }

        public static Parser<TInput, TValue2> Select<TInput, TValue, TValue2>(
            this Parser<TInput, TValue> parser,
            Func<TValue, TValue2> selector)
        {
            return input =>
            {
                var res = parser(input);
                if (res == null) return null;
                return new Result<TInput, TValue2>(selector(res.Value), res.Rest);
            };
        }
        
        public static Parser<TInput, TValue2> SelectMany<TInput, TValue, TIntermediate, TValue2>(
            this Parser<TInput, TValue> parser,
            Func<TValue, Parser<TInput, TIntermediate>> selector,
            Func<TValue, TIntermediate, TValue2> projector)
        {
            return input =>
            {
                var res = parser(input);
                if (res == null) return null;
                var val = res.Value;
                var res2 = selector(val)(res.Rest);
                if (res2 == null) return null;
                return new Result<TInput, TValue2>(projector(val, res2.Value), res2.Rest);
            };
        }

    }
}
