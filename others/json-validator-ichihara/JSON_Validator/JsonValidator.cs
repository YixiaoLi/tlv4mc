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
        readonly private string json = null;     // 検証するJSONファイル
        public string Message { get; set; }      // 主にエラー時にJSON.NETから得られる情報

        public enum Status { Valid, Invalid, JsonError, SchemaError };

        public JsonValidator(string schema, string json)
        {
            // ファイルオープン時の例外をキャッチするため
            // 引数をファイルパスから検証する文字列に変更
            this.schema = schema;
            this.json = json;
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
                this.Message = e.Message;
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
            catch (Exception e)
            {
             //   Console.Error.WriteLine(e.StackTrace);
                this.Message = e.Message;
                Console.Error.WriteLine(e.Message);
                return null;
            }
        }
    }
}
