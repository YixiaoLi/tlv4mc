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
            decimal resultTime = ApplicationFactory.BlackBoard.CursorTime.Value;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            //現在時刻よりもあとに基本条件のイベントが発生した時刻を探す
            //基本条件に該当する時刻がなくなるまでループ
            while ((resultTime = _searcher.searchForward()) > 0)
            {
                if (_refiningConditions.Count == 0)
                {
                    return resultTime;
                }

                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        VisualizeLog visLog = _visLogs[i];
                        if (_filter.checkSearchCondition(visLog, refiningCondition, resultTime))
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
                            return resultTime;
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
            decimal resultTime = ApplicationFactory.BlackBoard.CursorTime.Value;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            //現在時刻よりもあとに基本条件のイベントが発生した時刻を探す
            //基本条件に該当する時刻がなくなるまでループ
            while ((resultTime = _searcher.searchBackward()) > 0)
            {
                if (_refiningConditions.Count == 0)
                {
                    return resultTime;
                }

                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        VisualizeLog visLog = _visLogs[i];
                        if (_filter.checkSearchCondition(visLog, refiningCondition, resultTime))
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
                            return resultTime;
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
            List<decimal> resultTimes = new List<decimal>();

            //main条件に合致する全時刻を取得
            _searcher.setSearchData(_visLogs, _baseCondition, null);
            decimal[] tmpTimes = _searcher.searchWhole();

            foreach (decimal time in tmpTimes)
            {
                //絞り込み条件によるフィルタリング
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        VisualizeLog visLog = _visLogs[i];
                        if (_filter.checkSearchCondition(visLog, refiningCondition, time))
                        {
                            resultTimes.Add(time);
                        }
                    }
                }
            }
            return resultTimes.ToArray();
        }
    }
}
