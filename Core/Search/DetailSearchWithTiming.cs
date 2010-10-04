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

        public void setSearchData(SearchCondition mainCondition, List<SearchCondition> refiningConditions)
        {
            _mainCondition = mainCondition;
            _refiningConditions = refiningConditions;

            //絞り込み条件はリストの後ろから順に適用していくため、あらかじめ反転させておく
            if(_refiningConditions.Count > 1) _refiningConditions.Reverse(0,_refiningConditions.Count-1);
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
                if (_refiningConditions.Count > 0)
                {
                    if (checkConditions(resultTime, 0, _refiningConditions[0])) break;
                }
                else
                {
                    break;
                }
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

        }

        public decimal[] searchWhole()
        {
            List<decimal> resultTimes = new List<decimal>();
            return resultTimes.ToArray();
        }


        //再帰的に全ての絞り込み条件に合致するかどうかを調べる
        private Boolean checkConditions(decimal normTime, int conditionIndex, SearchCondition refiningCondition)
        {
            if (conditionIndex == _refiningConditions.Count)
            {
                return false;
            }


            if (refiningCondition.timing.Equals("以内"))
            {
            }
            else if (refiningCondition.timing.Equals("以前"))
            {
            }
            else if (refiningCondition.timing.Equals("以後"))
            {
                simpleSearch.setSearchData(_mainCondition, normTime);
                decimal tmpTime = simpleSearch.searchBackward();
                if (tmpTime == -1)
                {
                    return false;
                }

                decimal refiningTime = decimal.Parse(refiningCondition.timingValue);
                if (tmpTime + refiningTime < normTime) //絞り込み条件に合致した場合
                {
                    if (conditionIndex == _refiningConditions.Count - 1)
                    {   //他に絞り込み条件がなければ true を返す
                        return true;
                    }
                    else
                    {   //まだ絞り込み条件があるなら、次の条件とマッチングさせる
                        return checkConditions(normTime, conditionIndex + 1, _refiningConditions[conditionIndex]);
                    }
                }
                else
                {
                    return checkConditions(tmpTime, conditionIndex, _refiningConditions[conditionIndex]);;
                }
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
