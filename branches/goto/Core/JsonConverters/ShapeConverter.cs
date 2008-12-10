using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ShapeConverter : GeneralConverter<Shape>
	{
		public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
		{
			Shape shape = new Shape();
			shape.MetaData = ApplicationFactory.JsonSerializer.Deserialize<Json>(reader);
			return shape;
		}

		protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, Shape obj)
		{
			ApplicationFactory.JsonSerializer.Serialize(writer, obj.MetaData);
		}
	}
}
