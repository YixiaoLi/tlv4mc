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

        private Rectangle mainPanelRectangle;

        public CaptureCommand(TraceLogDisplayPanel tr)
        {
          // スクリーンショットで欲しいのは topTimeLineScale から BottomTimeLineScale までの領域である
          // 以下、不要な領域を削るための調整を行う
           Control[] ctls = tr.Controls.Find("viewingAreaToolStrip", true);
           Control[] ctls2 = tr.Controls.Find("searchToolStrip", true);
           Control[] ctls3 = tr.Controls.Find("topTimeLineScale", true);
           Control[] ctls4 = tr.Controls.Find("BottomTimeLineScale", true);
           Control[] ctls5 = tr.Controls.Find("treeGridView", true);

           int height = ctls[0].Height;
           int height2 = ctls2[0].Height;
           int height3 = ctls3[0].Height;
           int height4 = ctls4[0].Height;
           int height5 = ctls5[0].Height;

           mainPanelRectangle = tr.RectangleToScreen(tr.ClientRectangle);   // TraceLogDisplayPanel の現在位置を取得し、スクリーン上の絶対位置に変換
           mainPanelRectangle.Y = mainPanelRectangle.Y + height + height2;  //スクリーンショットの上面の位置を調整
           mainPanelRectangle.Height = height3 + height4 + height5;         //スクリーンショットの高さを調整

           //TreeGridViewに垂直スクロールバーが存在している場合、スクロールバーの幅分だけキャプチャ領域から削る処理
           int treeGridViewHscrollBarWidth = tr.Width - (ctls3[0].Left + ctls3[0].Width);
           if (treeGridViewHscrollBarWidth > 0)
           {
               mainPanelRectangle.Width -= treeGridViewHscrollBarWidth;
           }

        }

        public string Text { get; set; }

        public bool CanUndo
        {
            get { return false; }
            set { }
        }

        //現在の画像を撮影し、任意パスに保存する
        public void Do()
        {
            captureScreen();
        }

        public void Undo()
        {
        }

        private void captureScreen()
        {
            SaveFileDialog sfd = new SaveFileDialog();


            sfd.FileName = "新しいファイル";    //はじめのファイル名を指定する
            sfd.InitialDirectory = @"C:\";          //はじめに表示されるフォルダを指定する
            sfd.Filter =                            //[ファイルの種類]に表示される選択肢を指定する
                "ビットマップ(*.bmp)|*.bmp|" +
                "JPEG(*.jpg)|*.jpg|" +
                "PNG(*.png)|*.png|" +
                "GIF(*.gif)|*.gif|" +
                "すべてのファイル(*.*)|*.*";

            sfd.FilterIndex = 1;            //[ファイルの種類]ではじめに「ビットマップ」が選択されているようにする
            sfd.Title = "保存先のファイルを選択してください"; 
            sfd.RestoreDirectory = true;            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.OverwritePrompt = true;            //既に存在するファイル名を指定したとき警告する。デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;            //存在しないパスが指定されたとき警告を表示する。デフォルトでTrueなので指定する必要はない


            Bitmap bmp = new Bitmap(mainPanelRectangle.Width, mainPanelRectangle.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
  
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(mainPanelRectangle.X, mainPanelRectangle.Y, 0, 0,
                  mainPanelRectangle.Size, System.Drawing.CopyPixelOperation.SourceCopy);
            }

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream stream;
                stream = sfd.OpenFile();


                if (stream != null)
                {
                    switch (sfd.FilterIndex)
                    {
                        case 1: // bitmap 形式
                            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);                            
                            break;                        
                        case 2: // jpeg 形式
                            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);                            
                            break;
                        case 3: // png 形式
                            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);                            
                            break;
                        case 4: // gif 形式
                            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);  
                            break;
                        default: // その他不明な形式 (とりあえず Bmp として記録されるようにしておく)
                            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                    }
                    stream.Close();
                }

            }
        }
    }
}

