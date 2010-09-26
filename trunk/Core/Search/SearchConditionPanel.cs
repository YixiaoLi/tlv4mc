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
        private SearchCondition mainCondition = null;
        private List<SearchCondition> refiningSearchConditions = null;
        private int _conditionNumber;
        public int conditionNumber
        { 
            set{_conditionNumber = value;}
            get{return _conditionNumber;}
        }

        private System.Drawing.Point mainConditionLabelLocation;
        private System.Drawing.Point nextRefiningConditionLocation;

        public SearchConditionPanel(SearchCondition condition, int conditionNumber)
        {
            mainCondition = condition;
            refiningSearchConditions = new List<SearchCondition>();
            this._conditionNumber = conditionNumber;
            updateMainCondition();
            this.AutoScroll = true;
            this.Size = new System.Drawing.Size(584,209);
        }

        private void updateMainCondition()
        {
            //int conditionLabelLeftLocation = this.Location.X + 10;
            //int conditionLabelTopLocation = this.Location.Y + 10;
            int conditionLabelLeftLocation = 10;
            int conditionLabelTopLocation = 10;
            


            //main条件のラベルの作成
            Label conditionLabel = new Label();
            conditionLabel.Name = "ConditionLabel" + _conditionNumber;
            conditionLabel.Text = "検索条件：" + _conditionNumber;
            conditionLabel.AutoSize = true;
            conditionLabel.Location = new System.Drawing.Point(conditionLabelLeftLocation, conditionLabelTopLocation);
            mainConditionLabelLocation = conditionLabel.Location;

            //条件を表示するテキストボックスの作成
            TextBox conditionBox = new TextBox();
            conditionBox.Name = "ConditionBox:" + _conditionNumber;
            int conditionBoxLeftLocation = conditionLabel.Location.X;
            int conditionBoxTopLocation = conditionLabelTopLocation + conditionLabel.Size.Height + 5;
            conditionBox.Location = new System.Drawing.Point(conditionBoxLeftLocation, conditionBoxTopLocation);
            conditionBox.Width = 250;
            //conditionBox.Multiline = true;
            conditionBox.Visible = true;
            conditionBox.ReadOnly = true;

            //条件を消去するボタンの作成
            Button deleteButton = new Button();
            deleteButton.Name = "DeleteConditionButton:" + _conditionNumber;
            deleteButton.Text = "-";
            deleteButton.Tag = _conditionNumber -1;
            deleteButton.Size = new System.Drawing.Size(29, 23);
            int deleteButtonTopLocation = conditionLabel.Location.Y;
            int deleteButtonLeftLocation = conditionLabel.Location.X + conditionLabel.Size.Width + 40;
            deleteButton.Location = new System.Drawing.Point(deleteButtonLeftLocation, deleteButtonTopLocation);
            deleteButton.Click += (o, _e) =>
              {
                  ApplicationFactory.BlackBoard.DeletedSearchConditionNum = _conditionNumber;
              };

            //絞り込み条件の表示位置のY座標を設定
            nextRefiningConditionLocation = new System.Drawing.Point(conditionLabel.Location.X + 30, conditionBox.Location.Y + conditionBox.Size.Height + 20);

            
            string conditionText = mainCondition.resourceName + " : ";
            if (((mainCondition.eventName != null)) && (!mainCondition.ruleName.Equals("")))
            {
                conditionText += mainCondition.ruleName + " : ";
            }
            if ((mainCondition.eventName != null) && (!mainCondition.eventName.Equals("")))
            {
                    conditionText += mainCondition.eventName + " : ";
            }
            if ((mainCondition.eventDetail != null) && (!mainCondition.eventDetail.Equals("")))
            {
                conditionText += mainCondition.eventDetail;
            }

            conditionBox.Text = conditionText;
            this.Controls.Add(conditionLabel);
            this.Controls.Add(conditionBox);
            this.Controls.Add(deleteButton);
        }

        public void addRefiningSearchCondition(SearchCondition refiningCondition)
        {
            refiningSearchConditions.Add(refiningCondition);
            updateRefiningConditions();
        }

        private void updateRefiningConditions()
        {
            //パネル上に乗っているコンポーネントを一度消去する
            this.Controls.Clear();

            //検索条件を再度表示し直す
            updateMainCondition();

            //以下 refiningConditions に登録されている絞り込み条件一つ一つにラベル、テキストボックス、ボタンを割り当てる
            int refiningConditionID = 1;

            foreach (SearchCondition s in refiningSearchConditions)
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
                refiningConditionBox.Width = 250;
                refiningConditionBox.Height = 40;
                refiningConditionBox.Visible = true;
                refiningConditionBox.Multiline = true;
                refiningConditionBox.ReadOnly = true;

                //条件を消去するボタンの作成
                Button deleteRefiningConditionButton = new Button();
                deleteRefiningConditionButton.Name = "DeleteConditionButton:" + refiningConditionID;
                deleteRefiningConditionButton.Tag = refiningConditionID -1; 
                deleteRefiningConditionButton.Text = "-";
                deleteRefiningConditionButton.Size = new System.Drawing.Size(29,23);
                int deleteButtonTopLocation = refiningConditionLabel.Location.Y;
                int deleteButtonLeftLocation = refiningConditionLabel.Location.X + refiningConditionLabel.Size.Width + 40;
                deleteRefiningConditionButton.Location = new System.Drawing.Point(deleteButtonLeftLocation, deleteButtonTopLocation);
                deleteRefiningConditionButton.Click += (o, _e) =>
                {
                    refiningSearchConditions.RemoveAt((int)deleteRefiningConditionButton.Tag);
                    updateRefiningConditions();
                };

                //nextRefiningConditionLocation = System.Drawing.Point(refiningConditionLabel.Location.X ,refiningConditionLabel.Location.Y + refiningConditionBox.Size.Height + 5);
                nextRefiningConditionLocation.Y = refiningConditionBox.Location.Y + refiningConditionBox.Size.Height + 5;
                refiningConditionID++;

                string refiningConditionText = s.resourceName + " : ";
                if (((s.eventName != null)) && (!s.ruleName.Equals("")))
                {
                    refiningConditionText += s.ruleName + " : ";
                }
                if ((s.eventName != null) && (!s.eventName.Equals("")))
                {
                    refiningConditionText += s.eventName + " : ";
                }
                if ((s.eventDetail != null) && (!s.eventDetail.Equals("")))
                {
                    refiningConditionText += s.eventDetail;
                }
                if ((s.timing != null) && (!s.timing.Equals("")))
                {
                    refiningConditionText += System.Environment.NewLine + s.timing + " : ";
                }
                if ((s.timingValue != null) && (!s.timingValue.Equals("")))
                {
                    refiningConditionText += s.timingValue;
                }

                refiningConditionBox.Text = refiningConditionText;
                this.Controls.Add(refiningConditionLabel);
                this.Controls.Add(refiningConditionBox);
                this.Controls.Add(deleteRefiningConditionButton);
            }
        }


    }
}
