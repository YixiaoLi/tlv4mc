using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class NullObjectOfTraceLogParser : ITraceLogParser, INullObjectOfParser
    {
        protected TraceLogParser _parser;

        /// <summary>
        /// 既にパースが成功しているかどうか。
        /// 具体的には、ParserCombinatorsクラスのORメソッドを通った場合true。
        /// </summary>
        public bool Success{ get; set; }


        #region コンストラクタ
        protected NullObjectOfTraceLogParser() {}

        public NullObjectOfTraceLogParser(TraceLogParser parser)
        {
            _parser = parser;
        }
        #endregion
              
        
        #region ITraceLogParser メンバ

        #region プロパティ
        public string TimeValue
        {
            set{ /* 何もしない */ }
        }

        public string ObjectValue
        {
            set
            {
                if (Success)
                {
                    _parser.ObjectValue = value;
                }
            }
        }

        public string ObjectNameValue
        {
            set { /* 何もしない */ }
        }

        public string ObjectTypeValue
        {
            set { /* 何もしない */ }
        }

        public string BehaviorValue
        {
            set { /* 何もしない */ }
        }

        public string AttributeValue
        {
            set { /* 何もしない */ }
        }

        public string ValueValue
        {
            set { /* 何もしない */ }
        }

        public string ArgumentsValue
        {
            set { /* 何もしない */ }
        }

        public bool HasTimeValue
        {
            set { /* 何もしない */ }
        }

        public bool HasObjectNameValue
        {
            set { /* 何もしない */ }
        }

        public bool HasObjectTypeValue
        {
            set { /* 何もしない */ }
        }

#endregion


        public ITraceLogParser Line()
        {
            return this;
        }

        public ITraceLogParser Time()
        {
            return this;
        }

        // "[Time]"がないログもパースするために、_parse側のEvent()を実行している
        public ITraceLogParser Event()
        {
            return _parser.Event();
        }

        public ITraceLogParser OBject()
        {
            return this;
        }

        public ITraceLogParser ObjectName()
        {
            return this;
        }

        public ITraceLogParser ObjectTypeName()
        {
            return this;
        }

        public ITraceLogParser AttributeCondition()
        {
            return this;
        }

        public ITraceLogParser BooleanExpression()
        {
            return this;
        }

        public ITraceLogParser NextBooleanExpression()
        {
            return this;
        }

        public ITraceLogParser Boolean()
        {
            return this;
        }

        public ITraceLogParser ComparisonExpression()
        {
            return this;
        }

        public ITraceLogParser AttributeName_ComparisonExpression()
        {
            return this;
        }

        public ITraceLogParser Value_ComparisonExpression()
        {
            return this;
        }

        public ITraceLogParser AttributeName()
        {
            return this;
        }

        public ITraceLogParser LogicalOpe()
        {
            return this;
        }

        public ITraceLogParser ComparisonOpe()
        {
            return this;
        }

        public ITraceLogParser Value()
        {
            return this;
        }

        public ITraceLogParser AttributeOrBehavior()
        {
            return this;
        }

        public ITraceLogParser AttributeChange()
        {
            return this;
        }

        public ITraceLogParser BehaviorHappen()
        {
            return this;
        }

        public ITraceLogParser BehaviorName()
        {
            return this;
        }

        public ITraceLogParser Arguments()
        {
            return this;
        }

       
        #endregion


        #region 文字パーサ メンバ

        public ITraceLogParser Char(char c)
        {
            return this;
        }

        public ITraceLogParser Alpha()
        {
            return this;
        }

        public ITraceLogParser Num()
        {
            return this;
        }

        public ITraceLogParser AlphaNum()
        {
            return this;
        }

        public ITraceLogParser AnyCharOtherThan(char c)
        {
            return this;
        }

        public ITraceLogParser AnyCharOtherThan(char c1, char c2)
        {
            return this;
        }

        public ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3)
        {
            return this;
        }

        public ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3, char c4)
        {
            return this;
        }

        public ITraceLogParser AnyCharOtherThan(char[] clist)
        {
            return this;
        }
        #endregion


        #region パーサコンビネータ メンバ

        public ITraceLogParser Many(Func<ITraceLogParser> f)
        {
            return this;
        }

        public ITraceLogParser Many1(Func<ITraceLogParser> f)
        {
            return this;
        }

        public ITraceLogParser OR()
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

        #endregion
    }
}
