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

    // The result of a parse consists of a value and the unconsumed input.

    public class ParseResult<TInput, TValue>
    {
        public readonly TValue Value;
        public readonly TInput Rest;
        public ParseResult(TValue value, TInput rest) { Value = value; Rest = rest; }
    }

    // A Parser is a delegate which takes an input and returns a result.
    public delegate ParseResult<TInput, TValue> Parser<TInput, TValue>(TInput input);
}
