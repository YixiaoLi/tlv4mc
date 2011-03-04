using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions;

namespace NU.OJL.MPRTOS.TLV.Core.Search.Filters
{
    public class TimingFilter : DetailFilter
    {
        private List<VisualizeLog> _eventLogs;
        private BaseConditionWithTiming _timingCondition;

        public TimingFilter(List<VisualizeLog> eventLogs, BaseConditionWithTiming timingCondition, SearchFilter decorateFilter)
        {
            _eventLogs = eventLogs;
            _timingCondition = timingCondition;
            searchFilter = decorateFilter;
        }

        public override Boolean doMatching(VisualizeLog targetLog)
        {
            if (!searchFilter.doMatching(targetLog))
            {
                return false;
            }

            //以下、eventLogs内に格納されているイベントログ全てとのマッチングを行い、
            //targetLogの時刻に対して_timingConditionを満たすイベントログが存在するかどうか調べる
            decimal normTime = targetLog.fromTime;

            int logCount = 0;
            Boolean matchingFlag = false;

            foreach (VisualizeLog compareLog in _eventLogs)
            {
                if (targetLog.logID != compareLog.logID)
                {
                    if (doMatchWithBaseCondition(compareLog)) // 比較するログが絞込み条件の基本4条件を満たすかどうか調べる
                    {
                        if (doMatchWithTiming(targetLog.fromTime, compareLog))//targetLogに対する時間制約を満たすかどうか調べる
                        {
                            if (_timingCondition.denyCondition)
                            {
                                return false;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                logCount++;

                if (logCount == _eventLogs.Count)
                {
                    if (!_timingCondition.denyCondition)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private Boolean doMatchWithBaseCondition(VisualizeLog compareLog)
        {

            if (!compareLog.resourceName.Equals(_timingCondition.resourceName))
            {
                return false;
            }

            if (_timingCondition.ruleName != null)
            {
                if (!compareLog.ruleName.Equals(_timingCondition.ruleName))
                    return false;
            }

            if (_timingCondition.eventName != null)
            {
                if (!compareLog.evntName.Equals(_timingCondition.eventName))
                    return false;
            }

            if (_timingCondition.eventDetail != null)  // イベント詳細が指定されているかを確認
            {
                if (!compareLog.evntDetail.Equals(_timingCondition.eventDetail))
                    return false;
            }

            return true;
        }

        private Boolean doMatchWithTiming(decimal normTime, VisualizeLog compareLog)
        {
            if (_timingCondition.timing.Equals("以内に発生(基準時以前)"))
            {
                // 時間制約による判定
                if ((Math.Abs(compareLog.fromTime - normTime) <= decimal.Parse(_timingCondition.timingValue)) && (compareLog.fromTime < normTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (_timingCondition.timing.Equals("以内に発生(基準時以後)"))
            {
                if ((Math.Abs(compareLog.fromTime - normTime) <= decimal.Parse(_timingCondition.timingValue)) && (compareLog.fromTime > normTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (_timingCondition.timing.Equals("以上前に発生"))
            {
                if (normTime - compareLog.fromTime >= decimal.Parse(_timingCondition.timingValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (_timingCondition.timing.Equals("以上後に発生"))
            {
                if (compareLog.fromTime - normTime >= decimal.Parse(_timingCondition.timingValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
