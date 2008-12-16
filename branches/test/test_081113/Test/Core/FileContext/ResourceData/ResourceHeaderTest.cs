using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace NU.OJL.MPRTOS.TLV.Test
{
    
    
    /// <summary>
    ///ResourceHeaderTest のテスト クラスです。すべての
    ///ResourceHeaderTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ResourceHeaderTest
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
        ///ToJson のテスト
        ///</summary>
        [TestMethod()]
        public void ToJsonTest()
        {
            ResourceHeader target = new ResourceHeader(); // TODO: 適切な値に初期化してください
            string expected = string.Empty; // TODO: 適切な値に初期化してください
            string actual;
            actual = target.ToJson();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///Parse のテスト
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            ResourceHeader target = new ResourceHeader(); // TODO: 適切な値に初期化してください
            string data = string.Empty; // TODO: 適切な値に初期化してください
            ResourceHeader expected = null; // TODO: 適切な値に初期化してください
            ResourceHeader actual;
            actual = target.Parse(data);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }
    }
}
