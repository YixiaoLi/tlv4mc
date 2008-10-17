using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class OpenResourceFileAndTraceLogFileOpenForm : Form
    {
        [Flags]
        private enum inputFlags
        {
            NONE = 0x0,
            RESOURCE_FILE_PATH = 0x1,
            TRACELOG_FILE_PATH = 0x2,
            CONVERTRULE_FILE_PATH = 0x4,
            ALL = RESOURCE_FILE_PATH | TRACELOG_FILE_PATH | CONVERTRULE_FILE_PATH,
        }
        [Flags]
        private enum errorFlags
        {
            NONE = 0x00,
            NO_RESOURCE_FILE_PATH = 0x01,
            NO_TRACELOG_FILE_PATH = 0x02,
            NO_CONVERTRULE_FILE_PATH = 0x04,
            WRONG_RESOURCE_FILE_PATH = 0x08,
            WRONG_TRACELOG_FILE_PATH = 0x10,
            WRONG_CONVERTRULE_FILE_PATH = 0x20,
            ALL = NO_RESOURCE_FILE_PATH | NO_TRACELOG_FILE_PATH | NO_CONVERTRULE_FILE_PATH | WRONG_RESOURCE_FILE_PATH | WRONG_TRACELOG_FILE_PATH | WRONG_CONVERTRULE_FILE_PATH,
        }
        private inputFlags _inputFlags = inputFlags.NONE;
        private errorFlags _errorFlags = errorFlags.NO_RESOURCE_FILE_PATH | errorFlags.NO_CONVERTRULE_FILE_PATH | errorFlags.NO_TRACELOG_FILE_PATH;
        private string _resourceFilePath;
        private string _traceLogFilePath;
        private string _convertRuleFilePath;
        private string _resourceFileExt;
        private string _traceLogFileExt;
        private string _convertRuleFileExt;

        public string ResourceFilePath
        {
            get { return _resourceFilePath; }
            set
            {
                if(_resourceFilePath != value)
                {
                    _resourceFilePath = value;
                    if (_resourceFilePath == "" || Path.GetExtension(_resourceFilePath) != "." + _resourceFileExt)
                    {
                        InputFlags &= ~inputFlags.RESOURCE_FILE_PATH;
                        ErrorFlags |= errorFlags.NO_RESOURCE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.WRONG_RESOURCE_FILE_PATH;
                    }
                    else if (File.Exists(_resourceFilePath))
                    {
                        InputFlags |= inputFlags.RESOURCE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.WRONG_RESOURCE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.NO_RESOURCE_FILE_PATH;
                    }
                    else
                    {
                        InputFlags &= ~inputFlags.RESOURCE_FILE_PATH;
                        ErrorFlags |= errorFlags.WRONG_RESOURCE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.NO_RESOURCE_FILE_PATH;
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
                    if (_traceLogFilePath == "" || Path.GetExtension(_traceLogFilePath) != "." + _traceLogFileExt)
                    {
                        InputFlags &= ~inputFlags.TRACELOG_FILE_PATH;
                        ErrorFlags |= errorFlags.NO_TRACELOG_FILE_PATH;
                        ErrorFlags &= ~errorFlags.WRONG_TRACELOG_FILE_PATH;
                    }
                    else if (File.Exists(_traceLogFilePath))
                    {
                        InputFlags |= inputFlags.TRACELOG_FILE_PATH;
                        ErrorFlags &= ~errorFlags.WRONG_TRACELOG_FILE_PATH;
                        ErrorFlags &= ~errorFlags.NO_TRACELOG_FILE_PATH;
                    }
                    else
                    {
                        InputFlags &= ~inputFlags.TRACELOG_FILE_PATH;
                        ErrorFlags |= errorFlags.WRONG_TRACELOG_FILE_PATH;
                        ErrorFlags &= ~errorFlags.NO_TRACELOG_FILE_PATH;
                    }
                }
            }
        }
        public string ConvertRuleFilePath
        {
            get { return _convertRuleFilePath; }
            set
            {
                if (_convertRuleFilePath != value)
                {
                    _convertRuleFilePath = value;
                    if (_convertRuleFilePath == "" || Path.GetExtension(_convertRuleFilePath) != "." + _convertRuleFileExt)
                    {
                        InputFlags &= ~inputFlags.CONVERTRULE_FILE_PATH;
                        ErrorFlags |= errorFlags.NO_CONVERTRULE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.WRONG_CONVERTRULE_FILE_PATH;
                    }
                    else if (File.Exists(_convertRuleFilePath))
                    {
                        InputFlags |= inputFlags.CONVERTRULE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.WRONG_CONVERTRULE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.NO_CONVERTRULE_FILE_PATH;
                    }
                    else
                    {
                        InputFlags &= ~inputFlags.CONVERTRULE_FILE_PATH;
                        ErrorFlags |= errorFlags.WRONG_CONVERTRULE_FILE_PATH;
                        ErrorFlags &= ~errorFlags.NO_CONVERTRULE_FILE_PATH;
                    }
                }
            }
        }
        public string ResourceFileExt
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
        public string TraceLogFileExt
        {
            get { return _traceLogFileExt; }
            set
            {
                if(_traceLogFileExt != value)
                {
                    _traceLogFileExt = value;
                    traceLogFileOpenFileDialog.DefaultExt = _traceLogFileExt;
                    traceLogFileOpenFileDialog.Filter = "TraceLig File (*." + _traceLogFileExt + ")|*." + _traceLogFileExt;
                }
            }
        }
        public string ConvertRuleFileExt
        {
            get { return _convertRuleFileExt; }
            set
            {
                if(_convertRuleFileExt != value)
                {
                    _convertRuleFileExt = value;
                }
            }
        }
        private inputFlags InputFlags
        {
            get { return _inputFlags; }
            set
            {
                if (_inputFlags != value)
                {
                    _inputFlags = value;
                    if (_inputFlags == inputFlags.ALL && ErrorFlags == errorFlags.NONE)
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
        private errorFlags ErrorFlags
        {
            get { return _errorFlags; }
            set
            {
                if (_errorFlags != value)
                {
                    _errorFlags = value;
                    if (InputFlags == inputFlags.ALL && _errorFlags == errorFlags.NONE)
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
            ResourceFileExt = "res";
            TraceLogFileExt = "log";
            ConvertRuleFileExt = "zip";
        }

        protected override void OnLoad(EventArgs evntArgs)
        {
            base.OnLoad(evntArgs);
            updateErrorMessageBox();

            foreach (string filePath in Directory.GetFiles(Properties.Settings.Default.ConvertRulesDirectoryPath))
            {
                ConvertRule c = ConvertRule.GetInstance(filePath);
                if(c != null)
                {
                    convertRuleComboBox.Items.Add(c);
                }
            }

            if (convertRuleComboBox.Items.Count == 0)
            {
                MessageBox.Show("共通形式変換ルールが存在しません。\n共通形式変換ルールは\"" + Properties.Settings.Default.ConvertRulesDirectoryPath + "\"に一つ以上存在していなければなりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }

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

            convertRuleComboBox.SelectedValueChanged += (o, e)
                =>
                {
                    ConvertRuleFilePath = ((ConvertRule)convertRuleComboBox.SelectedItem).Path;
                    convertRuleMessageBox.Text = ((ConvertRule)convertRuleComboBox.SelectedItem).Description;
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

            okButton.Click += (o, e) =>
                {
                    ResourceFilePath = Path.GetFullPath(ResourceFilePath);
                    TraceLogFilePath = Path.GetFullPath(TraceLogFilePath);
                    DialogResult = DialogResult.OK;
                    Close();
                };
        }

        private void updateErrorMessageBox()
        {
            errorMessageBox.Text = "";

            if((ErrorFlags & errorFlags.NO_RESOURCE_FILE_PATH) != errorFlags.NONE)
            {
                errorMessageBox.Text += "リソースファイルのパスを入力して下さい。\n";
            }
            if ((ErrorFlags & errorFlags.NO_TRACELOG_FILE_PATH) != errorFlags.NONE)
            {
                errorMessageBox.Text += "トレースログファイルのパスを入力して下さい。\n";
            }
            if ((ErrorFlags & errorFlags.NO_CONVERTRULE_FILE_PATH) != errorFlags.NONE)
            {
                errorMessageBox.Text += "共通形式変換ルールを選択して下さい。\n";
            }
            if ((ErrorFlags & errorFlags.WRONG_RESOURCE_FILE_PATH) != errorFlags.NONE)
            {
                errorMessageBox.Text += "指定されたリソースファイルは存在しません。入力を確認してください。\n";
            }
            if ((ErrorFlags & errorFlags.WRONG_TRACELOG_FILE_PATH) != errorFlags.NONE)
            {
                errorMessageBox.Text += "指定されたトレースログファイルは存在しません。入力を確認してください。\n";
            }
            if (ErrorFlags == errorFlags.NONE)
            {
                errorMessageBox.Text += "OKボタンで確定して下さい。\nキャンセルボタンで確定せずに終了出来ます。";
                errorMessageBox.ForeColor = Color.Green;
            }
            else
            {
                errorMessageBox.ForeColor = Color.Red;
            }
        }
    }
}
