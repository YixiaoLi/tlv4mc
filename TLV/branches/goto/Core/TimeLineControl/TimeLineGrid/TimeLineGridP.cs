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
        private int allRowsHeight = 0;
        private int maxRowsHeight = 0;
        private RowSizeMode rowSizeMode = RowSizeMode.Fix;
        private Size parentSize = new Size(0,0);
        public event PropertyChangedEventHandler PropertyChanged;

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

        public RowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if (rowSizeMode != value)
                {
                    rowSizeMode = value;
                    autoResizeRows();
                    NotifyPropertyChanged("RowSizeMode");
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

        public void Add(IPresentation presentation)
        {

        }

        #endregion

        #region プライベートメソッド

        private void ParentSizeChanged(object sender, EventArgs e)
        {
            if (!parentSize.Equals(this.Parent.ClientSize))
            {
                if (parentSize.Height != this.Parent.ClientSize.Height)
                {
                    parentSize.Height = this.Parent.ClientSize.Height;
                    autoResizeRows();
                }
                if (parentSize.Width != this.Parent.ClientSize.Width)
                {
                    parentSize.Width = this.Parent.ClientSize.Width;
                    this.Width = parentSize.Width - this.Location.X * 2;
                }
            }
        }

        private void autoResizeRows()
        {
            int rowHeight = 0;
            switch(RowSizeMode)
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
                        rowHeight = parentSize.Height / (this.Rows.Count + 1);
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

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #endregion
    }
}
