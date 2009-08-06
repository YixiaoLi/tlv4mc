using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class VisualizeShapeData : IJsonable<VisualizeShapeData>
    {
        public Dictionary<string, EventShapes> RuleShapes;
        public Dictionary<string, EventShapes> RuleResourceShapes;

       public VisualizeShapeData() {
           RuleShapes = new Dictionary<string, EventShapes>();
           RuleResourceShapes = new Dictionary<string, EventShapes>();
       }

       public void Add(VisualizeRule rule, EventShapes shapes)
       {
           RuleShapes.Add(rule.Name, shapes);
       }

       public EventShapes Get(VisualizeRule rule) {
           return RuleShapes[rule.Name];
       }

       public EventShapes Get(VisualizeRule rule, Event e)
       {
           EventShapes es = new EventShapes();

           foreach (List<EventShape> shapes in Get(rule).List.Values)
           {
               foreach (EventShape shape in shapes)
               {
                   if (shape.Event.Name == e.Name)
                   {
                       es.Add(shape);
                   }
               }
           }
           return es;
       }
        
       public EventShapes Get(Resource res) {
           EventShapes es = new EventShapes();

           foreach (KeyValuePair<string, EventShapes> kvp in RuleResourceShapes) {
               string r = kvp.Key.Split(':')[1];
               if (res.Name == r) {
                   foreach (List<EventShape> shapes in kvp.Value.List.Values) {
                       foreach (EventShape shape in shapes) {
                           es.Add(shape);
                       }
                   }
               }
           }

           return es;
       }

       public void Add(VisualizeRule rule, Resource res, EventShapes shapes) {
           RuleResourceShapes.Add(rule.Name + ":" + res.Name, shapes);
       }
       public EventShapes Get(VisualizeRule rule, Resource res) {
           return RuleResourceShapes[rule.Name + ":" + res.Name];
       }

       public EventShapes Get(VisualizeRule rule, Event e, Resource res) {
           EventShapes es = new EventShapes();
           foreach (List<EventShape> shapes in Get(rule, res).List.Values)
           {
               foreach (EventShape shape in shapes) {
                   if (shape.Event.Name == e.Name) {
                       es.Add(shape);
                   }
               }
           }
           return es;
       }
        

        #region IJsonable<VisualizeShapeData> メンバ
        public string ToJson()
        {
            var dict =
               new Dictionary<string,Dictionary<string,EventShapes>>();
            dict.Add("RuleShapes", RuleShapes);
            dict.Add("RuleResourceShapes", RuleResourceShapes);

            return ApplicationFactory.JsonSerializer.Serialize(dict); 
        }

        public VisualizeShapeData Parse(string data)
        {
            Dictionary<string, Dictionary<string, EventShapes>> dict =
                ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EventShapes>>>(data);
            VisualizeShapeData vizShape= new VisualizeShapeData();
            vizShape.RuleShapes = dict["RuleShapes"];
            vizShape.RuleResourceShapes = dict["RuleResourceShapes"];

            return vizShape; 
        }

        #endregion
    }
}
