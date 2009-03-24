using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceHeaderConverter : GeneralConverter<ResourceHeader>
	{
		protected override void WriteJson(IJsonWriter writer, ResourceHeader resh)
		{
			writer.WriteObject(w =>
				{
					foreach (ResourceType rt in resh)
					{
						w.WriteProperty(rt.Name);
						w.WriteValue(rt, ApplicationFactory.JsonSerializer);
					}
				});
		}

		public override object ReadJson(IJsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				GeneralNamedCollection<ResourceType> resTypes = new GeneralNamedCollection<ResourceType>();
				while(reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string name = (string)reader.Value;
						ResourceType resType = ApplicationFactory.JsonSerializer.Deserialize<ResourceType>(reader);
						resType.Name = name;
						resTypes.Add(resType);
					}
					reader.Read();
				}

				ResourceHeader resh = new ResourceHeader(resTypes);
				return resh;
			}
			else if (reader.TokenType == JsonTokenType.StartArray)
			{
				ResourceHeader result = getResourceHeader(reader);
				return result;
			}
			else
			{
				throw new Exception("ResourceHeader記述の文法が間違っています。");
			}
		}

		private static ResourceHeader getResourceHeader(IJsonReader reader)
		{

			Json data = new Json(new Dictionary<string, Json>());

			while(reader.TokenType != JsonTokenType.EndArray)
			{
				if (reader.TokenType == JsonTokenType.String)
				{
					string name = (string)reader.Value;

					string[] resourceHeadersPaths = Directory.GetFiles(ApplicationData.Setting.ResourceHeadersDirectoryPath, "*." + Properties.Resources.ResourceHeaderFileExtension);

					foreach (string s in resourceHeadersPaths)
					{
						Json json = new Json().Parse(File.ReadAllText(s));
						foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
						{
							if (j.Key == name)
							{
								foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
								{
									data.Add(_j.Key, _j.Value.Value);
								}
							}
						}
					}
				}
				reader.Read();
			}

			string typesStr = data.ToJsonString();

			GeneralNamedCollection<ResourceType> types = ApplicationFactory.JsonSerializer.Deserialize<GeneralNamedCollection<ResourceType>>(typesStr);
			ResourceHeader result = new ResourceHeader(types);
			return result;
		}
	}
}
