using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search.Filters
{
    class SimpleFilter : SearchFilter
    {
        public Boolean checkSearchCondition(VisualizeLog visLog, SearchCondition condition, decimal normTime)
        {
            if (!visLog.resourceName.Equals(condition.resourceName))
                return false;

            if (condition.ruleName != null)
            {
                if (!visLog.ruleName.Equals(condition.ruleName))
                    return false;
            }

            if (condition.eventName != null)
            {
                if (!visLog.evntName.Equals(condition.eventName))
                    return false;
            }

            if (condition.eventDetail != null)  // イベント詳細が指定されているかを確認
            {
                if (!visLog.evntDetail.Equals(condition.eventDetail))
                    return false;
            }

            return true;
        }
    }
}
