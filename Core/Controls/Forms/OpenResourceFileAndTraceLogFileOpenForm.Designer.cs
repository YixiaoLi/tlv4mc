/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
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
    partial class OpenResourceFileAndTraceLogFileOpenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.okButton = new System.Windows.Forms.Button();
			this.resourceFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.traceLogFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.traceLogFileRefButton = new System.Windows.Forms.Button();
			this.traceLogFilePathTextBox = new System.Windows.Forms.TextBox();
			this.resourceFileRefButton = new System.Windows.Forms.Button();
			this.resourceFilePathTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.savePathRefButton = new System.Windows.Forms.Button();
			this.savePathTextBox = new System.Windows.Forms.TextBox();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.errorMessageBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Enabled = false;
			this.okButton.Location = new System.Drawing.Point(274, 158);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(355, 158);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 12);
			this.label2.TabIndex = 16;
			this.label2.Text = "トレースログファイル";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 12);
			this.label1.TabIndex = 15;
			this.label1.Text = "リソースファイル";
			// 
			// traceLogFileRefButton
			// 
			this.traceLogFileRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.traceLogFileRefButton.Location = new System.Drawing.Point(355, 33);
			this.traceLogFileRefButton.Name = "traceLogFileRefButton";
			this.traceLogFileRefButton.Size = new System.Drawing.Size(75, 23);
			this.traceLogFileRefButton.TabIndex = 1;
			this.traceLogFileRefButton.Text = "参照";
			this.traceLogFileRefButton.UseVisualStyleBackColor = true;
			// 
			// traceLogFilePathTextBox
			// 
			this.traceLogFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.traceLogFilePathTextBox.Location = new System.Drawing.Point(126, 35);
			this.traceLogFilePathTextBox.Name = "traceLogFilePathTextBox";
			this.traceLogFilePathTextBox.Size = new System.Drawing.Size(223, 19);
			this.traceLogFilePathTextBox.TabIndex = 100;
			// 
			// resourceFileRefButton
			// 
			this.resourceFileRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.resourceFileRefButton.Location = new System.Drawing.Point(355, 4);
			this.resourceFileRefButton.Name = "resourceFileRefButton";
			this.resourceFileRefButton.Size = new System.Drawing.Size(75, 23);
			this.resourceFileRefButton.TabIndex = 0;
			this.resourceFileRefButton.Text = "参照";
			this.resourceFileRefButton.UseVisualStyleBackColor = true;
			// 
			// resourceFilePathTextBox
			// 
			this.resourceFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.resourceFilePathTextBox.Location = new System.Drawing.Point(126, 6);
			this.resourceFilePathTextBox.Name = "resourceFilePathTextBox";
			this.resourceFilePathTextBox.Size = new System.Drawing.Size(223, 19);
			this.resourceFilePathTextBox.TabIndex = 100;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 67);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 24;
			this.label5.Text = "保存先";
			// 
			// savePathRefButton
			// 
			this.savePathRefButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.savePathRefButton.Location = new System.Drawing.Point(355, 62);
			this.savePathRefButton.Name = "savePathRefButton";
			this.savePathRefButton.Size = new System.Drawing.Size(75, 23);
			this.savePathRefButton.TabIndex = 2;
			this.savePathRefButton.Text = "参照";
			this.savePathRefButton.UseVisualStyleBackColor = true;
			// 
			// savePathTextBox
			// 
			this.savePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.savePathTextBox.Location = new System.Drawing.Point(126, 64);
			this.savePathTextBox.Name = "savePathTextBox";
			this.savePathTextBox.Size = new System.Drawing.Size(223, 19);
			this.savePathTextBox.TabIndex = 100;
			// 
			// errorMessageBox
			// 
			this.errorMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.errorMessageBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.errorMessageBox.Location = new System.Drawing.Point(14, 103);
			this.errorMessageBox.Multiline = true;
			this.errorMessageBox.Name = "errorMessageBox";
			this.errorMessageBox.ReadOnly = true;
			this.errorMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.errorMessageBox.Size = new System.Drawing.Size(416, 49);
			this.errorMessageBox.TabIndex = 100;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(50, 12);
			this.label6.TabIndex = 27;
			this.label6.Text = "メッセージ";
			// 
			// OpenResourceFileAndTraceLogFileOpenForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(442, 193);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.errorMessageBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.savePathRefButton);
			this.Controls.Add(this.savePathTextBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.traceLogFileRefButton);
			this.Controls.Add(this.traceLogFilePathTextBox);
			this.Controls.Add(this.resourceFileRefButton);
			this.Controls.Add(this.resourceFilePathTextBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(242, 190);
			this.Name = "OpenResourceFileAndTraceLogFileOpenForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "新規作成ウィザード";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.OpenFileDialog resourceFileOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog traceLogFileOpenFileDialog;
		private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button traceLogFileRefButton;
        private System.Windows.Forms.TextBox traceLogFilePathTextBox;
        private System.Windows.Forms.Button resourceFileRefButton;
		private System.Windows.Forms.TextBox resourceFilePathTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button savePathRefButton;
        private System.Windows.Forms.TextBox savePathTextBox;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox errorMessageBox;
        private System.Windows.Forms.Label label6;
    }
}