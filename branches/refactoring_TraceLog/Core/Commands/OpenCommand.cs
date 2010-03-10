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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.ComponentModel;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class OpenCommand : AbstractFileChangeCommand
	{

		private OpenFileDialog _ofd = new OpenFileDialog();
		private BackGroundWorkForm _bw = new BackGroundWorkForm() { Text = "共通形式トレースログファイルを展開中", CanCancel = false, Style = ProgressBarStyle.Marquee, StartPosition = FormStartPosition.CenterParent };
        private string _path = string.Empty;

		public OpenCommand()
			: this(string.Empty)
        {

        }

        public OpenCommand( string path)
        {
            _path = path;
			Text = "共通形式トレースログファイルを開く";
			_bw.DoWork += (o, e) =>
			{
				_bw.ReportProgress(0);
				ApplicationData.FileContext.Close();
				try
				{
					ApplicationData.FileContext.Open(_path);
				}
				catch (Exception _e)
				{
					MessageBox.Show("ファイルのオープンに失敗しました\n" + _e.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					ApplicationData.FileContext.Close();
				}
				_bw.ReportProgress(100);
			};
        }

        protected override void action()
        {
            
            DialogResult dr = DialogResult.OK;

            if (_path == string.Empty)
            {
                _ofd.DefaultExt = Properties.Resources.StandardFormatTraceLogFileExtension;
                _ofd.Filter = "Common Format TraceLog File (*." + _ofd.DefaultExt + ")|*." + _ofd.DefaultExt;
                dr = _ofd.ShowDialog();
                _path = _ofd.FileName;
            }

            if (dr == DialogResult.OK)
            {
                _bw.RunWorkerAsync();
            }
        }

    }
}
