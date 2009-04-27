/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
 *
 *  @(#) $Id$
 */
namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	partial class TextNumericUpDownTrackBarControl
	{
		/// <summary> 
		/// ɬ�פʥǥ������ѿ��Ǥ���
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// ������Υ꥽�����򤹤٤ƥ��꡼�󥢥åפ��ޤ���
		/// </summary>
		/// <param name="disposing">�ޥ͡��� �꥽�������˴�������� true���˴�����ʤ����� false �Ǥ���</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region ����ݡ��ͥ�� �ǥ����ʤ��������줿������

		/// <summary> 
		/// �ǥ����� ���ݡ��Ȥ�ɬ�פʥ᥽�åɤǤ������Υ᥽�åɤ����Ƥ� 
		/// ������ ���ǥ������ѹ����ʤ��Ǥ���������
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextNumericUpDownTrackBarControl));
			this.textBox = new System.Windows.Forms.TextBox();
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.trackBarButton = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBox.Location = new System.Drawing.Point(1, 9);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(38, 12);
			this.textBox.TabIndex = 2;
			this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// vScrollBar
			// 
			this.vScrollBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.vScrollBar.Location = new System.Drawing.Point(40, 8);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(14, 14);
			this.vScrollBar.TabIndex = 3;
			this.vScrollBar.Value = 50;
			// 
			// trackBarButton
			// 
			this.trackBarButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.trackBarButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.trackBarButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("trackBarButton.BackgroundImage")));
			this.trackBarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.trackBarButton.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
			this.trackBarButton.Location = new System.Drawing.Point(54, 8);
			this.trackBarButton.Name = "trackBarButton";
			this.trackBarButton.Size = new System.Drawing.Size(14, 14);
			this.trackBarButton.TabIndex = 4;
			this.trackBarButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.trackBarButton.UseVisualStyleBackColor = true;
			// 
			// TextNumericUpDownTrackBarControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.trackBarButton);
			this.Controls.Add(this.vScrollBar);
			this.Controls.Add(this.textBox);
			this.Name = "TextNumericUpDownTrackBarControl";
			this.Size = new System.Drawing.Size(68, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.CheckBox trackBarButton;
	}
}
