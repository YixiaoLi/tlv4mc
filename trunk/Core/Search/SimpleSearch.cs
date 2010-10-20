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

        public VisualizeLog searchForward()
        {
            VisualizeLog hitLog = null;
            _currentTime = ApplicationFactory.BlackBoard.CursorTime.Value;
            foreach (VisualizeLog visLog in _visLogs)
            {
                if (filter.checkSearchCondition(visLog, _condition, _currentTime))
                {
                    if (visLog.fromTime > _currentTime)
                    {
                        hitLog = visLog;
                        break;
                    }
                }
            }
            return hitLog;
        }


        public VisualizeLog searchBackward()
        {
            VisualizeLog hitLog = null ;
            _currentTime = ApplicationFactory.BlackBoard.CursorTime.Value;
            for(int i = _visLogs.Count -1  ; i>0; i--)
            {
                VisualizeLog visLog = _visLogs[i];
                if (filter.checkSearchCondition(visLog, _condition, _currentTime))
                {
                    if (visLog.fromTime < _currentTime)
                    {
                        hitLog  = visLog;
                        break;
                    }
                }
            }
            return hitLog;
        }


        public List<VisualizeLog> searchWhole()
        {
            List<VisualizeLog> hitLogs = new List<VisualizeLog>();

            foreach (VisualizeLog visLog in _visLogs)
            {
                if (filter.checkSearchCondition(visLog, _condition, _currentTime))
                {
                    hitLogs.Add(visLog);
                }
            }

            return hitLogs;
        }
    }
}
