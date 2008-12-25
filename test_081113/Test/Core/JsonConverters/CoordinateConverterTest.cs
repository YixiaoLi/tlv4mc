using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace Test
{
    
    
    /// <summary>
    ///CoordinateConverterTest のテスト クラスです。すべての
    ///CoordinateConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class CoordinateConverterTest
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
            CoordinateConverter target = new CoordinateConverter();
            Type actual;
            actual = target.Type;
            Assert.AreEqual(typeof(Coordinate), actual);
        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteJsonTest()
        {
            Coordinate cord = new Coordinate("1", "2");
            Assert.AreEqual("\"1,2\"", Write(cord) );
            cord = new Coordinate("-1", "foo");
            Assert.AreEqual("\"-1,foo\"", Write(cord));
        }

        private string Write(Coordinate cord)
        {
            CoordinateConverter target = new CoordinateConverter();
            // 似た名前のクラスが多いので、明示的に名前空間を指定している。
            System.IO.StringWriter sw = new System.IO.StringWriter();
            IJsonWriter writer = new NU.OJL.MPRTOS.TLV.Third.JsonWriter(new Newtonsoft.Json.JsonTextWriter(sw));
            target.WriteJson(writer, cord);
            return sw.ToString();
        }

        private Coordinate Read(string s)
        {
            CoordinateConverter target = new CoordinateConverter();
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(
               new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(s)));
            reader.Read();
            return (Coordinate)target.ReadJson(reader);
        } 

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadJsonTest1()
        {
            CoordinateConverter target = new CoordinateConverter();
            Assert.AreEqual(new Coordinate("1","2").ToString() , Read("\"1,2\"").ToString());
        }

        [TestMethod()]
        public void ReadJsonTest2()
        {
            CoordinateConverter target = new CoordinateConverter();
            Assert.AreEqual(new Coordinate("1","foo,bar").ToString(), Read("\"1,foo,bar\"").ToString());
        }

        [TestMethod()]
        public void ReadJsonTest3()
        {
            CoordinateConverter target = new CoordinateConverter();
            Assert.AreEqual(new Coordinate("1,baz", "foo").ToString(), Read("\"1,baz,foo\"").ToString());
        }

        [TestMethod()]
        public void ReadJsonTest4()
        {
            CoordinateConverter target = new CoordinateConverter();
            Assert.AreEqual(new Coordinate("123").ToString(), Read("\"123\"").ToString());
        }
    }
}
