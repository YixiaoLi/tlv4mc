using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
namespace Refrection
{
    class Program
    {

        Program(string[] asms) {
           foreach(var asm in asms)
           {
               printMethods(asm);
           } 
        }

        static void Main(string[] args)
        {
            string[] asms = new string[]  {"NU.OJL.MPRTOS.TLV.Base",
                                         "NU.OJL.MPRTOS.TLV.Core",
                                         "NU.OJL.MPRTOS.TLV.Third"};
            new Program(asms); 
        }

        void printMethods(string s)
        {
            Assembly asm = Assembly.Load(s);

            foreach (var m in from t in asm.GetTypes()
                              from m in t.GetMethods()
                              where m.IsPublic && m.DeclaringType == t
                              select replace(String.Format("\"{0}\",\"{1}\"", t,m)))
            {
                Console.WriteLine(m);
            }
        }

        string replace(string s)
        {
           IDictionary<String,String> dict = new Dictionary<String,String>();
           dict.Add("System.String", "string");
           dict.Add("System.Object", "object");
           dict.Add("Void", "void");
           dict.Add("Boolean", "bool");
           dict.Add("NU.OJL.MPRTOS.", "");
           foreach (KeyValuePair<String,String> pair in dict) {
               s = s.Replace(pair.Key, pair.Value);
           }
           return s;
        }
    }
}
