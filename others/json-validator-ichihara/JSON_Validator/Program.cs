using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JSON_Validator
{
    class JsonValidator
    {
        readonly private string schema = null;   // JSON schema
        readonly private string json = null;       // 検証するJSONファイル
        public enum Status { Valid, Invalid, JsonError, SchemaError };

        public JsonValidator(string schemaPath, string jsonPath)
        {
            this.schema = File.ReadAllText(schemaPath);
            this.json = File.ReadAllText(jsonPath);
        }


        public Status Run()
        {
            // 値設定　兼　ill-formed 調査
            JsonSchema schema = SchemaParse();
            JObject json = JsonParse();

            if (null == schema)
            {
                return Status.SchemaError;
            }
            else if (null == json)
            {
                return Status.JsonError;
            }
            else if (json.IsValid(schema))
            {
                return Status.Valid;
            }
            else
            {
                return Status.Invalid;
            }
        }

        // JsonSchemaを設定してそのオブジェクトを返すメソッド
        // 理由：JsonSchemaのill-formedを検出するため。
        private JsonSchema SchemaParse()
        {
            try
            {
                return JsonSchema.Parse(this.schema);  // JsonSchemaを設定 -> 細かいことは不明
            }
            catch (JsonReaderException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }

        // 検証するJSONファイルを設定してそのオブジェクトを返すメソッド
        // 検証するJSONファイルの ill-formed を検出するため
        private JObject JsonParse()
        {
            try
            {
                return JObject.Parse(this.json);  // 検証するJSONファイルを設定 -> 細かいことは不明
            }
            catch (JsonReaderException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return null;
            }
        }

        // テスト用メインメソッド
        static void Main(string[] args)
        {
            // ファイルパス
            string schemaPath = "C:/test/test.txt"; // JSON schema
            string jsonPath = "C:/test/json.txt";   // 検証するJSONファイル

            JsonValidator validator = new JsonValidator(schemaPath, jsonPath);

            switch (validator.Run())
            {
                case Status.Valid:
                    Console.WriteLine("JSON is Valid");
                    break;

                case Status.Invalid:
                    Console.WriteLine("JSON is Invalid");
                    break;

                case Status.JsonError:
                    Console.WriteLine("Can't parse JSON");
                    break;

                case Status.SchemaError:
                    Console.WriteLine("Can't parse JSON Schema");
                    break;

                default:
                    Console.WriteLine("error");
                    break;
            }
        }
    }
}
