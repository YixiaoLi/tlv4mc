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
        public Dictionary<string, EventShapes> RuleShapes;

        //  VisualizeRule Event
        public Dictionary<string, EventShapes> RuleEventShapes;

        //  Resource
        public Dictionary<string, EventShapes> ResourceShapes;

        //  VisualizeRule Resource
        public Dictionary<string, EventShapes> RuleResourceShapes;

        //  VisualizeRule Resource Event
        public Dictionary<string, EventShapes> RuleEventResourceShapes;



       public VisualizeShapeData() {
           RuleShapes = new Dictionary<string, EventShapes>();
           RuleEventShapes = new Dictionary<string, EventShapes>();
           ResourceShapes = new Dictionary<string, EventShapes>();
           RuleResourceShapes = new Dictionary<string, EventShapes>();
           RuleEventResourceShapes = new Dictionary<string, EventShapes>();
       }

       public void Add(VisualizeRule rule, EventShapes shapes)
       {
           RuleShapes.Add(rule.Name, shapes);
       }
       public EventShapes Get(VisualizeRule rule) {
           return RuleShapes[rule.Name];
       }

       public void Add(VisualizeRule rule, Event e, EventShapes shapes)
       {
           RuleEventShapes.Add(rule.Name + ":" + e.Name, shapes);
       }
       public EventShapes Get(VisualizeRule rule, Event e) {
           return RuleEventShapes[rule.Name + ":" + e.Name];
       }
        

       public void Add(Resource res, EventShapes shapes) {
           ResourceShapes.Add(res.Name, shapes); 
       }
       public EventShapes Get(Resource res) {
           return ResourceShapes[res.Name];
       }

       public void Add(VisualizeRule rule, Resource res, EventShapes shapes) {
           RuleResourceShapes.Add(rule.Name + ":" + res.Name, shapes);
       }
       public EventShapes Get(VisualizeRule rule, Resource res) {
           return RuleResourceShapes[rule.Name + ":" + res.Name];
       }

       public void Add(VisualizeRule rule, Event e, Resource res, EventShapes shapes)
       {
           RuleEventResourceShapes.Add(rule.Name + ":" + e.Name + ":" + res.Name, shapes);
       }
       public EventShapes Get(VisualizeRule rule, Event e, Resource res) {
           return RuleEventResourceShapes[rule.Name + ":" + e.Name + ":" + res.Name];
       }
        

        #region IJsonable<VisualizeShapeData> メンバ
        public string ToJson()
        {
            var dict =
               new Dictionary<string,Dictionary<string,EventShapes>>();
            dict.Add("RuleShapes", RuleShapes);
            dict.Add("RuleEventShapes", RuleEventShapes);
            dict.Add("ResourceShapes", ResourceShapes);
            dict.Add("RuleResourceShapes", RuleResourceShapes);
            dict.Add("RuleEventResourceShapes", RuleEventResourceShapes);
 
            return ApplicationFactory.JsonSerializer.Serialize(dict); 
        }

        public VisualizeShapeData Parse(string data)
        {
            Dictionary<string, Dictionary<string, EventShapes>> dict =
                ApplicationFactory.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, EventShapes>>>(data);
            VisualizeShapeData vizShape= new VisualizeShapeData();
            vizShape.RuleShapes = dict["RuleShapes"];
            vizShape.RuleEventShapes = dict["RuleEventShapes"];
            vizShape.ResourceShapes = dict["ResourceShapes"];
            vizShape.RuleResourceShapes = dict["RuleResourceShapes"];
            vizShape.RuleEventResourceShapes = dict["RuleEventResourceShapes"];

            return vizShape; 
        }

        #endregion
    }
}
