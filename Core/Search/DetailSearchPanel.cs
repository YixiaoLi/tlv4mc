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
        ConditionBeans searchConditions;

        public DetailSearchPanel(TraceLogVisualizerData data)
        {
            InitializeComponent();
            MainRuleForm.Enabled = false;
            MainEventForm.Enabled = false;
            MainEventDetailForm.Enabled = false;
            _data = data;
            searchConditions = new ConditionBeans();
            makeMainResourceForm();
            makeSubResourceForm();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.FormClosed += (o, _e) =>
            {
                ApplicationFactory.BlackBoard.DetailSearchFlag = 0;
            };

            MainResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                makeMainRuleForm();
            };

            MainRuleForm.SelectedIndexChanged += (o, _e) =>
            {
                makeMainEventForm();
            };

            MainEventForm.SelectedIndexChanged += (o, _e) =>
            {
                makeMainEventDetailForm();
            };

            SubResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                makeSubRuleForm();
                TimingForm.Enabled = true;
                TimingValueForm.Enabled = false;
            };

            SubRuleForm.SelectedIndexChanged += (o, _e) =>
            {
                makeSubEventForm();
            };

            SubEventForm.SelectedIndexChanged += (o, _e) =>
            {
                makeSubEventDetailForm();
            };


            TimingForm.SelectedIndexChanged += (o, _e) =>
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
                }
            };


            makeMainConditionButton.Click += (o, _e) =>
            {
                makeMainCondition();
            };

            addSubConditionButton.Click += (o, _e) =>
            {
                makeSubCondition();
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

                searchConditions.mainResourceName = null;
                searchConditions.mainRuleName = null;
                searchConditions.mainEventName = null;
                searchConditions.mainEventDetail = null;


                MainRuleForm.Enabled = false;
                MainEventForm.Enabled = false;
                MainEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeMainRuleForm()
        {
            if (MainRuleForm.Enabled == true)
            {
                MainRuleForm.Items.Clear();
                MainEventForm.Items.Clear();
                MainEventDetailForm.Items.Clear();

                searchConditions.mainRuleName = null;
                searchConditions.mainEventName = null;
                searchConditions.mainEventDetail = null;

                MainEventForm.Enabled = false;
                MainEventDetailForm.Enabled = false;
                //searchForwardButton.Enabled = false;
                //searchBackwardButton.Enabled = false;
                //this.searchWholeButton.Enabled = true;    //各検索ボタンを有効にする
            }
            else
            {
                MainRuleForm.Enabled = true;
            }


            //選ばれているリソースの種類を調べる
            searchConditions.mainResourceType = _data.ResourceData.Resources[(string)MainResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(searchConditions.mainResourceType))
                {
                    MainRuleForm.Items.Add(rule.DisplayName);
                }
            }

            //this.searchForwardButton.Enabled = true;
            //this.searchBackwardButton.Enabled = true;
            //this.searchWholeButton.Enabled = true;    //各検索ボタンを有効にする

            makeMainConditionButton.Enabled = true;
        }

        //イベント指定コンボボックスのアイテムをセット
        private void makeMainEventForm()
        {
            if (MainEventForm.Enabled == true)
            {
                MainEventForm.Items.Clear();
                MainEventDetailForm.Items.Clear();
                MainEventDetailForm.Enabled = false;

                searchConditions.mainEventName = null;
                searchConditions.mainEventDetail = null;
            }
            else
            {
                MainEventForm.Enabled = true;
            }

            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {

                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    searchConditions.mainRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(searchConditions.mainResourceType) && visRule.DisplayName.Equals(MainRuleForm.SelectedItem))
                {
                    searchConditions.mainRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[searchConditions.mainRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                MainEventForm.Items.Add(e.DisplayName);
            }
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeMainEventDetailForm()
        {
            if (MainEventDetailForm.Enabled == true)
            {
                MainEventDetailForm.Items.Clear();
            }
            else
            {
                MainEventDetailForm.Enabled = true;
            }

            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[searchConditions.mainRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(MainEventForm.SelectedItem))
                {
                    searchConditions.mainEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[searchConditions.mainRuleName].Shapes[searchConditions.mainEventName];
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

        private void makeSubResourceForm()
        {
            GeneralNamedCollection<Resource> resData = this._data.ResourceData.Resources;

            foreach (Resource res in resData)
            {
                if (!res.Name.Equals("CurrentContext"))
                    SubResourceForm.Items.Add(res.Name);
            }

            if (SubRuleForm.Enabled == true)
            {
                SubRuleForm.Items.Clear();
                SubEventForm.Items.Clear();
                SubEventDetailForm.Items.Clear();

                searchConditions.subResourceName = null;
                searchConditions.subRuleName = null;
                searchConditions.subEventName = null;
                searchConditions.subEventDetail = null;


                SubRuleForm.Enabled = false;
                SubEventForm.Enabled = false;
                SubEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeSubRuleForm()
        {
            if (SubRuleForm.Enabled == true)
            {
                SubRuleForm.Items.Clear();
                SubEventForm.Items.Clear();
                SubEventDetailForm.Items.Clear();

                searchConditions.subRuleName = null;
                searchConditions.subEventName = null;
                searchConditions.subEventDetail = null;

                SubEventForm.Enabled = false;
                SubEventDetailForm.Enabled = false;
                //searchForwardButton.Enabled = false;
                //searchBackwardButton.Enabled = false;
                //this.searchWholeButton.Enabled = true;    //各検索ボタンを有効にする
            }
            else
            {
                SubRuleForm.Enabled = true;
            }


            //選ばれているリソースの種類を調べる
            searchConditions.subResourceType = _data.ResourceData.Resources[(string)SubResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(searchConditions.subResourceType))
                {
                    SubRuleForm.Items.Add(rule.DisplayName);
                }
            }

            //this.searchForwardButton.Enabled = true;
            //this.searchBackwardButton.Enabled = true;
            //this.searchWholeButton.Enabled = true;    //各検索ボタンを有効にする

            addSubConditionButton.Enabled = true;
        }

        //イベント指定コンボボックスのアイテムをセット
        private void makeSubEventForm()
        {
            if (SubEventForm.Enabled == true)
            {
                SubEventForm.Items.Clear();
                SubEventDetailForm.Items.Clear();
                SubEventDetailForm.Enabled = false;

                searchConditions.subEventName = null;
                searchConditions.subEventDetail = null;
            }
            else
            {
                SubEventForm.Enabled = true;
            }

            //選択されているルール名(例："状態遷移")の正式名称(例："taskStateChange")を調べる
            foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
            {

                if (visRule.Target == null) // ルールのターゲットは CurrentContext
                {
                    searchConditions.subRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(searchConditions.subResourceType) && visRule.DisplayName.Equals(SubRuleForm.SelectedItem))
                {
                    searchConditions.subRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[searchConditions.subRuleName].Shapes;
            foreach (Event e in eventShapes)
            {
                SubEventForm.Items.Add(e.DisplayName);
            }
        }


        //イベント詳細指定コンボボックスのアイテムをセット
        private void makeSubEventDetailForm()
        {
            if (SubEventDetailForm.Enabled == true)
            {
                SubEventDetailForm.Items.Clear();
            }
            else
            {
                SubEventDetailForm.Enabled = true;
            }

            //選択されているイベント名(例："状態")の正式名称(例："stateChangeEvent")を調べる
            foreach (Event ev in _data.VisualizeData.VisualizeRules[searchConditions.subRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(SubEventForm.SelectedItem))
                {
                    searchConditions.subEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[searchConditions.subRuleName].Shapes[searchConditions.subEventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    SubEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        SubEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }
        }

        private void searchMainConditionClear()
        {
            MainResourceForm.Items.Clear();
            MainRuleForm.Items.Clear();
            MainEventForm.Items.Clear();
            MainEventDetailForm.Items.Clear();
            MainResourceForm.Text = null;
            MainRuleForm.Text = null;
            MainEventForm.Text = null;
            MainEventDetailForm.Text = null;
            searchConditions.clearMainCondition();
        }

        private void searchSubConditionClear()
        {
            SubResourceForm.Items.Clear();
            SubRuleForm.Items.Clear();
            SubEventForm.Items.Clear();
            SubEventDetailForm.Items.Clear();
            TimingForm.Items.Clear();
            TimingValueForm.Items.Clear();
            SubResourceForm.Text = null;
            SubRuleForm.Text = null;
            SubEventForm.Text = null;
            SubEventDetailForm.Text = null;
            TimingForm.Text = null;
            TimingValueForm.Text = null;
            searchConditions.clearSubCondition();
        }


        private void makeMainCondition()
        {
            string condition = MainResourceForm.Text;
            if ((MainRuleForm.Text != null) && (!MainRuleForm.Text.Equals("")))
            {
                condition += System.Environment.NewLine + MainRuleForm.Text;
            }

            if ((MainEventForm.Text != null) && (!MainEventForm.Text.Equals("")))
            {
                condition += System.Environment.NewLine + MainEventForm.Text;
            }

            if ((MainEventDetailForm.Text != null) && (!MainEventDetailForm.Text.Equals("")))
            {
                condition += System.Environment.NewLine + MainEventDetailForm.Text;
            }

            MainConditionBox.Text = condition + System.Environment.NewLine;
        }

        private void makeSubCondition()
        {
            TextBox nextSubConditionBox = new TextBox();
            nextSubConditionBox.Name = "SubConditionBox";
            nextSubConditionBox.Location = new System.Drawing.Point(MainConditionBox.Location.X, MainConditionBox.Location.Y + 10);
            nextSubConditionBox.Size = MainConditionBox.Size;
            nextSubConditionBox.Multiline = true;
            nextSubConditionBox.Visible = true;
            nextSubConditionBox.ReadOnly = true;
            this.panel2.Controls.Add(nextSubConditionBox);



            //int currentHeight = mainConditionBox.Location.Y;
            //int boxwidth = 1;

            string condition = SubResourceForm.Text;
            if ((SubRuleForm.Text != null) && (!SubRuleForm.Text.Equals("")))
            {
                condition += System.Environment.NewLine + SubRuleForm.Text;
            }

            if ((SubEventForm.Text != null) && (!SubEventForm.Text.Equals("")))
            {
                condition += System.Environment.NewLine + SubEventForm.Text;
            }

            if ((SubEventDetailForm.Text != null) && (!SubEventDetailForm.Text.Equals("")))
            {
                condition += System.Environment.NewLine + SubEventDetailForm.Text;
            }

            nextSubConditionBox.Text = condition + System.Environment.NewLine;
        }
    }
}
