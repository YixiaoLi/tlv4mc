using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    
    
    /// <summary>
    ///ConditionExpressionTest のテスト クラスです。すべての
    ///ConditionExpressionTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ConditionExpressionTest
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
        ///Result のテスト
        ///</summary>
        [TestMethod()]
        public void SimpleTest()
        {
            Assert.AreEqual(true, ConditionExpression.Result("1 < 3"));
            Assert.AreEqual(false, ConditionExpression.Result("1 > 3"));
        }

        [TestMethod()]
        public void AndTest()
        {
            // true && true = true 
            // true && false = false
            // false && true = false
            // false && false = false 
            Assert.AreEqual(true , ConditionExpression.Result("1 < 3 && 1 < 3"));
            Assert.AreEqual(false, ConditionExpression.Result("1 < 3 && 3 < 1"));
            Assert.AreEqual(false, ConditionExpression.Result("3 < 1 && 1 < 3"));
            Assert.AreEqual(false, ConditionExpression.Result("3 < 1 && 3 < 1"));
        }

        [TestMethod()]
        public void OrTest()
        {

            // true || false = true
            // true || false = true 
            // faslse || true = true
            // false || false = true 
            Assert.AreEqual(true, ConditionExpression.Result("1 < 3 || 1 < 3"));
            Assert.AreEqual(true , ConditionExpression.Result("1 < 3 || 3 < 1"));
            Assert.AreEqual(true , ConditionExpression.Result("3 < 1 || 1 < 3"));
            Assert.AreEqual(false, ConditionExpression.Result("3 < 1 || 3 < 1"));
        }

        [TestMethod()]
        public void NotTest() {
            Assert.AreEqual(false, ConditionExpression.Result("! true"));
            Assert.AreEqual(true, ConditionExpression.Result("! false"));
            Assert.AreEqual(false, ConditionExpression.Result("! (1 < 3)"));
            Assert.AreEqual(true, ConditionExpression.Result("! (3 < 1)"));
        }

        [TestMethod()]
        public void LiteralTest() {
            Assert.AreEqual(true, ConditionExpression.Result("true"));
            Assert.AreEqual(false, ConditionExpression.Result("false"));
            Assert.AreEqual(true, ConditionExpression.Result("true == true"));
            Assert.AreEqual(false, ConditionExpression.Result("true == false"));

            Assert.AreEqual(true, ConditionExpression.Result("true && true"));
            Assert.AreEqual(false, ConditionExpression.Result("true && false"));
            Assert.AreEqual(false, ConditionExpression.Result("false && true"));
            Assert.AreEqual(false, ConditionExpression.Result("false && false"));
        }

        [TestMethod()]
        public void ParenTest() {
            Assert.AreEqual(true, ConditionExpression.Result("(1 < 3) == true"));
        }
        
    }
}
