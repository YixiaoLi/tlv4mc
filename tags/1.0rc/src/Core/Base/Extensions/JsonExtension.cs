using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public static class JsonExtension
	{
		public static string ToJsonString(this Json json)
		{
			return ApplicationFactory.JsonSerializer.Serialize(json);
		}
		public static Json Parse(this Json json, string str)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<Json>(str);
		}
	}
}
