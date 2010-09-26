using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class SimpleSearch : TraceLogSearcher
    {
        private string _targetResource;
        private string _targetRule;
        private string _targetEvent;
        private string _targetEventDetail;
        private List<VisualizeLog> _visLogs;
        private decimal _currentTime;
        
       enum SearchType{
            Forward,
            Backward,
            Whole
        }

        public SimpleSearch(List<VisualizeLog> visLogs)
        {
            _targetResource = null;
            _targetRule = null;
            _targetEvent = null;
            _targetEventDetail = null;
            _visLogs = visLogs;
            _currentTime = 0;
        }

        public void setSearchData(SearchCondition condition)
        {
            _targetResource = condition.resourceName;
            _targetRule = condition.ruleName;
            _targetEvent = condition.eventName;
            _targetEventDetail = condition.eventDetail;
            _currentTime = ApplicationFactory.BlackBoard.CursorTime.Value;
        }

        public void setSearchData(SearchCondition condition, decimal currentTime)
        {
            _targetResource = condition.resourceName;
            _targetRule = condition.ruleName;
            _targetEvent = condition.eventName;
            _targetEventDetail = condition.eventDetail;
            _currentTime = currentTime;
        }


        public decimal searchForward()
        {
            decimal resultTime = -1;

            foreach (VisualizeLog visLog in _visLogs)
            {
                if (checkSearchCondition(SearchType.Forward, visLog))
                {
                    resultTime = visLog.fromTime;
                    break;
                }
            }

            return resultTime;
        }


        public decimal searchBackward()
        {
            decimal resultTime = -1;

            for(int i = _visLogs.Count -1  ; i>0; i--)
            {
                VisualizeLog visLog = _visLogs[i];
                if (checkSearchCondition(SearchType.Backward, visLog))
                {
                    resultTime = visLog.fromTime;
                    break;
                }
            }
            return resultTime;
        }


        public decimal[] searchWhole()
        {
            List<decimal> resultTimes = new List<decimal>();

            foreach (VisualizeLog visLog in _visLogs)
            {
                if (checkSearchCondition(SearchType.Whole, visLog))
                {
                    resultTimes.Add(visLog.fromTime);
                }
            }

            return resultTimes.ToArray<decimal>();
        }


        private Boolean checkSearchCondition(SearchType operation, VisualizeLog visLog)
        {
            if (!visLog.resourceName.Equals(_targetResource))
                return false;

            if (_targetRule != null)
            {
               if(!visLog.ruleName.Equals(_targetRule))
                  return false;
            }

            if(_targetEvent != null)
            {
               if(!visLog.evntName.Equals(_targetEvent))
                  return false;
            }

            if(_targetEventDetail != null)  // イベント詳細が指定されているかを確認
            {
                if (!visLog.evntDetail.Equals(_targetEventDetail))
                    return false;
            }


            if(operation == SearchType.Forward)
            {
                if (_currentTime < visLog.fromTime){  return true;}
                else{ return false;}
            }
            else if(operation == SearchType.Backward)
            {
                if (_currentTime > visLog.fromTime){  return true;}
                else{ return false;}
            }
            else if (operation == SearchType.Whole)
            {
                return true; //全体検索は全時刻を返すため、現在時刻との比較は必要ない
            }
            else
            {
                return false;
            }
        }
     }
}
