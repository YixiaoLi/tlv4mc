using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースを表すクラス
    /// </summary>
	public class ResourceList : GeneralKeyedJsonableCollection<string, Json, ResourceList>
    {
        /// <summary>
        /// <c>Resource</c>のインスタンスを生成する
        /// </summary>
		public ResourceList()
			: base(new Dictionary<string, Json>())
		{
		}

		public ResourceList(IDictionary<string, Json> d)
			:base(d)
		{
		}

	}

}
