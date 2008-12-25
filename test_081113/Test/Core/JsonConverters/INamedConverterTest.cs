using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace Test
{

    /// <summary>
    ///INamedConverterTest のテスト クラスです。すべての
    ///INamedConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class INamedConverterTest
    {

        public class NamedObject : INamed
        {
            public string Name { get; set; }
            public int Foo { get; set; }
            public string Bar { get; set; }
            public double Baz { get; set; }
        }
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
            INamedConverter<NamedObject> target = new INamedConverter<NamedObject>();
            Assert.AreEqual(typeof(NamedObject), target.Type);
        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteJsonTest()
        {
            INamedConverter<NamedObject> target = new INamedConverter<NamedObject>();
            NamedObject obj = new NamedObject();
            obj.Name = "NamedObject";
            obj.Foo = 42;
            obj.Bar = "hogehoge";
            obj.Baz = 3.14;

            System.IO.StringWriter sw = new System.IO.StringWriter();
            IJsonWriter writer = new NU.OJL.MPRTOS.TLV.Third.JsonWriter(new Newtonsoft.Json.JsonTextWriter(sw));
            target.WriteJson(writer, obj);
            Assert.AreEqual("{\"Foo\":42,\"Bar\":\"hogehoge\",\"Baz\":3.14}", sw.ToString());
        }

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadJsonTest()
        {
            // 最初のFooが"Foo"でないのは意図的
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(new Newtonsoft.Json.JsonTextReader(
              new System.IO.StringReader("{Foo:42,\"Bar\":\"hogehoge\",\"Baz\":3.14}")));
            reader.Read();
            INamedConverter<NamedObject> target = new INamedConverter<NamedObject>();

            NamedObject actual = (NamedObject)target.ReadJson(reader);
            Assert.AreEqual(42, actual.Foo);
            Assert.AreEqual("hogehoge", actual.Bar);
            Assert.AreEqual(3.14, actual.Baz);

        }

      [TestMethod()]
        public void ReadJsonFail(){
          try{
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(new Newtonsoft.Json.JsonTextReader(
              new System.IO.StringReader("{\"foo\":42,\"bar\":\"hogehoge\",\"baz\":3.14}")));
            reader.Read();
            INamedConverter<NamedObject> target = new INamedConverter<NamedObject>();
            target.ReadJson(reader);
          }catch{
              return;
          }
          Assert.Fail("not failed");

        }
    }
}
