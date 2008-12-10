using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public abstract class GeneralKeyedJsonableCollection<K, T, TList> : Dictionary<K, T>, IJsonable<TList>
		where TList : GeneralKeyedJsonableCollection<K, T, TList>
		where T : class
	{
		public GeneralKeyedJsonableCollection(IDictionary<K,T> d)
			: base(d)
		{
		}

		public GeneralKeyedJsonableCollection()
			: base()
		{
		}

		public TList Parse(string json)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<TList>(json);
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize((TList)this);
		}

	}
}
