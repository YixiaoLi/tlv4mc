using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class TraceLogParser : CharParsers<TInput>
    {
        private TraceLogParseResult _result = new TraceLogParseResult();
        private StringBuilder _stringBilder = new StringBuilder();

        public Parser<TInput, TraceLogParseResult> Line;
        public Parser<TInput, char[]> Whitespace;
        public Func<char, Parser<TInput, char>> WsChr;
        public Parser<TInput, string> Time;
        public Parser<TInput, string> Event;
        public Parser<TInput, string> Object;
        public Parser<TInput, string> ObjectName;
        public Parser<TInput, string> ObjectType;
        public Parser<TInput, string> Behavior;
        public Parser<TInput, string> Attribute;
        public Parser<TInput, string> Value;
        public Parser<TInput, string> Arguments;

        public Parser<TInput, string> AttributeChange;
        public Parser<TInput, string> BehaviorHappen;
        public Parser<TInput, string> AttributeCondition;
        public Parser<TInput, string> Name;

        public TraceLogParser()
        {
            AttributeCondition = from;

            BehaviorHappen = from b in Behavior
                             from u1 in Char('(')
                             from ag in Arguments
                             from u2 in Char(')')
                             select Base(_result.Behavior = b).Append(_result.Arguments = ag).ToString();

            AttributeChange = from a in Attribute
                              from eq in Char('=')
                              from v in Value
                              select Base(_result.Attribute = a).Append(_result.Value = v).ToString();

            ObjectType = from n in Name
                         select n;

            ObjectName = from n in Name
                         select n;

            Object =  (from t in ObjectType
                       from u1 in Char('(')
                       from ac in AttributeCondition
                       from u2 in Char(')')
                       select Base(_result.ObjectType = t).Append(u1).Append(ac).Append(u2).ToString())
                      .OR(
                      (from n in ObjectName
                       select _result.ObjectName = n));
                       
            Event = from o in Object
                    from dt in Char('.')
                    from ab in AttributeChange.OR(BehaviorHappen)
                    select _result.Object = o; 

            Time = from u1 in Char('[')
                   from t in Rep1(Char('-').OR(Char(char.IsLetterOrDigit)))
                   from u2 in Char(']')
                   select _result.Time = t;

            Line = (from t in Time
                    from e in Event
                    select _result)
                   .OR(
                   (from e in Event
                    select _result));
        }


        private StringBuilder Base(string s)
        {
            _stringBilder.Length = 0;
            _stringBilder.Append(s);
            return _stringBilder;
        }


    }
}
