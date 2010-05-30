using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class SimpleSearch : TraceLogSearcher
    {
        private string _targetResource;
        private string _targetRule;
        private string _targetEvent;
        private string _targetEventDetail;
        private VisualizeShapeData _visShapeData;
        private decimal _currentTime;

        public SimpleSearch()
        {
            _targetResource = null;
            _targetRule = null;
            _targetEvent = null;
            _targetEventDetail = null;
            _visShapeData = null;
            _currentTime = 0;
        }

        public void setSearchData(string resource, string rule, string ev, string detail, VisualizeShapeData visShapeData, decimal time)
        {
            _targetResource = resource;
            _targetRule = rule;
            _targetEvent = ev;
            _targetEventDetail = detail;
            _visShapeData = visShapeData;
            _currentTime = time;
        }


        public decimal searchForward()
        {
            decimal searchTime = -1;
            
            //対象タスクに対して対象ルールが適用された際のデータセットを取得
            EventShapes ruleAppliedData = null;
            List<EventShape> eventAppliedData = null;
            if (_visShapeData.RuleResourceShapes.ContainsKey(_targetRule + ":" + _targetResource))
            {
                ruleAppliedData = _visShapeData.RuleResourceShapes[_targetRule + ":" + _targetResource];

                if (ruleAppliedData.List.ContainsKey(_targetRule + ":" + _targetEvent))
                {
                    //対象ルールの中で、対象イベントが適用された際のデータセットを取得
                    eventAppliedData = ruleAppliedData.List[_targetRule + ":" + _targetEvent];
                }
            }

            if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail == null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    if (shape.From.Value > _currentTime)
                    {
                        searchTime = shape.From.Value;
                        break;
                    }
                }
            }
            else if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail != null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    if ( shape.EventDetail  != null )
                    {
                        if( shape.EventDetail.Equals(_targetEventDetail) && (shape.From.Value > _currentTime ))
                        searchTime = shape.From.Value;
                        break;
                    }
                }
            }
            else
            {
                //エラー処理
            }

            return searchTime;
        }


        public decimal searchBackward()
        {
            decimal searchTime = -1;

            //対象タスクに対して対象ルールが適用された際のデータセットを取得
            EventShapes ruleAppliedData = null;
            List<EventShape> eventAppliedData = null;
            if (_visShapeData.RuleResourceShapes.ContainsKey(_targetRule + ":" + _targetResource))
            {
                ruleAppliedData = _visShapeData.RuleResourceShapes[_targetRule + ":" + _targetResource];

                if (ruleAppliedData.List.ContainsKey(_targetRule + ":" + _targetEvent))
                {
                    //対象ルールの中で、対象イベントが適用された際のデータセットを取得
                    eventAppliedData = ruleAppliedData.List[_targetRule + ":" + _targetEvent];

                    // foreach 文で後進検索を書くために 対象イベントのリストを逆転させる
                    eventAppliedData.Reverse();
                }
            }


            if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail == null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    if (shape.From.Value > _currentTime)
                    {
                        searchTime = shape.From.Value;
                        break;
                    }
                }
                // 検索が終了したので時系列を戻す
                eventAppliedData.Reverse();
            }
            else if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail != null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    if (shape.EventDetail != null)
                    {
                        if (shape.EventDetail.Equals(_targetEventDetail) && (shape.From.Value > _currentTime))
                            searchTime = shape.From.Value;
                        break;
                    }
                }
                // 検索が終了したので時系列を戻す
                eventAppliedData.Reverse();
            }
            else
            {
                //エラー処理
            }
            return searchTime;
        }


        public decimal[] searchWhole()
        {
            List<decimal> searchTime = new List<decimal>();

            //対象タスクに対して対象ルールが適用された際のデータセットを取得
            EventShapes ruleAppliedData = null;
            List<EventShape> eventAppliedData = null;
            if (_visShapeData.RuleResourceShapes.ContainsKey(_targetRule + ":" + _targetResource))
            {
                ruleAppliedData = _visShapeData.RuleResourceShapes[_targetRule + ":" + _targetResource];

                if (ruleAppliedData.List.ContainsKey(_targetRule + ":" + _targetEvent))
                {
                    //対象ルールの中で、対象イベントが適用された際のデータセットを取得
                    eventAppliedData = ruleAppliedData.List[_targetRule + ":" + _targetEvent];
                }
            }

            if (ruleAppliedData != null && eventAppliedData != null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    searchTime.Add(shape.From.Value);
                }
            }

            return searchTime.ToArray<decimal>();
        }
     }
}
