﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class TraceLogParser : Parser, ITraceLogParser
    {
        private StringBuilder _time       = new StringBuilder();
        private StringBuilder _object     = new StringBuilder();
        private StringBuilder _objectName = new StringBuilder();
        private StringBuilder _objectType = new StringBuilder();
        private StringBuilder _behavior   = new StringBuilder();
        private StringBuilder _attribute  = new StringBuilder();
        private StringBuilder _value      = new StringBuilder();
        private StringBuilder _arguments  = new StringBuilder();


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

        private readonly char[] NON_OBJECTTYPENAME_CHAR = new char[]{'(', ')', '.', '!', '=', '<', '>'};
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

            var line = Char('[').Time().Char(']').Event();

            line.End();
            return this;
        }

        public ITraceLogParser Time()
        {
            Begin();

            var time = Many1(AlphaNum).Many(() => Char('.')).Many(AlphaNum);
                       
            time.TimeValue = _stack.Peek().Result.ToString();
            
            // マイナス値であったときも正しいが、Timeとして保持しないため吐き捨てておく
            time = time.OR().Char('-').Many1(AlphaNum).Many(() => Char('.')).Many(AlphaNum);

            return (ITraceLogParser)time.End();
        }

        public ITraceLogParser Event()
        {
            Begin();

            var event_ = OBject().Char('.').AttributeOrBehavior();

            return (ITraceLogParser)event_.End();
        }

        public ITraceLogParser OBject()
        {
            Begin();

            var object_ =
                ObjectTypeName().Char('(').AttributeCondition().Char(')')
                .OR().
                ObjectName();

            object_.ObjectValue = _stack.Peek().Result.ToString();

            // HasObjectTypeValueは、ObjectTypeName()が真でも、ほかで失敗すれば偽である。
            object_.HasObjectTypeValue = false;
            return (ITraceLogParser)object_.End();
        }

        public ITraceLogParser ObjectName()
        {
            Begin();

            var name = Many1(() => AnyCharOtherThan('(', ')', '.'));

            name.ObjectNameValue = _stack.Peek().Result.ToString();
            return (ITraceLogParser)name.End();
        }

        public ITraceLogParser ObjectTypeName()
        {
            Begin();

            var typeName = Many1(() => AnyCharOtherThan('(', ')', '.'));

            typeName.ObjectTypeValue = _stack.Peek().Result.ToString();
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

            attributeName.AttributeValue = _stack.Peek().Result.ToString();
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

            var value = Many1(() => AnyCharOtherThan(' '));

            value.ValueValue = _stack.Peek().Result.ToString();
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

            behaviorName.BehaviorValue = _stack.Peek().Result.ToString();
            return (ITraceLogParser)behaviorName.End();
        }


        public ITraceLogParser Arguments()
        {
            Begin();

            var arguments = Argument().NextArgument();

            arguments.ArgumentsValue = _stack.Peek().Result.ToString();
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

        // カッコのネストを正しくパースするために必要
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

        #region パーサコンビネータ
        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を複数回適用する。
        /// 正規表現の"*"に相当する。
        /// </summary>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>this</returns>
        public ITraceLogParser Many(Func<ITraceLogParser> f)
        {
            // パーサfでパースできないものが来るまでループ
            while (true)
            {
                if (f() is NullObjectOfTraceLogParser) break;
            }

            return this;
        }


        /// <summary>
        /// 引数で与えられたパーサ(メソッド)を1回以上適用する。
        /// 正規表現の"+"に相当する。
        /// </summary>
        /// <param name="f">パーサ(メソッド)</param>
        /// <returns>成功：this、失敗：NullObject</returns>
        public ITraceLogParser Many1(Func<ITraceLogParser> f)
        {
            if (f() is NullObjectOfTraceLogParser)
            {
                return (ITraceLogParser)_nullObject;
            }
            else
            {
                return Many(f);
            }
        }


        /// <summary>
        /// パーサ間のORをとる。
        /// ORの前までにパースが失敗した場合、ORの後のパーサで再度パースを試みる。
        /// ORの前までのパーサでパースできた場合、ORの後のパーサは無視する。
        /// </summary>
        /// <returns>これ以前のパースに成功：NullObject</returns>
        public ITraceLogParser OR()
        {
            base._nullObject.Success = true;
            return (ITraceLogParser)_nullObject;
        }
        #endregion


        #region 文字パーサ
        public ITraceLogParser Char(char c)
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }
            else if ( _input.Peek() == c)
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }


        public ITraceLogParser Alpha()
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();
            if ((('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }


        public ITraceLogParser Num()
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();
            if (('0' <= c && c <= '9'))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }


        public ITraceLogParser AlphaNum()
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();
            if ((('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')
                || ('0' <= c && c <= '9')))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }


        #region AnyCharOtherThanメソッド

        public ITraceLogParser AnyCharOtherThan(char c)
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }
            else if (_input.Peek() != c)
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }

        public ITraceLogParser AnyCharOtherThan(char c1, char c2)
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();
            if (((c != c1) && (c != c2)))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }

        public ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3)
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();
            if (((c != c1) && (c != c2) && (c != c3)))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }


        public ITraceLogParser AnyCharOtherThan(char c1, char c2, char c3, char c4)
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();
            if (((c != c1) && (c != c2) && (c != c3) && (c != c4)))
            {
                Append(_input.Read());
                return this;
            }
            else
            {
                return (ITraceLogParser)_nullObject;
            }
        }

        public ITraceLogParser AnyCharOtherThan(char[] clist)
        {
            if (_input.IsEmpty())
            {
                return (ITraceLogParser)_nullObject;
            }

            var c = _input.Peek();

            foreach( char n in clist )
            {
                if( n == c ) return (ITraceLogParser)_nullObject;
            }

            Append(_input.Read());
            return this;
        }

        public ITraceLogParser Epsilon()
        {
            return this;
        }
        #endregion
        #endregion
    }
}
