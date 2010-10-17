using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Core.Search.Filters;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class SimpleSearch : TraceLogSearcher
    {
        private List<VisualizeLog> _visLogs;
        private decimal _currentTime;
        private SearchCondition _condition;
        private SearchFilter filter;

        public SimpleSearch()
        {
            _currentTime = 0;
            filter = new SimpleFilter();
        }

        public void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition)
        {
            _visLogs = visLogs;
            _condition = condition;
            _currentTime = ApplicationFactory.BlackBoard.CursorTime.Value;
        }

        public void setSearchData(SearchCondition condition, decimal currentTime)
        {
            _condition = condition;
            _currentTime = currentTime;
        }

        public decimal searchForward()
        {
            decimal resultTime = -1;
            int count = 0;
            foreach (VisualizeLog visLog in _visLogs)
            {
                if (filter.checkSearchCondition(visLog, _condition, _currentTime))
                {
                    if (visLog.fromTime > _currentTime)
                    {
                        resultTime = visLog.fromTime;
                        break;
                    }
                }
                count++;
            }

            return resultTime;
        }


        public decimal searchBackward()
        {
            decimal resultTime = -1;

            for(int i = _visLogs.Count -1  ; i>0; i--)
            {
                VisualizeLog visLog = _visLogs[i];
                if (filter.checkSearchCondition(visLog, _condition, _currentTime))
                {
                    if (visLog.fromTime < _currentTime)
                    {
                        resultTime = visLog.fromTime;
                        break;
                    }
                }
            }
            return resultTime;
        }


        public decimal[] searchWhole()
        {
            List<decimal> resultTimes = new List<decimal>();

            foreach (VisualizeLog visLog in _visLogs)
            {
                if (filter.checkSearchCondition(visLog, _condition, _currentTime))
                {
                    resultTimes.Add(visLog.fromTime);
                }
            }

            return resultTimes.ToArray<decimal>();
        }
    }
}
