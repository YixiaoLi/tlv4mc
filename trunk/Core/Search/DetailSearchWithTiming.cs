using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class DetailSearchWithTiming : TraceLogSearcher
    {
        private SearchCondition _mainCondition = null;
        private List<SearchCondition> _refiningConditions = null;
        private List<VisualizeLog> _visLogs = null;
        private SimpleSearch simpleSearch = null;

        public DetailSearchWithTiming(List<VisualizeLog> logs)
        {
            _visLogs = logs;
            simpleSearch = new SimpleSearch(logs);
        }

        public void setData(SearchCondition mainCondition, List<SearchCondition> refiningConditions)
        {
            _mainCondition = mainCondition;
            _refiningConditions = refiningConditions;

            //絞り込み条件はリストの後ろから順に適用していくため、あらかじめ反転させておく
            _refiningConditions.Reverse(0,_refiningConditions.Count-1);
        }

        public decimal searchForward()
        {
            decimal resultTime = ApplicationFactory.BlackBoard.CursorTime.Value;

            while (resultTime > 0)
            {
                //main条件に合致する、現在時刻よりも前の一番近い時刻を検索
                simpleSearch.setSearchData(_mainCondition, resultTime);
                resultTime = simpleSearch.searchForward();

                //絞り込み条件によるフィルタリング
                if (checkConditions( resultTime, 0, _refiningConditions[0])) break;
            }

            if (resultTime > 0)
            { return resultTime; }
            else
            { return -1; }
        }

        public decimal searchBackward()
        {
            decimal resultTime = ApplicationFactory.BlackBoard.CursorTime.Value;

            while (resultTime > 0)
            {
                //main条件に合致する、現在時刻よりも前の一番近い時刻を検索
                simpleSearch.setSearchData(_mainCondition, resultTime);
                decimal result = simpleSearch.searchForward();

                //絞り込み条件によるフィルタリング
                if (checkConditions(resultTime, 0, _refiningConditions[0])) break;
            }
            return resultTime;

            return resultTime;
        }

        public decimal[] searchWhole()
        {
            List<decimal> resultTimes = new List<decimal>();
            return resultTimes.ToArray();
        }


        //再帰的に全ての絞り込み条件に合致するかどうかを調べる
        private Boolean checkConditions(decimal normTime, int conditionIndex, SearchCondition refiningCondition)
        {
            if (refiningCondition.timing.Equals("以内"))
            {
            }
            else if (refiningCondition.timing.Equals("以前"))
            {
            }
            else if (refiningCondition.timing.Equals("以後"))
            {
            }
            else if (refiningCondition.timing.Equals("直前"))
            {
            }
            else if (refiningCondition.timing.Equals("直後"))
            {
            }
            else
            {
                //例外発生  （notSurpotedTimingValueException を作る？）
                return false;
            }
            
            return false;
        }
    }
}
