using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ShapesListConverter : GeneralConverter<ShapesList>
	{
		public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				ShapesList list = new ShapesList();
				while (reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string key = reader.Value.ToString();
						GeneralNamedCollection<ShapeArgPair> spArgs = new GeneralNamedCollection<ShapeArgPair>();
						ShapeArgPair spArg = ApplicationFactory.JsonSerializer.Deserialize<ShapeArgPair>(reader);
						spArgs.Add(spArg.Name, spArg);
						list.Add(key, spArgs);
					}
					reader.Read();
				}
				return list;
			}
			else
			{
				ShapesList list = new ShapesList();
				GeneralNamedCollection<ShapeArgPair> spArgs = new GeneralNamedCollection<ShapeArgPair>();
				ShapeArgPair spArg = new ShapeArgPair();
				spArg.Name = reader.Value.ToString();
				spArgs.Add(spArg.Name, spArg);
				list.Add("True", spArgs);
				return list;
			}
		}

		protected override void WriteJson(IJsonWriter writer, ShapesList spList)
		{
			writer.WriteObject(w =>
				{
					foreach(KeyValuePair<string, GeneralNamedCollection<ShapeArgPair>> sp in spList)
					{
						w.WriteProperty(sp.Key);
						ApplicationFactory.JsonSerializer.Serialize(writer, sp.Value);
					}
				});
		}
	}
}
