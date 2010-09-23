using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    public partial class DetailSearchPanel : Form
    {

        protected TraceLogVisualizerData _data;
        ConditionBeans conditionRegister;
        private List<SearchConditionPanel> searchConditionPanels;

        public DetailSearchPanel(TraceLogVisualizerData data)
        {
            InitializeComponent();
            MainRuleForm.Enabled = false;
            MainEventForm.Enabled = false;
            MainEventDetailForm.Enabled = false;
            _data = data;
            conditionRegister = new ConditionBeans();
            searchConditionPanels = new List<SearchConditionPanel>();
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
            MainResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                if (MainResourceForm.SelectedIndex != -1)
                {
                    if (MainRuleForm.Enabled == true)
                    {
                        conditionRegister.mainRuleName = null;
                        conditionRegister.mainEventName = null;
                        conditionRegister.mainEventDetail = null;

                        MainEventForm.Enabled = false;
                        MainEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        MainRuleForm.Enabled = true;
                    }

                    makeMainRuleForm();
                    AddMainConditionButton.Enabled = true;
                }
            };

            MainRuleForm.SelectedIndexChanged += (o, _e) =>
            {

                if (MainRuleForm.SelectedIndex != -1)
                {
                    if (MainEventForm.Enabled == true)
                    {
                        conditionRegister.mainEventName = null;
                        conditionRegister.mainEventDetail = null;

                        MainEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        MainEventForm.Enabled = true;
                    }
                    makeMainEventForm();          
                }
            };

            MainEventForm.SelectedIndexChanged += (o, _e) =>
            {
                if (MainEventForm.SelectedIndex != -1)
                {
                    if (MainEventDetailForm.Enabled == true)
                    {
                        MainEventDetailForm.Items.Clear();
                    }
                    else
                    {
                        MainEventDetailForm.Enabled = true;
                    }
                    makeMainEventDetailForm();
                }
            };

            TargetConditionForm.SelectedIndexChanged += (o, _e) =>
            {
                if (TargetConditionForm.SelectedIndex != -1)
                {
                    makeRefiningConditionResourceForm();
                    RefiningConditionResourceForm.Enabled = true;
                }
            };

            RefiningConditionResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                if (RefiningConditionResourceForm.SelectedIndex != -1)
                {
                    if (RefiningConditionRuleForm.Enabled == true)
                    {
                        conditionRegister.refiningRuleName = null;
                        conditionRegister.refiningEventName = null;
                        conditionRegister.refiningEventDetail = null;

                        RefiningConditionEventForm.Enabled = false;
                        RefiningConditionEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        RefiningConditionRuleForm.Enabled = true;
                    }

                    TimingForm.Enabled = true;
                    TimingValueForm.Enabled = false;

                    makeRefiningConditionRuleForm();
                }
            };

            RefiningConditionRuleForm.SelectedIndexChanged += (o, _e) =>
            {
                if (RefiningConditionResourceForm.SelectedIndex != -1)
                {
                    if (RefiningConditionEventForm.Enabled == true)
                    {
                        conditionRegister.refiningEventName = null;
                        conditionRegister.refiningEventDetail = null;

                        RefiningConditionEventDetailForm.Enabled = false;
                    }
                    else
                    {
                        RefiningConditionEventForm.Enabled = true;
                    }

                    makeRefiningConditionEventForm();
                }
            };

            RefiningConditionEventForm.SelectedIndexChanged += (o, _e) =>
            {
                if (RefiningConditionEventForm.SelectedIndex != -1)
                makeRefiningConditionEventDetailForm();
            };


            TimingForm.SelectedIndexChanged += (o, _e) =>
            {
                if (TimingForm.SelectedIndex != -1)
                {
                    if (TimingValueForm.Enabled == true)
                    {
                        TimingValueForm.Text = "";
                    }
                    else
                    {
                        if (!TimingForm.SelectedItem.Equals("直前") || !TimingForm.SelectedItem.Equals("直後"))
                        {
                            TimingValueForm.Enabled = true;
                        }
                        else
                        {
                            TimingValueForm.Enabled = false;
                            AddRefiningConditionButton.Enabled = true;
                        }
                    }


                }
            };

            TimingValueForm.TextChanged += (o, _e) =>
            {
                if (!TimingValueForm.Text.Equals(""))
                {
                    AddRefiningConditionButton.Enabled = true;
                }
            };

            AddMainConditionButton.Click += (o, _e) =>
            {
                makeSearchConditionPanel();
                makeTargetConditionFormItems();
                conditionRegister.clearMainCondition();
                MainResourceForm.SelectedIndex = -1;
                MainRuleForm.Enabled = false;
                MainRuleForm.SelectedIndex = -1;
                MainEventForm.Enabled = false;
                MainEventForm.SelectedIndex = -1;
                MainEventDetailForm.Enabled = false;
                MainEventDetailForm.SelectedIndex = -1;
                TargetConditionForm.Enabled = true;
            };

            AddRefiningConditionButton.Click += (o, _e) =>
            {
                addRefiningSearchCondition();
                TargetConditionForm.SelectedIndex = -1;
                RefiningConditionResourceForm.Enabled = false;
                RefiningConditionResourceForm.SelectedIndex = -1;
                RefiningConditionRuleForm.Enabled = false;
                RefiningConditionRuleForm.SelectedIndex = -1;
                RefiningConditionEventForm.Enabled = false;
                RefiningConditionEventForm.SelectedIndex = -1;
                RefiningConditionEventDetailForm.Enabled = false;
                RefiningConditionEventDetailForm.SelectedIndex = -1;
                
                TimingForm.Enabled = false;
                TimingForm.SelectedIndex = -1;
                TimingValueForm.Enabled = false;
                TimingValueForm.Text = "";
                AddRefiningConditionButton.Enabled = false;

                conditionRegister.clearRefiningCondition();
            };

            ApplicationFactory.BlackBoard.DeletedSearchConditionNumChanged += (o, _e) =>
            {
                deleteCondition();
                if (searchConditionPanels.Count == 0)
                {
                    ForwardSearchButton.Enabled = false;
                    BackwardSearchButton.Enabled = false;
                    WholeSearchButton.Enabled = false;
                }
            };

            this.SizeChanged += (o, _e) =>
            {
                changePanelSize();
            };

        }

        //リソース指定コンボボックスのアイテムをセット
        private void makeMainResourceForm()
        {
            GeneralNamedCollection<Resource> resData = this._data.ResourceData.Resources;

            foreach (Resource res in resData)
            {
                if (!res.Name.Equals("CurrentContext"))
                    MainResourceForm.Items.Add(res.Name);
            }

            if (MainRuleForm.Enabled == true)
            {
                MainRuleForm.Items.Clear();
                MainEventForm.Items.Clear();
                MainEventDetailForm.Items.Clear();

                conditionRegister.mainResourceName = null;
                conditionRegister.mainRuleName = null;
                conditionRegister.mainEventName = null;
                conditionRegister.mainEventDetail = null;


                MainRuleForm.Enabled = false;
                MainEventForm.Enabled = false;
                MainEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeMainRuleForm()
        {
            MainRuleForm.Items.Clear();
            //選ばれているリソースの種類を調べる
            conditionRegister.mainResourceType = _data.ResourceData.Resources[(string)MainResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(conditionRegister.mainResourceType))
                {
                    MainRuleForm.Items.Add(rule.DisplayName);
                }
            }
        }


        //イベント指定コンボボックスのアイテムをセット
        private void makeMainEventForm()
        {
            MainEventForm.Items.Clear();
            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {
                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    conditionRegister.mainRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(conditionRegister.mainResourceType) && visRule.DisplayName.Equals(MainRuleForm.SelectedItem))
                {
                    conditionRegister.mainRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[conditionRegister.mainRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                MainEventForm.Items.Add(e.DisplayName);
            }
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeMainEventDetailForm()
        {
            MainEventDetailForm.Items.Clear();
            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[conditionRegister.mainRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(MainEventForm.SelectedItem))
                {
                    conditionRegister.mainEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[conditionRegister.mainRuleName].Shapes[conditionRegister.mainEventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    MainEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        MainEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
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
                    RefiningConditionResourceForm.Items.Add(res.Name);
            }

            if (RefiningConditionRuleForm.Enabled == true)
            {
                RefiningConditionRuleForm.Items.Clear();
                RefiningConditionEventForm.Items.Clear();
                RefiningConditionEventDetailForm.Items.Clear();

                conditionRegister.refiningResourceName = null;
                conditionRegister.refiningRuleName = null;
                conditionRegister.refiningEventName = null;
                conditionRegister.refiningEventDetail = null;


                RefiningConditionRuleForm.Enabled = false;
                RefiningConditionEventForm.Enabled = false;
                RefiningConditionEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeRefiningConditionRuleForm()
        {
            RefiningConditionRuleForm.Items.Clear();
            //選ばれているリソースの種類を調べる
            conditionRegister.refiningResourceType = _data.ResourceData.Resources[(string)RefiningConditionResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(conditionRegister.refiningResourceType))
                {
                    RefiningConditionRuleForm.Items.Add(rule.DisplayName);
                }
            }
        }

        //イベント指定コンボボックスのアイテムをセット
        private void makeRefiningConditionEventForm()
        {
            RefiningConditionEventForm.Items.Clear();

            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {

                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    conditionRegister.refiningRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(conditionRegister.refiningResourceType) && visRule.DisplayName.Equals(RefiningConditionRuleForm.SelectedItem))
                {
                    conditionRegister.refiningRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[conditionRegister.refiningRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                RefiningConditionEventForm.Items.Add(e.DisplayName);
            }
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeRefiningConditionEventDetailForm()
        {
            if (RefiningConditionEventDetailForm.Enabled == true)
            {
                RefiningConditionEventDetailForm.Items.Clear();
            }
            else
            {
                RefiningConditionEventDetailForm.Enabled = true;
            }

            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[conditionRegister.refiningRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(RefiningConditionEventForm.SelectedItem))
                {
                    conditionRegister.refiningEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[conditionRegister.refiningRuleName].Shapes[conditionRegister.refiningEventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    RefiningConditionEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        RefiningConditionEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }
        }

        private void clearSearchMainCondition()
        {
            MainResourceForm.Items.Clear();
            MainRuleForm.Items.Clear();
            MainEventForm.Items.Clear();
            MainEventDetailForm.Items.Clear();
            MainResourceForm.Text = null;
            MainRuleForm.Text = null;
            MainEventForm.Text = null;
            MainEventDetailForm.Text = null;
            conditionRegister.clearMainCondition();
        }

        private void clearSearchSubCondition()
        {
            RefiningConditionResourceForm.Items.Clear();
            RefiningConditionRuleForm.Items.Clear();
            RefiningConditionEventForm.Items.Clear();
            RefiningConditionEventDetailForm.Items.Clear();
            TimingForm.Items.Clear();
            TimingValueForm.Items.Clear();
            RefiningConditionResourceForm.Text = null;
            RefiningConditionRuleForm.Text = null;
            RefiningConditionEventForm.Text = null;
            RefiningConditionEventDetailForm.Text = null;
            TimingForm.Text = null;
            TimingValueForm.Text = null;
            conditionRegister.clearRefiningCondition();
        }

        private void makeTargetConditionFormItems()
        {
            TargetConditionForm.Items.Clear();
            int mainConditionCount = searchConditionPanels.Count;

            for (int i = 1; i<=mainConditionCount; i++)
            {
                TargetConditionForm.Items.Add(i.ToString());
            }
        }

        private void makeSearchConditionPanel()
        {
            SearchCondition mainSearchCondition = new SearchCondition();
            mainSearchCondition = new SearchCondition();
            mainSearchCondition.resourceName = (string)MainResourceForm.SelectedItem;
            mainSearchCondition.ruleName = (string)MainRuleForm.SelectedItem;
            mainSearchCondition.eventName = (string)MainEventForm.SelectedItem;
            mainSearchCondition.eventDetail = (string)MainEventDetailForm.SelectedItem;

            BackwardSearchButton.Enabled = true;
            ForwardSearchButton.Enabled = true;
            WholeSearchButton.Enabled = true;

            SearchConditionPanel panel = new SearchConditionPanel(mainSearchCondition, searchConditionPanels.Count+1);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.AutoScroll = true;
            panel.Width = ConditionDisplayPanel.Width - 15;
            panel.Height = 200;
            searchConditionPanels.Add(panel);
            updateConditionDisplayPanel();
            TargetConditionForm.Enabled = true;
        }

        private void addRefiningSearchCondition()
        {
            //絞り込み条件を追加する検索条件の番号
            int targetConditionPanelNum = int.Parse((string)TargetConditionForm.SelectedItem) - 1;
            SearchCondition refiningSearchCondition = new SearchCondition();
            SearchConditionPanel targetSearchCondition = searchConditionPanels[targetConditionPanelNum];

            refiningSearchCondition = new SearchCondition();
            refiningSearchCondition.resourceName = (string)RefiningConditionResourceForm.SelectedItem;
            refiningSearchCondition.ruleName = (string)RefiningConditionRuleForm.SelectedItem;
            refiningSearchCondition.eventName = (string)RefiningConditionEventForm.SelectedItem;
            refiningSearchCondition.eventDetail = (string)RefiningConditionEventDetailForm.SelectedItem;
            refiningSearchCondition.timing = (string)TimingForm.SelectedItem;
            refiningSearchCondition.timingValue = (string)TimingValueForm.SelectedItem;
            targetSearchCondition.addRefiningSearchCondition(refiningSearchCondition);
        }

        private void deleteCondition()
        {
            int deletedNum = ApplicationFactory.BlackBoard.DeletedSearchConditionNum;
            if (deletedNum != -1)
            {
                searchConditionPanels.RemoveAt(deletedNum - 1);

                //絞り込み対象選択フォームのアイテムをすべてクリア
                TargetConditionForm.Items.Clear();

                //各検索条件に番号を振り直す
                int panelNum = 1;
                foreach (SearchConditionPanel panel in searchConditionPanels)
                {
                    panel.conditionNumber = panelNum;
                    TargetConditionForm.Items.Add(panelNum);
                    panelNum++;
                }
                updateConditionDisplayPanel();
                ApplicationFactory.BlackBoard.DeletedSearchConditionNum = -1;
            }

            if (searchConditionPanels.Count == 0)
            {
                AddRefiningConditionButton.Enabled = false;
            }
        }

        private void updateConditionDisplayPanel()
        {
            //条件表示画面から全ての条件を一度消去する
            ConditionDisplayPanel.Controls.Clear();

            System.Drawing.Point nextPanelLocation = new System.Drawing.Point(0,0);

            //各検索条件を ConditionDisplayPanel上に再描画
            foreach(SearchConditionPanel panel in searchConditionPanels)
            {
                panel.Location = nextPanelLocation;
                ConditionDisplayPanel.Controls.Add(panel);
                nextPanelLocation.Y += panel.Size.Height;
            }
        }

        private void changePanelSize()
        {
            foreach (SearchConditionPanel panel in searchConditionPanels)
            {
                panel.Width = this.Width - 15;
                
            }
        }


    }
}
