using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    public partial class DetailSearchPanel : Form
    {
        private TraceLogVisualizerData _data;
        private List<VisualizeLog> _timeSortedLog;
        private ConditionBeans _conditionRegister;
        private List<SearchConditionPanel> _searchConditionPanels;
        private DetailSearchWithTiming _detailSearchWithTiming;

        private decimal _minTime;
        private decimal _maxTime;
        private int _timeRadix;

        public DetailSearchPanel(TraceLogVisualizerData data, decimal minTime, decimal maxTime, int timeRadix)
        {
            InitializeComponent();
            mainRuleForm.Enabled = false;
            mainEventForm.Enabled = false;
            mainEventDetailForm.Enabled = false;
            _data = data;
            makeTimeSortedLog();
            _conditionRegister = new ConditionBeans();
            _searchConditionPanels = new List<SearchConditionPanel>();
            _detailSearchWithTiming = new DetailSearchWithTiming(_timeSortedLog);
            _minTime = minTime;
            _maxTime = maxTime;
            _timeRadix = timeRadix;
            makeMainResourceForm();
            makeRefiningConditionResourceForm();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.FormClosed += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = 0;
            };

            //以下、各フォームを操作したときの動作を定義
            mainResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                if (mainResourceForm.SelectedIndex != -1) 
                {                                         
                    if (mainRuleForm.Enabled == true)
                    {
                        _conditionRegister.mainRuleName = null;
                        _conditionRegister.mainEventName = null;
                        _conditionRegister.mainEventDetail = null;

                        mainEventForm.Enabled = false;
                        mainEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        mainRuleForm.Enabled = true;
                    }

                    makeMainRuleForm();
                    addMainConditionButton.Enabled = true;
                }
            };

            mainRuleForm.SelectedIndexChanged += (o, _e) =>
            {

                if (mainRuleForm.SelectedIndex != -1)
                {
                    if (mainEventForm.Enabled == true)
                    {
                        _conditionRegister.mainEventName = null;
                        _conditionRegister.mainEventDetail = null;

                        mainEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        mainEventForm.Enabled = true;
                    }
                    makeMainEventForm();          
                }
            };

            mainEventForm.SelectedIndexChanged += (o, _e) =>
            {
                if (mainEventForm.SelectedIndex != -1)
                {
                    if (mainEventDetailForm.Enabled == true)
                    {
                        _conditionRegister.mainEventDetail = null;
                    }
                    else
                    {
                        mainEventDetailForm.Enabled = true;
                    }
                    makeMainEventDetailForm();
                }
            };

            targetConditionForm.SelectedIndexChanged += (o, _e) =>
            {
                if (targetConditionForm.SelectedIndex != -1)
                {
                    makeRefiningConditionResourceForm();
                    refiningConditionResourceForm.Enabled = true;
                }
            };

            refiningConditionResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                if (refiningConditionResourceForm.SelectedIndex != -1)
                {
                    if (refiningConditionRuleForm.Enabled == true)
                    {
                        _conditionRegister.refiningRuleName = null;
                        _conditionRegister.refiningEventName = null;
                        _conditionRegister.refiningEventDetail = null;


                        refiningConditionEventForm.Enabled = false;
                        refiningConditionEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        refiningConditionRuleForm.Enabled = true;
                    }

                    timingForm.Enabled = true;
                    timingValueForm.Enabled = false;
                    timingForm.SelectedIndex = -1;
                    _conditionRegister.timing = null;
                    _conditionRegister.timingValue = null;

                    makeRefiningConditionRuleForm();
                }
            };

            refiningConditionRuleForm.SelectedIndexChanged += (o, _e) =>
            {
                if (refiningConditionResourceForm.SelectedIndex != -1)
                {
                    if (refiningConditionEventForm.Enabled == true)
                    {
                        _conditionRegister.refiningEventName = null;
                        _conditionRegister.refiningEventDetail = null;

                        refiningConditionEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        refiningConditionEventForm.Enabled = true;
                    }

                    makeRefiningConditionEventForm();
                }
            };

            refiningConditionEventForm.SelectedIndexChanged += (o, _e) =>
            {
                if (refiningConditionEventForm.SelectedIndex != -1)
                {
                    if (refiningConditionEventDetailForm.Enabled == true)
                    {
                        _conditionRegister.refiningEventDetail = null;
                        refiningConditionEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        refiningConditionEventForm.Enabled = true;
                    }
                }

                makeRefiningConditionEventDetailForm();
            };


            timingForm.SelectedIndexChanged += (o, _e) =>
            {
                if (timingForm.SelectedIndex != -1)
                {
                    if (timingForm.SelectedItem.Equals("直前") || timingForm.SelectedItem.Equals("直後"))
                    {
                        timingValueForm.Text = "";
                        timingValueForm.Enabled = false;
                        addRefiningConditionButton.Enabled = true;
                    }
                    else
                    {
                        timingValueForm.Text = "";
                        timingValueForm.Enabled = true;
                    }
                    _conditionRegister.timing = (string)timingForm.SelectedItem;
                    _conditionRegister.timingValue = null;
                }
            };

            timingValueForm.TextChanged += (o, _e) =>
            {
                if (!timingValueForm.Text.Equals(""))
                {
                    addRefiningConditionButton.Enabled = true;
                }
            };

            addMainConditionButton.Click += (o, _e) =>
            {
                makeSearchConditionPanel();
                makeTargetConditionFormItems();
                _conditionRegister.clearMainCondition();
                mainResourceForm.SelectedIndex = -1;
                mainRuleForm.Enabled = false;
                mainRuleForm.SelectedIndex = -1;
                mainEventForm.Enabled = false;
                mainEventForm.SelectedIndex = -1;
                mainEventDetailForm.Enabled = false;
                mainEventDetailForm.SelectedIndex = -1;
                targetConditionForm.Enabled = true;
                addMainConditionButton.Enabled = false;
            };

            addRefiningConditionButton.Click += (o, _e) =>
            {
                addRefiningSearchCondition();
                targetConditionForm.SelectedIndex = -1;
                refiningConditionResourceForm.Enabled = false;
                refiningConditionResourceForm.SelectedIndex = -1;
                refiningConditionRuleForm.Enabled = false;
                refiningConditionRuleForm.SelectedIndex = -1;
                refiningConditionEventForm.Enabled = false;
                refiningConditionEventForm.SelectedIndex = -1;
                refiningConditionEventDetailForm.Enabled = false;
                refiningConditionEventDetailForm.SelectedIndex = -1;
                
                timingForm.Enabled = false;
                timingForm.SelectedIndex = -1;
                timingValueForm.Enabled = false;
                timingValueForm.Text = "";
                addRefiningConditionButton.Enabled = false;

                _conditionRegister.clearRefiningCondition();
            };

            ApplicationFactory.BlackBoard.DeletedSearchConditionNumChanged += (o, _e) =>
            {
                deleteCondition();
                if (_searchConditionPanels.Count == 0)
                {
                    searchForwardButton.Enabled = false;
                    searchBackwardButton.Enabled = false;
                    searchWholeButton.Enabled = false;
                }
            };

            searchForwardButton.Click += (o, _e) =>
            {
                searchForward();
            };

            searchBackwardButton.Click += (o, _e) =>
            {
                searchBackward();
            };

            searchWholeButton.Click += (o, _e) =>
            {
                searchWhole();
            };

            this.SizeChanged += (o, _e) =>
            {
                changePanelSize();
            };

            this.FormClosed += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = 0;
            };
        }

        //リソース指定コンボボックスのアイテムをセット
        private void makeMainResourceForm()
        {
            GeneralNamedCollection<Resource> resData = this._data.ResourceData.Resources;

            foreach (Resource res in resData)
            {
                if (!res.Name.Equals("CurrentContext"))
                    mainResourceForm.Items.Add(res.Name);
            }

            if (mainRuleForm.Enabled == true)
            {
                mainRuleForm.Items.Clear();
                mainEventForm.Items.Clear();
                mainEventDetailForm.Items.Clear();

                _conditionRegister.mainResourceName = null;
                _conditionRegister.mainRuleName = null;
                _conditionRegister.mainEventName = null;
                _conditionRegister.mainEventDetail = null;


                mainRuleForm.Enabled = false;
                mainEventForm.Enabled = false;
                mainEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeMainRuleForm()
        {
            mainRuleForm.Items.Clear();
            //選ばれているリソースの種類を調べる
            _conditionRegister.mainResourceType = _data.ResourceData.Resources[(string)mainResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(_conditionRegister.mainResourceType))
                {
                    mainRuleForm.Items.Add(rule.DisplayName);
                }
            }
        }


        //イベント指定コンボボックスのアイテムをセット
        private void makeMainEventForm()
        {
            mainEventForm.Items.Clear();
            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {
                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    _conditionRegister.mainRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(_conditionRegister.mainResourceType) && visRule.DisplayName.Equals(mainRuleForm.SelectedItem))
                {
                    _conditionRegister.mainRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                mainEventForm.Items.Add(e.DisplayName);
            }
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeMainEventDetailForm()
        {
            mainEventDetailForm.Items.Clear();
            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(mainEventForm.SelectedItem))
                {
                    _conditionRegister.mainEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[_conditionRegister.mainRuleName].Shapes[_conditionRegister.mainEventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    mainEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        mainEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }
        }

        private void makeRefiningConditionResourceForm()
        {
            GeneralNamedCollection<Resource> resData = this._data.ResourceData.Resources;

            foreach (Resource res in resData)
            {
                if (!res.Name.Equals("CurrentContext"))
                    refiningConditionResourceForm.Items.Add(res.Name);
            }

            if (refiningConditionRuleForm.Enabled == true)
            {
                refiningConditionRuleForm.Items.Clear();
                refiningConditionEventForm.Items.Clear();
                refiningConditionEventDetailForm.Items.Clear();

                _conditionRegister.refiningResourceName = null;
                _conditionRegister.refiningRuleName = null;
                _conditionRegister.refiningEventName = null;
                _conditionRegister.refiningEventDetail = null;


                refiningConditionRuleForm.Enabled = false;
                refiningConditionEventForm.Enabled = false;
                refiningConditionEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeRefiningConditionRuleForm()
        {
            refiningConditionRuleForm.Items.Clear();
            //選ばれているリソースの種類を調べる
            _conditionRegister.refiningResourceType = _data.ResourceData.Resources[(string)refiningConditionResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(_conditionRegister.refiningResourceType))
                {
                    refiningConditionRuleForm.Items.Add(rule.DisplayName);
                }
            }
        }

        //イベント指定コンボボックスのアイテムをセット
        private void makeRefiningConditionEventForm()
        {
            refiningConditionEventForm.Items.Clear();

            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {

                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    _conditionRegister.refiningRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(_conditionRegister.refiningResourceType) && visRule.DisplayName.Equals(refiningConditionRuleForm.SelectedItem))
                {
                    _conditionRegister.refiningRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[_conditionRegister.refiningRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                refiningConditionEventForm.Items.Add(e.DisplayName);
            }
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeRefiningConditionEventDetailForm()
        {
            if (refiningConditionEventDetailForm.Enabled == true)
            {
                refiningConditionEventDetailForm.Items.Clear();
            }
            else
            {
                refiningConditionEventDetailForm.Enabled = true;
            }

            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[_conditionRegister.refiningRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(refiningConditionEventForm.SelectedItem))
                {
                    _conditionRegister.refiningEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[_conditionRegister.refiningRuleName].Shapes[_conditionRegister.refiningEventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    refiningConditionEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        refiningConditionEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }
        }

        private void clearSearchMainCondition()
        {
            mainResourceForm.Items.Clear();
            mainRuleForm.Items.Clear();
            mainEventForm.Items.Clear();
            mainEventDetailForm.Items.Clear();
            mainResourceForm.Text = null;
            mainRuleForm.Text = null;
            mainEventForm.Text = null;
            mainEventDetailForm.Text = null;
            _conditionRegister.clearMainCondition();
        }

        private void clearSearchSubCondition()
        {
            refiningConditionResourceForm.Items.Clear();
            refiningConditionRuleForm.Items.Clear();
            refiningConditionEventForm.Items.Clear();
            refiningConditionEventDetailForm.Items.Clear();
            timingForm.Items.Clear();
            timingValueForm.Items.Clear();
            refiningConditionResourceForm.Text = null;
            refiningConditionRuleForm.Text = null;
            refiningConditionEventForm.Text = null;
            refiningConditionEventDetailForm.Text = null;
            timingForm.Text = null;
            timingValueForm.Text = null;
            _conditionRegister.clearRefiningCondition();
        }

        private void makeTargetConditionFormItems()
        {
            targetConditionForm.Items.Clear();
            int mainConditionCount = _searchConditionPanels.Count;

            for (int i = 1; i<=mainConditionCount; i++)
            {
                targetConditionForm.Items.Add(i.ToString());
            }
        }

        private void makeSearchConditionPanel()
        {
            SearchCondition mainSearchCondition = new SearchCondition();
            mainSearchCondition = new SearchCondition();
            mainSearchCondition.resourceName = (string)mainResourceForm.SelectedItem;
            mainSearchCondition.ruleName = _conditionRegister.mainRuleName;
            mainSearchCondition.eventName = _conditionRegister.mainEventName;
            mainSearchCondition.eventDetail = (string)mainEventDetailForm.SelectedItem;

            searchBackwardButton.Enabled = true;
            searchForwardButton.Enabled = true;
            searchWholeButton.Enabled = true;

            SearchConditionPanel panel = new SearchConditionPanel(mainSearchCondition, _searchConditionPanels.Count+1);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.AutoScroll = true;
            panel.Width = ConditionDisplayPanel.Width - 15;
            panel.Height = 200;
            _searchConditionPanels.Add(panel);
            updateConditionDisplayPanel();
            targetConditionForm.Enabled = true;
        }

        private void addRefiningSearchCondition()
        {
            //絞り込み条件を追加する検索条件の番号
            int targetConditionPanelNum = int.Parse((string)targetConditionForm.SelectedItem) - 1;
            SearchCondition refiningSearchCondition = new SearchCondition();
            SearchConditionPanel targetSearchCondition = _searchConditionPanels[targetConditionPanelNum];

            refiningSearchCondition = new SearchCondition();
            refiningSearchCondition.resourceName = (string)refiningConditionResourceForm.SelectedItem;
            refiningSearchCondition.ruleName = _conditionRegister.refiningRuleName;
            refiningSearchCondition.eventName = _conditionRegister.refiningEventName;
            refiningSearchCondition.eventDetail = (string)refiningConditionEventDetailForm.SelectedItem;
            refiningSearchCondition.timing = (string)timingForm.SelectedItem;
            refiningSearchCondition.timingValue = (string)timingValueForm.Text;
            targetSearchCondition.addRefiningSearchCondition(refiningSearchCondition);
        }

        private void deleteCondition()
        {
            int deletedNum = ApplicationFactory.BlackBoard.DeletedSearchConditionNum;
            if (deletedNum != -1)
            {
                _searchConditionPanels.RemoveAt(deletedNum - 1);

                //絞り込み対象選択フォームのアイテムをすべてクリア
                targetConditionForm.Items.Clear();

                //各検索条件に番号を振り直す
                int panelNum = 1;
                foreach (SearchConditionPanel panel in _searchConditionPanels)
                {
                    panel.conditionNumber = panelNum;
                    targetConditionForm.Items.Add(panelNum);
                    panelNum++;
                }
                updateConditionDisplayPanel();
                ApplicationFactory.BlackBoard.DeletedSearchConditionNum = -1;
            }

            if (_searchConditionPanels.Count == 0)
            {
                addRefiningConditionButton.Enabled = false;
            }
        }

        private void updateConditionDisplayPanel()
        {
            //条件表示画面から全ての条件を一度消去する
            ConditionDisplayPanel.Controls.Clear();

            System.Drawing.Point nextPanelLocation = new System.Drawing.Point(0,0);

            //各検索条件を ConditionDisplayPanel上に再描画
            foreach(SearchConditionPanel panel in _searchConditionPanels)
            {
                panel.Location = nextPanelLocation;
                ConditionDisplayPanel.Controls.Add(panel);
                nextPanelLocation.Y += panel.Size.Height;
            }
        }

        private void searchForward()
        {
            decimal jumpTime = -1;
            foreach (SearchConditionPanel panel in _searchConditionPanels)
            {
                _detailSearchWithTiming.setSearchData(panel.mainCondition, panel.refiningSearchConditions);
                decimal tmpJumpTime = _detailSearchWithTiming.searchForward();
                if (tmpJumpTime > jumpTime)
                {
                    jumpTime = tmpJumpTime;
                }
            }

            if (jumpTime >= 0)
            {
                decimal start = _minTime;
                decimal end = _maxTime;
                if (jumpTime < start) jumpTime = start;

                //カーソルを移動
                ApplicationFactory.BlackBoard.CursorTime = new Time(jumpTime.ToString(), _timeRadix);

                //トレースログビューアにおいて、該当時刻のログをフォーカス
                List<Time> focusTime = new List<Time>();
                focusTime.Add(ApplicationFactory.BlackBoard.CursorTime);
                ApplicationFactory.BlackBoard.SearchTime = focusTime;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("検索の終わりです");
            }

        }

        private void searchBackward()
        {
        }

        private void searchWhole()
        {
        }

        private void changePanelSize()
        {
            foreach (SearchConditionPanel panel in _searchConditionPanels)
            {
                panel.Width = ConditionDisplayPanel.Width - 20;
            }
        }

        private void makeTimeSortedLog()
        {
            _timeSortedLog = new List<VisualizeLog>();
            foreach (KeyValuePair<string, EventShapes> evntShapesList in this._data.VisualizeShapeData.RuleResourceShapes)
            {
                string[] ruleAndResName = evntShapesList.Key.Split(':'); //例えば"taskStateChange:LOGTASK"を切り分ける
                string resName = ruleAndResName[1];
                string ruleName = ruleAndResName[0];

                //以下の処理はループが深いので、ログの数が多くなると処理が非常に遅くなる可能性あり
                //リスト中の適切な位置に一つ一つデータを挿入していくことで、全部のデータを _timeSortedLog
                //へ格納し終わった段階でソートを完了させている（挿入ソート）。ただ、速度のことを考えると、
                //最初は時系列を無視して格納し最後にクイックソートを使って整列させた方がいいかもしれない。
                foreach (KeyValuePair<string, System.Collections.Generic.List<EventShape>> evntShapeList in evntShapesList.Value.List)
                {
                    string[] evntAndRuleName = evntShapeList.Key.Split(':');  // 例えば"taskStateChange:stateChangeEvent"を切り分ける
                    string evntName = evntAndRuleName[1];
                    foreach (EventShape evntShape in evntShapeList.Value)
                    {
                        //　evntShape を_timeSortedLog へ挿入すべき場所（インデックス）を探して格納する
                        if (_timeSortedLog.Count == 0)
                        {
                            _timeSortedLog.Add(new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, evntShape.From.Value));
                        }
                        else
                        {
                            for (int i = 0; i < _timeSortedLog.Count; i++)
                            {
                                VisualizeLog addedLog = _timeSortedLog[i];
                                if (evntShape.From.Value <= addedLog.fromTime)
                                {
                                    _timeSortedLog.Insert(i, new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, evntShape.From.Value));
                                    break;
                                }
                                else
                                {
                                    if (i == _timeSortedLog.Count - 1)
                                    {
                                        _timeSortedLog.Add(new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, evntShape.From.Value));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
