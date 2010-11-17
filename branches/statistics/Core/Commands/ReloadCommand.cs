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
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Core.Controls.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class ReloadCommand : AbstractFileChangeCommand
	{
		private OpenResourceFileAndTraceLogFileOpenForm _fileOpenDialog = new OpenResourceFileAndTraceLogFileOpenForm() { StartPosition = FormStartPosition.CenterParent};
		private BackGroundWorkForm _convertBw = new BackGroundWorkForm() { Text = "共通形式トレースログへ変換中", StartPosition = FormStartPosition.CenterParent };
		private BackGroundWorkForm _setDataBw = new BackGroundWorkForm() { Text = "初期化中", ProgressBarText = "", Message = "データを設定中", Style = ProgressBarStyle.Marquee, CanCancel = false, StartPosition = FormStartPosition.CenterParent };
		private TraceLogVisualizerData _cftl = null;
		private string _resFilePath;
		private string _logFilePath;

		public ReloadCommand()
		{
            _resFilePath = ApplicationData.FileContext.Data.ResourceData.Path;
            _logFilePath = ApplicationData.FileContext.Data.TraceLogData.Path;

			Text = "リソースファイルとトレースログファイルをリロード";

			_setDataBw.DoWork += (_o, _e) =>
			{
				ApplicationData.FileContext.Close();
				ApplicationData.FileContext.Data = _cftl;
				if (_fileOpenDialog.SaveFilePath != string.Empty)
				{
					ApplicationData.FileContext.Path = _fileOpenDialog.SaveFilePath;
					ApplicationData.FileContext.Save();
				}
			};

			_convertBw.RunWorkerCompleted += (o, e) =>
			{
				if (!e.Cancelled)
				{
					_setDataBw.RunWorkerAsync();
				}
			};

			_convertBw.DoWork += (o, _e) =>
			{
				try
				{
					string[] visualizeRuleFilePaths = Directory.GetFiles(ApplicationData.Setting.VisualizeRulesDirectoryPath, "*." + Properties.Resources.VisualizeRuleFileExtension);

					StandardFormatConverter cfc = new StandardFormatConverter(
						_resFilePath,
						_logFilePath,
						visualizeRuleFilePaths,
						(p, s) =>
						{
							if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
							_convertBw.ReportProgress((int)((double)p * 0.7));
							_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = s; }));
						});

                    StatisticsGenerator sg = new StatisticsGenerator(
                        _logFilePath,
                        cfc.ResourceData,
                        cfc.TraceLogData,
                        (p, s) =>
                        {
                            if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
                            _convertBw.ReportProgress((int)((double)p + 70));
                            _convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = s; }));
                        }
                    );

                    StatisticsData sd = null;
                    try
                    {
                        sd = sg.GenerateData();
                    }
                    catch (Exception e) // 統計情報の生成に失敗しても可視化を続ける
                    {
                        MessageForm mbox = new MessageForm(e.ToString(), "統計情報の生成に失敗しました");
                        mbox.ShowDialog();
                    }

					if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
					_convertBw.ReportProgress(90);
					_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = "共通形式データを生成中"; }));

					_cftl = new TraceLogVisualizerData(cfc.ResourceData, cfc.TraceLogData, cfc.VisualizeData, cfc.SettingData,cfc.VisualizeShapeData);

					if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
					_convertBw.ReportProgress(100);
					_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = "完了"; }));
				}
				catch (Exception e)
				{
                    MessageForm mbox = new MessageForm(e.ToString(), "変換に失敗しました。");
                    mbox.ShowDialog();
					//MessageBox.Show(e.Message, "共通形式への変換に失敗しました。", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_e.Cancel = true;
					return;
				}
			};
		}

        protected override void action()
        {
            _convertBw.RunWorkerAsync();
        }
    }
}
