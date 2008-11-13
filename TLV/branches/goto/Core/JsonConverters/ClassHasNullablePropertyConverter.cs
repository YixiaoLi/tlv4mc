using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ClassHasNullablePropertyConverter<T> : GeneralConverter<T>
		where T:class, new()
	{
		public override object ReadJson(IJsonReader reader)
		{
			T t = new T();

			while (reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;

					PropertyInfo pi = t.GetType().GetProperties().Single<PropertyInfo>(p => p.Name == key);

					object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

					pi.SetValue(t, o, null);
				}
				reader.Read();
			}

			return t;
		}

		protected override void WriteJson(IJsonWriter writer, T obj)
		{
			writer.WriteObject(w =>
			{
				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					object value = pi.GetValue(obj, null);

					if (value != null)
					{
						w.WriteProperty(pi.Name);
						w.WriteValue(value, ApplicationFactory.JsonSerializer);
					}
				}
			});
		}
	}
}
