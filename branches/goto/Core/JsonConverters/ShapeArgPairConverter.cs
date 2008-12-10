using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ShapeArgPairConverter : GeneralConverter<ShapeArgPair>
	{
		public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				ShapeArgPair spArgPair = new ShapeArgPair();
				while (reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string name = reader.Value.ToString();
						spArgPair.Name = name;
						try
						{
							Json[] args = ApplicationFactory.JsonSerializer.Deserialize<Json[]>(reader);
							spArgPair.Args = args;
						}
						catch
						{
							spArgPair.Args = null;
						}
					}
					reader.Read();
				}
				return spArgPair;
			}
			else
			{
				ShapeArgPair spArgPair = new ShapeArgPair();
				spArgPair.Name = reader.Value.ToString();
				return spArgPair;
			}
		}

		protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, ShapeArgPair obj)
		{
			if (obj.Args == null)
			{
				writer.WriteValue(obj.Name);
			}
			else
			{
				writer.WriteArray(w =>
				{
					foreach (Json arg in obj.Args)
					{
						w.WriteValue(arg, ApplicationFactory.JsonSerializer);
					}
				});
			}
		}
	}
}
