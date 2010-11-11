using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    class BaseConditionPanel : Panel
    {
        protected TraceLogVisualizerData _data;
        protected int _parentPanelID;
        protected System.Drawing.Size _parentPanelSize;
        protected Label _displayLabel;
        protected ComboBox _targetResourceForm;
        protected ComboBox _targetRuleForm;
        protected ComboBox _targetEventForm;
        protected ComboBox _targetEventDetailForm;
        protected CheckBox _denyConditionBox;
        protected SearchCondition _searchCondition;
        protected Button _deleteButton;
        public Button DeleteButton{ set {_deleteButton = value ;} get { return _deleteButton; }}

        protected BaseConditionPanel()
        {
        }

        public BaseConditionPanel(TraceLogVisualizerData data, int parentPanelID, System.Drawing.Size parentPanelSize)
        {
            _data = data;
            _parentPanelID = parentPanelID;
            _parentPanelSize = parentPanelSize;
            _searchCondition = new SearchCondition();
            initializeComponents();
        }

        private void initializeComponents()
        {
            _displayLabel = new Label();
            _displayLabel.Name = "displayLabel:" + _parentPanelID;
            _displayLabel.Text = "基本条件" + _parentPanelID;
            _displayLabel.Font = new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Underline);
            _targetResourceForm = new ComboBox();
            _targetResourceForm.Name = "resourceForm:" + _parentPanelID;
            _targetResourceForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetRuleForm = new ComboBox();
            _targetRuleForm.Name = "ruleForm:" + _parentPanelID;
            _targetRuleForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetEventForm = new ComboBox();
            _targetEventForm.Name = "eventForm:" + _parentPanelID;
            _targetEventForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _targetEventDetailForm = new ComboBox();
            _targetEventDetailForm.Name = "eventDetailForm:" + _parentPanelID;
            _targetEventDetailForm.DropDownStyle = ComboBoxStyle.DropDownList;
            _deleteButton = new Button();
            _deleteButton.Name = "deleteButton" + _parentPanelID;
            _deleteButton.Text = "削除";
            _deleteButton.Size = new System.Drawing.Size(37, 25);
            _denyConditionBox = new CheckBox();
            _denyConditionBox.Name = "denyConditionBox:" + _parentPanelID;
            _denyConditionBox.Text = "条件を否定";
            _denyConditionBox.Width = 100;
            _targetResourceForm.Enabled = false;
            _targetRuleForm.Enabled = false;
            _targetEventForm.Enabled = false;
            _targetEventDetailForm.Enabled = false;
            _denyConditionBox.Checked = false;

            setEventHandler();

            arrangeComboBoxSize(_parentPanelSize.Width);
            arrangeLocations();
            makeResourceForm();

            this.Controls.Add(_displayLabel);
            this.Controls.Add(_targetResourceForm);
            this.Controls.Add(_targetRuleForm);
            this.Controls.Add(_targetEventForm);
            this.Controls.Add(_targetEventDetailForm);
            //this.Controls.Add(_denyConditionBox);
            this.Controls.Add(_deleteButton);
            this.Size = new System.Drawing.Size(_parentPanelSize.Width - 25, _targetResourceForm.Location.Y + _targetResourceForm.Height + 1);
        }

        //各コンポーネントが選択された際のイベントハンドラーをイベントに追加
        protected void setEventHandler()
        {
            _targetResourceForm.SelectedIndexChanged += (o, _e) =>
            {
                _searchCondition.resourceName = (string)_targetResourceForm.SelectedItem;
                _searchCondition.resourceType = _searchCondition.resourceType = _data.ResourceData.Resources[(string)_targetResourceForm.SelectedItem].Type;
                _targetRuleForm.Enabled = false;
                _targetRuleForm.Items.Clear();
                _targetEventForm.Enabled = false;
                _targetEventForm.Items.Clear();
                _targetEventDetailForm.Enabled = false;
                _targetEventDetailForm.Items.Clear();

                _targetRuleForm.Enabled = true;
                _targetRuleForm.Items.Clear();
                makeRuleForm();


                _targetResourceForm.Width = getComponentLength(_targetResourceForm.Font, (string)_targetResourceForm.SelectedItem);
                arrangeLocations();
                changePanelSize(_targetEventDetailForm.Location.X + _targetEventDetailForm.Width);

                _searchCondition.ruleName = null;
                _searchCondition.ruleDisplayName = null;
                _searchCondition.eventName = null;
                _searchCondition.eventDisplayName = null;
                _searchCondition.eventDetail = null;
            };

            _targetRuleForm.SelectedIndexChanged += (o, _e) =>
            {
                _searchCondition.ruleDisplayName = (string)_targetRuleForm.SelectedItem;
                //ルールの表示名(例："状態遷移")から正式名称(例："taskStateChange")を調べる
                foreach (VisualizeRule visRule in _data.VisualizeData.VisualizeRules)
                {
                    if (visRule.Target == null) // ルールのターゲットが CurrentContextのとき
                    {
                        _searchCondition.ruleName = visRule.Name;
                    }
                    else if (visRule.Target.Equals(_searchCondition.resourceType) && visRule.DisplayName.Equals(_targetRuleForm.SelectedItem))
                    {
                        _searchCondition.ruleName = visRule.Name;
                        break;
                    }
                }

                _targetEventForm.Enabled = true;
                _targetEventForm.Items.Clear();
                makeEventForm();

                _targetEventDetailForm.Enabled = false;
                _targetEventDetailForm.Items.Clear();

                _targetRuleForm.Width = getComponentLength(_targetRuleForm.Font, (string)_targetRuleForm.SelectedItem);
                arrangeLocations();
                changePanelSize(_targetEventDetailForm.Location.X + _targetEventDetailForm.Width);

                _searchCondition.eventName = null;
                _searchCondition.eventDisplayName = null;
                _searchCondition.eventDetail = null;
            };

            _targetEventForm.SelectedIndexChanged += (o, _e) =>
            {
                _searchCondition.eventDisplayName = (string)_targetEventForm.SelectedItem;
                //イベントの表示名(例："状態")から正式名称(例："stateChangeEvent")を調べる
                foreach (Event ev in _data.VisualizeData.VisualizeRules[_searchCondition.ruleName].Shapes)
                {
                    if (ev.DisplayName.Equals(_targetEventForm.SelectedItem))
                    {
                        _searchCondition.eventName = ev.Name;
                        break;
                    }
                }

                _targetEventDetailForm.Enabled = true;
                _targetEventDetailForm.Items.Clear();
                makeEventDetailForm();

                _targetEventForm.Width = getComponentLength(_targetEventForm.Font, (string)_targetEventForm.SelectedItem);
                arrangeLocations();
                changePanelSize(_targetEventDetailForm.Location.X + _targetEventDetailForm.Width);

                _searchCondition.eventDetail = null;
            };

            _targetEventDetailForm.SelectedIndexChanged += (o, _e) =>
            {
                _searchCondition.eventDetail = (string)_targetEventDetailForm.SelectedItem;
                _targetEventDetailForm.Width = getComponentLength(_targetEventDetailForm.Font, (string)_targetEventDetailForm.SelectedItem);
                arrangeLocations();
                changePanelSize(_targetEventDetailForm.Location.X + _targetEventDetailForm.Width);
            };

            _denyConditionBox.CheckedChanged += (o, _e) =>
            {
                _searchCondition.denyCondition = _denyConditionBox.Checked;
            };

            this.Click += (o, _e) =>
            {
                this.Focus();
            };
        }

        public void setParentPanelID(int ID)
        {
            _displayLabel.Name = "displayLabel:" + ID;
            _displayLabel.Text = "基本条件:" + ID;
            _targetResourceForm.Name = "resourceForm:" + ID;
            _targetRuleForm.Name = "resourceForm:" + ID;
            _targetEventForm.Name = "resourceForm:" + ID;
            _targetEventDetailForm.Name = "resourceForm:" + ID;
            _deleteButton.Name = "deleteButton:" + ID;
        }

        //リソース指定コンボボックスのアイテムをセット
        protected void makeResourceForm()
        {
            _targetResourceForm.Enabled = true;
            GeneralNamedCollection<Resource> resData = this._data.ResourceData.Resources;

            foreach (Resource res in resData)
            {
                if (!res.Name.Equals("CurrentContext"))
                    _targetResourceForm.Items.Add(res.Name);
            }

            arrangeDropDownSize(_targetResourceForm);
        }

        //リソースが選択されたときにルール指定コンボボックスのアイテムをセットする
        protected void makeRuleForm()
        {
            GeneralNamedCollection<VisualizeRule> visRules = _data.VisualizeData.VisualizeRules;

            foreach (VisualizeRule rule in visRules)
            {
                if (rule.Target != null && rule.Target.Equals(_searchCondition.resourceType))
                {
                    _targetRuleForm.Items.Add(rule.DisplayName);
                }
            }
            arrangeDropDownSize(_targetRuleForm);
        }


        //イベント指定コンボボックスのアイテムをセット
        protected void makeEventForm()
        {
            GeneralNamedCollection<Event> eventShapes = _data.VisualizeData.VisualizeRules[_searchCondition.ruleName].Shapes;
            foreach (Event e in eventShapes)
            {
                _targetEventForm.Items.Add(e.DisplayName);
            }

            arrangeDropDownSize(_targetEventForm);
        }

        //イベント詳細指定コンボボックスのアイテムをセット
        protected void makeEventDetailForm()
        {
            //指定されたイベントが持つ RUNNABLE, RUNNING といった状態を切り出す
            Event e = _data.VisualizeData.VisualizeRules[_searchCondition.ruleName].Shapes[_searchCondition.eventName];
            foreach (Figure fg in e.Figures) // いつもe.Figuresの要素は一つしかないが、foreach で回しておく（どんなときに複数の要素を持つかは要調査）
            {
                if (fg.Figures == null) //選択されたイベントにイベント詳細が存在しない場合
                {
                    _targetEventDetailForm.Enabled = false;
                }
                else
                {
                    foreach (Figure fg2 in fg.Figures)
                    {                                                   // 処理の意図を以下に例示
                        String[] conditions = fg2.Condition.Split('='); // "($FROM_VAL)==RUNNING"  ⇒ "($FROM_VAL)", "","RUNNING"
                        _targetEventDetailForm.Items.Add(conditions[2]); // "RUNNING"をイベント詳細のコンボボックスへセット
                    }
                }

            }

            arrangeDropDownSize(_targetEventDetailForm);
        }

         protected void changePanelSize(int width)
         {
             if (width > _parentPanelSize.Width)
             {
                 this.Width = width;
             }
         }
    

        private void arrangeComboBoxSize(int width)
        {
            int boxSize = width / 6; //初期サイズを width / 6 に固定
            _targetResourceForm.Width = boxSize;
            _targetRuleForm.Width = boxSize;
            _targetEventForm.Width = boxSize;
            _targetEventDetailForm.Width = boxSize;
        }

        protected void arrangeLocations()
        {
            _displayLabel.Location = new System.Drawing.Point(10, 10);
            _targetResourceForm.Location = new System.Drawing.Point(_displayLabel.Location.X, _displayLabel.Location.Y + _displayLabel.Height + 1);
            _targetRuleForm.Location = new System.Drawing.Point(_targetResourceForm.Location.X + _targetResourceForm.Width + 5, _targetResourceForm.Location.Y);
            _targetEventForm.Location = new System.Drawing.Point(_targetRuleForm.Location.X + _targetRuleForm.Width + 5, _targetRuleForm.Location.Y);
            _targetEventDetailForm.Location = new System.Drawing.Point(_targetEventForm.Location.X + _targetEventForm.Width + 5, _targetEventForm.Location.Y);
            _denyConditionBox.Location = new System.Drawing.Point(_displayLabel.Location.X + _displayLabel.Width, _displayLabel.Location.Y);
            _deleteButton.Location = new System.Drawing.Point(_denyConditionBox.Location.X + _denyConditionBox.Width + 10, _denyConditionBox.Location.Y -2);

        }

        //コンボボックスのドロップダウンボックスのサイズを自動調整
        protected void arrangeDropDownSize(ComboBox targetBox)
        {
            int maxTextLength = 0;
            foreach (string A in targetBox.Items)
            {
                // 各行の文字バイト長から「横幅」を算出し、その最大値を求める
                maxTextLength = Math.Max(maxTextLength, getComponentLength(targetBox.Font, A));
            }
            targetBox.DropDownWidth = maxTextLength + 5;
        }


        protected int getComponentLength(System.Drawing.Font font, string text)
        {
            int len = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(text);
            int font_W = (int)Math.Ceiling(font.SizeInPoints * 2.0F / 3.0F);  // フォント幅を取得
            return len * font_W + 50;
        }

        public SearchCondition getSearchCondition()
        {
            return _searchCondition;
        }
    }
}
