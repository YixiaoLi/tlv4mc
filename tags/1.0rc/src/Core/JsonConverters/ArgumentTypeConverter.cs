using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArgumentTypeConverter : GeneralConverter<ArgumentType>
	{
		public override object ReadJson(IJsonReader reader)
		{
			ArgumentType argType = new ArgumentType();
			argType.Type = (JsonValueType)Enum.Parse(typeof(JsonValueType), reader.Value.ToString());
			return argType;
		}

		protected override void WriteJson(IJsonWriter writer, ArgumentType obj)
		{
			writer.WriteValue(obj.Type.ToString());
		}
	}
}
