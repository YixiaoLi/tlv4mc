using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search.Filters
{
    class SimpleFilter : SearchFilter
    {
        private List<VisualizeLog> _eventLogs;
        private BaseCondition _baseCondition;

        public SimpleFilter(List<VisualizeLog> eventLogs, BaseCondition baseCondition)
        {
            _eventLogs = eventLogs;
            _baseCondition = baseCondition;
        }

        public override Boolean doMatching(VisualizeLog targetLog)
        {
             if (!targetLog.resourceName.Equals(_baseCondition.resourceName))
                return false;

            if (_baseCondition.ruleName != null)
            {
                if (!targetLog.ruleName.Equals(_baseCondition.ruleName))
                    return false;
            }

            if (_baseCondition.eventName != null)
            {
                if (!targetLog.evntName.Equals(_baseCondition.eventName))
                    return false;
            }

            if (_baseCondition.eventDetail != null)  // イベント詳細が指定されているかを確認
            {
                if (!targetLog.evntDetail.Equals(_baseCondition.eventDetail))
                    return false;
            }

            return true;
        }
    }
}
