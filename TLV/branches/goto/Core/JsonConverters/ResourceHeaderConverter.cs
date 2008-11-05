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

			Json data = new Json(new Dictionary<string, Json>());

			string[] resourceHeadersPaths = Directory.GetFiles(ApplicationDatas.Setting["ResourceHeadersDirectoryPath"], "*." + Properties.Resources.ResourceHeaderFileExtension);
			// トレースログ変換ファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach (string s in resourceHeadersPaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (j.Key == name)
					{
						foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
						{
							data.Add(_j.Key, _j.Value);
						}
					}
				}
			}

			string typesStr = data.ToJsonString();

			Dictionary<string, ResourceType> types = ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, ResourceType>>(typesStr);

			ResourceHeader result = new ResourceHeader(name, types);

			return result;
		}
	}
}
