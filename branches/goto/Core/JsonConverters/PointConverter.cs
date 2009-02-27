﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class PointConverter : GeneralConverter<Point>
	{
		protected override void WriteJson(IJsonWriter writer, Point point)
		{
			writer.WriteValue(point.ToString());
		}

		public override object ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Point(value);
		}
	}
}