using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;
using NU.OJL.MPRTOS.TLV.Third;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace Test.Core.JsonConverters
{


    /// <summary>
    ///JsonConverterTest のテスト クラスです。すべての
    ///JsonConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class JsonConverterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Type のテスト
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            JsonConverter target = new JsonConverter(); // TODO: 適切な値に初期化してください
            Assert.AreEqual(typeof(Json), target.Type);
        }


        private string Write(object obj)
        {
            JsonConverter target = new JsonConverter();

            // 似た名前のクラスが多いので、明示的に名前空間を指定している。
            System.IO.StringWriter sw = new System.IO.StringWriter();
            IJsonWriter writer = new NU.OJL.MPRTOS.TLV.Third.JsonWriter(new Newtonsoft.Json.JsonTextWriter(sw));
            target.WriteJson(writer, obj);
            return sw.ToString();
        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteLiteralJsonTest()
        {
            Assert.AreEqual("42", Write(new Json(42)));
            Assert.AreEqual("-42", Write(new Json(-42)));
            Assert.AreEqual("\"So Long, and Thanks for All the Fish\"",
                            Write(new Json("So Long, and Thanks for All the Fish")));
            Assert.AreEqual("\"c\"", Write(new Json('c')));
            Assert.AreEqual("42.1", Write(new Json(42.1)));
        }


        [TestMethod()]
        public void WriteArrayJsonTest()
        {
            List<Json> simple = new List<Json>{
            new Json(1),
            new Json(2),
            new Json(3)
          };

            Assert.AreEqual("[1,2,3]", Write(new Json(simple)));

            List<Json> nest = new List<Json>{
            new Json(simple),
            new Json(2),
            new Json(3)
          };
            Assert.AreEqual("[[1,2,3],2,3]", Write(new Json(nest)));
        }


        [TestMethod()]
        public void WriteDictionaryJsonTest()
        {
            Dictionary<string, Json> simple = new Dictionary<string, Json>()
            {
              {"foo", new Json(1)},
              {"bar", new Json(2)},
              {"baz", new Json(3)},
            };
            Assert.AreEqual("{\"foo\":1,\"bar\":2,\"baz\":3}", Write(new Json(simple)));

            Dictionary<string, Json> nest = new Dictionary<string, Json>()
            {
              {"foo", new Json(simple) },
              {"bar", new Json(2)},
              {"baz", new Json(3)},
            };
            Assert.AreEqual("{\"foo\":{\"foo\":1,\"bar\":2,\"baz\":3},\"bar\":2,\"baz\":3}", Write(new Json(nest)));
        }

        private Json Read(string s)
        {
            JsonConverter target = new JsonConverter();
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(
              new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(s)));
            reader.Read();
            return (Json)target.ReadJson(reader);
        }

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadIntJsonTest()
        {
            Assert.AreEqual(42L, Read("42").Value);
            Assert.AreEqual(-42L, Read("-42").Value);
            Assert.AreEqual(-42.0, Read("-42.0").Value);
        }

        [TestMethod()]
        public void ReadStringJsonTest()
        {
            Assert.AreEqual("1", Read("\"1\"").Value);
            Assert.AreEqual("foo bar", Read("\"foo bar\"").Value);
            Assert.AreEqual("\"bar\"", Read("\"\\\"bar\\\"\"").Value);
        }

        [TestMethod()]
        public void ReadArrayJsonTest()
        {
            Json simple = Read("[1,2,3]");
            Assert.AreEqual(3, simple.Count);
            Assert.AreEqual(1L, simple[0].Value);
            Assert.AreEqual(2L, simple[1].Value);
            Assert.AreEqual(3L, simple[2].Value);

            Json nest = Read("[[1,2,3],2,3]");
            Json first = (Json)nest[0];
            Assert.AreEqual(3, nest.Count);

            Assert.AreEqual(3, first.Count);
            Assert.AreEqual(1L, first[0].Value);
            Assert.AreEqual(2L, first[1].Value);
            Assert.AreEqual(3L, first[2].Value);

            Assert.AreEqual(2L, nest[1].Value);
            Assert.AreEqual(3L, nest[2].Value);

            Json comma = Read("[1,2,3,]");
            Assert.AreEqual(3, comma.Count);
            Assert.AreEqual(1L, comma[0].Value);
            Assert.AreEqual(2L, comma[1].Value);
            Assert.AreEqual(3L, comma[2].Value);
        }

        [TestMethod()]
        public void ReadDictJsonTest()
        {
            Json simple = Read("{foo:1, bar:2, baz:3}");
            Assert.AreEqual(1L, simple["foo"].Value);
            Assert.AreEqual(2L, simple["bar"].Value);
            Assert.AreEqual(3L, simple["baz"].Value);

            Assert.AreEqual(1L, Read("{\"foo\":1}")["foo"].Value);
            Assert.AreEqual(":D", Read("{\"foo\":\":D\"}")["foo"].Value);

            Json nest = Read("{foo:{hoge:1, fuga:2}, bar:2, baz:3}");
            Assert.AreEqual(1L, nest["foo"]["hoge"].Value);
            Assert.AreEqual(2L, nest["foo"]["fuga"].Value);
            Assert.AreEqual(2L, nest["bar"].Value);
            Assert.AreEqual(3L, nest["baz"].Value);
        }


        private void InvalidInput(string s) {
            try
            {
                IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(new Newtonsoft.Json.JsonTextReader(
                  new System.IO.StringReader(s)));
                JsonConverter target = new JsonConverter();
                target.ReadJson(reader);
            }
            catch
            {
                return;
            }
            Assert.Fail("not failed: " + s);

        }
        [TestMethod()]
        public void ReadJsonFail()
        {
            Json comma = Read("[1,2,3,,,]");
            InvalidInput("[[42]");
            InvalidInput("]");
            InvalidInput("[");
            InvalidInput("{");
            InvalidInput("{{");

            InvalidInput("[[]");
            InvalidInput("{{}"); 
        }
    }
}
