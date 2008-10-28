﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースの属性を表すクラス
    /// </summary>
    [DataContract]
    public class Attribute
    {
        /// <summary>
        /// 属性名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 属性名を表示する際に使用されるテキスト
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性の値
        /// </summary>
        [DataMember]
        public string Value { get; set; }
        /// <summary>
        /// 属性の変数型
        /// </summary>
        [DataMember]
        public VariableType VariableType { get; set; }
        /// <summary>
        /// 属性の配置型
        /// </summary>
        [DataMember]
        public AllocationType AllocationType { get; set; }
        /// <summary>
        /// この属性の値を用いてグループ化するかどうか
        /// </summary>
        [DataMember]
        public bool Grouping { get; set; }

        /// <summary>
        /// <c>Attribute</c>のインスタンスを生成する
        /// </summary>
        public Attribute()
        {
            Name = string.Empty;
            DisplayName = string.Empty;
            Value = string.Empty;
            VariableType = VariableType.String;
            AllocationType = AllocationType.Static;
            Grouping = false;
        }
    }

    [DataContract]
    public enum AllocationType
    {
        [EnumMember]
        Static,
        [EnumMember]
        Dynamic
    }

    [DataContract]
    public enum VariableType
    {
        [EnumMember]
        String,
        [EnumMember]
        Decimal,
        [EnumMember]
        Enumeration,
        [EnumMember]
        Boolean
    }
}
