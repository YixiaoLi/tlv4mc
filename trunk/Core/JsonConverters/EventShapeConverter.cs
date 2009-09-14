
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class EventShapeConverter : GeneralConverter<EventShape>
	{
		public override object ReadJson(IJsonReader reader)
		{
            Time? from=null, to=null;
            Shape shape=null;
            Event evnt=new Event();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new ArgumentException();
            }

            reader.Read();
            while (reader.TokenType == JsonTokenType.PropertyName)
            {
                string name = (string)reader.Value;
                if (name == "From")
                {
                    from = ApplicationFactory.JsonSerializer.Deserialize<Time>(reader);
                }
                else if (name == "To")
                {
                    to = ApplicationFactory.JsonSerializer.Deserialize<Time>(reader);
                }
                else if (name == "Shape")
                {
                    shape = ApplicationFactory.JsonSerializer.Deserialize<Shape>(reader);
                }
                else if (name == "EventName") {
                    reader.Read();
                    evnt.Name = (string)reader.Value;
                }
                else if (name == "RuleName") {
                    reader.Read();
                    evnt.SetVisualizeRuleName((string)reader.Value);
                }
                reader.Read(); 
            }
            return new EventShape(from.Value, to.Value, shape, evnt);
		}

		protected override void WriteJson(IJsonWriter writer, EventShape obj)
		{
            writer.WriteObject(w => {
                w.WriteProperty("From");
                ApplicationFactory.JsonSerializer.Serialize(w, obj.From);

                w.WriteProperty("To");
                ApplicationFactory.JsonSerializer.Serialize(w, obj.To);

                w.WriteProperty("Shape");
                ApplicationFactory.JsonSerializer.Serialize(w, obj.Shape);

                w.WriteProperty("EventName");
                ApplicationFactory.JsonSerializer.Serialize(w, obj.Event.Name);

                w.WriteProperty("RuleName");
                ApplicationFactory.JsonSerializer.Serialize(w, obj.Event.GetVisualizeRuleName());
            });

		}
	}
}
