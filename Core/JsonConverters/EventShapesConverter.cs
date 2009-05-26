using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class EventShapesConverter : GeneralConverter<EventShapes>
    {
        protected override void WriteJson(NU.OJL.MPRTOS.TLV.Base.IJsonWriter writer, EventShapes obj)
        {
            ApplicationFactory.JsonSerializer.Serialize(writer, obj.List); 
        }

        public override object ReadJson(NU.OJL.MPRTOS.TLV.Base.IJsonReader reader)
        {
            EventShapes shapes = new EventShapes();
            shapes.List = ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, List<EventShape>>>(reader);
            return shapes; 
        }
    }
}
