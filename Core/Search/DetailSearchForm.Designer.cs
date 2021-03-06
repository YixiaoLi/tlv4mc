﻿/*
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
 */﻿
namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    partial class DetailSearchForm
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
            this.searchOperationArea = new System.Windows.Forms.Panel();
            this.markerDeleteButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cursorScrollBar = new System.Windows.Forms.HScrollBar();
            this.cursorScrollBarLabel = new System.Windows.Forms.Label();
            this.searchWholeButton = new System.Windows.Forms.Button();
            this.searchForwardButton = new System.Windows.Forms.Button();
            this.searchBackwardButton = new System.Windows.Forms.Button();
            this.addConditionButton = new System.Windows.Forms.Button();
            this.conditionSettingArea = new System.Windows.Forms.Panel();
            this.searchOperationArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchOperationArea
            // 
            this.searchOperationArea.AutoScroll = true;
            this.searchOperationArea.BackColor = System.Drawing.Color.AliceBlue;
            this.searchOperationArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchOperationArea.Controls.Add(this.markerDeleteButton);
            this.searchOperationArea.Controls.Add(this.label1);
            this.searchOperationArea.Controls.Add(this.cursorScrollBar);
            this.searchOperationArea.Controls.Add(this.cursorScrollBarLabel);
            this.searchOperationArea.Controls.Add(this.searchWholeButton);
            this.searchOperationArea.Controls.Add(this.searchForwardButton);
            this.searchOperationArea.Controls.Add(this.searchBackwardButton);
            this.searchOperationArea.Controls.Add(this.addConditionButton);
            this.searchOperationArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchOperationArea.Location = new System.Drawing.Point(0, 0);
            this.searchOperationArea.Name = "searchOperationArea";
            this.searchOperationArea.Size = new System.Drawing.Size(584, 108);
            this.searchOperationArea.TabIndex = 0;
            // 
            // markerDeleteButton
            // 
            this.markerDeleteButton.Location = new System.Drawing.Point(487, 71);
            this.markerDeleteButton.Name = "markerDeleteButton";
            this.markerDeleteButton.Size = new System.Drawing.Size(86, 23);
            this.markerDeleteButton.TabIndex = 7;
            this.markerDeleteButton.Text = "マーカーを削除";
            this.markerDeleteButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(10, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(550, 1);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // cursorScrollBar
            // 
            this.cursorScrollBar.Location = new System.Drawing.Point(172, 19);
            this.cursorScrollBar.Name = "cursorScrollBar";
            this.cursorScrollBar.Size = new System.Drawing.Size(363, 17);
            this.cursorScrollBar.TabIndex = 5;
            // 
            // cursorScrollBarLabel
            // 
            this.cursorScrollBarLabel.AutoSize = true;
            this.cursorScrollBarLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cursorScrollBarLabel.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cursorScrollBarLabel.Location = new System.Drawing.Point(23, 19);
            this.cursorScrollBarLabel.Name = "cursorScrollBarLabel";
            this.cursorScrollBarLabel.Size = new System.Drawing.Size(127, 20);
            this.cursorScrollBarLabel.TabIndex = 4;
            this.cursorScrollBarLabel.Text = "検索開始点の移動";
            // 
            // searchWholeButton
            // 
            this.searchWholeButton.Location = new System.Drawing.Point(370, 71);
            this.searchWholeButton.Name = "searchWholeButton";
            this.searchWholeButton.Size = new System.Drawing.Size(100, 23);
            this.searchWholeButton.TabIndex = 3;
            this.searchWholeButton.Text = "全体検索";
            this.searchWholeButton.UseVisualStyleBackColor = true;
            // 
            // searchForwardButton
            // 
            this.searchForwardButton.Location = new System.Drawing.Point(265, 71);
            this.searchForwardButton.Name = "searchForwardButton";
            this.searchForwardButton.Size = new System.Drawing.Size(100, 23);
            this.searchForwardButton.TabIndex = 2;
            this.searchForwardButton.Text = "次を検索";
            this.searchForwardButton.UseVisualStyleBackColor = true;
            // 
            // searchBackwardButton
            // 
            this.searchBackwardButton.Location = new System.Drawing.Point(159, 71);
            this.searchBackwardButton.Name = "searchBackwardButton";
            this.searchBackwardButton.Size = new System.Drawing.Size(100, 23);
            this.searchBackwardButton.TabIndex = 1;
            this.searchBackwardButton.Text = "後ろを検索";
            this.searchBackwardButton.UseVisualStyleBackColor = true;
            // 
            // addConditionButton
            // 
            this.addConditionButton.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addConditionButton.Location = new System.Drawing.Point(23, 70);
            this.addConditionButton.Name = "addConditionButton";
            this.addConditionButton.Size = new System.Drawing.Size(110, 23);
            this.addConditionButton.TabIndex = 0;
            this.addConditionButton.Text = "基本条件を追加";
            this.addConditionButton.UseVisualStyleBackColor = true;
            // 
            // conditionSettingArea
            // 
            this.conditionSettingArea.AutoScroll = true;
            this.conditionSettingArea.AutoSize = true;
            this.conditionSettingArea.BackColor = System.Drawing.SystemColors.Control;
            this.conditionSettingArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.conditionSettingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionSettingArea.Location = new System.Drawing.Point(0, 108);
            this.conditionSettingArea.Name = "conditionSettingArea";
            this.conditionSettingArea.Size = new System.Drawing.Size(584, 4);
            this.conditionSettingArea.TabIndex = 1;
            // 
            // DetailSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 112);
            this.Controls.Add(this.conditionSettingArea);
            this.Controls.Add(this.searchOperationArea);
            this.MaximumSize = new System.Drawing.Size(600, 850);
            this.Name = "DetailSearchForm";
            this.Text = "詳細検索フォーム";
            this.searchOperationArea.ResumeLayout(false);
            this.searchOperationArea.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel searchOperationArea;
        private System.Windows.Forms.Panel conditionSettingArea;
        private System.Windows.Forms.Button addConditionButton;
        private System.Windows.Forms.Button searchWholeButton;
        private System.Windows.Forms.Button searchForwardButton;
        private System.Windows.Forms.Button searchBackwardButton;
        private System.Windows.Forms.Label cursorScrollBarLabel;
        private System.Windows.Forms.HScrollBar cursorScrollBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button markerDeleteButton;
    }
}