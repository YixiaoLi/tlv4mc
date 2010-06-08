using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class SimpleSearch : TraceLogSearcher
    {
        private string _targetResource;
        private string _targetRule;
        private string _targetEvent;
        private string _targetEventDetail;
        private List<VisualizeLog> _visLogs;
        private decimal _currentTime;

        public SimpleSearch()
        {
            _targetResource = null;
            _targetRule = null;
            _targetEvent = null;
            _targetEventDetail = null;
            _visLogs = null;
            _currentTime = 0;
        }

        public void setSearchData(string resource, string rule, string ev, string detail, List<VisualizeLog> log, decimal time)
        {
            _targetResource = resource;
            _targetRule = rule;
            _targetEvent = ev;
            _targetEventDetail = detail;
            _visLogs = log;
            _currentTime = time;
        }


        public decimal searchForward()
        {
            decimal searchTime = -1;

            foreach (VisualizeLog visLog in _visLogs)
            {
                if (visLog.resourceName.Equals(_targetResource)) //リソース名の一致を確認
                {
                    if(visLog.ruleName.Equals(_targetRule))  //ルール名の一致を確認
                    {
                        if (visLog.evntName.Equals(_targetEvent)) //イベント名の一致を確認
                        {
                            if (_targetEventDetail != null)  // イベント詳細が指定されているかを確認
                            {
                                if (visLog.evntDetail.Equals(_targetEventDetail))  //イベント詳細の一致を確認
                                {
                                    if (_currentTime < visLog.fromTime)
                                    {
                                        searchTime = visLog.fromTime;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (_currentTime < visLog.fromTime)
                                {
                                    searchTime = visLog.fromTime;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // "*"を想定
                        }
                    }
                }
                else
                {
                    // "*"を想定
                }
            }
            return searchTime;
        }


        public decimal searchBackward()
        {
            decimal searchTime = -1;

            for(int i = _visLogs.Count -1  ; i>0; i--)
            {
                VisualizeLog visLog  = _visLogs[i];
                if (visLog.resourceName.Equals(_targetResource)) //リソース名の一致を確認
                {
                    if (visLog.ruleName.Equals(_targetRule))  //ルール名の一致を確認
                    {
                        if (visLog.evntName.Equals(_targetEvent)) //イベント名の一致を確認
                        {
                            if (_targetEventDetail != null)  // イベント詳細が指定されているかを確認
                            {
                                if (visLog.evntDetail.Equals(_targetEventDetail))  //イベント詳細の一致を確認
                                {
                                    if (_currentTime > visLog.fromTime)
                                    {
                                        searchTime = visLog.fromTime;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (_currentTime > visLog.fromTime)
                                {
                                    searchTime = visLog.fromTime;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // "*"を想定
                        }

                    }
                }
                else
                {
                    // "*"を想定
                }
            }
            return searchTime;
        }


        public decimal[] searchWhole()
        {
            List<decimal> searchTime = new List<decimal>();

            foreach (VisualizeLog visLog in _visLogs)
            {
                if (visLog.resourceName.Equals(_targetResource)) //リソース名の一致を確認
                {
                    if (visLog.ruleName.Equals(_targetRule))  //ルール名の一致を確認
                    {
                        if (visLog.evntName.Equals(_targetEvent)) //イベント名の一致を確認
                        {
                            if (_targetEventDetail != null)  // イベント詳細が指定されているかを確認
                            {
                                if (visLog.evntDetail.Equals(_targetEventDetail))  //イベント詳細の一致を確認
                                {
                                    searchTime.Add(visLog.fromTime);
                                }
                            }
                            else
                            {
                                searchTime.Add(visLog.fromTime);
                            }

                        }
                        else
                        {
                            // "*"を想定
                        }

                    }
                }
                else
                {
                    // "*"を想定
                }
            }

            return searchTime.ToArray<decimal>();
        }

        private Boolean checkSearchCondition(int operation, VisualizeLog visLog)
        {
            Boolean result = false;
            if (visLog.resourceName.Equals(_targetResource)) //リソース名の一致を確認
            {
                if (_targetRule != null && visLog.ruleName.Equals(_targetRule))  //ルール名の一致を確認
                {
                    if (_targetEvent != null && visLog.evntName.Equals(_targetEvent)) //イベント名の一致を確認
                    {
                        if (_targetEventDetail != null)  // イベント詳細が指定されているかを確認
                        {
                            if (_targetEventDetail != null && visLog.evntDetail.Equals(_targetEventDetail))  //イベント詳細の一致を確認
                            {
                                if (_currentTime < visLog.fromTime)
                                {
                                    // searchTime = visLog.fromTime;
                                    // break;
                                    result = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }
     }
}
