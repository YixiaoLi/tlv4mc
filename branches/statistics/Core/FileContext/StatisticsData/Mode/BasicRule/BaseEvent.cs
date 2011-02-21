using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
    public class BaseEvent
    {
        public List<string> ResourceNames { get; set; }
        public List<string> ResourceType { get; set; }
        public string AttributeName { get; set; }
        public Json AttributeValue { get; set; }
        public string BehaviorName { get; set; }
        public string BehaviorArg { get; set; }

        public BaseEvent()
        {
            ResourceNames = new List<string>();
            ResourceType = new List<string>();
            AttributeName = string.Empty;
            AttributeValue = Json.Empty;
            BehaviorName = string.Empty;
            BehaviorArg = string.Empty;
        }

        public List<string> GetResourceNameList(ResourceData data)
        {
            List<string> result = new List<string>();

            if (ResourceNames.Any<string>())
            {
                result.AddRange(ResourceNames);
            }
            foreach (string rt in ResourceType)
            {
                foreach (Resource res in data.Resources.Where<Resource>((r) => { return r.Type == rt && !result.Contains(r.Name); }))
                {
                    result.Add(res.Name);
                }
            }

            return result;
        }
    }
}
