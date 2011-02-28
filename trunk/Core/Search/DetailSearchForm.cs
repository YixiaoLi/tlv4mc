using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions;
using NU.OJL.MPRTOS.TLV.Core.Search.Filters;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    public partial class DetailSearchForm : Form
    {
        private TraceLogVisualizerData _data=null;
        private List<ConditionSettingPanel> _conditionSettingPanels = null; //検索条件設定パネルのリスト
        private TraceLogSearcher _searcher;
        private List<VisualizeLog> _eventLogs;
        private decimal _minTime = 0;
        private decimal _maxTime = 0;
        private int _timeRadix = 0;
        private int _nextPanelLocationY = 0;
        public Button MarkerDleteButton = null;


        public DetailSearchForm(TraceLogVisualizerData data, List<VisualizeLog> eventLogs, decimal minTime, decimal maxTime)
        {
            InitializeComponent();
            _data = data;
            _eventLogs = eventLogs;
            _conditionSettingPanels = new List<ConditionSettingPanel>();
            _searcher = new TraceLogSearcher();
            _minTime = minTime;
            _maxTime = maxTime;
            _timeRadix = _data.ResourceData.TimeRadix;
            MarkerDleteButton = markerDeleteButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            addConditionButton.Click += (o, _e) =>
            {
                addConditionSettingPanel();
            };

            this.FormClosing += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = false;
            };

            searchBackwardButton.Click += (o, _e) =>
            {
                if (_conditionSettingPanels.Count > 0)
                {
                    List<ErrorCondition> errorConditions = getErrorConditions(_conditionSettingPanels);
                    searchBackward(_conditionSettingPanels, errorConditions);
                }
                else
                {
                    MessageBox.Show("検索条件を作成してください");
                }
            };

            searchForwardButton.Click += (o, _e) =>
            {
                if (_conditionSettingPanels.Count > 0)
                {
                    List<ErrorCondition> errorConditions = getErrorConditions(_conditionSettingPanels);
                    searchForward(_conditionSettingPanels, errorConditions);
                }
                else
                {
                    MessageBox.Show("検索条件を作成してください");
                }
            };

            searchWholeButton.Click += (o, _e) =>
            {
                if (_conditionSettingPanels.Count > 0)
                {
                    List<ErrorCondition> errorConditions = getErrorConditions(_conditionSettingPanels);
                    searchWhole(_conditionSettingPanels, errorConditions);
                }
                else
                {
                    MessageBox.Show("検索条件を作成してください");
                }
            };

            cursorScrollBar.Scroll += (o, _e) =>
            {
                changeCursorValue();
            };

            conditionSettingArea.Scroll += (o, _e) =>
            {
                conditionSettingArea.Focus();
            };
        }

        private void addConditionSettingPanel()
        {
            this.HorizontalScroll.Value = 0;
            ConditionSettingPanel conditionSettingPanel = new ConditionSettingPanel(_data, _eventLogs, _conditionSettingPanels.Count);
            conditionSettingPanel.Location = new System.Drawing.Point(0, _nextPanelLocationY);
            _conditionSettingPanels.Add(conditionSettingPanel);
            this.Height += conditionSettingPanel.Height + 5;
            _nextPanelLocationY += conditionSettingPanel.Height + 5;
            conditionSettingPanel.SizeChanged += (o, _e) =>
            {
                if (conditionSettingArea.VerticalScroll.Visible)
                {
                    conditionSettingArea.VerticalScroll.Value = 0;
                    //上記処理がないと、番号の大きい（３番目以上）基本条件のパネルで絞込み条件の削除を
                    //行った際に、 conditionSettingAreaに出現していた垂直スクロールバーが消えてしまい、
                    //若い番号の基本条件にもどれなくなる不具合が発生する

                    conditionSettingArea.Focus();
                }

                conditionSettingArea.Controls.Clear();
                foreach (ConditionSettingPanel panel in _conditionSettingPanels)
                {
                    conditionSettingArea.Controls.Add(panel);
                }
            };

            conditionSettingPanel.SearchBackwardButton.Click += (o, _e) =>
            {
                // searchBackword を実行するには ConditionSettingPanel のリストを渡す必要がある。
                // よって自分自身を唯一の要素とするリストを作成し、 searchBackwardを実行する
                List<ConditionSettingPanel> panels = new List<ConditionSettingPanel>();
                panels.Add(_conditionSettingPanels[conditionSettingPanel.panelID]);
                List<ErrorCondition> errorConditions = getErrorConditions(panels);
                searchBackward(panels, errorConditions);
            };
            conditionSettingPanel.SearchForwardButton.Click += (o, _e) =>
            {
                List<ConditionSettingPanel> panels = new List<ConditionSettingPanel>();
                panels.Add(_conditionSettingPanels[conditionSettingPanel.panelID]);
                List<ErrorCondition> errorConditions = getErrorConditions(panels);
                searchForward(panels, errorConditions);
            };

            conditionSettingPanel.SearchWholeButton.Click += (o, _e) =>
            {
                List<ConditionSettingPanel> panels = new List<ConditionSettingPanel>();
                panels.Add(_conditionSettingPanels[conditionSettingPanel.panelID]);
                List<ErrorCondition> errorConditions = getErrorConditions(panels);
                searchWhole(panels, errorConditions);
            };

            conditionSettingPanel.baseConditionPanel.deleteButton.Click += (o, _e) =>
            {
                deleteBaseCondition((int)conditionSettingPanel.panelID);
            };

            conditionSettingArea.Controls.Add(conditionSettingPanel);
        }

        private void deleteBaseCondition(int panelID)
        {
            if (_conditionSettingPanels.Count > 0)
            {
                _conditionSettingPanels[0].Focus();
            }
            _conditionSettingPanels.RemoveAt(panelID);
            updatePanel();
        }

        private void updatePanel() 
        {
            //一度 全てのパネルを消去し、その後あらためてパネル配置する
            conditionSettingArea.Controls.Clear();
            int nextPanelID =0;
            _nextPanelLocationY = 0;
            this.Height = searchOperationArea.Height + 35;

         
            foreach(ConditionSettingPanel panel in _conditionSettingPanels)
            {
                panel.Location = new System.Drawing.Point(conditionSettingArea.Location.X, _nextPanelLocationY);
                panel.setPanelID(nextPanelID);
                if (panel.Width < conditionSettingArea.Width)
                {
                    panel.Width = conditionSettingArea.Width - 2;
                }
                conditionSettingArea.Controls.Add(panel);
                _nextPanelLocationY += panel.Height + 5;
                nextPanelID++;
                this.Height += panel.Height;
                //conditionSettingArea.Height += panel.Height;
            }
        }

        private void searchForward(List<ConditionSettingPanel> conditionSettingPanels, List<ErrorCondition> errorConditions)
        {
            if (errorConditions.Count == 0)
            {
                VisualizeLog hitLog = null;

                //検索条件を１セットずつ調べ、現在時刻により近い時刻を検索結果とする
                foreach (ConditionSettingPanel panel in conditionSettingPanels)
                {
                    List<SearchFilter> filters = panel.getFilters();
                    foreach (SearchFilter filter in filters)
                    {
                        _searcher.setSearchData(_eventLogs, filter);

                        VisualizeLog tmpHitLog = _searcher.searchForward();
                        if (hitLog == null) //最初のループ時の処理
                        {
                            hitLog = tmpHitLog;
                        }
                        else
                        {
                            if (tmpHitLog != null)
                            {
                                if (hitLog.fromTime > tmpHitLog.fromTime) //より現在時刻に近い方のログを採用
                                {
                                    hitLog = tmpHitLog;
                                }
                            }
                        }
                    }
                }

                if (hitLog != null)
                {
                    decimal start = _minTime;
                    decimal end = _maxTime;
                    if (hitLog.fromTime < start)
                    {
                        //カーソルを移動
                        ApplicationFactory.BlackBoard.CursorTime = new Time(start.ToString(), _timeRadix);
                    }
                    else
                    {
                        ApplicationFactory.BlackBoard.CursorTime = new Time(hitLog.fromTime.ToString(), _timeRadix);
                    }
                    //トレースログビューアにおいて、該当時刻のログをフォーカス
                    List<Time> focusTime = new List<Time>();
                    focusTime.Add(ApplicationFactory.BlackBoard.CursorTime);
                    ApplicationFactory.BlackBoard.SearchTime = focusTime;

                    //カーソルスクロールの位置を調整
                    decimal rate = (ApplicationFactory.BlackBoard.CursorTime.Value - _minTime) / (_maxTime - _minTime);
                    cursorScrollBar.Value = (int)(cursorScrollBar.Maximum * rate);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("検索の終わりです");
                }
            }
            else
            {
                string errorMessage = "";
                foreach (ErrorCondition error in errorConditions)
                {
                    errorMessage += error.ErrorMessage;
                }
                MessageBox.Show(System.Environment.NewLine + "検索条件に以下のエラーがあります" + System.Environment.NewLine + //
                                System.Environment.NewLine + errorMessage, "検索条件のエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void searchBackward(List<ConditionSettingPanel> conditionSettingPanels, List<ErrorCondition> errorConditions)
        {
            if (errorConditions.Count == 0)
            {
                VisualizeLog hitLog = null;
                //検索条件を１セットずつ調べ、現在時刻により近い時刻を検索結果とする
                foreach (ConditionSettingPanel panel in conditionSettingPanels)
                {
                    List<SearchFilter> filters = panel.getFilters();
                    foreach (SearchFilter filter in filters)
                    {
                        _searcher.setSearchData(_eventLogs, filter);

                        VisualizeLog tmpHitLog = _searcher.searchBackward();
                        if (hitLog == null) //最初のループ時の処理
                        {
                            hitLog = tmpHitLog;
                        }
                        else
                        {
                            if (tmpHitLog != null)
                            {
                                if (hitLog.fromTime > tmpHitLog.fromTime) //より現在時刻に近い方のログを採用
                                {
                                    hitLog = tmpHitLog;
                                }
                            }
                        }
                    }
                }

                if (hitLog != null)
                {
                    decimal start = _minTime;
                    decimal end = _maxTime;
                    if (hitLog.fromTime < start)
                    {
                        //カーソルを移動
                        ApplicationFactory.BlackBoard.CursorTime = new Time(start.ToString(), _timeRadix);
                    }
                    else
                    {
                        ApplicationFactory.BlackBoard.CursorTime = new Time(hitLog.fromTime.ToString(), _timeRadix);
                    }
                    //トレースログビューアにおいて、該当時刻のログをフォーカス
                    List<Time> focusTime = new List<Time>();
                    focusTime.Add(ApplicationFactory.BlackBoard.CursorTime);
                    ApplicationFactory.BlackBoard.SearchTime = focusTime;

                    //カーソルスクロールの位置を調整
                    decimal rate = (ApplicationFactory.BlackBoard.CursorTime.Value - _minTime) / (_maxTime - _minTime);
                    cursorScrollBar.Value = (int)(cursorScrollBar.Maximum * rate);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("検索の終わりです");
                }
            } 
            else
            {
                string errorMessage = "";
                foreach (ErrorCondition error in errorConditions)
                {
                    errorMessage += error.ErrorMessage;
                }

                MessageBox.Show(System.Environment.NewLine + "検索条件に以下のエラーがあります" + System.Environment.NewLine + //
                                System.Environment.NewLine + errorMessage, "検索条件のエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void searchWhole(List<ConditionSettingPanel> conditionSettingPanels, List<ErrorCondition> errorConditions)
        {
            if (errorConditions.Count == 0)
            {
                List<VisualizeLog> hitLogs = new List<VisualizeLog>();
                Color color = ApplicationFactory.ColorFactory.RamdomColor(); //マーカーの色

                foreach (ConditionSettingPanel panel in conditionSettingPanels)
                {
                    List<SearchFilter> filters = panel.getFilters();
                    foreach (SearchFilter filter in filters)
                    {
                        _searcher.setSearchData(_eventLogs, filter);

                        List<VisualizeLog> candidateHitLogs = _searcher.searchWhole();
                        foreach (VisualizeLog candidateHitLog in candidateHitLogs)
                        {
                            hitLogs.Add(candidateHitLog);
                        }
                    }
                }

                if (hitLogs.Count > 0)
                {
                    List<Time> resultTime = new List<Time>();
                    foreach (VisualizeLog hitLog in hitLogs)
                    {
                        ApplicationData.FileContext.Data.SettingData.LocalSetting.TimeLineMarkerManager.AddMarker(color, new Time(hitLog.fromTime.ToString(), _timeRadix));
                        resultTime.Add(new Time(hitLog.fromTime.ToString(), _timeRadix));
                    }
                    ApplicationFactory.BlackBoard.SearchTime = resultTime;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("該当イベントは存在しません");
                }


                // 検索ボタンが押された際に、Macroviewer にもマーカーを反映させるには再描画を
                // 促す必要がある。現在時刻が変更された際に再描画処理が発生するため、これを利用
                // する。（一瞬だけ現在時刻を変更する）
                Time current = ApplicationFactory.BlackBoard.CursorTime;
                Time dummy = new Time((current.Value - 1).ToString(), _timeRadix);
                ApplicationFactory.BlackBoard.CursorTime = dummy;
                ApplicationFactory.BlackBoard.CursorTime = current;
            }
            else
            {
                string errorMessage = "";
                foreach (ErrorCondition error in errorConditions)
                {
                    errorMessage += error.ErrorMessage;
                }
                MessageBox.Show(System.Environment.NewLine + "検索条件に以下のエラーがあります" + System.Environment.NewLine + //
                                System.Environment.NewLine + errorMessage, "検索条件のエラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
     
        private void changeCursorValue()
        {
            int hscrollBarValue = cursorScrollBar.Value;
            decimal cursorTime = _maxTime  * hscrollBarValue / cursorScrollBar.Maximum + _minTime;
            if (cursorTime > _maxTime)
            {
                cursorTime = _maxTime;
            }

            ApplicationFactory.BlackBoard.CursorTime = new Time(cursorTime.ToString(),_timeRadix);
        }

        private List<ErrorCondition> getErrorConditions(List<ConditionSettingPanel> targetPanels)
        {
            List<ErrorCondition> errorConditions = new List<ErrorCondition>();
            foreach (ConditionSettingPanel targetPanel in targetPanels)
            {
                List<ErrorCondition> tmpErrorConditions = targetPanel.getErrorConditions();
                foreach (ErrorCondition errorCondition in tmpErrorConditions)
                {
                    errorConditions.Add(errorCondition);
                }
            }
            return errorConditions;
        }


    }
}
