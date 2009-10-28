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
    class Validator
    {

        public struct Result
        {
            public enum Type
            {
                Valid,
                Invalid,
                IllFormed
            }
            readonly public Type type;
            readonly public string message;

            public Result(Type type, string message) {
                this.type = type;
                this.message = message;
            }
            public Result(Type type) {
                this.type = type;
                this.message = "";
            }
        }

        
        static private T open<T>(string  path, Func<StreamReader,T> f){
             //定義したJSONスキーマの読込み
             StreamReader sr = new StreamReader(
                     path, Encoding.GetEncoding("Shift_JIS"));
               T res;
                try
                {
                   res = f(sr);

                }finally{
                    sr.Close();

                }
                  return res;
        }

        static public Result validatePath(string schemaPath, string jsonPath)
        {
            string schema = open(schemaPath, sr => { return sr.ReadToEnd(); });
            string json = open(jsonPath, sr => { return sr.ReadToEnd(); });
            return validate(schema, json);
        }


        static public Result validate(string schemaContent, string json) {
            JsonSchema schema = JsonSchema.Parse(schemaContent);
           
            try
            {
                JObject jsonObj = JObject.Parse(json);
                if (jsonObj.IsValid(schema))
                {
                    return new Result(Result.Type.Valid);
                }
                else
                {
                    return new Result(Result.Type.Invalid);
                }
            }catch(JsonReaderException e){
                 return new Result(Result.Type.IllFormed,e.Message);
            }
        }
    }
}