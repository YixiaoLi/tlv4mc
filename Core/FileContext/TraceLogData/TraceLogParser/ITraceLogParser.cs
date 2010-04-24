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

        #region TraceLogのパーサ(パースメソッド)
        // 各メソッド名は /doc/rule-manual.pdf の「2.1.2標準形式ログの定義」に従う。
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
        ITraceLogParser Argument();
        ITraceLogParser NextArgument();
        #endregion

        #region パーサコンビネータ
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を複数回適用する。
        /// 正規表現の"*"に相当する。
        /// </summary>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>TraceLogParser</returns>
        ITraceLogParser Many(Func<ITraceLogParser> f);
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を1回以上適用する。
        /// 正規表現の"+"に相当する。
        /// </summary>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser Many1(Func<ITraceLogParser> f);
        /// <summary>
        /// パーサ間のORをとる。
        /// ORの前までにパースが失敗した場合、ORの後のパーサで再度パースを試みる。
        /// ORの前までのパーサでパースできた場合、ORの後のパーサは無視する。
        /// </summary>
        /// <returns>これ以前のパースに成功：NullObject, これ以前のパースに失敗：TraceLogParser</returns>
        ITraceLogParser OR();
        #endregion
        
        #region 文字パーサ
        /// <summary>
        /// 指定の一文字をパースする
        /// </summary>
        /// <param name="c">パースしたい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser Char(char c);
        /// <summary>
        /// アルファベットをパースする
        /// </summary>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser Alpha();
        /// <summary>
        /// 数字をパースする
        /// </summary>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser Num();
        /// <summary>
        /// アルファベットと数字をパースする
        /// </summary>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser AlphaNum();

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser AnyCharOtherThan(char c);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser AnyCharOtherThan(char c1, char c2);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <param name="c4">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3, char c4);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="clist">除外したい文字を集めた配列</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        ITraceLogParser AnyCharOtherThan(char[] clist);
        /// <summary>
        /// 空文字列をパースするεを表す。
        /// なのでスペースなどをパースするものではない。
        /// </summary>
        /// <returns>TraceLogParser</returns>
        ITraceLogParser Epsilon();
        #endregion
               
    }
}
