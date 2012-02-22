using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions
{
    public class BaseCondition
    {
        private int _conditionID;
        private string _resourceName;
        private string _resourceType;
        private string _ruleName;
        private string _ruleDisplayName;
        private string _eventName;
        private string _eventDisplayName;
        private string _eventDetail;
        private decimal _normTime;

        public int conditionID { set { _conditionID = value; } get { return _conditionID; } }
        public string resourceName { set { _resourceName = value; } get { return _resourceName; } }
        public string resourceType { set { _resourceType = value; } get { return _resourceType; } }
        public string ruleName { set { _ruleName = value; } get { return _ruleName; } }
        public string ruleDisplayName { set { _ruleDisplayName = value; } get { return _ruleDisplayName; } }
        public string eventName { set { _eventName = value; } get { return _eventName; } }
        public string eventDisplayName { set { _eventDisplayName = value; } get { return _eventDisplayName; } }
        public string eventDetail { set { _eventDetail = value; } get { return _eventDetail; } }
        public decimal normTime { set { _normTime = value; } get { return _normTime; } }

        public BaseCondition()
        {
            _resourceName = null;
            _resourceType = null;
            _ruleName = null;
            _eventName = null;
            _eventDetail = null;
            _normTime = -1;
        }
    }
}
