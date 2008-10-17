using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        private inputFlags _inputFlags = inputFlags.NONE;
        private string _resourceFilePath;
        private string _traceLogFilePath;
        private string _convertRuleFilePath;

        public string ResourceFilePath
        {
            get { return _resourceFilePath; }
            set
            {
                if(_resourceFilePath != value)
                {
                    _resourceFilePath = value;
                    if (File.Exists(_resourceFilePath))
                    {
                        InputFlags |= inputFlags.RESOURCE_FILE_PATH;
                    }
                    else
                    {
                        InputFlags &= ~inputFlags.RESOURCE_FILE_PATH;
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
                    if (File.Exists(_traceLogFilePath))
                    {
                        InputFlags |= inputFlags.TRACELOG_FILE_PATH;
                    }
                    else
                    {
                        InputFlags &= ~inputFlags.TRACELOG_FILE_PATH;
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
                    if (File.Exists(_convertRuleFilePath))
                    {
                        InputFlags |= inputFlags.CONVERTRULE_FILE_PATH;
                    }
                    else
                    {
                        InputFlags &= ~inputFlags.CONVERTRULE_FILE_PATH;
                    }
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
                    if (_inputFlags != inputFlags.ALL)
                    {
                        okButton.Enabled = false;
                    }
                    else
                    {
                        okButton.Enabled = true;
                    }
                }
            }
        }

        public OpenResourceFileAndTraceLogFileOpenForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs evntArgs)
        {
            base.OnLoad(evntArgs);

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
                    DialogResult = DialogResult.OK;
                    Close();
                };
        }
    }
}
