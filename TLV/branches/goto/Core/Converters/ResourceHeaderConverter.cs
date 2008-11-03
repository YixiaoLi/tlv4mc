using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceHeaderConverter : IJsonConverter<ResourceHeader>
	{
		public void WriteJson(IJsonWriter writer, ResourceHeader obj)
		{
			writer.Write(JsonTokenType.String, obj.Name);
		}

		public ResourceHeader ReadJson(IJsonReader reader)
		{
			string name = (string)reader.Value;

			Json data = new Json();
			data.makeObject();
			data.Add("Enums", new Json());
			data.Add("Types", new Json());
			data["Enums"].makeObject();
			data["Types"].makeObject();

			string[] resourceHeadersPaths = Directory.GetFiles(ApplicationDatas.Setting["ResourceHeadersDirectoryPath"], "*." + Properties.Resources.ResourceHeaderFileExtension);
			// トレースログ変換ファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach (string s in resourceHeadersPaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePaierEnumerator())
				{
					if (j.Key == name)
					{
						foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePaierEnumerator())
						{
							if (_j.Key == "Enums")
							{
								foreach (KeyValuePair<string, Json> __j in _j.Value.GetKeyValuePaierEnumerator())
								{
									data["Enums"].Add(__j.Key, __j.Value);
								}
							}
							if (_j.Key == "Types")
							{
								foreach (KeyValuePair<string, Json> __j in _j.Value.GetKeyValuePaierEnumerator())
								{
									data["Types"].Add(__j.Key, __j.Value);
								}
							}
						}
					}
				}
			}

			string enumsStr = data["Enums"].ToJsonString();
			string typesStr = data["Types"].ToJsonString();

			Dictionary<string, string[]> enums = ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, string[]>>(enumsStr);
			Dictionary<string, ResourceType> types = ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, ResourceType>>(typesStr);

			ResourceHeader result = new ResourceHeader(name, enums, types);

			return result;
		}
	}
}
