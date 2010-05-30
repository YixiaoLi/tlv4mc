/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TraceLogDisplayPanel
	{
		/// <summary> 
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraceLogDisplayPanel));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.informationToolStrip = new System.Windows.Forms.ToolStrip();
            this.timePerSclaeLabel = new System.Windows.Forms.ToolStripLabel();
            this.timePerSclaeUnitLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.viewableSpanTextBox = new System.Windows.Forms.ToolStripLabel();
            this.bottomTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
            this.topTimeLineScale = new NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineScale();
            this.treeGridView = new NU.OJL.MPRTOS.TLV.Third.TreeGridView();
            this.viewingAreaToolStrip = new System.Windows.Forms.ToolStrip();
            this.viewingTimeRangeLabel = new System.Windows.Forms.ToolStripLabel();
            this.viewingTimeRangeFromTextBox = new NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown();
            this.viewingTimeRangeFromScaleLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.viewingTimeRangeToTextBox = new NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown();
            this.viewingTimeRangeToScaleLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pixelPerScaleToolStripTextNumericUpDown = new NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.autoResizeRowHeightToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.rowHeightToolStripTextNumericUpDown = new NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.searchToolStrip = new System.Windows.Forms.ToolStrip();
            this.searchLabelName = new System.Windows.Forms.ToolStripLabel();
            this.targetResourceForm = new System.Windows.Forms.ToolStripComboBox();
            this.targetRuleForm = new System.Windows.Forms.ToolStripComboBox();
            this.targetEventForm = new System.Windows.Forms.ToolStripComboBox();
            this.targetEventDetailForm = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.searchBackwardButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.searchForwardButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.searchWholeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.markerDellButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.informationToolStrip.SuspendLayout();
            this.viewingAreaToolStrip.SuspendLayout();
            this.searchToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Location = new System.Drawing.Point(245, 54);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(525, 16);
            this.hScrollBar.TabIndex = 3;
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.informationToolStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.bottomTimeLineScale);
            this.toolStripContainer.ContentPanel.Controls.Add(this.topTimeLineScale);
            this.toolStripContainer.ContentPanel.Controls.Add(this.hScrollBar);
            this.toolStripContainer.ContentPanel.Controls.Add(this.treeGridView);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(773, 323);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(773, 398);
            this.toolStripContainer.TabIndex = 4;
            this.toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.viewingAreaToolStrip);
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.searchToolStrip);
            // 
            // informationToolStrip
            // 
            this.informationToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.informationToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timePerSclaeLabel,
            this.timePerSclaeUnitLabel,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.viewableSpanTextBox});
            this.informationToolStrip.Location = new System.Drawing.Point(3, 0);
            this.informationToolStrip.Name = "informationToolStrip";
            this.informationToolStrip.Size = new System.Drawing.Size(144, 25);
            this.informationToolStrip.TabIndex = 2;
            // 
            // timePerSclaeLabel
            // 
            this.timePerSclaeLabel.Name = "timePerSclaeLabel";
            this.timePerSclaeLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // timePerSclaeUnitLabel
            // 
            this.timePerSclaeUnitLabel.Name = "timePerSclaeUnitLabel";
            this.timePerSclaeUnitLabel.Size = new System.Drawing.Size(43, 22);
            this.timePerSclaeUnitLabel.Text = "/目盛り";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(83, 22);
            this.toolStripLabel2.Text = "表示可能領域：";
            // 
            // viewableSpanTextBox
            // 
            this.viewableSpanTextBox.Name = "viewableSpanTextBox";
            this.viewableSpanTextBox.Size = new System.Drawing.Size(0, 22);
            // 
            // bottomTimeLineScale
            // 
            this.bottomTimeLineScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomTimeLineScale.BackColor = System.Drawing.Color.Black;
            this.bottomTimeLineScale.CursorMode = NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineControl.CursorModes.Normal;
            this.bottomTimeLineScale.CursorTimeDrawed = true;
            this.bottomTimeLineScale.CursorTimeTracked = true;
            this.bottomTimeLineScale.DisplayCursorTime = true;
            this.bottomTimeLineScale.Font = new System.Drawing.Font("Courier New", 8F);
            this.bottomTimeLineScale.Location = new System.Drawing.Point(245, 34);
            this.bottomTimeLineScale.Name = "bottomTimeLineScale";
            this.bottomTimeLineScale.ScaleMarkDirection = NU.OJL.MPRTOS.TLV.Core.Controls.ScaleMarkDirection.Top;
            this.bottomTimeLineScale.SelectedTimeRangeTracked = true;
            this.bottomTimeLineScale.Size = new System.Drawing.Size(527, 20);
            this.bottomTimeLineScale.TabIndex = 5;
            this.bottomTimeLineScale.TimeLine = null;
            this.bottomTimeLineScale.TimeLineWidth = 527;
            this.bottomTimeLineScale.TimeLineX = 0;
            // 
            // topTimeLineScale
            // 
            this.topTimeLineScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.topTimeLineScale.BackColor = System.Drawing.Color.Black;
            this.topTimeLineScale.CursorMode = NU.OJL.MPRTOS.TLV.Core.Controls.TimeLineControl.CursorModes.Normal;
            this.topTimeLineScale.CursorTimeDrawed = true;
            this.topTimeLineScale.CursorTimeTracked = true;
            this.topTimeLineScale.DisplayCursorTime = true;
            this.topTimeLineScale.Font = new System.Drawing.Font("Courier New", 8F);
            this.topTimeLineScale.Location = new System.Drawing.Point(245, 1);
            this.topTimeLineScale.Name = "topTimeLineScale";
            this.topTimeLineScale.ScaleMarkDirection = NU.OJL.MPRTOS.TLV.Core.Controls.ScaleMarkDirection.Bottom;
            this.topTimeLineScale.SelectedTimeRangeTracked = true;
            this.topTimeLineScale.Size = new System.Drawing.Size(527, 20);
            this.topTimeLineScale.TabIndex = 4;
            this.topTimeLineScale.TimeLine = null;
            this.topTimeLineScale.TimeLineWidth = 527;
            this.topTimeLineScale.TimeLineX = 0;
            // 
            // treeGridView
            // 
            this.treeGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGridView.Location = new System.Drawing.Point(1, 21);
            this.treeGridView.Name = "treeGridView";
            this.treeGridView.Size = new System.Drawing.Size(771, 13);
            this.treeGridView.TabIndex = 0;
            this.treeGridView.Click += new System.EventHandler(this.treeGridView_Click);
            // 
            // viewingAreaToolStrip
            // 
            this.viewingAreaToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.viewingAreaToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewingTimeRangeLabel,
            this.viewingTimeRangeFromTextBox,
            this.viewingTimeRangeFromScaleLabel,
            this.toolStripLabel4,
            this.viewingTimeRangeToTextBox,
            this.viewingTimeRangeToScaleLabel,
            this.toolStripSeparator1,
            this.pixelPerScaleToolStripTextNumericUpDown,
            this.toolStripLabel1,
            this.toolStripSeparator3,
            this.autoResizeRowHeightToolStripButton,
            this.toolStripLabel3,
            this.rowHeightToolStripTextNumericUpDown,
            this.toolStripLabel5});
            this.viewingAreaToolStrip.Location = new System.Drawing.Point(3, 0);
            this.viewingAreaToolStrip.Name = "viewingAreaToolStrip";
            this.viewingAreaToolStrip.Size = new System.Drawing.Size(553, 25);
            this.viewingAreaToolStrip.TabIndex = 1;
            // 
            // viewingTimeRangeLabel
            // 
            this.viewingTimeRangeLabel.Name = "viewingTimeRangeLabel";
            this.viewingTimeRangeLabel.Size = new System.Drawing.Size(59, 22);
            this.viewingTimeRangeLabel.Text = "表示領域：";
            // 
            // viewingTimeRangeFromTextBox
            // 
            this.viewingTimeRangeFromTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.viewingTimeRangeFromTextBox.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.viewingTimeRangeFromTextBox.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.viewingTimeRangeFromTextBox.Name = "viewingTimeRangeFromTextBox";
            this.viewingTimeRangeFromTextBox.Radix = 10;
            this.viewingTimeRangeFromTextBox.Size = new System.Drawing.Size(78, 22);
            this.viewingTimeRangeFromTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            // 
            // viewingTimeRangeFromScaleLabel
            // 
            this.viewingTimeRangeFromScaleLabel.Name = "viewingTimeRangeFromScaleLabel";
            this.viewingTimeRangeFromScaleLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel4.Text = "〜";
            // 
            // viewingTimeRangeToTextBox
            // 
            this.viewingTimeRangeToTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.viewingTimeRangeToTextBox.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.viewingTimeRangeToTextBox.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.viewingTimeRangeToTextBox.Name = "viewingTimeRangeToTextBox";
            this.viewingTimeRangeToTextBox.Radix = 10;
            this.viewingTimeRangeToTextBox.Size = new System.Drawing.Size(78, 22);
            this.viewingTimeRangeToTextBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            // 
            // viewingTimeRangeToScaleLabel
            // 
            this.viewingTimeRangeToScaleLabel.Name = "viewingTimeRangeToScaleLabel";
            this.viewingTimeRangeToScaleLabel.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // pixelPerScaleToolStripTextNumericUpDown
            // 
            this.pixelPerScaleToolStripTextNumericUpDown.BackColor = System.Drawing.SystemColors.Control;
            this.pixelPerScaleToolStripTextNumericUpDown.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.pixelPerScaleToolStripTextNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pixelPerScaleToolStripTextNumericUpDown.Name = "pixelPerScaleToolStripTextNumericUpDown";
            this.pixelPerScaleToolStripTextNumericUpDown.Radix = 10;
            this.pixelPerScaleToolStripTextNumericUpDown.Size = new System.Drawing.Size(78, 22);
            this.pixelPerScaleToolStripTextNumericUpDown.Value = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel1.Text = "px/目盛り";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // autoResizeRowHeightToolStripButton
            // 
            this.autoResizeRowHeightToolStripButton.CheckOnClick = true;
            this.autoResizeRowHeightToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.autoResizeRowHeightToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("autoResizeRowHeightToolStripButton.Image")));
            this.autoResizeRowHeightToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoResizeRowHeightToolStripButton.Name = "autoResizeRowHeightToolStripButton";
            this.autoResizeRowHeightToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.autoResizeRowHeightToolStripButton.Text = "toolStripButton1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel3.Text = "行サイズ";
            // 
            // rowHeightToolStripTextNumericUpDown
            // 
            this.rowHeightToolStripTextNumericUpDown.BackColor = System.Drawing.SystemColors.Control;
            this.rowHeightToolStripTextNumericUpDown.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.rowHeightToolStripTextNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rowHeightToolStripTextNumericUpDown.Name = "rowHeightToolStripTextNumericUpDown";
            this.rowHeightToolStripTextNumericUpDown.Radix = 10;
            this.rowHeightToolStripTextNumericUpDown.Size = new System.Drawing.Size(78, 22);
            this.rowHeightToolStripTextNumericUpDown.Value = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel5.Text = "px";
            // 
            // searchToolStrip
            // 
            this.searchToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.searchToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchLabelName,
            this.targetResourceForm,
            this.targetRuleForm,
            this.targetEventForm,
            this.targetEventDetailForm,
            this.toolStripSeparator6,
            this.searchBackwardButton,
            this.toolStripSeparator4,
            this.toolStripSeparator5,
            this.searchForwardButton,
            this.toolStripSeparator7,
            this.searchWholeButton,
            this.toolStripSeparator8,
            this.toolStripSeparator9,
            this.markerDellButton});
            this.searchToolStrip.Location = new System.Drawing.Point(3, 25);
            this.searchToolStrip.Name = "searchToolStrip";
            this.searchToolStrip.Size = new System.Drawing.Size(638, 25);
            this.searchToolStrip.TabIndex = 2;
            // 
            // searchLabelName
            // 
            this.searchLabelName.Name = "searchLabelName";
            this.searchLabelName.Size = new System.Drawing.Size(59, 22);
            this.searchLabelName.Text = "簡易検索：";
            // 
            // targetResourceForm
            // 
            this.targetResourceForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetResourceForm.DropDownWidth = 150;
            this.targetResourceForm.Name = "targetResourceForm";
            this.targetResourceForm.Size = new System.Drawing.Size(100, 25);
            this.targetResourceForm.ToolTipText = "リソースの指定";
            this.targetResourceForm.Click += new System.EventHandler(this.TargetResourceForm_Click);
            // 
            // targetRuleForm
            // 
            this.targetRuleForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetRuleForm.DropDownWidth = 150;
            this.targetRuleForm.Name = "targetRuleForm";
            this.targetRuleForm.Size = new System.Drawing.Size(100, 25);
            this.targetRuleForm.ToolTipText = "ルールの指定";
            this.targetRuleForm.Visible = false;
            // 
            // targetEventForm
            // 
            this.targetEventForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetEventForm.DropDownWidth = 150;
            this.targetEventForm.Name = "targetEventForm";
            this.targetEventForm.Size = new System.Drawing.Size(100, 25);
            this.targetEventForm.ToolTipText = "イベントの指定";
            this.targetEventForm.Visible = false;
            // 
            // targetEventDetailForm
            // 
            this.targetEventDetailForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetEventDetailForm.DropDownWidth = 150;
            this.targetEventDetailForm.Name = "targetEventDetailForm";
            this.targetEventDetailForm.Size = new System.Drawing.Size(100, 25);
            this.targetEventDetailForm.ToolTipText = "イベント詳細の指定";
            this.targetEventDetailForm.Visible = false;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // searchBackwardButton
            // 
            this.searchBackwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchBackwardButton.Enabled = false;
            this.searchBackwardButton.Image = global::NU.OJL.MPRTOS.TLV.Core.Properties.Resources.BackwardArrow;
            this.searchBackwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchBackwardButton.Name = "searchBackwardButton";
            this.searchBackwardButton.Size = new System.Drawing.Size(23, 22);
            this.searchBackwardButton.Text = "後ろを検索";
            this.searchBackwardButton.Click += new System.EventHandler(this.searchBackwardButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // searchForwardButton
            // 
            this.searchForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchForwardButton.Enabled = false;
            this.searchForwardButton.Image = global::NU.OJL.MPRTOS.TLV.Core.Properties.Resources.ForwardArrow;
            this.searchForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchForwardButton.Name = "searchForwardButton";
            this.searchForwardButton.Size = new System.Drawing.Size(23, 22);
            this.searchForwardButton.Text = "次を検索";
            this.searchForwardButton.Click += new System.EventHandler(this.searchForwardButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // searchWholeButton
            // 
            this.searchWholeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.searchWholeButton.Image = ((System.Drawing.Image)(resources.GetObject("searchWholeButton.Image")));
            this.searchWholeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchWholeButton.Name = "searchWholeButton";
            this.searchWholeButton.Size = new System.Drawing.Size(23, 22);
            this.searchWholeButton.Text = "全体検索";
            this.searchWholeButton.Click += new System.EventHandler(this.searchWholeButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // markerDellButton
            // 
            this.markerDellButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.markerDellButton.Image = ((System.Drawing.Image)(resources.GetObject("markerDellButton.Image")));
            this.markerDellButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markerDellButton.Name = "markerDellButton";
            this.markerDellButton.Size = new System.Drawing.Size(23, 22);
            this.markerDellButton.Text = "マーカー全消去";
            this.markerDellButton.Click += new System.EventHandler(this.markerDellButton_Click);
            // 
            // TraceLogDisplayPanel
            // 
            this.Controls.Add(this.toolStripContainer);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "TraceLogDisplayPanel";
            this.Size = new System.Drawing.Size(773, 398);
            this.TimeLineWidth = 773;
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.informationToolStrip.ResumeLayout(false);
            this.informationToolStrip.PerformLayout();
            this.viewingAreaToolStrip.ResumeLayout(false);
            this.viewingAreaToolStrip.PerformLayout();
            this.searchToolStrip.ResumeLayout(false);
            this.searchToolStrip.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private NU.OJL.MPRTOS.TLV.Third.TreeGridView treeGridView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.HScrollBar hScrollBar;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.ToolStrip viewingAreaToolStrip;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeLabel;
		private NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown viewingTimeRangeFromTextBox;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeFromScaleLabel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel4;
		private NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown viewingTimeRangeToTextBox;
		private System.Windows.Forms.ToolStripLabel viewingTimeRangeToScaleLabel;
		private TimeLineScale bottomTimeLineScale;
		private TimeLineScale topTimeLineScale;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown pixelPerScaleToolStripTextNumericUpDown;
		private System.Windows.Forms.ToolStrip informationToolStrip;
		private System.Windows.Forms.ToolStripLabel timePerSclaeLabel;
		private System.Windows.Forms.ToolStripLabel timePerSclaeUnitLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripLabel viewableSpanTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private NU.OJL.MPRTOS.TLV.Base.Controls.ToolStripTextNumericUpDown rowHeightToolStripTextNumericUpDown;
		private System.Windows.Forms.ToolStripLabel toolStripLabel5;
		private System.Windows.Forms.ToolStripButton autoResizeRowHeightToolStripButton;
        private System.Windows.Forms.ToolStrip searchToolStrip;
        private System.Windows.Forms.ToolStripLabel searchLabelName;
        private System.Windows.Forms.ToolStripComboBox targetResourceForm;
        private System.Windows.Forms.ToolStripComboBox targetRuleForm;
        private System.Windows.Forms.ToolStripButton searchBackwardButton;
        private System.Windows.Forms.ToolStripButton searchForwardButton;
        private System.Windows.Forms.ToolStripComboBox targetEventForm;
        private System.Windows.Forms.ToolStripComboBox targetEventDetailForm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton searchWholeButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton markerDellButton;


	}
}
