using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public abstract class TraceLogParser<TInput> : CharParsers<TInput>
    {
        private TraceLogParseResult _result = new TraceLogParseResult();
        private StringBuilder _stringBilder = new StringBuilder();

        public Parser<TInput, string> Line;
        public Parser<TInput, string> Time;
        public Parser<TInput, string> Event;
        public Parser<TInput, string> Object;
        public Parser<TInput, string> ObjectName;
        public Parser<TInput, string> ObjectType;
        public Parser<TInput, string> Behavior;
        public Parser<TInput, string> Attribute;
        public Parser<TInput, char[]> Value;
        public Parser<TInput, char[]> Arguments;

        public Parser<TInput, string> AttributeChange;
        public Parser<TInput, string> BehaviorHappen;
        public Parser<TInput, string> AttributeCondition;
        public Parser<TInput, string> Name;

        public Parser<TInput, string> BooleanExp;
        public Parser<TInput, char[]> Bool;
        public Parser<TInput, string> NextBooleanExp;
        public Parser<TInput, string> ComparisonExp;
        public Parser<TInput, string> LogicalOpe;
        public Parser<TInput, string> ComparisonOpe;
        public Parser<TInput, char[]> ComparisonValue;

        public Parser<TInput, string> MinusTime;
        public Parser<TInput, string> Op;


        public TraceLogParser()
        {
            Name = (from n in Rep1(Char(char.IsLetterOrDigit).OR(Char('_')))
                    select new string(n))
                   .OR(
                   (from dl in Char('$')
                    from u1 in Char('{')
                    from n in Rep1(Char(char.IsLetterOrDigit).OR(Char('_')))
                    from u2 in Char('}')
                    select Base(dl).Append(u1).Append(n).Append(u2).ToString()));

            Op = from op in Rep1(Char(char.IsSymbol).OR(Char('!')))
                 select new string(op);

            LogicalOpe = from lo in Op
                         where lo == "&&" || lo == "||"
                         select lo;

            ComparisonValue = from c in Rep1(OtherThan(IsParenthesis))
                              select c;

            ComparisonOpe = from op in Op
                            where op == "==" || op == "!=" || op == ">"
                               || op == "<"  || op == "<=" || op == ">="
                            select op;

            ComparisonExp = from n in Name
                            from co in ComparisonOpe
                            from cv in ComparisonValue
                            select Base(n).Append(co).Append(cv).ToString();

            Bool = from b in Rep1(Char(char.IsLetter))
                   where new string(b) == "true" || new string(b) == "false"
                   select b;

            NextBooleanExp = (from lo in LogicalOpe
                              from be in BooleanExp
                              from nbe in NextBooleanExp
                              select Base(lo).Append(be).Append(nbe).ToString())
                             .OR(
                             (from n in Rep(Char(char.IsSymbol))
                              select new string(n)));


            BooleanExp = (from bo in Bool
                          from nbe in NextBooleanExp
                          select Base(bo).Append(nbe).ToString())
                         .OR(
                         (from ce in ComparisonExp
                          from nbe in NextBooleanExp
                          select Base(ce).Append(nbe).ToString()))
                         .OR(
                         (from u1 in Char('(')
                          from be in BooleanExp
                          from u2 in Char(')')
                          from nbe in NextBooleanExp
                          select Base(u1).Append(be).Append(u2).Append(nbe).ToString()));


            Arguments = from a in Rep(OtherThan(IsParenthesis))
                        select a;

            BehaviorHappen = from b in Name
                             from u1 in Char('(')
                             from a in Arguments
                             from u2 in Char(')')
                             select Base(_result.Behavior = b).Append(_result.Arguments = new string(a)).ToString();

            Value = from v in Rep1(AnyChar)
                    select v;

            AttributeChange = (from a in Name
                               from eq in Char('=')
                               from v in Value
                               select Base(_result.Attribute = a).Append(_result.Value = new string(v)).ToString())
                              .OR(
                              (from a in Name
                               select _result.Attribute = a));

            Object =  (from t in Name
                       from u1 in Char('(')
                       from ac in BooleanExp //Rep1(OtherThan(')'))
                       from u2 in Char(')')
                       select Base(_result.ObjectType = t).Append(u1).Append(ac).Append(u2).ToString())
                      .OR(
                      (from n in Name
                       select _result.ObjectName = n));
                       
            Event = (from o in Object
                     from dt in Char('.')
                     from ab in BehaviorHappen.OR(AttributeChange)
                     select _result.Object = o)
                    .OR(
                    (from o in Object
                     select _result.Object = o)); 


            // Timeとしてパースしたくないが正常に処理するために意味のない文字をselectしている
            MinusTime = from u1 in Char('[')
                        from m in Char('-')
                        from t in Rep1(Char(char.IsLetterOrDigit).OR(Char('.')))
                        from u2 in Char(']')
                        select u1.ToString();

            Time = from u1 in Char('[')
                   from t in Rep1(Char(char.IsLetterOrDigit).OR(Char('.')))
                   from u2 in Char(']')
                   select _result.Time = new string(t);


            Line = (from t in Time
                    from e in Event
                    select e)
                   .OR(
                   (from mt in MinusTime
                    from e in Event
                    select e))
                   .OR(
                   (from e in Event
                    select e));
        }


        private StringBuilder Base(Object s)
        {
            _stringBilder.Length = 0;
            _stringBilder.Append(s);
            return _stringBilder;
        }

        private bool IsParenthesis(char c)
        {
            return (c == '(' || c == ')');
        }

        public TraceLogParseResult Parse(TInput s)
        {
            _result.init();
            Line(s);
            return _result;
        }

        public Parser<TInput, string> Epsilon()
        {
            return input =>
                {
                    return new ParseResult<TInput, string>("", input);
                };
        }
    }
}
