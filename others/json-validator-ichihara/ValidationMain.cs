using System;

public class ValidationMain
{
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
