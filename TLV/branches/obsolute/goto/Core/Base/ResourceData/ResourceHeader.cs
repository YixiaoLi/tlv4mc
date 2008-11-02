using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースリスト
    /// </summary>
	public class ResourceHeader : IJsonable<ResourceHeader>
	{
		private IJsonSerializer _json = ApplicationFactory.JsonSerializer;
		public Dictionary<string, string[]> Enums { get; private set; }
		public Dictionary<string, ResourceType> Types { get; private set; }

		public ResourceHeader()
			:base()
		{
			Enums = new Dictionary<string, string[]>();
			Types = new Dictionary<string, ResourceType>();
		}

		public ResourceHeader(string[] reshPath, string resPath)
			: this()
		{
			// リソースヘッダファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach (string p in reshPath)
			{
				Json reshJson = ApplicationFactory.JsonSerializer.Deserialize<Json>(File.ReadAllText(p));

				foreach (KeyValuePair<string, Json> j in reshJson.GetKeyValuePaierEnumerator())
				{
					switch(j.Key)
					{
						case "Enums":
							foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePaierEnumerator())
							{
								Enums.Add(_j.Key, _j.Value);
							}
							break;
						case "Types":
							foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePaierEnumerator())
							{
								Types.Add(_j.Key, ApplicationFactory.JsonSerializer.Deserialize<ResourceType>(_j.Value.ToJsonString()));
							}
							break;
					}
				}
			}
		}

		public ResourceHeader(string json)
			: this()
		{
			ResourceHeader rd = new ResourceHeader().Parse(json);
			Enums = rd.Enums;
			Types = rd.Types;
		}

		public string ToJson()
		{
			return _json.Serialize(this);
		}

		public ResourceHeader Parse(string resourceData)
		{
			ResourceHeader data = _json.Deserialize<ResourceHeader>(resourceData);
			return data;
		}

	}
}
