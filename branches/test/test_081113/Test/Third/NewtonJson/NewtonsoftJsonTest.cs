using NU.OJL.MPRTOS.TLV.Third;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Test
{
    
    
    /// <summary>
    ///NewtonsoftJsonTest のテスト クラスです。すべての
    ///NewtonsoftJsonTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class NewtonsoftJsonTest
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
        ///Serialize のテスト
        ///</summary>
        [TestMethod()]
        public void SerializeTest1()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            IJsonWriter writer = null; // TODO: 適切な値に初期化してください
            object obj = null; // TODO: 適切な値に初期化してください
            target.Serialize(writer, obj);
            Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }

        /// <summary>
        ///Serialize のテスト
        ///</summary>
        [TestMethod()]
        public void SerializeTest()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            object obj = null; // TODO: 適切な値に初期化してください
            string expected = string.Empty; // TODO: 適切な値に初期化してください
            string actual;
            actual = target.Serialize(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///HasConverter のテスト
        ///</summary>
        [TestMethod()]
        public void HasConverterTest()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            Type type = null; // TODO: 適切な値に初期化してください
            bool expected = false; // TODO: 適切な値に初期化してください
            bool actual;
            actual = target.HasConverter(type);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///GetConverter のテスト
        ///</summary>
        [TestMethod()]
        public void GetConverterTest()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            Type type = null; // TODO: 適切な値に初期化してください
            IJsonConverter expected = null; // TODO: 適切な値に初期化してください
            IJsonConverter actual;
            actual = target.GetConverter(type);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///Deserialize のテスト
        ///</summary>
        public void DeserializeTest3Helper<T>()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            string json = string.Empty; // TODO: 適切な値に初期化してください
            T expected = default(T); // TODO: 適切な値に初期化してください
            T actual;
            actual = target.Deserialize<T>(json);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        [TestMethod()]
        public void DeserializeTest3()
        {
            DeserializeTest3Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///Deserialize のテスト
        ///</summary>
        public void DeserializeTest2Helper<T>()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            IJsonReader reader = null; // TODO: 適切な値に初期化してください
            T expected = default(T); // TODO: 適切な値に初期化してください
            T actual;
            actual = target.Deserialize<T>(reader);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        [TestMethod()]
        public void DeserializeTest2()
        {
            DeserializeTest2Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///Deserialize のテスト
        ///</summary>
        [TestMethod()]
        public void DeserializeTest1()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            IJsonReader reader = null; // TODO: 適切な値に初期化してください
            Type type = null; // TODO: 適切な値に初期化してください
            object expected = null; // TODO: 適切な値に初期化してください
            object actual;
            actual = target.Deserialize(reader, type);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///Deserialize のテスト
        ///</summary>
        [TestMethod()]
        public void DeserializeTest()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            string json = string.Empty; // TODO: 適切な値に初期化してください
            Type type = null; // TODO: 適切な値に初期化してください
            object expected = null; // TODO: 適切な値に初期化してください
            object actual;
            actual = target.Deserialize(json, type);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///AddConverter のテスト
        ///</summary>
        [TestMethod()]
        public void AddConverterTest()
        {
            NewtonsoftJson target = new NewtonsoftJson(); // TODO: 適切な値に初期化してください
            IJsonConverter converter = null; // TODO: 適切な値に初期化してください
            target.AddConverter(converter);
            Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }

        /// <summary>
        ///NewtonsoftJson コンストラクタ のテスト
        ///</summary>
        [TestMethod()]
        public void NewtonsoftJsonConstructorTest()
        {
            NewtonsoftJson target = new NewtonsoftJson();
            Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
        }
    }
}
