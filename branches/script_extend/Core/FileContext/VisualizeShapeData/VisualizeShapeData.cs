using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class VisualizeShapeData : IJsonable<VisualizeShapeData>
    {
        //  Event
        public Dictionary<string, EventShapes> EventShapes;

        //  VisualizeRule Event
        public Dictionary<string, EventShapes> RuleEventShapes;

        //  Resource
        public Dictionary<string, EventShapes> ResourceShapes;

        //  VisualizeRule Resource
        public Dictionary<string, EventShapes> RuleResourceShapes;

        //  VisualizeRule Resource Event
        public Dictionary<string, EventShapes> RuleResourceEventShapes;



       public VisualizeShapeData() {
           EventShapes = new Dictionary<string, EventShapes>();
           RuleEventShapes = new Dictionary<string, EventShapes>();
           ResourceShapes = new Dictionary<string, EventShapes>();
           RuleResourceShapes = new Dictionary<string, EventShapes>();
           RuleResourceEventShapes = new Dictionary<string, EventShapes>();
       } 

        #region IJsonable<VisualizeShapeData> メンバ
        public string ToJson()
        {
            var dict =
               new Dictionary<string, Dictionary<string, EventShapes>>();
            dict.Add("EventShapes", EventShapes);
            dict.Add("RuleEventShapes", RuleEventShapes);
            dict.Add("ResourceShapes", ResourceShapes);
            dict.Add("RuleResourceShapes", RuleResourceShapes);
            dict.Add("RuleResourceEventShapes", RuleResourceEventShapes);
 
            return ApplicationFactory.JsonSerializer.Serialize(dict); 
        }

        public VisualizeShapeData Parse(string data)
        {
            Dictionary<string, Dictionary<string, EventShapes>> dict =
                ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EventShapes>>>(data);
            VisualizeShapeData vizShape= new VisualizeShapeData();
            vizShape.EventShapes = dict["EventShapes"];
            vizShape.RuleEventShapes = dict["RuleEventShapes"];
            vizShape.ResourceShapes = dict["ResourceShapes"];
            vizShape.RuleResourceShapes = dict["RuleResourceShapes"];
            vizShape.RuleResourceEventShapes = dict["RuleResourceEventShapes"];

            return vizShape; 
        }

        #endregion
    }
}
