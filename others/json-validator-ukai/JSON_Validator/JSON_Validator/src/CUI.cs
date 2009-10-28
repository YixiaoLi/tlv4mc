using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace JSON_Validator
{
    class CUI
    {
        static void Main(string[] args)
        {
            try
            {
                string my_schema = "../../resource/JSON_Schema1.txt";
                string JSON_input = "../../resource/JSON_input1.txt";

                Validator.Result result = Validator.validateFile(my_schema, JSON_input);
                switch (result.type)
                {
                    case Validator.Result.Type.Valid:
                        Console.WriteLine("Your input data is valid\n");
                        break;
                    case Validator.Result.Type.Invalid:
                        Console.WriteLine("Your input data is invalid\n");
                        break;
                    case Validator.Result.Type.IllFormed:
                        Console.WriteLine("Your input data is ill-formed\n");
                        Console.WriteLine(result.message);
                        break;
                    default:
                        Console.Write("Unexpected error was occured\n");
                        break;
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}
