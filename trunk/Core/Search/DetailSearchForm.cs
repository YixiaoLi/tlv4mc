using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    public partial class detailSearchForm : Form
    {
        private TraceLogVisualizerData _data=null;
        private List<ConditionSettingPanel> _conditionSettingPanels = null; //検索条件設定パネルのリスト
        private TraceLogSearcher _searcher;
        private List<VisualizeLog> _visLogs;
        private decimal _minTime = 0;
        private decimal _maxTime = 0;
        private int _timeRadix = 0;
        private int _nextPanelID = 1;
        private int _nextPanelLocationY = 0;
        public Button MarkerDleteButton = null;


        public detailSearchForm(TraceLogVisualizerData data, decimal minTime, decimal maxTime)
        {
            InitializeComponent();
            _data = data;
            _conditionSettingPanels = new List<ConditionSettingPanel>();
            _searcher = new DetailSearchWithTiming();
            _visLogs = getTimeSortedLog();
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
                    searchBackward(_conditionSettingPanels);
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
                    searchForward(_conditionSettingPanels);
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
                    searchWhole(_conditionSettingPanels);
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
            ConditionSettingPanel conditionSettingPanel = new ConditionSettingPanel(_data, _nextPanelID, conditionSettingArea.Size);
            conditionSettingPanel.Location = new System.Drawing.Point(0, _nextPanelLocationY);
            _conditionSettingPanels.Add(conditionSettingPanel);
            this.Height += conditionSettingPanel.Height;
            _nextPanelLocationY += conditionSettingPanel.Height + 5;
            _nextPanelID++;
            conditionSettingPanel.SizeChanged += (o, _e) =>
            {
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
                panels.Add(_conditionSettingPanels[conditionSettingPanel.getPanelID() - 1]);
                searchBackward(panels);
            };

            conditionSettingPanel.SearchForwardButton.Click += (o, _e) =>
            {
                List<ConditionSettingPanel> panels = new List<ConditionSettingPanel>();
                panels.Add(_conditionSettingPanels[conditionSettingPanel.getPanelID() - 1]);
                searchForward(panels);
            };

            conditionSettingPanel.SearchWholeButton.Click += (o, _e) =>
            {
                List<ConditionSettingPanel> panels = new List<ConditionSettingPanel>();
                panels.Add(_conditionSettingPanels[conditionSettingPanel.getPanelID() - 1]);
                searchWhole(panels);
            };

            conditionSettingPanel.baseConditionPanel.DeleteButton.Click += (o, _e) =>
            {
                deleteBaseCondition((int)conditionSettingPanel.getPanelID());
            };

            conditionSettingArea.Controls.Add(conditionSettingPanel);
        }

        private void deleteBaseCondition(int panelID)
        {
            this.Focus();
            _conditionSettingPanels.RemoveAt(panelID-1);
            updatePanel();
        }

        private void updatePanel() 
        {
            //一度 全てのパネルを消去し、その後あらためてパネル配置する
            conditionSettingArea.Controls.Clear();
            _nextPanelLocationY = 0;
            _nextPanelID = 1;
            this.Height = searchOperationArea.Height + 35;

         
            foreach(ConditionSettingPanel panel in _conditionSettingPanels)
            {
                panel.Location = new System.Drawing.Point(conditionSettingArea.Location.X, _nextPanelLocationY);
                panel.setPanelID(_nextPanelID);
                conditionSettingArea.Controls.Add(panel);
                _nextPanelLocationY += panel.Height + 5;
                _nextPanelID++;
                this.Height += panel.Height;
                //conditionSettingArea.Height += panel.Height;
            }
        }

        private void searchForward(List<ConditionSettingPanel> conditionSettingPanels)
        {
            List<ErrorCondition> errorConditions = checkSearchConditions(conditionSettingPanels);
            if (errorConditions.Count == 0)
            {
                VisualizeLog hitLog = null;

                //検索条件を１セットずつ調べ、現在時刻により近い時刻を検索結果とする
                foreach (ConditionSettingPanel panel in conditionSettingPanels)
                {
                    _searcher.setSearchData(_visLogs, panel.getBaseCondition(), panel.getRefiningConditions(), panel.isAnd());
                    //if (panel.getRefiningConditions().Count > 1) ApplicationFactory.BlackBoard.isAnd = panel.isAnd();

                    VisualizeLog tmpHitLog = _searcher.searchForward(ApplicationFactory.BlackBoard.CursorTime.Value);
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

        private void searchBackward(List<ConditionSettingPanel> conditionSettingPanels)
        {
            List<ErrorCondition> errorConditions = checkSearchConditions(conditionSettingPanels);
            if (errorConditions.Count == 0)
            {
                VisualizeLog hitLog = null;
                //検索条件を１セットずつ調べ、現在時刻により近い時刻を検索結果とする
                foreach (ConditionSettingPanel panel in conditionSettingPanels)
                {
                    _searcher.setSearchData(_visLogs, panel.getBaseCondition(), panel.getRefiningConditions(), panel.isAnd());
                    //if (panel.getRefiningConditions().Count > 1) ApplicationFactory.BlackBoard.isAnd = panel.isAnd();

                    VisualizeLog tmpHitLog = _searcher.searchBackward(ApplicationFactory.BlackBoard.CursorTime.Value);
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


        private void searchWhole(List<ConditionSettingPanel> conditionSettingPanels)
        {
            List<ErrorCondition> errorConditions = checkSearchConditions(conditionSettingPanels);
            if (errorConditions.Count == 0)
            {
                List<VisualizeLog> hitLogs = new List<VisualizeLog>();
                Color color = ApplicationFactory.ColorFactory.RamdomColor(); //マーカーの色

                foreach (ConditionSettingPanel panel in conditionSettingPanels)
                {
                    _searcher.setSearchData(_visLogs, panel.getBaseCondition(), panel.getRefiningConditions(), panel.isAnd());
                    List<VisualizeLog> candidateHitLogs = _searcher.searchWhole();
                    foreach (VisualizeLog candidateHitLog in candidateHitLogs)
                    {
                        hitLogs.Add(candidateHitLog);
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

        private List<VisualizeLog> getTimeSortedLog()
        {
            List<VisualizeLog> sortedLogs = new List<VisualizeLog>(); //イベント情報を格納する

            //_visLogs の要素を作成し、 随時_visLogs に加えていく
            foreach (KeyValuePair<string, EventShapes> evntShapesList in this._data.VisualizeShapeData.RuleResourceShapes)
            {
                string[] ruleAndResName = evntShapesList.Key.Split(':'); //例えば"taskStateChange:LOGTASK"を切り分ける
                string resName = ruleAndResName[1];
                string ruleName = ruleAndResName[0];

                foreach (KeyValuePair<string, System.Collections.Generic.List<EventShape>> evntShapeList in evntShapesList.Value.List)
                {
                    string[] evntAndRuleName = evntShapeList.Key.Split(':');  // 例えば"taskStateChange:stateChangeEvent"を切り分ける
                    string evntName = evntAndRuleName[1];
                    foreach (EventShape evntShape in evntShapeList.Value)
                    {
                        if (evntShape.From.Value < 0)
                        {
                            sortedLogs.Add(new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, 0));
                        }
                        else
                        {
                            sortedLogs.Add(new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, evntShape.From.Value));
                        }
                    }
                }
            }

            //_visLogsの要素を時間でソート
            VisualizeLog[] tmpLogs = sortedLogs.ToArray();
            Array.Sort(tmpLogs);
            return tmpLogs.ToList();
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

        //全ての検索条件のフォーマットが正しいかどうかチェックする
        //不正になるのは、リソースが指定されていない、タイミングが指定されていない、
        //タイミングの値が指定されていない、タイミングの値が不正な値、の4通り
        private List<ErrorCondition> checkSearchConditions(List<ConditionSettingPanel> conditionSettingPanels)
        {
            int conditionSettingPanelNum = 1;
            int refiningConditionNum = 1;
            List<ErrorCondition> errorConditions = new List<ErrorCondition>();

            foreach (ConditionSettingPanel panel in conditionSettingPanels)
            {
                ErrorCondition errorCondition = new ErrorCondition();
                if (panel.getBaseCondition().resourceName == null)
                {
                    errorCondition.PanelNum = conditionSettingPanelNum;
                    errorCondition.ErrorMessage += "基本条件" + conditionSettingPanelNum + //
                                                   " のリソースが指定されていません" + System.Environment.NewLine;
                }

                foreach (SearchCondition refiningCondition in panel.getRefiningConditions())
                {
                    if (refiningCondition.resourceName == null)
                    {
                        errorCondition.PanelNum = conditionSettingPanelNum;
                        errorCondition.ConditionNum = refiningConditionNum;
                        errorCondition.ErrorMessage += "基本条件" + errorCondition.PanelNum + //
                                                       "_絞込み条件" + errorCondition.ConditionNum + " のリソースが指定されていません" + System.Environment.NewLine;
                    }
                    
                    if (refiningCondition.timing == null)
                    {
                        errorCondition.PanelNum = conditionSettingPanelNum;
                        errorCondition.ConditionNum = refiningConditionNum;
                        errorCondition.ErrorMessage += "基本条件" + errorCondition.PanelNum + //
                                                       "_絞込み条件" + errorCondition.ConditionNum + " のタイミングが指定されていません" + System.Environment.NewLine;
                    }

                    if (refiningCondition.timingValue == null)
                    {
                        errorCondition.PanelNum = conditionSettingPanelNum;
                        errorCondition.ConditionNum = refiningConditionNum;
                        errorCondition.ErrorMessage += "基本条件" + errorCondition.PanelNum + //
                                                       "_絞込み条件" + errorCondition.ConditionNum + " のタイミング値が指定されていません" + System.Environment.NewLine;
                    }
                    else
                    {
                        try
                        {
                            decimal timingValue = decimal.Parse(refiningCondition.timingValue);
                        }
                        catch (FormatException _e)
                        {
                            errorCondition.PanelNum = conditionSettingPanelNum;
                            errorCondition.ConditionNum = refiningConditionNum;
                            errorCondition.ErrorMessage += "基本条件" + errorCondition.PanelNum + //
                                                           "_絞込み条件" + errorCondition.ConditionNum + " のタイミング値が不正です" + System.Environment.NewLine;
                        }
                        catch (Exception _e)
                        {
                            errorCondition.PanelNum = conditionSettingPanelNum;
                            errorCondition.ConditionNum = refiningConditionNum;
                            errorCondition.ErrorMessage += "基本条件" + errorCondition.PanelNum + //
                                                           "_絞込み条件" + errorCondition.ConditionNum + " になんらかのエラーがあります" + System.Environment.NewLine;
                        }
                    }
                    refiningConditionNum++;
                }

                if (errorCondition.PanelNum != 0)
                {
                    errorConditions.Add(errorCondition);
                }
                conditionSettingPanelNum++;
                refiningConditionNum = 1;
            }

            return errorConditions;
        }


        private class ErrorCondition
        {
            private int _panelNum;
            private int _conditionNum;
            private string _errorMessage;
            public int PanelNum { set { _panelNum = value; } get { return _panelNum; } }
            public int ConditionNum { set { _conditionNum = value; } get { return _conditionNum; } }
            public string ErrorMessage { set { _errorMessage = value; } get { return _errorMessage; } }

            public ErrorCondition()
            {
                _panelNum = 0;
                _conditionNum = 0;
                _errorMessage = "";
            }
        }
    }
}
