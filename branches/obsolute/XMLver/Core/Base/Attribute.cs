using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースの属性を表すクラス
    /// </summary>
    [XmlRoot("attribute", Namespace = "http://133.6.51.8/svn/ojl-mprtos/TLV/Resource")]
    public class Attribute
    {
        /// <summary>
        /// 属性名
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// 属性名を表示する際に使用されるテキスト
        /// </summary>
        [XmlElement("displayName")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性の値
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }
        /// <summary>
        /// 属性の変数型
        /// </summary>
        [XmlElement("variableType")]
        public VariableType VariableType { get; set; }
        /// <summary>
        /// 属性の配置型
        /// </summary>
        [XmlElement("allocationType")]
        public AllocationType AllocationType { get; set; }
        /// <summary>
        /// この属性の値を用いてグループ化するかどうか
        /// </summary>
        [XmlElement("grouping")]
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

    public enum AllocationType
    {
        Static,
        Dynamic
    }

    public enum VariableType
    {
        String,
        Decimal,
        Enumeration,
        Boolean
    }
}
