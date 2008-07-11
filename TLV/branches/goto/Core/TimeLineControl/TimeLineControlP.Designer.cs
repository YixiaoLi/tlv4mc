using System;
using System.Windows.Forms;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public partial class TimeLineControlP
    {
        private ToolStripContainer toolStripContainer;
        private ToolStrip scaleToolStrip;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLineControlP));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.scaleToolStrip = new System.Windows.Forms.ToolStrip();
            this.nsPerScaleMarkAddButton = new System.Windows.Forms.ToolStripButton();
            this.nsPerScaleMarkButton = new System.Windows.Forms.ToolStripSplitButton();
            this.nsPerScaleMarkTrackBar = new NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar();
            this.nsPerScaleMarkSubtractButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pixelPerScaleMarkAddButton = new System.Windows.Forms.ToolStripButton();
            this.pixelPerScaleMarkButton = new System.Windows.Forms.ToolStripSplitButton();
            this.pixelPerScaleMarkButtonTrackBar = new NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar();
            this.pixelPerScaleMarkSubtractButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.fillFixRowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rowHeightAddButton = new System.Windows.Forms.ToolStripButton();
            this.rowHeightButton = new System.Windows.Forms.ToolStripSplitButton();
            this.rowHeightTrackBar = new NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar();
            this.rowHeightSubtractButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.buttonToolStrip = new System.Windows.Forms.ToolStrip();
            this.cursorButton = new System.Windows.Forms.ToolStripButton();
            this.handButton = new System.Windows.Forms.ToolStripButton();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.scaleToolStrip.SuspendLayout();
            this.buttonToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(871, 333);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(871, 358);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.scaleToolStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.buttonToolStrip);
            // 
            // scaleToolStrip
            // 
            this.scaleToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.scaleToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nsPerScaleMarkAddButton,
            this.nsPerScaleMarkButton,
            this.nsPerScaleMarkSubtractButton,
            this.toolStripSeparator2,
            this.pixelPerScaleMarkAddButton,
            this.pixelPerScaleMarkButton,
            this.pixelPerScaleMarkSubtractButton,
            this.toolStripSeparator3,
            this.fillFixRowButton,
            this.toolStripSeparator4,
            this.rowHeightAddButton,
            this.rowHeightButton,
            this.rowHeightSubtractButton,
            this.toolStripSeparator5,
            this.toolStripButton1});
            this.scaleToolStrip.Location = new System.Drawing.Point(3, 0);
            this.scaleToolStrip.Name = "scaleToolStrip";
            this.scaleToolStrip.Size = new System.Drawing.Size(590, 25);
            this.scaleToolStrip.TabIndex = 0;
            // 
            // nsPerScaleMarkAddButton
            // 
            this.nsPerScaleMarkAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nsPerScaleMarkAddButton.Image = ((System.Drawing.Image)(resources.GetObject("nsPerScaleMarkAddButton.Image")));
            this.nsPerScaleMarkAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nsPerScaleMarkAddButton.Name = "nsPerScaleMarkAddButton";
            this.nsPerScaleMarkAddButton.Size = new System.Drawing.Size(23, 22);
            this.nsPerScaleMarkAddButton.Text = "+ 1 ns/目盛";
            // 
            // nsPerScaleMarkButton
            // 
            this.nsPerScaleMarkButton.BackColor = System.Drawing.SystemColors.Control;
            this.nsPerScaleMarkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.nsPerScaleMarkButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nsPerScaleMarkTrackBar});
            this.nsPerScaleMarkButton.Image = ((System.Drawing.Image)(resources.GetObject("nsPerScaleMarkButton.Image")));
            this.nsPerScaleMarkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nsPerScaleMarkButton.Name = "nsPerScaleMarkButton";
            this.nsPerScaleMarkButton.Size = new System.Drawing.Size(65, 22);
            this.nsPerScaleMarkButton.Text = "ns/目盛";
            // 
            // nsPerScaleMarkTrackBar
            // 
            this.nsPerScaleMarkTrackBar.AutoSize = false;
            this.nsPerScaleMarkTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.nsPerScaleMarkTrackBar.LargeChange = 1;
            this.nsPerScaleMarkTrackBar.Maximum = 100;
            this.nsPerScaleMarkTrackBar.MaxLabel = "maxValue";
            this.nsPerScaleMarkTrackBar.Minimum = 0;
            this.nsPerScaleMarkTrackBar.MinLabel = "minValue";
            this.nsPerScaleMarkTrackBar.Name = "nsPerScaleMarkTrackBar";
            this.nsPerScaleMarkTrackBar.NowLabel = "0";
            this.nsPerScaleMarkTrackBar.PostFixText = "";
            this.nsPerScaleMarkTrackBar.PreFixText = "";
            this.nsPerScaleMarkTrackBar.Size = new System.Drawing.Size(400, 36);
            this.nsPerScaleMarkTrackBar.SmallChange = 1;
            this.nsPerScaleMarkTrackBar.Text = "toolStripLabeledTrackBar1";
            this.nsPerScaleMarkTrackBar.TickFrequency = 10;
            this.nsPerScaleMarkTrackBar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.nsPerScaleMarkTrackBar.TrackBarSize = new System.Drawing.Size(400, 36);
            this.nsPerScaleMarkTrackBar.Value = 0;
            // 
            // nsPerScaleMarkSubtractButton
            // 
            this.nsPerScaleMarkSubtractButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nsPerScaleMarkSubtractButton.Image = ((System.Drawing.Image)(resources.GetObject("nsPerScaleMarkSubtractButton.Image")));
            this.nsPerScaleMarkSubtractButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nsPerScaleMarkSubtractButton.Name = "nsPerScaleMarkSubtractButton";
            this.nsPerScaleMarkSubtractButton.Size = new System.Drawing.Size(23, 22);
            this.nsPerScaleMarkSubtractButton.Text = "- 1 ns/目盛";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pixelPerScaleMarkAddButton
            // 
            this.pixelPerScaleMarkAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pixelPerScaleMarkAddButton.Image = ((System.Drawing.Image)(resources.GetObject("pixelPerScaleMarkAddButton.Image")));
            this.pixelPerScaleMarkAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pixelPerScaleMarkAddButton.Name = "pixelPerScaleMarkAddButton";
            this.pixelPerScaleMarkAddButton.Size = new System.Drawing.Size(23, 22);
            this.pixelPerScaleMarkAddButton.Text = "+ 1 pixel/目盛";
            // 
            // pixelPerScaleMarkButton
            // 
            this.pixelPerScaleMarkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pixelPerScaleMarkButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pixelPerScaleMarkButtonTrackBar});
            this.pixelPerScaleMarkButton.Image = ((System.Drawing.Image)(resources.GetObject("pixelPerScaleMarkButton.Image")));
            this.pixelPerScaleMarkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pixelPerScaleMarkButton.Name = "pixelPerScaleMarkButton";
            this.pixelPerScaleMarkButton.Size = new System.Drawing.Size(77, 22);
            this.pixelPerScaleMarkButton.Text = "pixel/目盛";
            // 
            // pixelPerScaleMarkButtonTrackBar
            // 
            this.pixelPerScaleMarkButtonTrackBar.AutoSize = false;
            this.pixelPerScaleMarkButtonTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.pixelPerScaleMarkButtonTrackBar.LargeChange = 1;
            this.pixelPerScaleMarkButtonTrackBar.Maximum = 10;
            this.pixelPerScaleMarkButtonTrackBar.MaxLabel = "minValue";
            this.pixelPerScaleMarkButtonTrackBar.Minimum = 0;
            this.pixelPerScaleMarkButtonTrackBar.MinLabel = "minValue";
            this.pixelPerScaleMarkButtonTrackBar.Name = "pixelPerScaleMarkButtonTrackBar";
            this.pixelPerScaleMarkButtonTrackBar.NowLabel = "nowValue";
            this.pixelPerScaleMarkButtonTrackBar.PostFixText = "";
            this.pixelPerScaleMarkButtonTrackBar.PreFixText = "";
            this.pixelPerScaleMarkButtonTrackBar.Size = new System.Drawing.Size(400, 36);
            this.pixelPerScaleMarkButtonTrackBar.SmallChange = 1;
            this.pixelPerScaleMarkButtonTrackBar.Text = "toolStripLabeledTrackBar1";
            this.pixelPerScaleMarkButtonTrackBar.TickFrequency = 100;
            this.pixelPerScaleMarkButtonTrackBar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.pixelPerScaleMarkButtonTrackBar.TrackBarSize = new System.Drawing.Size(400, 36);
            this.pixelPerScaleMarkButtonTrackBar.Value = 0;
            // 
            // pixelPerScaleMarkSubtractButton
            // 
            this.pixelPerScaleMarkSubtractButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pixelPerScaleMarkSubtractButton.Image = ((System.Drawing.Image)(resources.GetObject("pixelPerScaleMarkSubtractButton.Image")));
            this.pixelPerScaleMarkSubtractButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pixelPerScaleMarkSubtractButton.Name = "pixelPerScaleMarkSubtractButton";
            this.pixelPerScaleMarkSubtractButton.Size = new System.Drawing.Size(23, 22);
            this.pixelPerScaleMarkSubtractButton.Text = "- 1 pixel/目盛";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // fillFixRowButton
            // 
            this.fillFixRowButton.Image = ((System.Drawing.Image)(resources.GetObject("fillFixRowButton.Image")));
            this.fillFixRowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillFixRowButton.Name = "fillFixRowButton";
            this.fillFixRowButton.Size = new System.Drawing.Size(116, 22);
            this.fillFixRowButton.Text = "行サイズ自動調整";
            this.fillFixRowButton.Click += new System.EventHandler(this.fillFixRowButtonClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // rowHeightAddButton
            // 
            this.rowHeightAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rowHeightAddButton.Image = ((System.Drawing.Image)(resources.GetObject("rowHeightAddButton.Image")));
            this.rowHeightAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rowHeightAddButton.Name = "rowHeightAddButton";
            this.rowHeightAddButton.Size = new System.Drawing.Size(23, 22);
            this.rowHeightAddButton.Text = "行サイズ + 1 pixel";
            // 
            // rowHeightButton
            // 
            this.rowHeightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rowHeightButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rowHeightTrackBar});
            this.rowHeightButton.Image = ((System.Drawing.Image)(resources.GetObject("rowHeightButton.Image")));
            this.rowHeightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rowHeightButton.Name = "rowHeightButton";
            this.rowHeightButton.Size = new System.Drawing.Size(35, 22);
            this.rowHeightButton.Text = "25";
            // 
            // rowHeightTrackBar
            // 
            this.rowHeightTrackBar.AutoSize = false;
            this.rowHeightTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.rowHeightTrackBar.LargeChange = 1;
            this.rowHeightTrackBar.Maximum = 10;
            this.rowHeightTrackBar.MaxLabel = "minValue";
            this.rowHeightTrackBar.Minimum = 0;
            this.rowHeightTrackBar.MinLabel = "minValue";
            this.rowHeightTrackBar.Name = "rowHeightTrackBar";
            this.rowHeightTrackBar.NowLabel = "nowValue";
            this.rowHeightTrackBar.PostFixText = "";
            this.rowHeightTrackBar.PreFixText = "";
            this.rowHeightTrackBar.Size = new System.Drawing.Size(400, 36);
            this.rowHeightTrackBar.SmallChange = 1;
            this.rowHeightTrackBar.Text = "toolStripLabeledTrackBar1";
            this.rowHeightTrackBar.TickFrequency = 100;
            this.rowHeightTrackBar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.rowHeightTrackBar.TrackBarSize = new System.Drawing.Size(400, 36);
            this.rowHeightTrackBar.Value = 0;
            // 
            // rowHeightSubtractButton
            // 
            this.rowHeightSubtractButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rowHeightSubtractButton.Image = ((System.Drawing.Image)(resources.GetObject("rowHeightSubtractButton.Image")));
            this.rowHeightSubtractButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rowHeightSubtractButton.Name = "rowHeightSubtractButton";
            this.rowHeightSubtractButton.Size = new System.Drawing.Size(23, 22);
            this.rowHeightSubtractButton.Text = "行サイズ - 1 pixel";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(94, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // buttonToolStrip
            // 
            this.buttonToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.buttonToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cursorButton,
            this.handButton,
            this.zoomInButton,
            this.zoomOutButton});
            this.buttonToolStrip.Location = new System.Drawing.Point(593, 0);
            this.buttonToolStrip.Name = "buttonToolStrip";
            this.buttonToolStrip.Size = new System.Drawing.Size(102, 25);
            this.buttonToolStrip.TabIndex = 1;
            // 
            // cursorButton
            // 
            this.cursorButton.Checked = true;
            this.cursorButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cursorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cursorButton.Image = ((System.Drawing.Image)(resources.GetObject("cursorButton.Image")));
            this.cursorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cursorButton.Name = "cursorButton";
            this.cursorButton.Size = new System.Drawing.Size(23, 22);
            this.cursorButton.Text = "通常のカーソル";
            // 
            // handButton
            // 
            this.handButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.handButton.Image = global::NU.OJL.MPRTOS.TLV.Core.Properties.Resources.handButton;
            this.handButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.handButton.Name = "handButton";
            this.handButton.Size = new System.Drawing.Size(23, 22);
            this.handButton.Text = "つかみ移動";
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomInButton.Image")));
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 22);
            this.zoomInButton.Text = "拡大";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutButton.Image")));
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.zoomOutButton.Text = "縮小";
            // 
            // TimeLineControlP
            // 
            this.ClientSize = new System.Drawing.Size(871, 358);
            this.Controls.Add(this.toolStripContainer);
            this.Name = "TimeLineControlP";
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.scaleToolStrip.ResumeLayout(false);
            this.scaleToolStrip.PerformLayout();
            this.buttonToolStrip.ResumeLayout(false);
            this.buttonToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        private ToolStripButton fillFixRowButton;
        private ToolStripSplitButton nsPerScaleMarkButton;
        private NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar nsPerScaleMarkTrackBar;
        private ToolStripSplitButton pixelPerScaleMarkButton;
        private NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar pixelPerScaleMarkButtonTrackBar;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton nsPerScaleMarkAddButton;
        private ToolStripButton nsPerScaleMarkSubtractButton;
        private ToolStripButton pixelPerScaleMarkAddButton;
        private ToolStripButton pixelPerScaleMarkSubtractButton;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton rowHeightAddButton;
        private ToolStripSplitButton rowHeightButton;
        private ToolStripButton rowHeightSubtractButton;
        private NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar rowHeightTrackBar;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton toolStripButton1;
        private ToolStrip buttonToolStrip;
        private ToolStripButton cursorButton;
        private ToolStripButton handButton;
        private ToolStripButton zoomInButton;
        private ToolStripButton zoomOutButton;
    }
}
