using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace JSON_Validator
{
    class RunValidator
    {
        static void Main(string[] args)
        {
            //定義したJSONスキーマの読込み
            StreamReader sr = new StreamReader(
                    "../../resource/JSON_Schema1.txt", Encoding.GetEncoding("Shift_JIS"));
            string my_schema = sr.ReadToEnd();

            //JSONファイルの読込み
            sr = new StreamReader(
                    "../../resource/JSON_input1.txt", Encoding.GetEncoding("Shift_JIS"));
            string JSON_input = sr.ReadToEnd();
            sr.Close();

            if(validate(my_schema, JSON_input))
            {
                Console.Write("Your input data is valid\n");
            }
            else
            {
                Console.Write("Your input data is invalid\n");
            }
        }


        static private bool validate(string my_schema, string json_input) {
            JsonSchema schema = JsonSchema.Parse(my_schema);

            try
            {
                JObject my_task = JObject.Parse(json_input);
                return  my_task.IsValid(schema);
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}