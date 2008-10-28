using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースを表すクラス
    /// </summary>
    [XmlRoot("resource", Namespace = "http://133.6.51.8/svn/ojl-mprtos/TLV/Resource")]
    public class Resource
    {
        /// <summary>
        /// リソース名
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// 属性リスト
        /// </summary>
        [XmlElement("attributes")]
        public AttributeList Attributes { get; set; }

        /// <summary>
        /// <c>Resource</c>のインスタンスを生成する
        /// </summary>
        public Resource()
        {
            Name = string.Empty;
            Attributes = new AttributeList();
        }
    }
}
