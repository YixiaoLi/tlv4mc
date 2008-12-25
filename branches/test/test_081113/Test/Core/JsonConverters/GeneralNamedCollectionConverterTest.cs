using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace Test
{
    
    
    /// <summary>
    ///GeneralNamedCollectionConverterTest のテスト クラスです。すべての
    ///GeneralNamedCollectionConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class GeneralNamedCollectionConverterTest
    {

        public class NamedObject : INamed
        {
            public string Name { get; set; }
            public int Foo { get; set; }

           public NamedObject(string name,int foo){
               Name = name;
               Foo = foo;
           } 
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
            var target = new GeneralNamedCollectionConverter<NamedObject, GeneralNamedCollection<NamedObject>>();
            Assert.AreEqual(typeof(GeneralNamedCollection<NamedObject>),
               target.Type); 

        }

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()] 
        public void ReadJsonTest()
        {
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(
               new Newtonsoft.Json.JsonTextReader(
                 new System.IO.StringReader("{\"foo\":{\"Name\":\"foo\",\"Foo\":0},\"bar\":{\"Name\":\"bar\",\"Foo\":1}}")));

            var target = new GeneralNamedCollectionConverter<NamedObject, GeneralNamedCollection<NamedObject>>();
            GeneralNamedCollection<NamedObject> dict = (GeneralNamedCollection<NamedObject>)target.ReadJson(reader);
            NamedObject foo = dict["foo"];
            Assert.AreEqual("foo", foo.Name);
            Assert.AreEqual(0, foo.Foo);

            NamedObject bar = dict["bar"];
            Assert.AreEqual("bar", bar.Name);
            Assert.AreEqual(1, bar.Foo);

        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteJsonTest()
        {
            var target = new GeneralNamedCollectionConverter<NamedObject, GeneralNamedCollection<NamedObject>>();
            var list = new GeneralNamedCollection<NamedObject>();
            list.Add("1", new NamedObject("foo", 0));
            list.Add("2", new NamedObject("bar", 1));

            System.IO.StringWriter sw = new System.IO.StringWriter();
            IJsonWriter writer = new NU.OJL.MPRTOS.TLV.Third.JsonWriter(new Newtonsoft.Json.JsonTextWriter(sw));
            target.WriteJson(writer, list);
            Assert.AreEqual("{\"foo\":{\"Name\":\"foo\",\"Foo\":0},\"bar\":{\"Name\":\"bar\",\"Foo\":1}}", sw.ToString());
        }
    }
}
