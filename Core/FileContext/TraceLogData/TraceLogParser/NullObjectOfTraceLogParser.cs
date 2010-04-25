using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class NullObjectOfTraceLogParser : NullObjectOfParser, ITraceLogParser
    {
        #region コンストラクタ
        protected NullObjectOfTraceLogParser() {}

        public NullObjectOfTraceLogParser(TraceLogParser parser)
        {
            base._parser = parser;
        }
        #endregion
              
        
        #region ITraceLogParser メンバ

        #region プロパティ
        public string TimeValue
        {
            set{ /* 何もしない */ }
        }

		// ObjectTypeName側のパースに成功した場合、こちらを実行することになる
        public string ObjectValue
        {
            set
            {
                if (Success)
                {
                    ((ITraceLogParser) _parser).ObjectValue = value;
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
            return ((ITraceLogParser)_parser).Event();
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

        public ITraceLogParser Argument()
        {
            return this;
        }

        public ITraceLogParser NextArgument()
        {
            return this;
        }
        #endregion



        // 以降、thisを返すのみのものは、速度の問題によりスーパークラスに委譲せず、再定義している。
        #region 文字パーサ メンバ

        public new ITraceLogParser Char(char c)
        {
            return this;
        }

        public new ITraceLogParser Alpha()
        {
            return this;
        }

        public new ITraceLogParser Num()
        {
            return this;
        }

        public new ITraceLogParser AlphaNum()
        {
            return this;
        }

        public new ITraceLogParser AnyCharOtherThan(char c)
        {
            return this;
        }

        public new ITraceLogParser AnyCharOtherThan(char c1, char c2)
        {
            return this;
        }

        public new ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3)
        {
            return this;
        }

        public new ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3, char c4)
        {
            return this;
        }

        public new ITraceLogParser AnyCharOtherThan(char[] clist)
        {
            return this;
        }

        public new ITraceLogParser Epsilon()
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

        public new ITraceLogParser OR()
        {
            return (ITraceLogParser)base.OR();
        }
        
        #endregion



    }
}
