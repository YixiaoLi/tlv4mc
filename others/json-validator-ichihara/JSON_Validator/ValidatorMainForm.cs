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
    // Validate開始ボタンでJsonValidator.Statusを使用する際
    // 長すぎるためエイリアスを活用
    using Status = JSON_Validator.JsonValidator.Status;

    public partial class ValidatorMainForm : Form
    {
        public ValidatorMainForm()
        {
            InitializeComponent();
        }

        // 起動と同時にフォームをアクティブにするため
        private void ValidatorMainForm_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        /* コントロールをダブルクリックして生成された文をそのまま使用 */
        /* わかりやすいように名前を変えたほうが良い？　　　　　　　　 */

        // Shemaファイル入力ダイアログからの値入力
        // テキストボックスにファイルパスを入力するのみ
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        // JSONファイル入力ダイアログからの値入力
        // テキストボックスにファイルパスを入力するのみ
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
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

            switch (validator.Run())
            {
                case Status.Valid:
                    MessageBox.Show(/*this,*/"JSON is valid.", "Result");
                    break;

                case Status.Invalid:
                    MessageBox.Show(/*this,*/"JSON is Invalid.", "Result");
                    break;

                case Status.SchemaError:
                    MessageBox.Show(/*this,*/"Can't parse JSON Schema.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case Status.JsonError:
                    MessageBox.Show(/*this,*/"Can't parse JSON.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new ValidatorMainForm());
        }

    }
}
