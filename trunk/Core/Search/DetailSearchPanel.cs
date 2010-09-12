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
        private SearchCondition mainSearchCondition; //メインの検索条件を保持する変数
        private List<SearchCondition> subSearchConditions; //サブの検索条件を保持する変数
        
        public DetailSearchPanel(TraceLogVisualizerData data)
        {
            InitializeComponent();
            MainRuleForm.Enabled = false;
            MainEventForm.Enabled = false;
            MainEventDetailForm.Enabled = false;
            _data = data;
            conditionRegister = new ConditionBeans();
            mainSearchCondition = null;
            subSearchConditions = new List<SearchCondition>();
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


            MakeMainConditionButton.Click += (o, _e) =>
            {
                makeMainCondition();
            };

            AddSubConditionButton.Click += (o, _e) =>
            {
                makeSubCondition();
            };

            DeleteMainConditionButton.Click += (o, _e) =>
            {
                deleteMainCondition();
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
            MakeMainConditionButton.Enabled = true;

            if (MainRuleForm.Enabled == true)
            {
                MainRuleForm.Items.Clear();
                MainEventForm.Items.Clear();
                MainEventDetailForm.Items.Clear();

                conditionRegister.mainRuleName = null;
                conditionRegister.mainEventName = null;
                conditionRegister.mainEventDetail = null;

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
            conditionRegister.mainResourceType = _data.ResourceData.Resources[(string)MainResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(conditionRegister.mainResourceType))
                {
                    MainRuleForm.Items.Add(rule.DisplayName);
                }
            }

            //this.searchForwardButton.Enabled = true;
            //this.searchBackwardButton.Enabled = true;
            //this.searchWholeButton.Enabled = true;    //各検索ボタンを有効にする

            MakeMainConditionButton.Enabled = true;
        }

        //イベント指定コンボボックスのアイテムをセット
        private void makeMainEventForm()
        {
            if (MainEventForm.Enabled == true)
            {
                MainEventForm.Items.Clear();
                MainEventDetailForm.Items.Clear();
                MainEventDetailForm.Enabled = false;

                conditionRegister.mainEventName = null;
                conditionRegister.mainEventDetail = null;
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
            if (MainEventDetailForm.Enabled == true)
            {
                MainEventDetailForm.Items.Clear();
            }
            else
            {
                MainEventDetailForm.Enabled = true;
            }

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

                conditionRegister.subResourceName = null;
                conditionRegister.subRuleName = null;
                conditionRegister.subEventName = null;
                conditionRegister.subEventDetail = null;


                SubRuleForm.Enabled = false;
                SubEventForm.Enabled = false;
                SubEventDetailForm.Enabled = false;
            }
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        private void makeSubRuleForm()
        {
            AddSubConditionButton.Enabled = true;

            if (SubRuleForm.Enabled == true)
            {
                SubRuleForm.Items.Clear();
                SubEventForm.Items.Clear();
                SubEventDetailForm.Items.Clear();

                conditionRegister.subRuleName = null;
                conditionRegister.subEventName = null;
                conditionRegister.subEventDetail = null;

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
            conditionRegister.subResourceType = _data.ResourceData.Resources[(string)SubResourceForm.SelectedItem].Type;
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(conditionRegister.subResourceType))
                {
                    SubRuleForm.Items.Add(rule.DisplayName);
                }
            }

            //this.searchForwardButton.Enabled = true;
            //this.searchBackwardButton.Enabled = true;
            //this.searchWholeButton.Enabled = true;    //各検索ボタンを有効にする

            AddSubConditionButton.Enabled = true;
        }

        //イベント指定コンボボックスのアイテムをセット
        private void makeSubEventForm()
        {
            if (SubEventForm.Enabled == true)
            {
                SubEventForm.Items.Clear();
                SubEventDetailForm.Items.Clear();
                SubEventDetailForm.Enabled = false;

                conditionRegister.subEventName = null;
                conditionRegister.subEventDetail = null;
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
                    conditionRegister.subRuleName = visRule.Name;
                }
                else if (visRule.Target.Equals(conditionRegister.subResourceType) && visRule.DisplayName.Equals(SubRuleForm.SelectedItem))
                {
                    conditionRegister.subRuleName = visRule.Name;
                    break;
                }
            }

            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[conditionRegister.subRuleName].Shapes;
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
            foreach (Event ev in _data.VisualizeData.VisualizeRules[conditionRegister.subRuleName].Shapes)
            {
                if (ev.DisplayName.Equals(SubEventForm.SelectedItem))
                {
                    conditionRegister.subEventName = ev.Name;
                    break;
                }
            }

            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[conditionRegister.subRuleName].Shapes[conditionRegister.subEventName];
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
            conditionRegister.clearSubCondition();
        }


        private void makeMainCondition()
        {
            if (mainSearchCondition == null)
            {
                mainSearchCondition = new SearchCondition();
                mainSearchCondition.resourceName = (string)MainResourceForm.SelectedItem;
                mainSearchCondition.ruleName = (string)MainRuleForm.SelectedItem;
                mainSearchCondition.eventName = (string)MainEventForm.SelectedItem;
                mainSearchCondition.eventDetail = (string)MainEventDetailForm.SelectedItem;
                updateMainConditionDisplay();

                BackwardSearchButton.Enabled = true;
                ForwardSearchButton.Enabled = true;
                WholeSearchButton.Enabled = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Mainの検索条件は２つ以上指定することができません。\n既存の条件を削除してください\n");
            }
        }

        private void updateMainConditionDisplay()
        {
            string condition = mainSearchCondition.resourceName;
            if ((mainSearchCondition.ruleName != null) && (!mainSearchCondition.ruleName.Equals("")))
            {
                condition += System.Environment.NewLine + mainSearchCondition.ruleName;
            }
            if ((mainSearchCondition.eventName != null) && (!mainSearchCondition.eventName.Equals("")))
            {
                condition += System.Environment.NewLine + mainSearchCondition.eventName;
            }
            if ((mainSearchCondition.eventDetail != null) && (!mainSearchCondition.eventDetail.Equals("")))
            {
                condition += System.Environment.NewLine + mainSearchCondition.eventDetail;
            }

            MainConditionBox.Text = condition;
            DeleteMainConditionButton.Enabled = true;
        }

        private void deleteMainCondition()
        {
            mainSearchCondition = null;
            MainConditionBox.Text = "";
            BackwardSearchButton.Enabled = false;
            ForwardSearchButton.Enabled = false;
            WholeSearchButton.Enabled = false;
            DeleteMainConditionButton.Enabled = false;
        }


        private void makeSubCondition()
        {
            SearchCondition subCondition = new SearchCondition();
            subCondition.resourceName = (string)SubResourceForm.SelectedItem;
            subCondition.ruleName = (string)SubRuleForm.SelectedItem;
            subCondition.eventName = (string)SubEventForm.SelectedItem;
            subCondition.eventDetail = (string)SubEventDetailForm.SelectedItem;
            subCondition.timing = (string)TimingForm.SelectedItem;
            subCondition.timingValue = (string)TimingValueForm.SelectedItem;
            subSearchConditions.Add(subCondition);

            updateSubConditionsDisplay();
        }


        private void updateSubConditionsDisplay()
        {
            //Sub条件表示画面から一度すべてのSubConditionTextBox を消去
            this.SubConditionDisplay.Controls.Clear();

            //searchConditions に登録されている検索条件一つ一つにラベルとテキストボックスを割り当てていく処理
            int conditionLabelLeftLocation = MainConditionBox.Location.X;
            int conditionLabelTopLocation = 10;
            int subConditionID = 1;

            foreach(SearchCondition s in subSearchConditions){
                //条件の番号を表示するラベルの作成
                Label conditionLabel = new Label();
                conditionLabel.Name = "SubConditionLabel" + subConditionID;
                conditionLabel.Text = "SubCondition:" + subConditionID;
                conditionLabel.AutoSize = true;
                conditionLabel.Location = new System.Drawing.Point(conditionLabelLeftLocation, conditionLabelTopLocation);
                conditionLabel.Font = MainConditionLabel.Font;


                //条件を表示するテキストボックスの作成
                TextBox conditionBox = new TextBox();
                conditionBox.Name = "SubConditionBox" + subConditionID;
                conditionBox.Size = MainConditionBox.Size;
                int conditionBoxLeftLocation = MainConditionBox.Location.X;
                int conditionBoxTopLocation = conditionLabelTopLocation + conditionLabel.Size.Height + 5 ;
                conditionBox.Location = new System.Drawing.Point(conditionBoxLeftLocation, conditionBoxTopLocation);
                conditionBox.Multiline = true;
                conditionBox.Visible = true;
                conditionBox.ReadOnly = true;

                //条件を消去するボタンの作成
                Button deleteButton = new Button();
                deleteButton.Name = "DeleteSubConditionButton" + subConditionID;
                deleteButton.Tag = subConditionID - 1;
                deleteButton.Text = "-";
                deleteButton.Size = MakeMainConditionButton.Size;
                int deleteButtonTopLocation = conditionLabel.Location.Y;
                int deleteBUttonLeftLocation = conditionLabel.Location.X + conditionLabel.Size.Width + 40;
                deleteButton.Location = new System.Drawing.Point(deleteBUttonLeftLocation, deleteButtonTopLocation);
                deleteButton.Click += (o, _e) =>
                {
                    subSearchConditions.RemoveAt((int)deleteButton.Tag);
                    updateSubConditionsDisplay();
                };

                conditionLabelTopLocation = conditionBox.Location.Y + conditionBox.Size.Height + 5;
                subConditionID++;
                

                string condition = s.resourceName;
                if (((s.eventName != null)) && (!s.ruleName.Equals("")))
                {
                    condition += System.Environment.NewLine + s.ruleName;
                }
                if ((s.eventName != null) && (!s.eventName.Equals("")))
                {
                    condition += System.Environment.NewLine + s.eventName;
                }
                if ((s.eventDetail != null) && (!s.eventDetail.Equals("")))
                {
                    condition += System.Environment.NewLine + s.eventDetail;
                }
                if ((s.timing != null) && (!s.timing.Equals("")))
                {
                    condition += System.Environment.NewLine + s.timing;
                }
                if ((s.timingValue != null) && (!s.timingValue.Equals("")))
                {
                    condition += System.Environment.NewLine + s.timingValue;
                }

                conditionBox.Text = condition + System.Environment.NewLine;
                this.SubConditionDisplay.Controls.Add(conditionLabel);
                this.SubConditionDisplay.Controls.Add(conditionBox);
                this.SubConditionDisplay.Controls.Add(deleteButton);
            }
        }

    }
}
