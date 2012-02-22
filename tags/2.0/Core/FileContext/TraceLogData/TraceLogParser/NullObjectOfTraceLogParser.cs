/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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

        public ITraceLogParser Event()
        {
            return this;
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
