using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceHeaderConverter : IJsonConverter
	{
		public Type Type { get { return typeof(ResourceHeader);} }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.String, ((ResourceHeader)obj).Name);
		}

		public object ReadJson(IJsonReader reader)
		{
			string name = (string)reader.Value;

			Json data = new Json(new Dictionary<string, Json>());

			string[] resourceHeadersPaths = Directory.GetFiles(ApplicationDatas.Setting["ResourceHeadersDirectoryPath"], "*." + Properties.Resources.ResourceHeaderFileExtension);

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

			string typesStr = data.ToJsonString();

			ResourceTypeList types = ApplicationFactory.JsonSerializer.Deserialize<ResourceTypeList>(typesStr);

			ResourceHeader result = new ResourceHeader(name, types);

			return result;
		}
	}
}
