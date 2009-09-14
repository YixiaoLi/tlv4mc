
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineConverter : GeneralConverter<TimeLine>
	{
		public override object ReadJson(IJsonReader reader)
		{
			Time from;
			Time to;
			Time min;
			Time max;

			Match m = Regex.Match((string)reader.Value, @"\[(?<from>[^-]+)-(?<to>[^\]]+)\]in\[(?<min>[^-]+)-(?<max>[^\]]+)\]");

			from = ApplicationFactory.JsonSerializer.Deserialize<Time>("\"" + m.Groups["from"].Value + "\"");
			to = ApplicationFactory.JsonSerializer.Deserialize<Time>("\"" + m.Groups["to"].Value + "\"");
			min = ApplicationFactory.JsonSerializer.Deserialize<Time>("\"" + m.Groups["min"].Value + "\"");
			max = ApplicationFactory.JsonSerializer.Deserialize<Time>("\"" + m.Groups["max"].Value + "\"");

			return new TimeLine(from, to, min, max);
		}

		protected override void WriteJson(IJsonWriter writer, TimeLine timeLine)
		{
			string from = ApplicationFactory.JsonSerializer.Serialize(timeLine.FromTime).Replace("\"","");
			string to = ApplicationFactory.JsonSerializer.Serialize(timeLine.ToTime).Replace("\"", "");
			string min = ApplicationFactory.JsonSerializer.Serialize(timeLine.MinTime).Replace("\"", "");
			string max = ApplicationFactory.JsonSerializer.Serialize(timeLine.MaxTime).Replace("\"", "");

			writer.WriteValue("[" + from + "-" + to + "]in[" + min + "-" + max + "]");
		}
	}
}
