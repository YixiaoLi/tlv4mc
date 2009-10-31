using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JSON_Validator
{
    class Starter
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new ValidatorMainForm());
        }
    }
}
