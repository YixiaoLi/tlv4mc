using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceListConverter : IJsonConverter
	{
		public Type Type { get { return typeof(ResourceList); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.StartObject);
			foreach (KeyValuePair<string, GeneralNamedCollection<Resource>> type_res in ((ResourceList)obj))
			{
				writer.Write(JsonTokenType.PropertyName, type_res.Key);
				writer.Write(JsonTokenType.StartObject);
				foreach (Resource res in type_res.Value)
				{
					writer.Write(JsonTokenType.PropertyName, res.Name);
					writer.Write(JsonTokenType.StartObject);
					foreach (Attribute attr in res.Attributes)
					{
						writer.Write(JsonTokenType.PropertyName, attr.Name);
						ApplicationFactory.JsonSerializer.Serialize(attr.Value);
					}
					writer.Write(JsonTokenType.EndObject);
				}
				writer.Write(JsonTokenType.EndObject);
			}
			writer.Write(JsonTokenType.EndObject);
		}

		public object ReadJson(IJsonReader reader)
		{
			ResourceList resList = new ResourceList();

			while(reader.TokenType != JsonTokenType.EndObject)
			{
				if(reader.TokenType == JsonTokenType.PropertyName)
				{
					string type = (string)reader.Value;
					GeneralNamedCollection<Resource> ress = new GeneralNamedCollection<Resource>();
					while (reader.TokenType != JsonTokenType.EndObject)
					{
						if (reader.TokenType == JsonTokenType.PropertyName)
						{
							string name = (string)reader.Value;
							reader.Read();
							Resource res = ApplicationFactory.JsonSerializer.Deserialize<Resource>(reader);
							res.Type = type;
							res.Name = name;
							ress.Add(name, res);
						}
					}
					resList.Add(type, ress);
				}
			}
			return resList;
		}
	}
}
