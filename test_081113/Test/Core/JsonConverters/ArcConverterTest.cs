using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace Test
{
   
    /// <summary>
    ///ArcConverterTest のテスト クラスです。すべての
    ///ArcConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ArcConverterTest
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
            ArcConverter target = new ArcConverter();
            Assert.AreEqual(typeof(Arc), target.Type); 
        }

        private Arc Read(string s)
        {
            ArcConverter target = new ArcConverter();
            System.IO.StringReader sr = new System.IO.StringReader(s);
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(
               new Newtonsoft.Json.JsonTextReader(sr));
            return (Arc)target.ReadJson(reader);
        } 

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadJsonTest()
        {
            Assert.AreEqual(new Arc(0, 1).ToString(), Read("[0,1]").ToString());
            Assert.AreEqual(new Arc(0, 1).ToString(), Read("[0, 1]").ToString());
            Assert.AreEqual(new Arc(0, -1).ToString(), Read("[0, -1]").ToString());
        }

        private string Write(Arc arc) {
            ArcConverter target = new ArcConverter();

            // 似た名前のクラスが多いので、明示的に名前空間を指定している。
            System.IO.StringWriter sw = new System.IO.StringWriter();
            IJsonWriter writer = new NU.OJL.MPRTOS.TLV.Third.JsonWriter(new Newtonsoft.Json.JsonTextWriter(sw));
            target.WriteJson(writer, arc);
            return sw.ToString(); 
        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteJsonTest()
        {
            Assert.AreEqual("[\"0\",\"0\"]", Write(new Arc(0, 0)));
            Assert.AreEqual("[\"0.1\",\"4.2\"]", Write(new Arc(0.1f, 4.2f)));
            Assert.AreEqual("[\"0.1\",\"-4.2\"]", Write(new Arc(0.1f, -4.2f)));
        }
    }
}
