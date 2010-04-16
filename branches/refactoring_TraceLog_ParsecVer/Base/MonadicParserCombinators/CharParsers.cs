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

    // Contains a few parsers that parse characters from an input stream
    public abstract class CharParsers<TInput> : Parsers<TInput>
    {
        public abstract Parser<TInput, char> AnyChar { get; }

        public Parser<TInput, char> Char(char ch)
        {
            return from c in AnyChar where c == ch select c;
        }
        
        public Parser<TInput, char> Char(Predicate<char> pred)
        {
            return from c in AnyChar where pred(c) select c;
        }

        public Parser<TInput, char> OtherThan(char ch)
        {
            return from c in AnyChar where c != ch select c;
        }

        public Parser<TInput, char> OtherThan(Predicate<char> pred)
        {
            return from c in AnyChar where pred(c) == false select c;
        }
    }
}
