/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Parser;

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
        new ITraceLogParser OR();
        #endregion
        
        #region 文字パーサ
        /// <summary>
        /// 指定の一文字をパースする
        /// </summary>
        /// <param name="c">パースしたい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser Char(char c);
        /// <summary>
        /// アルファベットをパースする
        /// </summary>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser Alpha();
        /// <summary>
        /// 数字をパースする
        /// </summary>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser Num();
        /// <summary>
        /// アルファベットと数字をパースする
        /// </summary>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser AlphaNum();

        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser AnyCharOtherThan(char c);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser AnyCharOtherThan(char c1, char c2);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="c1">除外したい文字</param>
        /// <param name="c2">除外したい文字</param>
        /// <param name="c3">除外したい文字</param>
        /// <param name="c4">除外したい文字</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3, char c4);
        /// <summary>
        /// 指定した文字以外の文字をパースする
        /// </summary>
        /// <param name="clist">除外したい文字を集めた配列</param>
        /// <returns>成功：TraceLogParser, 失敗：NullObject</returns>
        new ITraceLogParser AnyCharOtherThan(char[] clist);
        /// <summary>
        /// 空文字列をパースするεを表す。
        /// なのでスペースなどをパースするものではない。
        /// </summary>
        /// <returns>TraceLogParser</returns>
        new ITraceLogParser Epsilon();
        #endregion
               
    }
}
