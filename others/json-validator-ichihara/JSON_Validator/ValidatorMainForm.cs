using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

        // Shemaファイル入力ダイアログからの値入力
        // テキストボックスにファイルパスを入力するのみ
        private void schemaFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = schemaFileDialog.FileName;
        }

        // JSONファイル入力ダイアログからの値入力
        // テキストボックスにファイルパスを入力するのみ
        private void jsonFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            textBox2.Text = jsonFileDialog.FileName;
        }

        // Schemaファイル名をテキストボックスに入力する際に
        // ファイル入力ダイアログを使用する場合のダイアログ起動ボタン
        private void SchemaAddButton_Click(object sender, EventArgs e)
        {
            schemaFileDialog.ShowDialog();
        }

        // JSONファイル名をテキストボックスに入力する際に
        // ファイル入力ダイアログを使用する場合のダイアログ起動ボタン
        private void JsonAddButton_Click(object sender, EventArgs e)
        {
            jsonFileDialog.ShowDialog();
        }

        // Validate開始ボタン
        private void CheckButton_Click(object sender, EventArgs e)
        {
            string schema = null;  // Schemaファイルの中身
            string json = null;    // JSONファイルの中身

            try
            {
                schema = File.ReadAllText(textBox1.Text);
                json = File.ReadAllText(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(/*this,*/ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            JsonValidator validator = new JsonValidator(schema, json);

            switch (validator.Run())
            {
                case Status.Valid:
                    MessageBox.Show(/*this,*/"JSON is valid.", "Result");
                    break;

                case Status.Invalid:
                    MessageBox.Show(/*this,*/"JSON is Invalid.", "Result");
                    break;

                case Status.SchemaError:
                    MessageBox.Show(/*this,*/"Can't parse JSON Schema.\n" + validator.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case Status.JsonError:
                    MessageBox.Show(/*this,*/"Can't parse JSON.\n" + validator.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

        }
    }
}
