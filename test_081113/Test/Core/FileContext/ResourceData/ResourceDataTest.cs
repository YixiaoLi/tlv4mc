using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
namespace NU.OJL.MPRTOS.TLV.Test
{
    
    
    /// <summary>
    ///ResourceDataTest のテスト クラスです。すべての
    ///ResourceDataTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ResourceDataTest
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
            ResourceData target = new ResourceData(); 
            string expected = "{\r\n  \"TimeScale\": \"\",\r\n  \"TimeRadix\": 10,\r\n  \"ConvertRule\": \"\"\r\n}";
            string actual;
            actual = target.ToJson();
            //System.Diagnostics.Debug.WriteLine(expected);
            //System.Diagnostics.Debug.WriteLine(expected.Length);
            //System.Diagnostics.Debug.WriteLine(actual.Length);
            //System.Diagnostics.Debug.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Parse のテスト
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            ResourceData target = new ResourceData();
            string resourceData = "{\r\n  \"TimeScale\": \"20\",\r\n  \"TimeRadix\": 10,\r\n  \"ConvertRule\": \"foo\"\r\n}";
            ResourceData expected = target; 
            ResourceData actual;
            actual = target.Parse(resourceData);
            System.Diagnostics.Debug.WriteLine(actual.TimeScale);
            System.Diagnostics.Debug.WriteLine(actual.TimeRadix);
            System.Diagnostics.Debug.WriteLine(actual.ConvertRule);
            Assert.AreEqual("20", actual.TimeScale);
            Assert.AreEqual(10, actual.TimeRadix);
            Assert.AreEqual("foo", actual.ConvertRule);
        }
    }
}
