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
        private decimal _normTime;
        private Boolean _isAnd = true;

        public DetailSearchWithTiming()
        {
            _searcher = new SimpleSearch();
        }

        public override void setSearchData(List<VisualizeLog> logs, SearchCondition mainCondition, List<SearchCondition> refiningConditions)
        {
            _visLogs = logs;
            _baseCondition = mainCondition;
            _refiningConditions = refiningConditions;
        }

        public override void setSearchData(List<VisualizeLog> logs, SearchCondition mainCondition, List<SearchCondition> refiningConditions, Boolean isAnd)
        {
            _visLogs = logs;
            _baseCondition = mainCondition;
            _refiningConditions = refiningConditions;
            _isAnd = isAnd;
        }

        public override VisualizeLog searchForward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            while ((hitLog = _searcher.searchForward(_normTime)) != null) //現在時刻よりもあとに基本条件のイベントが発生しているログを見つけ、絞込み条件と照合する
            {
                _normTime = hitLog.fromTime;
                if (_refiningConditions.Count == 0)
                {
                    return hitLog;
                }

                //絞り込み条件によるフィルタリング
                int refiningConditionNum = 0;
                foreach(SearchCondition refiningCondition in _refiningConditions)
                {
                    refiningConditionNum++;
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        if (checkSearchCondition(_visLogs[i], refiningCondition, hitLog.fromTime))
                        {
                            matchingFlag = true;
                            break;
                        }

                        if (i == _visLogs.Count - 1)
                        {
                            matchingFlag = false;
                        }
                    }

                    if (refiningCondition.denyCondition)
                    {
                        matchingFlag = !matchingFlag; //現在調べている絞込み条件で、条件の否定が選択されている場合、フィルタリング結果を反転させる
                    }

                    if (matchingFlag)
                    {
                        if (_isAnd) //ORの場合
                        {
                            return hitLog;
                        }
                        else //ANDの場合
                        {
                            if (refiningConditionNum == _refiningConditions.Count) //全部の絞り込み条件にマッチしたとき
                            {
                                return hitLog;
                            }
                        }
                    }
                    else
                    {
                        if (_isAnd) //ANDの場合
                        {
                            break; //今回の candidateHitLog は絞込み条件を満たさないので、次の candidateHitLog を調査する
                        }
                    }
                }
            }

            //ここまで来るのは該当するイベントがなかった場合
            return null;
        }

        public override VisualizeLog searchBackward(decimal normTime)
        {
            _normTime = normTime;
            VisualizeLog hitLog = null;
            Boolean matchingFlag = false;
            _searcher.setSearchData(_visLogs, _baseCondition, null);

            //現在時刻よりもあとに基本条件のイベントが発生した時刻を探す
            while ((hitLog = _searcher.searchBackward(_normTime)) !=  null)
            {
                _normTime = hitLog.fromTime;
                if (_refiningConditions.Count == 0)
                {
                    return hitLog;
                }

                //絞り込み条件によるフィルタリング
                int refiningConditionNum = 0;
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    refiningConditionNum++;
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        if (checkSearchCondition(_visLogs[i], refiningCondition, hitLog.fromTime))
                        {
                            matchingFlag = true;
                            break;
                        }

                        if (i == _visLogs.Count - 1)
                        {
                            matchingFlag = false;
                        }
                    }

                    if (refiningCondition.denyCondition)
                    {
                        matchingFlag = !matchingFlag;
                    }


                    if (matchingFlag)
                    {
                        if (_isAnd) //ORの場合
                        {
                            return hitLog;
                        }
                        else //ANDの場合
                        {
                            if (refiningConditionNum == _refiningConditions.Count) //全部の絞り込み条件にマッチしたとき
                            {
                                return hitLog;
                            }
                        }
                    }
                    else
                    {
                        if (_isAnd) //ANDの場合
                        {
                             break;
                        }
                    }
                }
            }
            //ここまで来るのは該当するイベントがなかった場合
            return null;
        }

        public override List<VisualizeLog> searchWhole()
        {
            _searcher.setSearchData(_visLogs, _baseCondition, null);
            List<VisualizeLog> candidateHitLogs = _searcher.searchWhole(); //基本条件に合致する全可視化ログを取得
            List<VisualizeLog> hitLogs = new List<VisualizeLog>();
            
            foreach (VisualizeLog candidateHitLog in candidateHitLogs)//hitLogs の各要素に対して絞込み条件でフィルタリングをかける
            {
                if (_refiningConditions.Count == 0) //絞込み条件がなければ、候補として挙がったログはすべて条件を満たす
                {
                    hitLogs.Add(candidateHitLog);
                }

                int refiningConditionNum = 0;
                Boolean matchingFlag = false;
                foreach (SearchCondition refiningCondition in _refiningConditions)
                {
                    refiningConditionNum++;
                    for (int i = 0; i < _visLogs.Count; i++)
                    {
                        if (checkSearchCondition(_visLogs[i], refiningCondition, candidateHitLog.fromTime))
                        {
                            matchingFlag = true;
                            break;
                        }

                        if (i == _visLogs.Count - 1)
                        {
                            matchingFlag = false;
                        }
                    }

                    if (refiningCondition.denyCondition)
                    {
                        matchingFlag = !matchingFlag;
                    }

                    if (matchingFlag)
                    {
                        if (_isAnd) //ORの場合
                        {
                            hitLogs.Add(candidateHitLog); //ORの場合は一つの絞り込み条件にマッチすればいいため、一つ当たった時点で candidateHitLog が正式採用される
                            break;
                        }
                        else //ANDの場合
                        {
                            if (refiningConditionNum == _refiningConditions.Count) //全部の絞り込み条件にマッチしたとき
                            {
                                hitLogs.Add(candidateHitLog);
                            }
                        }
                    }
                    else
                    {
                        if (_isAnd) //ANDの場合
                        {
                            break; //ANDの場合は全部の絞込み条件にマッチしないといけないので、一つはずした時点で candidateHitLog は候補から外れる
                        }
                    }
                }
            }
            return hitLogs;
        }

        private Boolean checkSearchCondition(VisualizeLog visLog, SearchCondition condition, decimal normTime)
        {
            if (!visLog.resourceName.Equals(condition.resourceName))
                return false;

            if (condition.ruleName != null)
            {
                if (!visLog.ruleName.Equals(condition.ruleName))
                    return false;
            }

            if (condition.eventName != null)
            {
                if (!visLog.evntName.Equals(condition.eventName))
                    return false;
            }

            if (condition.eventDetail != null)  // イベント詳細が指定されているかを確認
            {
                if (!visLog.evntDetail.Equals(condition.eventDetail))
                    return false;
            }

            if (condition.timing.Equals("以内に発生(基準時以前)"))
            {
                // 時間制約による判定
                if ((Math.Abs(visLog.fromTime - normTime) <= decimal.Parse(condition.timingValue)) && (visLog.fromTime < normTime))
                {
                    return true;
                }
            }
            else if (condition.timing.Equals("以内に発生(基準時以後)"))
            {
                if ((Math.Abs(visLog.fromTime - normTime) <= decimal.Parse(condition.timingValue)) && (visLog.fromTime > normTime))
                {
                    return true;
                }
            }
            else if (condition.timing.Equals("以上前に発生"))
            {
                if (normTime - visLog.fromTime >= decimal.Parse(condition.timingValue))
                {
                    return true;
                }
            }
            else if (condition.timing.Equals("以上後に発生"))
            {
                if (visLog.fromTime - normTime >= decimal.Parse(condition.timingValue))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        
        
    }
}
