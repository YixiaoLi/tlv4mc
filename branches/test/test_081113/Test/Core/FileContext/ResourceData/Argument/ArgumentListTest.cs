using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
namespace NU.OJL.MPRTOS.TLV.Test
{
    
    
    /// <summary>
    ///ArgumentListTest のテスト クラスです。すべての
    ///ArgumentListTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ArgumentListTest
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
        ///ToString のテスト
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            ArgumentList target = new ArgumentList(); 
            Json hoge = new Json();
            Json fuga = new Json();
            hoge.Value = "hoge";
            fuga.Value = "fuga";
            target.Add(hoge);
            target.Add(fuga);

            string expected = "hoge, fuga"; 
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
