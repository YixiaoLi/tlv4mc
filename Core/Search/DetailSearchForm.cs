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
    public partial class DetailSearchForm : Form
    {
        private TraceLogVisualizerData _data;
        private List<VisualizeLog> _visLogs;
        private ConditionRegister _conditionRegister;
        private List<SearchConditionPanel> _searchConditionPanels;
        private TraceLogSearcher _searcher;

        private decimal _minTime;
        private decimal _maxTime;
        private int _timeRadix;

        public DetailSearchForm(TraceLogVisualizerData data, decimal minTime, decimal maxTime, int timeRadix)
        {
            InitializeComponent();
            mainRuleForm.Enabled = false;
            mainEventForm.Enabled = false;
            mainEventDetailForm.Enabled = false;
            _data = data;
            sortByTime();
            _conditionRegister = new ConditionRegister();
            _searchConditionPanels = new List<SearchConditionPanel>();
            _searcher = new DetailSearchWithTiming();
            _minTime = minTime;
            _maxTime = maxTime;
            _timeRadix = timeRadix;
            timeValueLabel.Text = _data.ResourceData.TimeScale;
            makeMainResourceForm();
            makeRefiningConditionResourceForm();
            arrangeDropDownSize(timingForm);
        }

        protected override void OnLoad(EventArgs e)
        {
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
                        mainRuleForm.SelectedIndex = -1;
                        mainEventForm.SelectedIndex = -1;
                        mainEventDetailForm.SelectedIndex = -1;
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
                        mainEventForm.SelectedIndex = -1;
                        mainEventDetailForm.SelectedIndex = -1;
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

                    timingValueForm.Enabled = true;
                    timingValueForm.Text = "";
                    timingForm.Enabled = true;
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
                        refiningConditionEventDetailForm.Enabled = true;
                    }
                }

                makeRefiningConditionEventDetailForm();
            };


            timingForm.SelectedIndexChanged += (o, _e) =>
            {
                _conditionRegister.timing = (string)timingForm.SelectedItem;
                addRefiningConditionButton.Enabled = true;
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
                if (timingValueForm.Text.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("タイミングの値を設定してください");
                }
                else
                {
                    try
                    {
                       decimal value = decimal.Parse(timingValueForm.Text);
                       if (value < 0)
                       {
                           System.Windows.Forms.MessageBox.Show("0より大きい値を指定してください");
                       }
                       else //正常値が入力された場合
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
                       }
                    }
                    catch (Exception exception)
                   {
                     System.Windows.Forms.MessageBox.Show("入力値が不正な値です");
                   }
                }
            };

            //SearchConditionPanel上で基本条件が消去された場合、その条件の番号が DeletedSearchConditionNum に記録される
            //このイベントが発生した場合、消去された条件番号に対応する SearchConditionPanel を消去する
            //DetailSearchPanelがクローズする際にイベントハンドラを削除したいため、ラムダ式で登録しない
            ApplicationFactory.BlackBoard.DeletedSearchConditionNumChanged +=
                                    new EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralChangedEventArgs<int>>(deleteCondition);

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

            this.FormClosing += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = 0;
                ApplicationFactory.BlackBoard.DeletedSearchConditionNum = -1;
                ApplicationFactory.BlackBoard.DeletedSearchConditionNumChanged -= new EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralChangedEventArgs<int>>(deleteCondition);
            };

            settingCursorButton.Click += (o, _e) =>
            {
                try
                {
                    decimal time = decimal.Parse(settingCursorBox.Text);
                    if (time < _minTime)
                    {
                        time = _minTime;
                    }
                    else if (time > _maxTime)
                    {
                        time = _maxTime;
                    }
                    else { }

                    ApplicationFactory.BlackBoard.CursorTime = new Time(time.ToString(), _timeRadix);
                }
                catch (FormatException fe)
                {
                    MessageBox.Show(fe.Message);
                }
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

            arrangeDropDownSize(mainResourceForm);
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
            arrangeDropDownSize(mainRuleForm);
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

            arrangeDropDownSize(mainEventForm);
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
            arrangeDropDownSize(mainEventDetailForm);
        }

        private void makeRefiningConditionResourceForm()
        {
            refiningConditionResourceForm.Items.Clear();
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
            arrangeDropDownSize(refiningConditionResourceForm);
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
            arrangeDropDownSize(refiningConditionRuleForm);
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
            arrangeDropDownSize(refiningConditionEventForm);
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeRefiningConditionEventDetailForm()
        {
            refiningConditionEventDetailForm.Items.Clear();

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
            arrangeDropDownSize(refiningConditionEventDetailForm);
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
            arrangeDropDownSize(targetConditionForm);
        }

        private void makeSearchConditionPanel()
        {
            SearchCondition mainSearchCondition = new SearchCondition();
            mainSearchCondition = new SearchCondition();
            mainSearchCondition.resourceName = (string)mainResourceForm.SelectedItem;
            mainSearchCondition.ruleName = _conditionRegister.mainRuleName;
            mainSearchCondition.ruleDisplayName = (string)mainRuleForm.SelectedItem; 
            mainSearchCondition.eventName = _conditionRegister.mainEventName;
            mainSearchCondition.eventDisplayName = (string)mainEventForm.SelectedItem; 
            mainSearchCondition.eventDetail = (string)mainEventDetailForm.SelectedItem;

            searchBackwardButton.Enabled = true;
            searchForwardButton.Enabled = true;
            searchWholeButton.Enabled = true;

            SearchConditionPanel panel = new SearchConditionPanel(this,mainSearchCondition, _searchConditionPanels.Count+1, _data.ResourceData.TimeScale);
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

            refiningSearchCondition = new SearchCondition();
            refiningSearchCondition.resourceName = (string)refiningConditionResourceForm.SelectedItem;
            refiningSearchCondition.ruleDisplayName = (string)refiningConditionRuleForm.SelectedItem;
            refiningSearchCondition.ruleName = _conditionRegister.refiningRuleName;
            refiningSearchCondition.eventDisplayName = (string)refiningConditionEventForm.SelectedItem;
            refiningSearchCondition.eventName = _conditionRegister.refiningEventName;
            refiningSearchCondition.eventDetail = (string)refiningConditionEventDetailForm.SelectedItem;
            refiningSearchCondition.timing = (string)timingForm.SelectedItem;
            refiningSearchCondition.timingValue = (string)timingValueForm.Text;
            _searchConditionPanels[targetConditionPanelNum].addRefiningSearchCondition(refiningSearchCondition);
        }

        private void deleteCondition(object sender, System.EventArgs e)
        {
            int deletedNum = ApplicationFactory.BlackBoard.DeletedSearchConditionNum;
            if (deletedNum != -1)
            {
                try
                {
                    int j;
                    _searchConditionPanels.RemoveAt(deletedNum - 1);
                    int i;
                }
                catch (Exception _e)
                {
                    updateConditionDisplayPanel();
                }

                //絞込み対象となる基本条件を選択するフォームのアイテムをすべてクリア
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
                //検索条件が一つもない場合は検索ボタンを操作不可にする
                searchForwardButton.Enabled = false;
                searchBackwardButton.Enabled = false;
                searchWholeButton.Enabled = false;

                //絞込み条件の操作領域をすべて Enable にし、かつ各フォームを空白表示にする
                refiningConditionResourceForm.Enabled = false;
                refiningConditionRuleForm.Enabled = false;
                refiningConditionEventForm.Enabled = false;
                refiningConditionEventDetailForm.Enabled = false;
                timingForm.Enabled = false;
                timingValueForm.Enabled = false;
                addRefiningConditionButton.Enabled = false;
                refiningConditionResourceForm.SelectedIndex = -1;
                refiningConditionRuleForm.SelectedIndex = -1;
                refiningConditionEventForm.SelectedIndex = -1;
                refiningConditionEventDetailForm.SelectedIndex = -1;
                timingForm.SelectedIndex = -1;
                timingValueForm.Text = "";
            }
        }
       
        private void updateConditionDisplayPanel()
        {
            //条件表示画面から全ての条件を一度消去する
            ConditionDisplayPanel.Controls.Clear();

            System.Drawing.Point nextPanelLocation = new System.Drawing.Point(0, 0);

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
            VisualizeLog hitLog = null;

            //検索条件を１セットずつ調べ、現在時刻により近い時刻を検索結果とする
            foreach (SearchConditionPanel panel in _searchConditionPanels)
            {
                _searcher.setSearchData(_visLogs, panel.mainCondition, panel.refiningConditions);
                if (panel.andButton != null) ApplicationFactory.BlackBoard.isAnd = panel.andButton.Checked;

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
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("検索の終わりです");
            }

        }

        private void searchBackward()
        {

            VisualizeLog hitLog = null;

            //検索条件を１セットずつ調べ、現在時刻により近い時刻を検索結果とする
            foreach (SearchConditionPanel panel in _searchConditionPanels)
            {
                _searcher.setSearchData(_visLogs, panel.mainCondition, panel.refiningConditions);
                if (panel.andButton != null) ApplicationFactory.BlackBoard.isAnd = panel.andButton.Checked;

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
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("検索の終わりです");
            }
        }

        private void searchWhole()
        {
            List<VisualizeLog> hitLogs = new List<VisualizeLog>();
            Color color = ApplicationFactory.ColorFactory.RamdomColor(); //マーカーの色

            foreach(SearchConditionPanel panel in _searchConditionPanels)
            {
                _searcher.setSearchData(_visLogs, panel.mainCondition, panel.refiningConditions);
                List<VisualizeLog> candidateHitLogs = _searcher.searchWhole();
                foreach (VisualizeLog candidateHitLog in candidateHitLogs)
                {
                    hitLogs.Add(candidateHitLog);
                }
            }

            if (hitLogs.Count > 0)
            {
                List<Time> resultTime = new List<Time>();
                foreach(VisualizeLog hitLog in hitLogs)
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

        private void changePanelSize()
        {
            foreach (SearchConditionPanel panel in _searchConditionPanels)
            {
                panel.Width = ConditionDisplayPanel.Width - 20;
            }
        }

        private void sortByTime()
        {
            _visLogs = new List<VisualizeLog>(); //イベント情報を格納する

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
                            _visLogs.Add(new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, 0));
                        }
                        else
                        {
                            _visLogs.Add(new VisualizeLog(resName, ruleName, evntShape.Event.Name, evntShape.EventDetail, evntShape.From.Value));
                        }
                    }
                }
            }

           //_visLogsの要素を時間でソート
            VisualizeLog[] tmpLogs = _visLogs.ToArray();
            Array.Sort(tmpLogs);
            _visLogs = tmpLogs.ToList();
        }

        //コンボボックスのドロップダウンボックスのサイズを自動調整
        private void arrangeDropDownSize(ComboBox targetBox)
        {
            int maxTextLength = 0;
            int font_W = (int)Math.Ceiling(
                             targetBox.Font.SizeInPoints * 2.0F / 3.0F);  // フォント幅を取得

            foreach (string A in targetBox.Items)
            {
                // 各行の文字バイト長から「横幅」を算出し、その最大値を求める
                int len = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(A);
                maxTextLength = Math.Max(maxTextLength, len * font_W);
            }
            targetBox.DropDownWidth = maxTextLength + 10;
        }
    }
}
