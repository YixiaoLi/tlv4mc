using NU.OJL.MPRTOS.TLV.Third;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;

namespace Test
{
    
    
    /// <summary>
    ///EnumConverterTest のテスト クラスです。すべての
    ///EnumConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class EnumConverterTest
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
        ///CanConvert のテスト
        ///</summary>
        [TestMethod()]
        public void CanConvertTest()
        {
            EnumConverter target = new EnumConverter();
            Type objectType = typeof(NU.OJL.MPRTOS.TLV.Core.AllocationType);
            bool expected = true;
            bool actual;
            actual = target.CanConvert(objectType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadJsonTest()
        {
            EnumConverter target = new EnumConverter();
            Newtonsoft.Json.JsonReader reader = new Newtonsoft.Json.JsonTextReader(new StringReader("{ \"NU.OJL.MPRTOS.TLV.Core.AllocationType\" : \"Dynamic\" }"));
            System.Diagnostics.Debug.WriteLine("hoge");
            Type objectType = typeof(NU.OJL.MPRTOS.TLV.Core.AllocationType); 
            object expected = NU.OJL.MPRTOS.TLV.Core.AllocationType.Dynamic;
            object actual;
            actual = target.ReadJson(reader, objectType);
            Assert.AreEqual(expected, actual);
        }

    }
}
