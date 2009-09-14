
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
		
		public virtual bool CanConvert(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				type = type.GetGenericArguments()[0];

			return Type.IsAssignableFrom(type) || type.IsSubclassOf(Type);
		}

		public void WriteJson(IJsonWriter writer, object obj)
		{
			WriteJson(writer, (T)obj);
		}

		public abstract object ReadJson(IJsonReader reader);

		protected abstract void WriteJson(IJsonWriter writer, T obj);

	}
}
