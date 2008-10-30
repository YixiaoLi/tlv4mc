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
	public class GeneralReadOnlyJsonableCollection<T, TList> : ReadOnlyCollection<T>, IJsonable<TList>
		where TList : GeneralReadOnlyJsonableCollection<T, TList>
		where T : class
	{
		private static IJsonSerializer _json = ApplicationFactory.JsonSerializer;
		
		public GeneralReadOnlyJsonableCollection()
			:base(new List<T>())
		{
		
		}

		public GeneralReadOnlyJsonableCollection(IList<T> list)
			: base(list)
		{
		}

		public TList Parse(string traceLogData)
		{
			return _json.Deserialize<TList>(traceLogData);
		}

		public string ToJson()
		{
			return _json.Serialize<TList>((TList)this);
		}

	}
}
