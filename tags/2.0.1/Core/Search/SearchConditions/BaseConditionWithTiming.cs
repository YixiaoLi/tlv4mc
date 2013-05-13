using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions
{
    public class BaseConditionWithTiming : BaseCondition
    {
        private string _timing;
        private string _timingValue;
        private Boolean _denyCondition;

        public string timing { set { _timing = value; } get { return _timing; } }
        public string timingValue { set { _timingValue = value; } get { return _timingValue; ; } }
        public Boolean denyCondition { set { _denyCondition = value; } get { return _denyCondition; ; } }

        public BaseConditionWithTiming()
        {
            _timing = null;
            _timingValue = null;
            _denyCondition = false;
        }
    }
}
