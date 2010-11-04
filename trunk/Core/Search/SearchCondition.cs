﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //検索条件
    class SearchCondition
    {
        private string _resourceName;
        private string _resourceType;
        private string _ruleName;
        private string _ruleDisplayName;
        private string _eventName;
        private string _eventDisplayName;
        private string _eventDetail;
        private string _timing;
        private string _timingValue;
        private Boolean _denyCondition;

        public string resourceName { set { this._resourceName = value; } get { return _resourceName; } }
        public string resourceType { set { this._resourceType = value; } get { return _resourceType; } }
        public string ruleName { set { this._ruleName = value; } get { return _ruleName; } }
        public string ruleDisplayName { set { this._ruleDisplayName = value; } get { return _ruleDisplayName; } }
        public string eventName { set { this._eventName = value; } get { return _eventName; } }
        public string eventDisplayName { set { this._eventDisplayName = value; } get { return _eventDisplayName; } }
        public string eventDetail { set { this._eventDetail = value; } get { return _eventDetail; } }
        public string timing { set { this._timing = value; } get { return _timing; } }
        public string timingValue { set { this._timingValue = value; } get { return _timingValue; } }
        public Boolean denyCondition { set { this._denyCondition = value; } get { return _denyCondition; } }


        public SearchCondition()
        {
            _resourceName = null;
            _resourceType = null;
            _ruleName = null;
            _eventName = null;
            _eventDetail = null;
            _timing = null;
            _timingValue = null;
            _denyCondition = false;
        }
    }
}
