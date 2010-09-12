using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //一つの検索条件を担当するクラス
    class SearchCondition
    {
        private string _resourceName;
        private string _ruleName;
        private string _eventName;
        private string _eventDetail;
        private string _timing;
        private string _timingValue;

        public string resourceName { set { this._resourceName = value; } get { return _resourceName; } }
        public string ruleName { set { this._ruleName = value; } get { return _ruleName; } }
        public string eventName { set { this._eventName = value; } get { return _eventName; } }
        public string eventDetail{ set { this._eventDetail = value; } get { return _eventDetail; } }
        public string timing { set { this._timing = value; } get { return _timing; } }
        public string timingValue { set { this._timingValue = value; } get { return _timingValue; } }

        public SearchCondition()
        {
            _resourceName = null;
            _ruleName = null;
            _eventName = null;
            _eventDetail = null;
            _timing = null;
            _timingValue = null;
        }
    }
}
