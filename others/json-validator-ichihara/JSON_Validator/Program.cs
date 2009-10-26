using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JSON_Validator
{
    class JsonValidator
    {
        private String schemaJson = null;   // JSON schema
        private String trgJson = null;       // 検証するJSONファイル

        // set*()を実行していれば ture
        private bool ssflag = false;
        private bool sjflag = false;


        // JSON schemaの内容をString型の変数に格納メソッド
        public void SetSchema(String path)
        {
            try
            {
                schemaJson = File.ReadAllText(path);
            }
            catch ( Exception e )
            {
                errWrite("setSchema", e.GetType().FullName);
                Environment.Exit(1);
            }

            ssflag = true;
        }

        // 検証するJSONファイルの内容をString型の変数に格納するメソッド
        public void SetJson(String path)
        {
            try
            {
                trgJson = File.ReadAllText(path);
            }
            catch( Exception e )
            {
                errWrite("setJson", e.GetType().FullName);
                Environment.Exit(1);
            }

            sjflag = true;
        }


        public void Run()
        {
            if (!ssflag && !sjflag)    // set*()をしていない場合
            {
                errWrite("run", "set*() has not been called .");  // 英語が怪しい
                return;
            }

            // 値設定　兼　ill-formed 調査
            JsonSchema schema = SchemaParse();
            JObject person = JsonParse();

            if (person.IsValid(schema)) // 検証   C#ならもっと美しくできる？
            {
                Console.Write("Valid\n");
            }
            else
            {
                Console.Write("Invalid\n");
            }
        }

        // JsonSchemaを設定してそのオブジェクトを返すメソッド
        // 理由：JsonSchemaのill-formedを検出するため。
        private JsonSchema SchemaParse()
        {
            JsonSchema schema = null;

            try
            {
                schema = JsonSchema.Parse(schemaJson);  // JsonSchemaを設定 -> 細かいことは不明
            }
            catch(JsonReaderException e)
            {
                Console.Write("JSON schema is ill-formed\n"); // 英語が怪しい
                //Console.Write(e.Message);                     // 誤り箇所の指摘 -> Invalid property identifier character: ?. Line *, position *.
                Environment.Exit(1);
            }
            return schema;
        }

        // 検証するJSONファイルを設定してそのオブジェクトを返すメソッド
        // 検証するJSONファイルの ill-formed を検出するため
        private JObject JsonParse()
        {
            JObject json = null;

            try
            {
                json = JObject.Parse(trgJson);  // 検証するJSONファイルを設定 -> 細かいことは不明
            }
            catch(JsonReaderException e)
            {
                Console.Write("JSON is ill-formed\n"); // 英語が怪しい
                //Console.Write(e.Message);
                Environment.Exit(1);
            }
            return json;
        }

        // どのメソッドでどういう例外が飛んだかを標準出力するメソッド
        // mName: メソッド名
        // eName: 例外名など
        private void errWrite(String mName, String eName)
        {
            Console.Write("ERROR: {0} : {1}\n", mName, eName);
        }



        // テスト用メインメソッド
        static void Main(string[] args)
        {
            // ファイルパス
            String schemaPath = "F:\\test\\test.txt"; // JSON schema
            String jsonPath = "F:\\test\\json.txt";   // 検証するJSONファイル

            JsonValidator sample = new JsonValidator();

            // ファイル読み込み
            sample.SetSchema(schemaPath);
            sample.SetJson(jsonPath);

            sample.Run();

        }
    }
}
