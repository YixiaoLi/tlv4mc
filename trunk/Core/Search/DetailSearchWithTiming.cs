using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Core.Search.Filters;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class DetailSearchWithTiming : TraceLogSearcher
    {
        private SearchCondition _baseCondition = null;
        private List<SearchCondition> _refiningConditions = null;
        private List<VisualizeLog> _visLogs = null;
        private TraceLogSearcher _searcher = null;
        private SearchFilter _filter = null;
        private decimal _normTime;

        public DetailSearchWithTiming()
        {
            _searcher = new SimpleSearch();
            _filter = new TimingFilter();
        }

        public void setSearchData(List<VisualizeLog> logs, SearchCondition mainCondition, List<SearchCondition> refiningConditions)
        {
            _visLogs = logs;
            _baseCondition = mainCondition;
            _refiningConditions = refiningConditions;
        }

        public VisualizeLog searchForward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            while ((hitLog = _searcher.searchForward(normTime)) != null) //現在時刻よりもあとに基本条件のイベントが発生しているログを見つけ、絞込み条件と照合する
            {
                normTime = hitLog.fromTime;
                if (_refiningConditions.Count == 0)
                {
                    return hitLog;
                }

                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        if (_filter.checkSearchCondition(_visLogs[i], refiningCondition, hitLog.fromTime))
                        {
                            matchingFlag = true;
                            break;
                        }

                        if (i == _visLogs.Count - 1)
                        {
                            matchingFlag = false;
                        }
                    }

                    if (matchingFlag)
                    {
                        if (!ApplicationFactory.BlackBoard.isAnd) //ORの場合
                        {
                            return hitLog;
                        }
                    }
                    else
                    {
                        if (ApplicationFactory.BlackBoard.isAnd) //ANDの場合
                        {
                            return null;
                        }
                    }
                }
            }

            //ここまで来るのは該当するイベントがなかった場合
            return null;
        }

        public VisualizeLog searchBackward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            //現在時刻よりもあとに基本条件のイベントが発生した時刻を探す
            while ((hitLog = _searcher.searchBackward(normTime)) !=  null)
            {
                _normTime = hitLog.fromTime;
                if (_refiningConditions.Count == 0)
                {
                    return hitLog;
                }

                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        if (_filter.checkSearchCondition(_visLogs[i], refiningCondition, hitLog.fromTime))
                        {
                            matchingFlag = true;
                            break;
                        }

                        if (i == _visLogs.Count - 1)
                        {
                            matchingFlag = false;
                        }
                    }

                    if (matchingFlag)
                    {
                        if (!ApplicationFactory.BlackBoard.isAnd) //ORの場合
                        {
                            return hitLog;
                        }
                    }
                    else
                    {
                        if (ApplicationFactory.BlackBoard.isAnd) //ANDの場合
                        {
                            return null;
                        }
                    }
                }
            }
            //ここまで来るのは該当するイベントがなかった場合
            return null;
        }

        public List<VisualizeLog> searchWhole()
        {
            //main条件に合致する全時刻を取得
            _searcher.setSearchData(_visLogs, _baseCondition, null);
            List<VisualizeLog> hitLogs =  _searcher.searchWhole();

            foreach(VisualizeLog hitLog in hitLogs)
            {
                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        if (_filter.checkSearchCondition(_visLogs[i], refiningCondition, hitLog.fromTime))
                        {
                            hitLogs.Add(hitLog);
                        }
                    }
                }
            }
            return hitLogs;
        }
    }
}
