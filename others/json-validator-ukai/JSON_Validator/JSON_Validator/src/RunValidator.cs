using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JSON_Validator
{
    class RunValidator
    {
        enum Result_Type
        {
            VALID,
            INVALID,
            Ill_FORMED
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

            Result_Type result = validate(my_schema, JSON_input);
            if( result == Result_Type.VALID)
            {
                Console.Write("Your input data is valid\n");
            }
            else if( result == Result_Type.INVALID)
            {
                Console.Write("Your input data is invalid\n");
            }
            else if (result == Result_Type.Ill_FORMED)
            {
                Console.Write("Your input data is ill-formed\n");
            }
            else
            {
                Console.Write("Unexpected error was occured\n");
            }
        }


        static private Result_Type validate(string my_schema, string json_input) {
            JsonSchema schema = JsonSchema.Parse(my_schema);
            JObject jsonObj;
           
            try
            {
                jsonObj = JObject.Parse(json_input);
                if (jsonObj.IsValid(schema))
                {
                    return Result_Type.VALID;
                }
                else
                {
                    return Result_Type.INVALID;
                }
            }catch(JsonReaderException e){
                Console.WriteLine(e.Message);
                return Result_Type.Ill_FORMED;
            }
        }
    }
}