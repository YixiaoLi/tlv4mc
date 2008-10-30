using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// <c>Attribute</c>のリスト
    /// </summary>
	public class AttributeList : GeneralKeyedJsonableCollection<string, Attribute, AttributeList>
	{
		public AttributeList()
			: base(new Dictionary<string, Attribute>())
		{
		}

		public AttributeList(IDictionary<string, Attribute> d)
			:base(d)
		{
		}
	}
}
