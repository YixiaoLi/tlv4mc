using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースを表すクラス
    /// </summary>
    [DataContract]
    public class Resource
    {
        /// <summary>
        /// リソース名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 属性リスト
        /// </summary>
        [DataMember]
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
