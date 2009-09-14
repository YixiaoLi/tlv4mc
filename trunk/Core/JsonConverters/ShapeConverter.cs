
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
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ArgumentException();
            }
           Shape shape = new Shape();

           reader.Read();
           while (reader.TokenType == JsonTokenType.PropertyName)
           {
               string name = (string)reader.Value;
               if (name == "Alpha")
               {
                   shape.Alpha = ApplicationFactory.JsonSerializer.Deserialize<int>(reader);
               }
               else if (name == "Area")
               {
                   shape.Area = ApplicationFactory.JsonSerializer.Deserialize<Area>(reader);  
               }
               else if (name == "Arc")
               {
                   shape.Arc = ApplicationFactory.JsonSerializer.Deserialize<Arc>(reader); 
               }
               else if (name == "Fill")
               {
                   shape.Fill = ApplicationFactory.JsonSerializer.Deserialize<System.Drawing.Color>(reader);  
               }
               else if (name == "Font")
               {
                   shape.Font = ApplicationFactory.JsonSerializer.Deserialize<Font>(reader);
               }
               else if (name == "Location")
               {
                   shape.Location = ApplicationFactory.JsonSerializer.Deserialize<Point>(reader);  
               }
               else if (name == "MetaData")
               {
                   shape.MetaData = ApplicationFactory.JsonSerializer.Deserialize<Json>(reader);
               }
               else if (name == "Offset")
               {
                   shape.Offset = ApplicationFactory.JsonSerializer.Deserialize<Size>(reader);
               }
               else if (name == "Pen")
               {
                   shape.Pen = ApplicationFactory.JsonSerializer.Deserialize<Pen>(reader);
               }
               else if (name == "Points")
               {
                   shape.Points = ApplicationFactory.JsonSerializer.Deserialize<PointList>(reader);  
               }
               else if (name == "Size")
               {
                   shape.Size = ApplicationFactory.JsonSerializer.Deserialize<Size>(reader);  
               }
               else if (name == "Text")
               {
                   reader.Read();  
                   shape.Text = (string)reader.Value;  
               }
               else if (name == "Type")
               {
                   shape.Type = ApplicationFactory.JsonSerializer.Deserialize<ShapeType>(reader);
               }
               else {
                   throw new ArgumentException();
               }
               reader.Read(); 
           }

           if (reader.TokenType != JsonTokenType.EndObject)
           {
               throw new ArgumentException();
           }
           shape.SetDefaultValue();
           shape.ChackValidate();
			return shape;
		}

		protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, Shape obj)
		{
            writer.WriteObject(w =>
            {
                if (obj.Alpha.HasValue)
                {
                    w.WriteProperty("Alpha");
                    w.WriteValue(obj.Alpha.Value);
                }
                if (obj.Area != null)
                {
                    w.WriteProperty("Area");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Area);  
                }
                if (obj.Arc != null)
                {
                    w.WriteProperty("Arc");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Arc);  
                }

                if (obj.Fill.HasValue)
                {
                    w.WriteProperty("Fill");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Fill.Value);  
                }

                if (obj.Font != null)
                {
                    w.WriteProperty("Font");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Font);  
                }

                if (obj.Location != null)
                {
                    w.WriteProperty("Location");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Location);  
                }

                if (obj.MetaData != null)
                {
                    w.WriteProperty("MetaData");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.MetaData);  
                }

                if (obj.Offset != null)
                {
                    w.WriteProperty("Offset");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Offset);  
                }

                if (obj.Pen != null)
                {
                    w.WriteProperty("Pen");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Pen);  
                }

                if (obj.Points != null)
                {
                    w.WriteProperty("Points");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Points);  
                }

                if (obj.Size != null)
                {
                    w.WriteProperty("Size");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Size);
                }

                if (obj.Text != null)
                {
                    w.WriteProperty("Text");
                    w.WriteValue(obj.Text);
                }

                if (obj.Type.HasValue)
                {
                    w.WriteProperty("Type");
                    ApplicationFactory.JsonSerializer.Serialize(w, obj.Type.Value);
                }

            });
//			ApplicationFactory.JsonSerializer.Serialize(writer, obj);
		}
	}
}
