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

        public decimal searchForward()
        {
            decimal normTime = ApplicationFactory.BlackBoard.CursorTime.Value;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            //現在時刻よりもあとに基本条件のイベントが発生した時刻（基準時）を探す
            while ((normTime = _searcher.searchForward()) > 0)
            {
                if (_refiningConditions.Count == 0)
                {
                    return normTime;
                }

                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        VisualizeLog visLog = _visLogs[i];
                        if (_filter.checkSearchCondition(visLog, refiningCondition, normTime))
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
                            return normTime;
                        }
                    }
                    else
                    {
                        if (ApplicationFactory.BlackBoard.isAnd) //ANDの場合
                        {
                            return -1;
                        }
                    }
                }
            }

            //ここまで来るのは該当するイベントがなかった場合
            return -1;
        }

        public decimal searchBackward()
        {
            decimal normTime = ApplicationFactory.BlackBoard.CursorTime.Value;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            //現在時刻よりもあとに基本条件のイベントが発生した時刻を探す
            while ((normTime = _searcher.searchBackward()) > 0)
            {
                if (_refiningConditions.Count == 0)
                {
                    return normTime;
                }

                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        VisualizeLog visLog = _visLogs[i];
                        if (_filter.checkSearchCondition(visLog, refiningCondition, normTime))
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
                            return normTime;
                        }
                    }
                    else
                    {
                        if (ApplicationFactory.BlackBoard.isAnd) //ANDの場合
                        {
                            return -1;
                        }
                    }
                }
            }
            //ここまで来るのは該当するイベントがなかった場合
            return -1;
        }

        public decimal[] searchWhole()
        {
            List<decimal> normTimes = new List<decimal>();

            //main条件に合致する全時刻を取得
            _searcher.setSearchData(_visLogs, _baseCondition, null);
            decimal[] tmpNormTimes = _searcher.searchWhole();

            foreach (decimal normTime in tmpNormTimes)
            {
                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        VisualizeLog visLog = _visLogs[i];
                        if (_filter.checkSearchCondition(visLog, refiningCondition, normTime))
                        {
                            normTimes.Add(normTime);
                        }
                    }
                }
            }
            return normTimes.ToArray();
        }
    }
}
