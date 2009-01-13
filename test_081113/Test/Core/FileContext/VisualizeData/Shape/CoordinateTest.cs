using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    
    
    /// <summary>
    ///CoordinateTest のテスト クラスです。すべての
    ///CoordinateTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class CoordinateTest
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
        ///Y のテスト
        ///</summary>
        [TestMethod()]
        public void YTest()
        {
            Coordinate target = new Coordinate("1", "2");
            string expected = "3";
            string actual;
            target.Y = expected;
            actual = target.Y;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///X のテスト
        ///</summary>
        [TestMethod()]
        public void XTest()
        {
            Coordinate target = new Coordinate("4","5");
            string expected = "6";
            string actual;
            target.X = expected;
            actual = target.X;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ToString のテスト
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Coordinate target = new Coordinate("7,8");
            string expected = "7,8";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
