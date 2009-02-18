using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
		{
			Regex.CacheSize = 1000;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
