/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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
    public class TraceLogParser : Parser, ITraceLogParser
    {
        private StringBuilder _time = new StringBuilder();
        private StringBuilder _object = new StringBuilder();
        private StringBuilder _objectName = new StringBuilder();
        private StringBuilder _objectType = new StringBuilder();
        private StringBuilder _behavior = new StringBuilder();
        private StringBuilder _attribute = new StringBuilder();
        private StringBuilder _value = new StringBuilder();
        private StringBuilder _arguments = new StringBuilder();


        #region プロパティ
        public string TimeValue
        {
            get
            {
                return HasTimeValue ? _time.ToString() : null;
            }
            set
            {
                _time.Length = 0;
                HasTimeValue = true;
                _time.Append(value);
            }
        }

		//　ObjectTypeName側が成功した場合、NullObjectのプロパティを介して設定
        public string ObjectValue
        {
            get
            {
                return _object.Length > 0 ? _object.ToString() : null;
            }
            set
            {
                _object.Length = 0;
                _object.Append(value);
            }
        }


        public string ObjectNameValue
        {
            get
            {
                return HasObjectNameValue ? _objectName.ToString() : null;
            }
            set
            {
                _objectName.Length = 0;
                HasObjectNameValue = true;
                _objectName.Append(value);
            }
        }


        public string ObjectTypeValue
        {
            get
            {
                return HasObjectTypeValue ? _objectType.ToString() : null;
            }
            set
            {
                _objectType.Length = 0;
                HasObjectTypeValue = true;
                _objectType.Append(value);
            }
        }


        public string BehaviorValue
        {
            get
            {
                return _behavior.Length > 0 ? _behavior.ToString() : null;
            }
            set
            {
                _behavior.Length = 0;
                _behavior.Append(value);
            }
        }


        public string AttributeValue
        {
            get
            {
                return _attribute.Length > 0 ? _attribute.ToString() : null;
            }
            set
            {
                _attribute.Length = 0;
                _attribute.Append(value);
            }
        }


        public string ValueValue
        {
            get
            {
                return _value.Length > 0 ? _value.ToString() : null;
            }
            set
            {
                _value.Length = 0;
                _value.Append(value);
            }
        }


        public string ArgumentsValue
        {
            get
            {
                // ""(空白文字列)がありえるため、_behaviorの有無に依存する
                return _behavior.Length > 0 ? _arguments.ToString() : null;
            }
            set
            {
                _arguments.Length = 0;
                _arguments.Append(value);
            }
        }

        // TimeValue、ObjectNameValue、ObjectTypeValueのsetにてtrueにしている
        public bool HasTimeValue { get; set; }
        public bool HasObjectNameValue { get; set; }
        public bool HasObjectTypeValue { get; set; }
        #endregion


        #region 定数

        private readonly char[] NON_OBJECTTYPENAME_CHAR = new char[] { '(', ')', '.', '!', '=', '<', '>' };
        private readonly char[] NON_ATTRIBUTENAME_CHAR = new char[] { '!', '=', '<', '>', '(', ')' };

        #endregion


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TraceLogParser()
            : base(50)
        {
            base._nullObject = new NullObjectOfTraceLogParser(this);
        }


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="input">パース対象</param>
        public void Init(char[] input)
        {
            _input.Write(input);

            _time.Length = 0;
            _object.Length = 0;
            _objectName.Length = 0;
            _objectType.Length = 0;
            _behavior.Length = 0;
            _attribute.Length = 0;
            _value.Length = 0;
            _arguments.Length = 0;
            HasTimeValue = false;
            HasObjectNameValue = false;
            HasObjectTypeValue = false;
        }

        public override void Parse(char[] input)
        {
            Init(input);

            // パース開始
            Line();
        }


        #region ITraceLogParser メンバ

        public ITraceLogParser Line()
        {
            Begin();

            var line = Char('[').Time().Char(']').Event()
                       .OR().
                       Event();  // 不完全な標準形式トレースログだが、可視化ルール等に記述されているため必要

            line.End();
            return this;
        }

        public ITraceLogParser Time()
        {
            Begin();

            var time = Many1(AlphaNum).Many(() => Char('.')).Many(AlphaNum);

            time.TimeValue = Result();

            // マイナス値であったときも正しいが、Timeとして保持しないため吐き捨てておく
            time = time.OR().Char('-').Many1(AlphaNum).Many(() => Char('.')).Many(AlphaNum);

            return (ITraceLogParser)time.End();
        }

        public ITraceLogParser Event()
        {
            Begin();

            var event_ = OBject().Char('.').AttributeOrBehavior()
                         .OR().
                         OBject();  // 不完全な標準形式トレースログだが、可視化ルール等に記述されていたり、マクロ適用時に使用するため必要

            return (ITraceLogParser)event_.End();
        }

        public ITraceLogParser OBject()
        {
            Begin();

            // ObjectTypeNameとObjectNameの順番を変更しないでください
            var object_ =
                ObjectTypeName().Char('(').AttributeCondition().Char(')')
                .OR().
                ObjectName();

            object_.ObjectValue = Result();

            // HasObjectTypeValueは、ObjectTypeName()が真でも、ほかで失敗すれば偽である。
            object_.HasObjectTypeValue = false;
            return (ITraceLogParser)object_.End();
        }

        public ITraceLogParser ObjectName()
        {
            Begin();

            var name = Many1(() => AnyCharOtherThan('(', ')', '.'));

            name.ObjectNameValue = Result();
            return (ITraceLogParser)name.End();
        }

        public ITraceLogParser ObjectTypeName()
        {
            Begin();

            var typeName = Many1(() => AnyCharOtherThan('(', ')', '.'));

            typeName.ObjectTypeValue = Result();
            return (ITraceLogParser)typeName.End();
        }

        public ITraceLogParser AttributeCondition()
        {
            Begin();

            var attributeCondition = BooleanExpression();

            return (ITraceLogParser)attributeCondition.End();
        }

        public ITraceLogParser BooleanExpression()
        {
            Begin();

            var booleanExpression =
                Boolean().NextBooleanExpression()
                .OR().
                ComparisonExpression().NextBooleanExpression()
                .OR().
                Char('(').BooleanExpression().Char(')').NextBooleanExpression();

            return (ITraceLogParser)booleanExpression.End();
        }

        public ITraceLogParser NextBooleanExpression()
        {
            Begin();

            var booleanExpression = LogicalOpe().BooleanExpression().NextBooleanExpression()
                                    .OR().
                                    Epsilon();

            booleanExpression.End();

            return this;
        }

        public ITraceLogParser Boolean()
        {
            Begin();

            var boolean =
                Char('t').Char('r').Char('u').Char('e')
                .OR().
                Char('f').Char('a').Char('l').Char('s').Char('e');

            return (ITraceLogParser)boolean.End();
        }

        public ITraceLogParser ComparisonExpression()
        {
            Begin();

            var comparisonExpression =
                AttributeName_ComparisonExpression().
                ComparisonOpe().
                Value_ComparisonExpression();

            return (ITraceLogParser)comparisonExpression.End();
        }

        public ITraceLogParser AttributeName_ComparisonExpression()
        {
            Begin();

            var attributeName = Many1(() => AnyCharOtherThan(NON_ATTRIBUTENAME_CHAR));

            return (ITraceLogParser)attributeName.End();
        }

        public ITraceLogParser Value_ComparisonExpression()
        {
            Begin();

            var value = Many(() => AnyCharOtherThan('(', ')', '&', '|')).
                        Char('(').Many(Value_ComparisonExpression).Char(')').
                        Many(Value_ComparisonExpression)
                        .OR().         // ここまでで例えば ...(..(..)..)().. といったカッコのネストをパース可能
                        Many1(() => AnyCharOtherThan('(', ')', '&', '|'));

            return (ITraceLogParser)value.End();
        }

        public ITraceLogParser AttributeName()
        {
            Begin();

            var attributeName = Many1(() => AnyCharOtherThan('!', '=', '<', '>'));

            attributeName.AttributeValue = Result();
            return (ITraceLogParser)attributeName.End();
        }

        public ITraceLogParser LogicalOpe()
        {
            Begin();

            var logicalOpe = Char('&').Char('&').OR().Char('|').Char('|');

            return (ITraceLogParser)logicalOpe.End();
        }

        public ITraceLogParser ComparisonOpe()
        {
            Begin();

            var comparisonOpe =
                      Char('=').Char('=')
                .OR().Char('!').Char('=')
                .OR().Char('<')
                .OR().Char('>')
                .OR().Char('<').Char('=')
                .OR().Char('>').Char('=');

            return (ITraceLogParser)comparisonOpe.End();
        }

        public ITraceLogParser Value()
        {
            Begin();

            // v1.3rc現在、Valueが一番最後の要素であるため、実質全ての文字をパースしています
            var value = Many1(() => AnyCharOtherThan(' '));

            value.ValueValue = Result();
            return (ITraceLogParser)value.End();
        }

        public ITraceLogParser AttributeOrBehavior()
        {
            Begin();

            // 注意：順番を変えないでください(次の理由が当てはまらなくなった場合のみ変えてください)。
            // ＜理由＞
            // AttributeChange()は、"="がないものも通るため、AttributeName()が真であればAttributeChange()も真である。
            // (なので、現状のAttributeChange()は常に真扱い)
            // BehaviorHappen()は、"(" ")"が必要なので、真偽どちらもありうる。
            var attributeOrBehavior = BehaviorHappen().OR().AttributeChange();

            // BehaviorValueは、BehaviorName()が真でも、ほかで失敗すればNullである。
            // また、attributeOrBehaviorがthisであるのは、BehaviorHappen()が偽である場合である。
            attributeOrBehavior.BehaviorValue = null;

            return (ITraceLogParser)attributeOrBehavior.End();
        }

        public ITraceLogParser AttributeChange()
        {
            Begin();

            var attributeCange = AttributeName().Char('=').Value();

            attributeCange.End();

            // "="がないログ形式も通るため、AttributeName()が真であればAttributeChange()も真である。
            // なので、現状は常に真扱い。
            return this;
        }

        public ITraceLogParser BehaviorHappen()
        {
            Begin();

            var behaviorHappen = BehaviorName().Char('(').Arguments().Char(')');

            return (ITraceLogParser)behaviorHappen.End();
        }

        public ITraceLogParser BehaviorName()
        {
            Begin();

            var behaviorName = Many1(() => AnyCharOtherThan('(', '='));

            behaviorName.BehaviorValue = Result();
            return (ITraceLogParser)behaviorName.End();
        }


        public ITraceLogParser Arguments()
        {
            Begin();

            var arguments = Argument().NextArgument();

            arguments.ArgumentsValue = Result();
            return (ITraceLogParser)arguments.End();
        }

        public ITraceLogParser NextArgument()
        {
            Begin();

            var nextArgument = Char(',').Argument().NextArgument()
                           .OR().
                           Epsilon();

            return (ITraceLogParser)nextArgument.End();
        }

        public ITraceLogParser Argument()
        {
            Begin();

            var argument = Many(() => AnyCharOtherThan('(', ')', ',')).
                           Char('(').Argument().Char(')').
                           Argument()
                           .OR().    // ここまでで例えば ...(..(..)..)().. といったカッコのネストをパース可能
                           Many(() => AnyCharOtherThan('(', ')', ','));

            return (ITraceLogParser)argument.End();

        }

        #endregion



        // 以降、スーパークラス(Parserクラス)への委譲

        #region パーサコンビネータ
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を複数回適用する。
        /// 正規表現の"*"に相当する。
        /// </summary>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>this</returns>
        public ITraceLogParser Many(Func<ITraceLogParser> f)
        {
            return (ITraceLogParser)base.Many<ITraceLogParser>(f);
        }


        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を1回以上適用する。
        /// 正規表現の"+"に相当する。
        /// </summary>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>成功：this、失敗：NullObject</returns>
        public ITraceLogParser Many1(Func<ITraceLogParser> f)
        {
            return (ITraceLogParser)base.Many1<ITraceLogParser>(f);
        }


        /// <summary>
        /// パーサ間のORをとる。
        /// ORの前までにパースが失敗した場合、ORの後のパーサで再度パースを試みる。
        /// ORの前までのパーサでパースできた場合、ORの後のパーサは無視する。
        /// </summary>
        /// <returns>これ以前のパースに成功：NullObject</returns>
        public new ITraceLogParser OR()
        {
            return (ITraceLogParser)base.OR();
        }
        #endregion


        #region 文字パーサ
        public new ITraceLogParser Char(char c)
        {
            return (ITraceLogParser)base.Char(c);

        }


        public new ITraceLogParser Alpha()
        {
            return (ITraceLogParser)base.Alpha();
        }


        public new ITraceLogParser Num()
        {

            return (ITraceLogParser)base.Num();

        }


        public new ITraceLogParser AlphaNum()
        {

            return (ITraceLogParser)base.AlphaNum();

        }


        #region AnyCharOtherThanメソッド

        public new ITraceLogParser AnyCharOtherThan(char c)
        {

            return (ITraceLogParser)base.AnyCharOtherThan(c);

        }

        public new ITraceLogParser AnyCharOtherThan(char c1, char c2)
        {
            return (ITraceLogParser)base.AnyCharOtherThan(c1, c2);
        }

        public new ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3)
        {
            return (ITraceLogParser)base.AnyCharOtherThan(c1, c2, c3);
        }


        public new ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3, char c4)
        {
            return (ITraceLogParser)base.AnyCharOtherThan(c1, c2, c3, c4);
        }

        public new ITraceLogParser AnyCharOtherThan(char[] clist)
        {
            return (ITraceLogParser)base.AnyCharOtherThan(clist);
        }

        public new ITraceLogParser Epsilon()
        {
            return (ITraceLogParser)base.Epsilon();
        }
        #endregion
        #endregion
    }
}
