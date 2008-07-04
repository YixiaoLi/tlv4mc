using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public enum RowSizeMode
    {
        Fix,
        Fill
    }

    public partial class TimeLineGridP : DataGridView, IPresentation
    {
        #region メンバ

        private TimeLineColumn timeLineColumn = new TimeLineColumn();
        private int allRowsHeight = 0;
        private int maxRowsHeight = 0;
        private Size parentSize = new Size(0,0);
        private RowSizeMode rowSizeMode = RowSizeMode.Fix;

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
                    autoResizeRows();
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

            this.AutoResizeColumns();
            this.Width = parentSize.Width - this.Location.X * 2;
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
                Size clientSize = ((Control)sender).ClientSize;

                if (parentSize.Height != clientSize.Height)
                {
                    autoResizeRows();
                }
                if (parentSize.Width != clientSize.Width)
                {
                    this.Width = parentSize.Width - this.Location.X * 2;
                }

                parentSize = clientSize;
            }
        }

        public void Add(IPresentation presentation)
        {

        }

        #endregion

        #region プライベートメソッド

        private void autoResizeRows()
        {
            int rowHeight = 0;
            switch(rowSizeMode)
            {
                case RowSizeMode.Fix:
                    if (parentSize.Height < allRowsHeight)
                    {
                        maxRowsHeight = parentSize.Height - ((parentSize.Height - this.ColumnHeadersHeight) % this.RowTemplate.Height);
                        this.Height = maxRowsHeight;
                    }
                    else if (this.Height != allRowsHeight)
                    {
                        this.Height = allRowsHeight;
                    }
                    rowHeight = this.RowTemplate.Height;
                    break;

                case RowSizeMode.Fill:
                    if (parentSize.Height != 0 && this.Rows.Count != 0)
                    {
                        rowHeight = parentSize.Height / this.Rows.Count;
                        rowHeight = rowHeight < this.RowTemplate.Height ? this.RowTemplate.Height : rowHeight;
                        maxRowsHeight = parentSize.Height - ((parentSize.Height - this.ColumnHeadersHeight) % rowHeight);
                        this.Height = maxRowsHeight;
                    }
                    break;
            }
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.Height != rowHeight)
                {
                    row.Height = rowHeight;
                }
            }
        }

        #endregion

        #endregion
    }
}
