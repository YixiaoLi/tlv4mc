using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public abstract class GeneralConverter<T> : IJsonConverter
	{
		public Type Type { get { return typeof(T); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			WriteJson(writer, (T)obj);
		}

		public abstract object ReadJson(IJsonReader reader);

		protected abstract void WriteJson(IJsonWriter writer, T obj);
	}
}
