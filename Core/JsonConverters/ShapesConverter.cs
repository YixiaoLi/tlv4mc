
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ShapesConverter : GeneralConverter<Shapes>
	{
		public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
		{
			Shapes shapes = new Shapes();
			while(reader.TokenType != JsonTokenType.EndArray)
			{
				if (reader.TokenType == JsonTokenType.StartObject)
				{
					Shape sp = new Shape();
					Json json = ApplicationFactory.JsonSerializer.Deserialize<Json>(reader);
					sp.MetaData = json;
					shapes.Add(sp);
				}
				reader.Read();
			}
			return shapes;
		}

		protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, Shapes shapes)
		{
			writer.WriteArray(w=>
				{
					foreach(Shape sp in shapes)
					{
						ApplicationFactory.JsonSerializer.Serialize(writer, sp);
					}	
				});
		}
	}
}
