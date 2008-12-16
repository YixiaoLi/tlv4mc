using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    
    
    /// <summary>
    ///ComparisonExpressionTest のテスト クラスです。すべての
    ///ComparisonExpressionTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ComparisonExpressionTest
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
        /// テスト補助
        ///</summary>
        public void Eval3<T>(string left, string ope, string right, bool expected)
            where T : IComparable, IConvertible
        {
            bool actual;
            actual = ComparisonExpression.Result<T>(left, ope, right);
            Assert.AreEqual(expected, actual);
        }
      

        [TestMethod()]
        public void ResultTest1()
        {
           // intの場合
            Eval3<int>("1","==", "1", true);
            Eval3<int>("1", "==", "2", false);

            Eval3<int>("1", "!=", "2", true);
            Eval3<int>("1", "!=", "1", false);

            Eval3<int>("1", "<=", "1", true);
            Eval3<int>("1", "<=", "2", true);
            Eval3<int>("3", "<=", "2", false);

            Eval3<int>("2", ">=", "1", true);
            Eval3<int>("1", ">=", "1", true);
            Eval3<int>("1", ">=", "2", false);

            Eval3<int>("1", "<", "2", true);
            Eval3<int>("2", "<", "2", false);
            Eval3<int>("3", "<", "2", false);

            Eval3<int>("2", ">", "1", true);
            Eval3<int>("2", ">", "2", false);
            Eval3<int>("1", ">", "2", false);

           // stringの場合
            Eval3<int>("100", "<", "2", false);
            Eval3<string>("abc", "==", "abc", true);
            Eval3<string>("abc", "==", "ABC", false);
            Eval3<string>("abc", "==", "", false);
            Eval3<string>("", "==", "abc", false);
            Eval3<string>("abc", "==", null, false);
            Eval3<string>(null, "==", "abc", false);
            Eval3<string>("", "==", "", true);

            Eval3<string>("abc", "!=", "abce", true);
            Eval3<string>("abc", "!=", "abc", false);

            Eval3<string>("abc", "<=", "ABC", true);
            Eval3<string>("abc", "<=", "abc", true);
            Eval3<string>("ABC", "<=", "abc", false);

            Eval3<string>("ABC", ">=", "abc", true);
            Eval3<string>("abc", ">=", "abc", true);
            Eval3<string>("abc", ">=", "ABC", false);

            Eval3<string>("abc", "<", "ABC", true);
            Eval3<string>("abc", "<", "abc", false);
            Eval3<string>("abc", "<", "abc", false);

            Eval3<string>("ABC", ">", "abc", true);
            Eval3<string>("abc", ">", "abc", false);
            Eval3<string>("abc", ">", "ABC", false);
        }

       /// <summary>
       /// 正しくキャッシュが行えるかのテスト
       /// </summary>
        [TestMethod]
        public void CacheTest() {
            // 型が違う場合は、同じ式でも違う式を返す(see ticket #8)
            Eval3<int>("100", "<", "2", false);
            Eval3<string>("100", "<", "2", true);
        }

        /// <summary>
        /// テスト補助
        ///</summary>
        public void Eval1<T>(string expr, bool expected)
            where T : IComparable, IConvertible
        {
            bool actual;
            actual = ComparisonExpression.Result<T>(expr);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ResultTest()
        {
            // intの場合
            Eval1<int>("1 == 1", true);
            Eval1<int>("1 == 2", false);

            Eval1<int>("1 != 2", true);
            Eval1<int>("1 != 1", false);

            Eval1<int>("1 <= 1", true);
            Eval1<int>("1 <= 2", true);
            Eval1<int>("3 <= 2", false);

            Eval1<int>("2 >= 1", true);
            Eval1<int>("1 >= 1", true);
            Eval1<int>("1 >= 2", false);

            Eval1<int>("1 < 2", true);
            Eval1<int>("2 < 2", false);
            Eval1<int>("3 < 2", false);

            Eval1<int>("2 > 1", true);
            Eval1<int>("2 > 2", false);
            Eval1<int>("1 > 2", false);

            // stringの場合
            Eval1<int>("100 < 2", false);
            Eval1<string>("abc == abc", true);
            Eval1<string>("abc == ABC", false);
            Eval1<string>("abc == ", false);
            Eval1<string>(" == abc", false);
            Eval1<string>(" == ", true);

            Eval1<string>("abc != abce", true);
            Eval1<string>("abc != abc", false);

            Eval1<string>("abc <= ABC", true);
            Eval1<string>("abc <= abc", true);
            Eval1<string>("ABC <= abc", false);

            Eval1<string>("ABC >= abc", true);
            Eval1<string>("abc >= abc", true);
            Eval1<string>("abc >= ABC", false);

            Eval1<string>("abc < ABC", true);
            Eval1<string>("abc < abc", false);
            Eval1<string>("abc < abc", false);

            Eval1<string>("ABC > abc", true);
            Eval1<string>("abc > abc", false);
            Eval1<string>("abc > ABC", false);

            //  ill patter
            Eval1<string>("abc == abc", true);
            Eval1<string>("[abc] == [abc]", true); // see ticket #9
        }
    }
}
