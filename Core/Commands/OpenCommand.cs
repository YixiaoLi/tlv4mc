
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
