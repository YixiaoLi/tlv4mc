using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    
    
    /// <summary>
    ///AreaTest のテスト クラスです。すべての
    ///AreaTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class AreaTest
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
        ///Size のテスト
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            Coordinate location = null;
            Size size = new Size("1,2");
            Area target = new Area(location, size);
            Size expected = new Size("1", "2");
            Size actual;
            target.Size = expected;
            actual = target.Size;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        /// <summary>
        ///Location のテスト
        ///</summary>
        [TestMethod()]
        public void LocationTest()
        {
            Coordinate location = new Coordinate("3,4");
            Size size = null; 
            Area target = new Area(location, size);
            Coordinate expected = new Coordinate("3", "4");
            Coordinate actual;
            target.Location = expected;
            actual = target.Location;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        /// <summary>
        ///ToString のテスト
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Coordinate location = new Coordinate("5,6");
            Size size = new Size("7,8");
            Area target = new Area(location, size);
            string expected = "[" + location.ToString() + "," + size.ToString() + "]";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
