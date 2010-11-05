using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    //詳細検索画面に張り付ける検索条件用のパネル
    //検索条件ごとにこのクラスを割り当てる
    class SearchConditionPanel : Panel
    {
        public SearchCondition mainCondition = null;
        public List<SearchCondition> refiningConditions = null;
        private int _panelNumber;
        public int panelNumber { set { _panelNumber = value; } get { return _panelNumber; } }

        private Label conditionLabel;
        private TextBox conditionBox;
        public Button deleteButton;
        private System.Drawing.Point mainConditionLabelLocation;
        private System.Drawing.Point nextRefiningConditionLocation;

        public RadioButton andButton = null;
        public RadioButton orButton = null;
        private string _timeScale; //タイムラインの時間単位（s, ms, μsなど）

        public SearchConditionPanel(SearchCondition condition, int panelNumber, string timeScale)
        {
            mainCondition = condition;
            refiningConditions = new List<SearchCondition>();
            this._panelNumber = panelNumber;
            updateMainCondition();
            this.AutoScroll = true;
            this.Size = new System.Drawing.Size(584,209);
            _timeScale = timeScale; 
        }

        private void updateMainCondition()
        {
            //int conditionLabelLeftLocation = this.Location.X + 10;
            //int conditionLabelTopLocation = this.Location.Y + 10;
            int conditionLabelLeftLocation = 10;
            int conditionLabelTopLocation = 10;

            //main条件のラベルの作成
            conditionLabel = new Label();
            conditionLabel.Name = "ConditionLabel" + _panelNumber;
            conditionLabel.Text = "基本条件：" + _panelNumber;
            conditionLabel.AutoSize = true;
            conditionLabel.Location = new System.Drawing.Point(conditionLabelLeftLocation, conditionLabelTopLocation);
            mainConditionLabelLocation = conditionLabel.Location;

            //条件を表示するテキストボックスの作成
            conditionBox = new TextBox();
            conditionBox.Name = "ConditionBox:" + _panelNumber;
            int conditionBoxLeftLocation = conditionLabel.Location.X;
            int conditionBoxTopLocation = conditionLabelTopLocation + conditionLabel.Size.Height + 5;
            conditionBox.Location = new System.Drawing.Point(conditionBoxLeftLocation, conditionBoxTopLocation);
            //conditionBox.Multiline = true;
            conditionBox.Visible = true;
            conditionBox.ReadOnly = true;


            //条件を消去するボタンの作成
            deleteButton = new Button();
            deleteButton.Name = "DeleteConditionButton:" + _panelNumber;
            deleteButton.Text = "削除";
            deleteButton.Tag = _panelNumber -1;
            deleteButton.Size = new System.Drawing.Size(37, 23);
            int deleteButtonTopLocation = conditionLabel.Location.Y;
            int deleteButtonLeftLocation = conditionLabel.Location.X + conditionLabel.Size.Width + 40;
            deleteButton.Location = new System.Drawing.Point(deleteButtonLeftLocation, deleteButtonTopLocation);
            deleteButton.Click += (o, _e) =>
              {
                  ApplicationFactory.BlackBoard.DeletedSearchConditionNum = _panelNumber;
              };

            //絞り込み条件の表示位置のY座標を設定
            nextRefiningConditionLocation = new System.Drawing.Point(conditionLabel.Location.X + 30, conditionBox.Location.Y + conditionBox.Size.Height + 20);

            
            string conditionText = mainCondition.resourceName + " : ";
            if (((mainCondition.ruleDisplayName != null)) && (!mainCondition.ruleDisplayName.Equals("")))
            {
                conditionText += mainCondition.ruleDisplayName + " : ";
            }
            if ((mainCondition.eventDisplayName != null) && (!mainCondition.eventDisplayName.Equals("")))
            {
                    conditionText += mainCondition.eventDisplayName + " : ";
            }
            if ((mainCondition.eventDetail != null) && (!mainCondition.eventDetail.Equals("")))
            {
                conditionText += mainCondition.eventDetail;
            }

            conditionBox.Text = conditionText;
            arrangeTextBoxSize(conditionBox);
            this.Controls.Add(conditionLabel);
            this.Controls.Add(conditionBox);
            this.Controls.Add(deleteButton);
        }

        public void addRefiningSearchCondition(SearchCondition refiningCondition)
        {
            refiningConditions.Add(refiningCondition);
            updateRefiningConditions();
        }

        private void updateRefiningConditions()
        {
            //パネル上に乗っているコンポーネントを一度消去する
            this.Controls.Clear();

            //基本条件を再度表示し直す
            updateMainCondition();

            //絞り込み条件が２つ以上ある場合、「全ての条件に一致する」「いずれかの条件に一致する」を選択するラジオボタンを配置する
            if (refiningConditions.Count >1 )
            {
                andButton = new RadioButton();
                andButton.Name = "andButton" + _panelNumber;
                andButton.Text = "全ての条件に一致";
                andButton.Tag = _panelNumber;
                andButton.Location = new System.Drawing.Point(conditionBox.Location.X, conditionBox.Location.Y + conditionBox.Height + 10);

                orButton = new RadioButton();
                orButton.Name = "orButton" + _panelNumber;
                orButton.Text = "いずれかの条件に一致";
                orButton.Tag = _panelNumber;
                orButton.Location = new System.Drawing.Point(andButton.Location.X + andButton.Width + 5 , conditionBox.Location.Y + conditionBox.Height + 10);
                orButton.Checked = true;

                nextRefiningConditionLocation = new System.Drawing.Point(nextRefiningConditionLocation.X, nextRefiningConditionLocation.Y + andButton.Height + 5);

                this.Controls.Add(andButton);
                this.Controls.Add(orButton);
            }

            //以下 refiningConditions に登録されている絞り込み条件一つ一つにラベル、テキストボックス、ボタンを割り当てる
            int refiningConditionID = 1;

            foreach (SearchCondition s in refiningConditions)
            {
                //条件の番号を表示するラベルの作成
                Label refiningConditionLabel = new Label();
                refiningConditionLabel.Name = "RefiningConditionLabel:" + refiningConditionID;
                refiningConditionLabel.Text = "絞り込み条件:" + refiningConditionID;
                refiningConditionLabel.AutoSize = true;
                refiningConditionLabel.Location = nextRefiningConditionLocation;


                //条件を表示するテキストボックスの作成
                TextBox refiningConditionBox = new TextBox();
                refiningConditionBox.Name = "RefiningConditionBox:" + refiningConditionID;
                int conditionBoxLeftLocation = refiningConditionLabel.Location.X;
                int conditionBoxTopLocation = refiningConditionLabel.Location.Y + refiningConditionLabel.Size.Height + 5;
                refiningConditionBox.Location = new System.Drawing.Point(conditionBoxLeftLocation, conditionBoxTopLocation);
                refiningConditionBox.Height = 45;
                refiningConditionBox.Visible = true;
                refiningConditionBox.Multiline = true;
                refiningConditionBox.ReadOnly = true;

                //条件の否定をとるためのチェックボックスを作成
                CheckBox denyConditionCheckBox = new CheckBox();
                denyConditionCheckBox.Name = "denyConditionCheckBox:" + refiningConditionID;
                denyConditionCheckBox.Text = "条件を否定";
                denyConditionCheckBox.Size = new System.Drawing.Size(120, 23);
                int denyConditionLocationY = refiningConditionLabel.Location.Y;
                int denyConditionLocationX = refiningConditionLabel.Location.X + refiningConditionLabel.Size.Width + 10;
                denyConditionCheckBox.Location = new System.Drawing.Point(denyConditionLocationX, denyConditionLocationY);
                if (s.denyCondition)
                {
                    denyConditionCheckBox.Checked = true;
                }

                denyConditionCheckBox.CheckedChanged += (o, _e) =>
                {
                    s.denyCondition = denyConditionCheckBox.Checked;
                };

                //条件を消去するボタンの作成
                Button deleteRefiningConditionButton = new Button();
                deleteRefiningConditionButton.Name = "DeleteConditionButton:" + refiningConditionID;
                deleteRefiningConditionButton.Tag = refiningConditionID -1; 
                deleteRefiningConditionButton.Text = "削除";
                deleteRefiningConditionButton.Size = new System.Drawing.Size(37, 23);
                int deleteButtonLocationY = refiningConditionLabel.Location.Y;
                int deleteButtonLocationX = denyConditionLocationX + denyConditionCheckBox.Width + 20;
                deleteRefiningConditionButton.Location = new System.Drawing.Point(deleteButtonLocationX, deleteButtonLocationY);
                deleteRefiningConditionButton.Click += (o, _e) =>
                {
                    refiningConditions.RemoveAt((int)deleteRefiningConditionButton.Tag);
                    updateRefiningConditions();
                };


                //nextRefiningConditionLocation = System.Drawing.Point(refiningConditionLabel.Location.X ,refiningConditionLabel.Location.Y + refiningConditionBox.Size.Height + 5);
                nextRefiningConditionLocation.Y = refiningConditionBox.Location.Y + refiningConditionBox.Size.Height + 5;
                refiningConditionID++;


                //テキストボックスに表示する文字列を作成
                string refiningConditionText = s.resourceName + " : ";
                if (((s.eventDisplayName != null)) && (!s.ruleDisplayName.Equals("")))
                {
                    refiningConditionText += s.ruleDisplayName + " : ";
                }

                if ((s.eventDisplayName != null) && (!s.eventDisplayName.Equals("")))
                {
                    refiningConditionText += " : " + s.eventDisplayName;
                }

                if ((s.eventDetail != null) && (!s.eventDetail.Equals("")))
                {
                    refiningConditionText += " : " + s.eventDetail;
                }

                if ((s.timingValue != null) && (!s.timingValue.Equals("")))
                {
                    refiningConditionText +=  System.Environment.NewLine + "基本条件のイベント発生時刻に対して " + s.timingValue + _timeScale;
                }

                if ((s.timing != null) && (!s.timing.Equals("")))
                {
                    refiningConditionText += s.timing;
                }

                refiningConditionBox.Text = refiningConditionText;
                arrangeTextBoxSize(refiningConditionBox);
                this.Controls.Add(refiningConditionLabel);
                this.Controls.Add(refiningConditionBox);
                this.Controls.Add(denyConditionCheckBox);
                this.Controls.Add(deleteRefiningConditionButton);
            }
        }

        //テキストボックスのサイズを自動調整
        private void arrangeTextBoxSize(TextBox targetBox)
        {
            int maxTextLength = 0;
            int font_W = (int)Math.Ceiling(
                             targetBox.Font.SizeInPoints * 2.0F / 3.0F);  // フォント幅を取得

            foreach (string A in targetBox.Lines)
            {
                // 各行の文字バイト長から「横幅」を算出し、その最大値を求める
                int len = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(A);
                maxTextLength = Math.Max(maxTextLength, len * font_W);
            }
            targetBox.Width = maxTextLength + 10;
        }
    }
}
