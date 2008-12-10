using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Shape : IHavingNullableProperty
	{
		private Json _metaData;
		private List<string> _argPropList = new List<string>();
		public static Shape Default
		{
			get
			{
				Shape shape = new Shape();
				shape.Pen = new Pen();
				shape.Pen.Width = 1.0f;
				shape.Pen.Color = Color.Black;
				shape.Pen.DashCap = DashCap.Flat;
				shape.Pen.DashStyle = DashStyle.Solid;
				shape.Pen.Alpha = 255;
				shape.Pen.DashPattern = new float[] { 1.0f, 1.0f };
				shape.Font = new Font();
				shape.Font.Color = Color.Black;
				shape.Font.Family = FontFamily.GenericSansSerif;
				shape.Font.Size = 10.5f;
				shape.Font.Style = FontStyle.Regular;
				shape.Font.Align = ContentAlignment.MiddleCenter;
				shape.Type = ShapeType.Undefined;
				shape.Align = ContentAlignment.BottomLeft;
				shape.Arc = new Arc(0.0f, 90.0f);
				shape.Area = new Area("0,0","100%,100%");
				shape.Location = new Point("0,0");
				shape.Size = new Size("100%,100%");
				shape.Points = new PointList() { new Point("0,0"), new Point("100%,0"), new Point("100%,100%"), new Point("0,100%") };
				shape.Fill = Color.White;
				shape.Offset = new Point("0,0");
				shape.Text = string.Empty;
				shape.Alpha = 255;
				return shape;
			}
		}

		public ShapeType? Type { get; set; }
		public string Text { get; set; }
		public PointList Points { get; set; }
		public Point Offset { get; set; }
		public Point Location { get; set; }
		public Size Size { get;set; }
		public Arc Arc { get; set; }
		public Pen Pen { get; set; }
		public Font Font { get; set; }
		public Color? Fill { get; set; }
		public ContentAlignment? Align { get; set; }
		public Area Area { get; set; }
		public int? Alpha { get; set; }

		public Json MetaData
		{
			get { return _metaData; }
			set
			{
				_metaData = value;

				PropertyInfo[] pis = typeof(Shape).GetProperties();

				foreach (KeyValuePair<string,Json> kvp in _metaData.GetKeyValuePairEnumerator())
				{
					PropertyInfo pi;

					try
					{
						pi = pis.Single(p => p.Name == kvp.Key);
					}
					catch
					{
						continue;
					}
					if (!kvp.Value.ToJsonString().Contains("args"))
					{
						object obj = ApplicationFactory.JsonSerializer.Deserialize(kvp.Value.ToJsonString(), pi.PropertyType);
						pi.SetValue(this, obj, null);
					}
					else
					{
						_argPropList.Add(kvp.Key);
					}
				}
			}
		}

		public void SetArgs(params Json[] args)
		{
			foreach(string str in _argPropList)
			{
				PropertyInfo pi = typeof(Shape).GetProperties().Single(p=>p.Name == str);
				string value = _metaData[str].ToJsonString();
				foreach(Match m in Regex.Matches(value, @"args\[(?<id>\d+)\]"))
				{
					int i = int.Parse(m.Groups["id"].Value);
					if (args != null && args.Length - 1 > i)
						value = value.Replace(m.Value, args[i].ToJsonString());
				}
				pi.SetValue(this, ApplicationFactory.JsonSerializer.Deserialize(value, pi.PropertyType), null);
			}
		}

		public Shape()
		{
		}

		public void SetDefaultValueToNullProperty()
		{
			foreach (PropertyInfo pi in typeof(Shape).GetProperties())
			{
				if (pi.GetValue(this, null) == null)
				{
					pi.SetValue(this, pi.GetValue(Shape.Default, null), null);
				}
			}
		}

	}
}
