using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class EventShapesConverter : GeneralConverter<EventShapes>
    {
        protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, EventShapes obj)
        {
            writer.WriteObject(w =>
            {
                foreach (KeyValuePair<string, List<EventShape>> kvp in obj.List)
                {
                    w.WriteProperty(kvp.Key);
                    w.WriteValue(kvp.Value, ApplicationFactory.JsonSerializer);
                }
            });
        }

        public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
        {
            EventShapes shapes = new EventShapes();
            //shapes.List = ry.JsonSerializer.Deserialize<Dictionary<string, List<EventShape>>>(reader);
            if (reader.TokenType == JsonTokenType.StartObject) {
                while (reader.TokenType != JsonTokenType.EndObject) {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string name = (string)reader.Value;
                        List<EventShape> l = ApplicationFactory.JsonSerializer.Deserialize<List<EventShape>>(reader);
                        shapes.List.Add(name, l);
                    }
                    reader.Read();
                }
            }

            return shapes; 
        }
    }
}
