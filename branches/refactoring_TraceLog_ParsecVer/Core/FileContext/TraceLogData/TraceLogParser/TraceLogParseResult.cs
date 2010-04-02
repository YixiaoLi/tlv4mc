using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class TraceLogParseResult
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
        public string Time
        {
            get
            {
                return HasTime ? _time.ToString() : null; 
            }

            set
            {
                if (value.Length > 0)
                {
                    _time.Length = 0;
                    _time.Append(value);
                    HasTime = true;
                }
            }
        }

        public string Object
        {
            get
            {
                return _object.Length > 0 ? _object.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _object.Length = 0;
                    _object.Append(value);
                }
            }
        }


        public string ObjectName
        {
            get
            {
                return HasObjectName ? _objectName.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _objectName.Length = 0;
                    HasObjectName = true;
                    _objectName.Append(value);
                }
            }
        }


        public string ObjectType
        {
            get
            {
                return HasObjectType ? _objectType.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _objectType.Length = 0;
                    HasObjectType = true;
                    _objectType.Append(value);
                }
            }
        }


        public string Behavior
        {
            get
            {
                return _behavior.Length > 0 ? _behavior.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _behavior.Length = 0;
                    _behavior.Append(value);
                }
            }
        }


        public string Attribute
        {
            get
            {
                return _attribute.Length > 0 ? _attribute.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _attribute.Length = 0;
                    _attribute.Append(value);
                }
            }
        }


        public string Value
        {
            get
            {
                return _value.Length > 0 ? _value.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _value.Length = 0;
                    _value.Append(value);
                }
            }
        }


        public string Arguments
        {
            get
            {
                // ""(空白文字列)がありえるため、_behaviorの有無に依存する
                return _behavior.Length > 0 ? _arguments.ToString() : null;
            }
            set
            {
                if (value.Length > 0)
                {
                    _arguments.Length = 0;
                    _arguments.Append(value);
                }
            }
        }


        public bool HasTime { get; set; }
        public bool HasObjectName { get; set; }
        public bool HasObjectType { get; set; }
        #endregion


        public void init()
        {
            _time.Length       = 0;
            _object.Length     = 0;
            _objectName.Length = 0;
            _objectType.Length = 0;
            _behavior.Length   = 0;
            _attribute.Length  = 0;
            _value.Length      = 0;
            _arguments.Length  = 0;
            HasTime            = false;
            HasObjectName      = false;
            HasObjectType      = false;
        }
    }
}
