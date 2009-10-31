using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JSON_Validator
{
    public partial class ValidatorMainForm : Form
    {
        public ValidatorMainForm()
        {
            InitializeComponent();
        }

        /* コントロールをダブルクリックして生成された文をそのまま使用 */
        /* わかりやすいように名前を変えたほうが良い？　　　　　　　　 */

        // Shemaファイル入力ダイアログからの値入力
        // テキストボックスにファイルパスを入力するのみ
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();

            textBox1.Text = openFileDialog1.FileName;
        }


        // JSONファイル入力ダイアログからの値入力
        // テキストボックスにファイルパスを入力するのみ
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            this.Activate();

            textBox2.Text = openFileDialog2.FileName;
        }

        // Schemaファイル名をテキストボックスに入力する際に
        // ファイル入力ダイアログを使用する場合のダイアログ起動ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        // JSONファイル名をテキストボックスに入力する際に
        // ファイル入力ダイアログを使用する場合のダイアログ起動ボタン
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        // Validate開始ボタン
        private void button3_Click(object sender, EventArgs e)
        {
            JsonValidator validator = new JsonValidator(textBox1.Text, textBox2.Text);


        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new ValidatorMainForm());
        }


    }
}
