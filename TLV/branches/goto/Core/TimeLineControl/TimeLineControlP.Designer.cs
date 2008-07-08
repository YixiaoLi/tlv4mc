using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public partial class TimeLineControlP
    {
        private ToolStripContainer toolStripContainer;
        private ToolStrip toolStrip;
        private ToolStripButton zoomInButton;
        private ToolStripButton zoomOutButton;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLineControlP));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.zoomInButton = new System.Windows.Forms.ToolStripButton();
            this.zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.nsPerScaleMarkButton = new System.Windows.Forms.ToolStripSplitButton();
            this.nsPerScaleMarkTrackBar = new NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar();
            this.pixelPerScaleMarkButton = new System.Windows.Forms.ToolStripSplitButton();
            this.pixelPerScaleMarkButtonTrackBar = new NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar();
            this.fillFixRowButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(478, 333);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(478, 358);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.nsPerScaleMarkButton,
            this.pixelPerScaleMarkButton,
            this.fillFixRowButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(478, 25);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 0;
            // 
            // zoomInButton
            // 
            this.zoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInButton.Image = global::NU.OJL.MPRTOS.TLV.Core.Properties.Resources.zoomInButton;
            this.zoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(23, 22);
            this.zoomInButton.Text = "拡大";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutButton.Image = global::NU.OJL.MPRTOS.TLV.Core.Properties.Resources.zoomOutButton;
            this.zoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(23, 22);
            this.zoomOutButton.Text = "縮小";
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
            this.nsPerScaleMarkTrackBar.Size = new System.Drawing.Size(400, 36);
            this.nsPerScaleMarkTrackBar.SmallChange = 1;
            this.nsPerScaleMarkTrackBar.Text = "toolStripLabeledTrackBar1";
            this.nsPerScaleMarkTrackBar.TickFrequency = 10;
            this.nsPerScaleMarkTrackBar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.nsPerScaleMarkTrackBar.TrackBarSize = new System.Drawing.Size(400, 36);
            this.nsPerScaleMarkTrackBar.Value = 0;
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
            this.pixelPerScaleMarkButtonTrackBar.LargeChange = 0;
            this.pixelPerScaleMarkButtonTrackBar.Maximum = 10;
            this.pixelPerScaleMarkButtonTrackBar.MaxLabel = "minValue";
            this.pixelPerScaleMarkButtonTrackBar.Minimum = 0;
            this.pixelPerScaleMarkButtonTrackBar.MinLabel = "minValue";
            this.pixelPerScaleMarkButtonTrackBar.Name = "pixelPerScaleMarkButtonTrackBar";
            this.pixelPerScaleMarkButtonTrackBar.NowLabel = "nowValue";
            this.pixelPerScaleMarkButtonTrackBar.PostFixText = "";
            this.pixelPerScaleMarkButtonTrackBar.Size = new System.Drawing.Size(400, 36);
            this.pixelPerScaleMarkButtonTrackBar.SmallChange = 1;
            this.pixelPerScaleMarkButtonTrackBar.Text = "toolStripLabeledTrackBar1";
            this.pixelPerScaleMarkButtonTrackBar.TickFrequency = 100;
            this.pixelPerScaleMarkButtonTrackBar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.pixelPerScaleMarkButtonTrackBar.TrackBarSize = new System.Drawing.Size(400, 36);
            this.pixelPerScaleMarkButtonTrackBar.Value = 0;
            // 
            // fillFixRowButton
            // 
            this.fillFixRowButton.Image = global::NU.OJL.MPRTOS.TLV.Core.Properties.Resources.fillRowButton;
            this.fillFixRowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillFixRowButton.Name = "fillFixRowButton";
            this.fillFixRowButton.Size = new System.Drawing.Size(129, 22);
            this.fillFixRowButton.Text = "行サイズを可変にする";
            this.fillFixRowButton.Click += new System.EventHandler(this.fillFixRowButtonClick);
            // 
            // TimeLineControlP
            // 
            this.ClientSize = new System.Drawing.Size(478, 358);
            this.Controls.Add(this.toolStripContainer);
            this.Name = "TimeLineControlP";
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        private ToolStripButton fillFixRowButton;
        private ToolStripSplitButton nsPerScaleMarkButton;
        private NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar nsPerScaleMarkTrackBar;
        private ToolStripSplitButton pixelPerScaleMarkButton;
        private NU.OJL.MPRTOS.TLV.Base.ToolStripLabeledTrackBar pixelPerScaleMarkButtonTrackBar;
    }
}
