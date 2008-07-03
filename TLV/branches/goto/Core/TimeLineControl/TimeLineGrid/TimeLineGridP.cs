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
        private TimeLineColumn timeLineColumn = new TimeLineColumn();
        private int allRowsHeight;
        private Size parentSize;

        private int AllRowsHeight
        {
            get { return allRowsHeight; }
            set
            {
                if (allRowsHeight != value)
                {
                    allRowsHeight = value;
                    this.Height = allRowsHeight;
                }
            }
        }

        public TimeLineGridP(string name)
        {
            this.Name = name;

            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RowTemplate.Resizable = DataGridViewTriState.False;
            this.BorderStyle = BorderStyle.None;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MultiSelect = false;
            this.AllowUserToOrderColumns = true;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersHeight = 30;
            this.RowTemplate.Height = 25;

            this.allRowsHeight = this.ColumnHeadersHeight;

            timeLineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            timeLineColumn.HeaderText = "タイムライン";
        }

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

        public void ParentSizeChanged(object sender, EventArgs e)
        {
            if (! parentSize.Equals(((Control)sender).ClientSize))
            {
                parentSize = ((Control)sender).ClientSize;
                if (parentSize.Height < allRowsHeight)
                {
                    this.Height = parentSize.Height;
                }
                else if (this.Height != allRowsHeight)
                {
                    this.Height = allRowsHeight;
                }

                this.Width = parentSize.Width - this.Location.X * 2;
            }
        }

        public void Add(IPresentation presentation)
        {

        }

    }
}
