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
	public class GeneralJsonableCollection<T, TList> : Collection<T>, IJsonable<TList>
		where TList : GeneralJsonableCollection<T, TList>
		where T : class
	{
		public GeneralJsonableCollection()
			:base(new List<T>())
		{
		
		}

		public GeneralJsonableCollection(IList<T> list)
			: base(list)
		{
		}

		public TList Parse(string traceLogData)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<TList>(traceLogData);
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize<TList>((TList)this);
		}

	}
}
