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
	public class ResourceList : GeneralKeyedJsonableCollection<string, Dictionary<string, string>, ResourceList>
    {
        /// <summary>
        /// <c>Resource</c>のインスタンスを生成する
        /// </summary>
		public ResourceList()
			: base(new Dictionary<string, Dictionary<string, string>>())
		{
		}

		public ResourceList(IDictionary<string, Dictionary<string, string>> d)
			:base(d)
		{
		}

	}

}
