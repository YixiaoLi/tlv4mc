/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;


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

                        if (i == _visLogs.Count - 1)//ログを全部なめても該当ログがない場合
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
                        if (_isAnd) //ANDの場合
                        {
                            if (refiningConditionNum == _refiningConditions.Count) //全部の絞り込み条件にマッチしたとき
                            {
                                return hitLog;
                            }
                        }
                        else //ORの場合
                        {
                            return hitLog;
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
                else
                {
                    return false;
                }
            }
            else if (condition.timing.Equals("以内に発生(基準時以後)"))
            {
                if ((Math.Abs(visLog.fromTime - normTime) <= decimal.Parse(condition.timingValue)) && (visLog.fromTime > normTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (condition.timing.Equals("以上前に発生"))
            {
                if (normTime - visLog.fromTime >= decimal.Parse(condition.timingValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (condition.timing.Equals("以上後に発生"))
            {
                if (visLog.fromTime - normTime >= decimal.Parse(condition.timingValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
