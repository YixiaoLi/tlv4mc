/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class OpenResourceFileAndTraceLogFileOpenForm : Form
    {
        [Flags]
        private enum InputFlags
        {
            NONE = 0x0,
            RESOURCE_FILE_PATH = 0x1,
            TRACELOG_FILE_PATH = 0x2,
            SAVE_FILE_PATH = 0x4,
            NEED = RESOURCE_FILE_PATH | TRACELOG_FILE_PATH,
            ALL = RESOURCE_FILE_PATH | TRACELOG_FILE_PATH | SAVE_FILE_PATH,
        }
        [Flags]
        private enum ErrorFlags
        {
            NONE = 0x00,
            NO_RESOURCE_FILE_PATH = 0x01,
            NO_TRACELOG_FILE_PATH = 0x02,
            WRONG_RESOURCE_FILE_PATH = 0x04,
            WRONG_TRACELOG_FILE_PATH = 0x08,
            ALL = NO_RESOURCE_FILE_PATH | NO_TRACELOG_FILE_PATH | WRONG_RESOURCE_FILE_PATH | WRONG_TRACELOG_FILE_PATH,
        }
        private InputFlags _inputFlags = InputFlags.NONE;
        private ErrorFlags _errorFlags = ErrorFlags.NO_RESOURCE_FILE_PATH | ErrorFlags.NO_TRACELOG_FILE_PATH;
        private string _resourceFilePath = string.Empty;
        private string _traceLogFilePath = string.Empty;
        private string _saveFilePath = string.Empty;
        private string _resourceFileExt;
        private string _traceLogFileExt;
        private string _saveFileExt;

        public string ResourceFilePath
        {
            get { return _resourceFilePath; }
            set
            {
                if(_resourceFilePath != value)
                {
                    _resourceFilePath = value;

					if (resourceFilePathTextBox.Text != _resourceFilePath)
						resourceFilePathTextBox.Text = _resourceFilePath;

                    if (_resourceFilePath == "")
                    {
                        InputFlag &= ~InputFlags.RESOURCE_FILE_PATH;
                        ErrorFlag |= ErrorFlags.NO_RESOURCE_FILE_PATH;
                        ErrorFlag &= ~ErrorFlags.WRONG_RESOURCE_FILE_PATH;
                    }
					else if (!File.Exists(_resourceFilePath) || Path.GetExtension(_resourceFilePath) != "." + _resourceFileExt)
                    {
                        InputFlag &= ~InputFlags.RESOURCE_FILE_PATH;
						ErrorFlag &= ~ErrorFlags.NO_RESOURCE_FILE_PATH;
						ErrorFlag |= ErrorFlags.WRONG_RESOURCE_FILE_PATH;
                    }
                    else
                    {
                        InputFlag |= InputFlags.RESOURCE_FILE_PATH;
						ErrorFlag &= ~ErrorFlags.NO_RESOURCE_FILE_PATH;
						ErrorFlag &= ~ErrorFlags.WRONG_RESOURCE_FILE_PATH;
                    }
                }
            }
        }
        public string TraceLogFilePath
        {
            get { return _traceLogFilePath; }
            set
            {
                if (_traceLogFilePath != value)
                {
                    _traceLogFilePath = value;

					if (traceLogFilePathTextBox.Text != _traceLogFilePath)
						traceLogFilePathTextBox.Text = _traceLogFilePath;

					if (_traceLogFilePath == "")
                    {
                        InputFlag &= ~InputFlags.TRACELOG_FILE_PATH;
                        ErrorFlag |= ErrorFlags.NO_TRACELOG_FILE_PATH;
                        ErrorFlag &= ~ErrorFlags.WRONG_TRACELOG_FILE_PATH;
                    }
					else if (!File.Exists(_traceLogFilePath) || Path.GetExtension(_traceLogFilePath) != "." + _traceLogFileExt)
                    {
                        InputFlag &= ~InputFlags.TRACELOG_FILE_PATH;
						ErrorFlag &= ~ErrorFlags.NO_TRACELOG_FILE_PATH;
						ErrorFlag |= ErrorFlags.WRONG_TRACELOG_FILE_PATH;
                    }
                    else
                    {
                        InputFlag |= InputFlags.TRACELOG_FILE_PATH;
						ErrorFlag &= ~ErrorFlags.NO_TRACELOG_FILE_PATH;
						ErrorFlag &= ~ErrorFlags.WRONG_TRACELOG_FILE_PATH;
                    }
                }
            }
        }
        public string SaveFilePath
        {
            get { return _saveFilePath; }
            set{
                if (_saveFilePath != value)
                {
                    _saveFilePath = value;
                    if (_saveFilePath == "")
                    {
                        InputFlag &= ~InputFlags.SAVE_FILE_PATH;
                    }
                    else
                    {
                        InputFlag |= InputFlags.SAVE_FILE_PATH;
                    }
                }
            }
        }
        private string ResourceFileExt
        {
            get { return _resourceFileExt; }
            set
            {
                if(_resourceFileExt != value)
                {
                    _resourceFileExt = value;
                    resourceFileOpenFileDialog.DefaultExt = _resourceFileExt;
                    resourceFileOpenFileDialog.Filter = "Resource File (*." + _resourceFileExt + ")|*." + _resourceFileExt;
                }
            }
        }
        private string TraceLogFileExt
        {
            get { return _traceLogFileExt; }
            set
            {
                if(_traceLogFileExt != value)
                {
                    _traceLogFileExt = value;
                    traceLogFileOpenFileDialog.DefaultExt = _traceLogFileExt;
                    traceLogFileOpenFileDialog.Filter = "TraceLog File (*." + _traceLogFileExt + ")|*." + _traceLogFileExt;
                }
            }
        }
        private string SaveFileExt
        {
            get { return _saveFileExt; }
            set
            {
                if (_saveFileExt != value)
                {
                    _saveFileExt = value;
                    saveFileDialog.DefaultExt = _saveFilePath;
                    saveFileDialog.Filter = "Common Format TraceLog File (*." + _saveFileExt + ")|*." + _saveFileExt;
                }
            }
        }
        private InputFlags InputFlag
        {
            get { return _inputFlags; }
            set
            {
                if (_inputFlags != value)
                {
                    _inputFlags = value;
                    if ((_inputFlags & InputFlags.NEED) == InputFlags.NEED && ErrorFlag == ErrorFlags.NONE)
                    {
                        okButton.Enabled = true;
                    }
                    else
                    {
                        okButton.Enabled = false;
                    }
                    updateErrorMessageBox();
                }
            }
        }
        private ErrorFlags ErrorFlag
        {
            get { return _errorFlags; }
            set
            {
                if (_errorFlags != value)
                {
                    _errorFlags = value;
                    if ((_inputFlags & InputFlags.NEED) == InputFlags.NEED && _errorFlags == ErrorFlags.NONE)
                    {
                        okButton.Enabled = true;
                    }
                    else
                    {
                        okButton.Enabled = false;
                    }
                    updateErrorMessageBox();
                }
            }
        }

        public OpenResourceFileAndTraceLogFileOpenForm()
        {
            InitializeComponent();
            ResourceFileExt = Properties.Resources.ResourceFileExtension;
            TraceLogFileExt = Properties.Resources.TraceLogFileExtension;
            SaveFileExt = Properties.Resources.StandardFormatTraceLogFileExtension;
        }

        protected override void OnLoad(EventArgs evntArgs)
        {
            base.OnLoad(evntArgs);
            updateErrorMessageBox();

            resourceFilePathTextBox.TextChanged += (o, e)
                =>
                {
                    ResourceFilePath = resourceFilePathTextBox.Text;
                };

            traceLogFilePathTextBox.TextChanged += (o, e)
                =>
                {
                    TraceLogFilePath = traceLogFilePathTextBox.Text;
                };

            savePathTextBox.TextChanged += (o, e)
                =>
                {
                    SaveFilePath = savePathTextBox.Text;
                };

            resourceFileRefButton.Click += (o, e)
                =>
                {
                    if(resourceFileOpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        resourceFilePathTextBox.Text = resourceFileOpenFileDialog.FileName;
                    }
                };

            traceLogFileRefButton.Click += (o, e)
                =>
                {
                    if (traceLogFileOpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        traceLogFilePathTextBox.Text = traceLogFileOpenFileDialog.FileName;
                    }
                };

            savePathRefButton.Click += (o, e)
                =>
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        savePathTextBox.Text = saveFileDialog.FileName;
                    }
                };

            okButton.Click += (o, e) =>
                {
                    ResourceFilePath = Path.GetFullPath(ResourceFilePath);
                    TraceLogFilePath = Path.GetFullPath(TraceLogFilePath);
                    if (SaveFilePath != string.Empty)
                        SaveFilePath = Path.GetFullPath(SaveFilePath);
                    DialogResult = DialogResult.OK;
                    Close();
                };
        }

        private void updateErrorMessageBox()
        {
            errorMessageBox.Clear();
            string[] errorMessages = new string[3];

			if ((ErrorFlag & ErrorFlags.NO_RESOURCE_FILE_PATH) != ErrorFlags.NONE)
			{
				errorMessages[0] = "・リソースファイルのパスを入力して下さい。";
			}
			else if ((ErrorFlag & ErrorFlags.WRONG_RESOURCE_FILE_PATH) != ErrorFlags.NONE)
			{
				errorMessages[0] = "・リソースファイルとして指定されたファイルはリソースファイルではありません。";
			}
            if ((ErrorFlag & ErrorFlags.NO_TRACELOG_FILE_PATH) != ErrorFlags.NONE)
            {
				errorMessages[errorMessages[0] == null ? 0 : 1] = "・トレースログファイルのパスを入力して下さい。";
            }
			else if ((ErrorFlag & ErrorFlags.WRONG_TRACELOG_FILE_PATH) != ErrorFlags.NONE)
			{
				errorMessages[errorMessages[0] == null ? 0 : 1] = "・トレースログファイルとして指定されたファイルはトレースログファイルではありません。";
			}
            if (ErrorFlag == ErrorFlags.NONE)
            {
                errorMessages[0] = "・OKボタンで確定して下さい。キャンセルボタンで確定せずに終了出来ます。";
                if((InputFlag & InputFlags.SAVE_FILE_PATH) == InputFlags.NONE)
                {
                    errorMessages[1] = "・保存先を設定することが出来ます。設定せずに作成することもできます。";
                }
				errorMessageBox.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
				errorMessageBox.ForeColor = System.Drawing.Color.Red;
            }
            errorMessageBox.Lines = errorMessages;
        }
    }
}
