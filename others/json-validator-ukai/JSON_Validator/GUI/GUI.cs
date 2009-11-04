using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JSON_Validator;

namespace GUI
{
    public partial class JSON_GUI_Validator : Form
    {
        string jsonFileName = "";
        string jsonSchemaFileName = "";

        public JSON_GUI_Validator()
        {
            InitializeComponent();
        }

        private void JSON_GUI_Validator_Load(object sender, EventArgs e)
        {

        }

        private void JSONFile_TextChanged(object sender, EventArgs e)
        {
        }

        private void JSONSchemaFile_TextChanged(object sender, EventArgs e)
        {
        }

        private void JSONButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.jsonFileName = dlg.FileName;
                JSONFile.Text = jsonFileName;
            }

        }

        private void SchemaButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.jsonSchemaFileName = dlg.FileName;
                JSONSchemaFile.Text = jsonSchemaFileName;
            }

        }

        private void ResultReport_TextChanged(object sender, EventArgs e)
        {
        }

        private void ValidateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Validator.Result result = Validator.validateFile(jsonSchemaFileName,jsonFileName);
                switch (result.type)
                {
                    case Validator.Result.Type.Valid:
                        ResultReport.Text = "Your input data is valid\n";
                        break;
                    case Validator.Result.Type.Invalid:
                        ResultReport.Text = "Your input data is invalid\n";
                        break;
                    case Validator.Result.Type.IllFormed:
                        ResultReport.Text = "Your input data is ill-formed\n"　//
                                            + result.message;
                        break;
                    default:
                        ResultReport.Text = "Unexpected error was occured\n";
                        break;
                }
            }
            catch (Exception exception)
            {
                ResultReport.Text = exception.Message;
            }

        }


    }
}
