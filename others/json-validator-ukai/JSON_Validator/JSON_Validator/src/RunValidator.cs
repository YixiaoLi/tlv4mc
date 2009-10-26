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
        enum Validation_Type
        {
            VALIDE,
            INVALIDE,
            WELL_FORMED
        }

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

            Validation_Type result = validate(my_schema, JSON_input);
            if( result == Validation_Type.VALIDE)
            {
                Console.Write("Your input data is valid\n");
            }
            else if( result == Validation_Type.INVALIDE)
            {
                Console.Write("Your input data is invalid\n");
            }
            else if (result == Validation_Type.WELL_FORMED)
            {
                Console.Write("Your input data is well-formed, but invalid\n");
            }
            else
            {
                Console.Write("Unexpected error was occured\n");
            }
        }


        static private Validation_Type validate(string my_schema, string json_input) {
            JsonSchema schema = JsonSchema.Parse(my_schema);
            JObject my_task;
           
            try
            {
                //JSONファイルの
                my_task = JObject.Parse(json_input);
                if (my_task.IsValid(schema))
                {
                    return Validation_Type.VALIDE;
                }
                else
                {
                    return Validation_Type.WELL_FORMED;
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return Validation_Type.INVALIDE;
            }
        }
    }
}