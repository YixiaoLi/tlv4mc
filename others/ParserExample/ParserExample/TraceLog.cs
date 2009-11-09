using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserExample
{
    public class TraceLog
    {
        class Error : Exception { }
        public string Object;
        public string ObjectName;
        public string ObjectType;

        public TraceLog(string log) {
            var stream = new Stream<char>(log.ToCharArray());

            try
            {
                trying<char,Object>((s =>
                {
                    this.ObjectType = this.Object = this.ParseObjectType(s);
                    return null;
                }),stream);
            }
            catch (Error e)
            {
                this.ObjectName = this.Object = this.ParseObjectName(stream);
            }
        }

        private S trying<T,S>(Func<Stream<T>,S> f, Stream<T> stream) where T : struct {
            var state = stream.Save();
            try
            {
                return f(stream);
            }
            catch (Error e) {
                stream.Restore(state);
                throw e;
            }
        }

        private string ParseObjectName(Stream<char> stream) {
            List<char> xs = many<char,char>(AlphaNum, stream);
            return Str(xs);
        }

        private string ParseObjectType(Stream<char> stream) {
            List<char> xs = many<char, char>(AlphaNum, stream);
            Char('(', stream);
            List<char> lhs = many<char, char>(Alpha,stream);
            Char('=',stream);
            List<char> rhs = many<char, char>(AlphaNum,stream);
            Char(')',stream);

            return Str(xs) + "(" + Str(lhs) + "=" + Str(rhs) + ")";
        }

        private string Str(List<char> xs){
            return new string(xs.ToArray());
        }

        private void Char(char c, Stream<char> stream) {
            var x = stream.Peek();
            if (x.HasValue && x.Value == c)
            {
                stream.Read();
            }
            else
            {
                throw new Error();
            }
        }

        private char Alpha(Stream<char> stream)
        {
            char? c = stream.Peek();
            if (c.HasValue && (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')))
            {
                return stream.Read();
            }
            else
            {
                throw new Error();
            }
        }

        private char AlphaNum(Stream<char> stream){
            char? c = stream.Peek();
            if (c.HasValue && (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9')))
            {
                return stream.Read();
            }
            else {
                throw new Error();
            }
        }

        private List<S> many<T ,S>(Func<Stream<T>,S> f,Stream<T> stream ) where T : struct {
            var xs = new List<S>();

            try
            {
                while (true)
                {
                    xs.Add(f(stream));
                }
            }
            catch (Error e)
            {}
            return xs;
        }
    }
}
