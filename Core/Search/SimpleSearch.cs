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
                    if (  shape.From.Value > _currentTime  &&  shape.EventDetail  != null )
                    {
                        if (shape.EventDetail.Equals(_targetEventDetail))
                        {
                            searchTime = shape.From.Value;
                            break;
                        }
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
                }
            }


            if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail == null)
            {
                for (int i = eventAppliedData.Count; i < 0; i--)
                {
                    if (eventAppliedData[i].From.Value < _currentTime)
                    {
                        searchTime = eventAppliedData[i].From.Value;
                        break;
                    }
                }
            }
            else if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail != null)
            {
                for (int i = eventAppliedData.Count; i < 0; i--)
                {
                    if (eventAppliedData[i].From.Value < _currentTime &&  eventAppliedData[i].EventDetail != null)
                    {
                        if (eventAppliedData[i].EventDetail.Equals(_targetEventDetail))
                        {
                            searchTime = eventAppliedData[i].From.Value;
                            break;
                        }
                    }
                }
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

            if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail == null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    searchTime.Add(shape.From.Value);
                }
            }
            else if (ruleAppliedData != null && eventAppliedData != null && _targetEventDetail != null)
            {
                foreach (EventShape shape in eventAppliedData)
                {
                    if (shape.EventDetail.Equals(_targetEventDetail))
                    {
                        searchTime.Add(shape.From.Value);
                    }
                }

            }
            else
            {
                //エラー処理
            }

            return searchTime.ToArray<decimal>();
        }
     }
}
