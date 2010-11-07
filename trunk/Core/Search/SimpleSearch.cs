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
        private decimal _normTime;
        private SearchCondition _condition;
        private SearchFilter _filter;

        public SimpleSearch()
        {
            _normTime = 0;
            _filter = new SimpleFilter();
        }

        public override void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition)
        {
            _visLogs = visLogs;
            _condition = condition;
        }

        // 簡易検索では↑の setSearchData と全く同じ動作
        public override void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition, Boolean isAnd)
        {
            _visLogs = visLogs;
            _condition = condition;
        }

        public override VisualizeLog searchForward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null;
            foreach (VisualizeLog visLog in _visLogs)
            {
                if (_filter.checkSearchCondition(visLog, _condition, _normTime))
                {
                    if (visLog.fromTime > _normTime)
                    {
                        hitLog = visLog;
                        break;
                    }
                }
            }
            return hitLog;
        }


        public override VisualizeLog searchBackward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null ;
            for(int i = _visLogs.Count -1  ; i>0; i--)
            {
                VisualizeLog visLog = _visLogs[i];
                if (_filter.checkSearchCondition(visLog, _condition, _normTime))
                {
                    if (visLog.fromTime < _normTime)
                    {
                        hitLog  = visLog;
                        break;
                    }
                }
            }
            return hitLog;
        }


        public override List<VisualizeLog> searchWhole()
        {
            List<VisualizeLog> hitLogs = new List<VisualizeLog>();
            foreach (VisualizeLog visLog in _visLogs)
            {
                if (_filter.checkSearchCondition(visLog, _condition, _normTime))
                {
                    hitLogs.Add(visLog);
                }
            }

            return hitLogs;
        }
    }
}
