using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    
    public partial class TimeLineGridP : DataGridView, IPresentation
    {
        #region メンバ

        private TimeLineColumn timeLineColumn = new TimeLineColumn();
        private int allRowsHeight;
        private Size parentSize = new Size(0,0);

        #endregion

        #region プロパティ

        private int AllRowsHeight
        {
            get { return allRowsHeight; }
            set
            {
                if (allRowsHeight != value)
                {
                    allRowsHeight = value;
                    autoRowsResize();
                }
            }
        }

        #endregion

        #region コンストラクタ

        public TimeLineGridP(string name)
        {

            #region スーパークラスプロパティ初期化

            this.AllowUserToOrderColumns = true;
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RowTemplate.Resizable = DataGridViewTriState.False;
            this.BorderStyle = BorderStyle.None;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MultiSelect = false;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersHeight = 30;
            this.RowTemplate.Height = 25;

            #endregion

            #region プロパティ初期化

            this.Name = name;
            this.AllRowsHeight = this.ColumnHeadersHeight;

            #endregion

            #region timeLineColumnプロパティ初期化

            timeLineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            timeLineColumn.HeaderText = "タイムライン";

            #endregion
        }

        #endregion

        #region メソッド

        #region オーバライドメソッド

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            if (e.Column.ValueType == typeof(Log))
            {
                this.Columns.Remove(e.Column);
                this.Columns.Add(timeLineColumn);
            }

            if(this.Columns.Contains(timeLineColumn))
            {
                if (timeLineColumn.DisplayIndex != this.Columns.Count - 1)
                {
                    timeLineColumn.DisplayIndex = this.Columns.Count - 1;
                }
                if (this.Columns.Count > 1 && this.Columns.GetPreviousColumn(timeLineColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Frozen == false)
                {
                    this.Columns.GetPreviousColumn(timeLineColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Frozen = true;
                }
            }
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            AllRowsHeight += e.RowCount * this.RowTemplate.Height;
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            AllRowsHeight -= e.RowCount * this.RowTemplate.Height;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            this.Parent.ClientSizeChanged += ParentSizeChanged;
        }

        #endregion

        #region パブリックメソッド

        public void ParentSizeChanged(object sender, EventArgs e)
        {
            if (!parentSize.Equals(((Control)sender).ClientSize))
            {
                parentSize = ((Control)sender).ClientSize;

                autoRowsResize();

                this.Width = parentSize.Width - this.Location.X * 2;
            }
        }

        public void Add(IPresentation presentation)
        {

        }

        #endregion

        #region プライベートメソッド

        private void autoRowsResize()
        {
            if (parentSize.Height < allRowsHeight)
            {
                this.Height = parentSize.Height;
            }
            else if (this.Height != allRowsHeight)
            {
                this.Height = allRowsHeight;
            }
        }

        #endregion

        #endregion
    }
}
