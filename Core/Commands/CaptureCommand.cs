using System;
using System.Windows.Forms;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls.Forms;



namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    class CaptureCommand : ICommand
    {
        //private CaptureForm _captureDialog = new CaptureForm();
        private Rectangle rc;

        public CaptureCommand(Rectangle rc)
        {
            this.rc = rc;
        }

        public string Text { get; set; }

        public bool CanUndo
        {
            get { return false; }
            set { }
        }

        public void Do()
        {
            //_captureDialog.ShowDialog();
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
            rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(rc.X, rc.Y, 0, 0,
                  rc.Size, System.Drawing.CopyPixelOperation.SourceCopy);
            }

            // ビットマップ画像として保存して表示
            string filePath = @"C:\screen.bmp";
            bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
            System.Diagnostics.Process.Start(filePath);
        }

        public void Undo()
        {
        }

    }
}

