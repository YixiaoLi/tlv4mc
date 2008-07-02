using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.Main;

namespace NU.OJL.MPRTOS.TLV.Main
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainAgent("MainForm").ApplicationContext);
        }
    }
}