using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public interface ITraceLogParser : IParser
    {
        string TimeValue { set; }
        string ObjectValue { set; }
        string ObjectNameValue { set; }
        string ObjectTypeValue { set; }
        string BehaviorValue { set; }
        string AttributeValue { set; }
        string ValueValue { set; }
        string ArgumentsValue { set; }
        bool HasTimeValue { set; }
        bool HasObjectNameValue { set; }
        bool HasObjectTypeValue { set; }

        ITraceLogParser Line();
        ITraceLogParser Time();
        ITraceLogParser Event();
        ITraceLogParser OBject();
        ITraceLogParser ObjectName();
        ITraceLogParser ObjectTypeName();
        ITraceLogParser AttributeCondition();
        ITraceLogParser BooleanExpression();
        ITraceLogParser NextBooleanExpression();
        ITraceLogParser Boolean();
        ITraceLogParser ComparisonExpression();
        ITraceLogParser AttributeName_ComparisonExpression();
        ITraceLogParser Value_ComparisonExpression();
        ITraceLogParser AttributeName();
        ITraceLogParser LogicalOpe();
        ITraceLogParser ComparisonOpe();
        ITraceLogParser Value();
        ITraceLogParser AttributeOrBehavior();
        ITraceLogParser AttributeChange();
        ITraceLogParser BehaviorHappen();
        ITraceLogParser BehaviorName();
        ITraceLogParser Arguments();

        #region 文字パーサ
        /// <summary>
        /// 指定の一文字をパースする
        /// </summary>
        /// <param name="c">パースしたい文字</param>
        /// <returns>成功：this, 失敗：NullObject</returns>
        ITraceLogParser Char(char c);
        /// <summary>
        /// アルファベットをパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        ITraceLogParser Alpha();
        /// <summary>
        /// 数字をパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        ITraceLogParser Num();
        /// <summary>
        /// アルファベットと数字をパースする
        /// </summary>
        /// <returns>成功：this, 失敗：NullObject</returns>
        ITraceLogParser AlphaNum();

        ITraceLogParser AnyCharOtherThan(char c);
        ITraceLogParser AnyCharOtherThan(char c1, char c2);
        ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3);
        #endregion

        #region パーサコンビネータ
        ITraceLogParser Many(Func<ITraceLogParser> f);
        ITraceLogParser Many1(Func<ITraceLogParser> f);
        ITraceLogParser OR();
        #endregion
    }
}
