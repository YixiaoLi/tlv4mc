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
            Type actual;
            actual = target.Type;
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteJsonTest()
        {
            JsonConverter target = new JsonConverter(); // TODO: 適切な値に初期化してください
            IJsonWriter writer = null; // TODO: 適切な値に初期化してください
            object obj = null; // TODO: 適切な値に初期化してください
            target.WriteJson(writer, obj);
            Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadJsonTest()
        {
            JsonConverter target = new JsonConverter();
            
            //プロジェクトルートからの相対パスで書きたいけど書き方がわからない
            string jsonFilename = "D:\\mydoc\\OJL\\tlv\\branches\\test\\test_081113\\Test\\Core\\JsonConverters\\yanagi.json";
            StreamReader strReader = new System.IO.StreamReader(jsonFilename);
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(
                new Newtonsoft.Json.JsonTextReader(strReader) );
            
            Json expected = new Json();
            //expected.Value
            Json actual;
            actual = (Json)target.ReadJson(reader);

            System.Diagnostics.Debug.WriteLine(actual.GetType());
            System.Diagnostics.Debug.WriteLine(actual.Value.GetType());
            System.Diagnostics.Debug.WriteLine(actual.Value is List<Json>);
            System.Diagnostics.Debug.WriteLine(actual.Value is Dictionary<string, Json>);
            System.Console.WriteLine(actual.ToString());

            
            


            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///JsonConverter コンストラクタ のテスト
        ///</summary>
        [TestMethod()]
        public void JsonConverterConstructorTest()
        {
            JsonConverter target = new JsonConverter();
            Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
        }
    }
}
