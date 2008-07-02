using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public partial class TimeLineControlP
    {
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.timeLineGrid = new NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid();
            this.topTimeLine = new NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine();
            this.bottomTimeLine = new NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine();
            this.timeLineScrollBar = new System.Windows.Forms.HScrollBar();
            this.timeLineGridVScrollBar = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeLineGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.timeLineGrid, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.topTimeLine, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.bottomTimeLine, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.timeLineScrollBar, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.timeLineGridVScrollBar, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(287, 224);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // timeLineGrid
            // 
            this.timeLineGrid.AllowDrop = true;
            this.timeLineGrid.AllowUserToAddRows = false;
            this.timeLineGrid.AllowUserToDeleteRows = false;
            this.timeLineGrid.AllowUserToOrderColumns = true;
            this.timeLineGrid.AllowUserToResizeRows = false;
            this.timeLineGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeLineGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.timeLineGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.timeLineGrid.ColumnHeadersHeight = 30;
            this.timeLineGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.timeLineGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineGrid.Location = new System.Drawing.Point(0, 30);
            this.timeLineGrid.Margin = new System.Windows.Forms.Padding(0);
            this.timeLineGrid.MultiSelect = false;
            this.timeLineGrid.Name = "timeLineGrid";
            this.timeLineGrid.RowHeadersVisible = false;
            this.timeLineGrid.RowSizeMode = NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGridRowSizeMode.Fill;
            this.timeLineGrid.RowTemplate.Height = 25;
            this.timeLineGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.timeLineGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.timeLineGrid.Size = new System.Drawing.Size(271, 148);
            this.timeLineGrid.TabIndex = 0;
            this.timeLineGrid.TimeLinePositionX = 0;
            // 
            // topTimeLine
            // 
            this.topTimeLine.BackColor = System.Drawing.Color.Transparent;
            this.topTimeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTimeLine.Location = new System.Drawing.Point(0, 0);
            this.topTimeLine.Margin = new System.Windows.Forms.Padding(0);
            this.topTimeLine.Name = "topTimeLine";
            this.topTimeLine.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.topTimeLine.ResizingCursorClip = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.topTimeLine.Size = new System.Drawing.Size(271, 30);
            this.topTimeLine.TabIndex = 1;
            this.topTimeLine.TimeLinePisitionX = 0;
            this.topTimeLine.TimeLinePositionMinimumX = 0;
            // 
            // bottomTimeLine
            // 
            this.bottomTimeLine.BackColor = System.Drawing.Color.Transparent;
            this.bottomTimeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomTimeLine.Location = new System.Drawing.Point(0, 178);
            this.bottomTimeLine.Margin = new System.Windows.Forms.Padding(0);
            this.bottomTimeLine.Name = "bottomTimeLine";
            this.bottomTimeLine.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.bottomTimeLine.ResizingCursorClip = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.bottomTimeLine.Size = new System.Drawing.Size(271, 30);
            this.bottomTimeLine.TabIndex = 2;
            this.bottomTimeLine.TimeLinePisitionX = 0;
            this.bottomTimeLine.TimeLinePositionMinimumX = 0;
            // 
            // timeLineScrollBar
            // 
            this.timeLineScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineScrollBar.Location = new System.Drawing.Point(0, 208);
            this.timeLineScrollBar.Name = "timeLineScrollBar";
            this.timeLineScrollBar.Size = new System.Drawing.Size(271, 16);
            this.timeLineScrollBar.TabIndex = 2;
            // 
            // timeLineGridVScrollBar
            // 
            this.timeLineGridVScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLineGridVScrollBar.Location = new System.Drawing.Point(271, 30);
            this.timeLineGridVScrollBar.Name = "timeLineGridVScrollBar";
            this.timeLineGridVScrollBar.Size = new System.Drawing.Size(16, 148);
            this.timeLineGridVScrollBar.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(12, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 224);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(166, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "行高さ設定変更";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 33);
            this.button2.TabIndex = 4;
            this.button2.Text = "データソース設置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TimeLineControlP
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(311, 287);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "TimeLineControlP";
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeLineGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TimeLineGrid timeLineGrid;
        private TableLayoutPanel tableLayoutPanel;
        private TimeLine topTimeLine;
        private TimeLine bottomTimeLine;
        private HScrollBar timeLineScrollBar;
        private VScrollBar timeLineGridVScrollBar;
        private Panel panel1;
        private Button button1;
        private Button button2;
    }
}
