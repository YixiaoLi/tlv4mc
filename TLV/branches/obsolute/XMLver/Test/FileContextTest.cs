using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class FileContextTest
    {


        private TestContext testContextInstance;

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


        public void SaveAsTestHelper<T>(string path)
            where T : class, IFileContextData, new()
        {
            FileContext<T> target = new FileContext<T>();
            target.SaveAs(path);
        }

        [TestMethod()]
        public void SaveAsTest()
        {
            try
            {
                SaveAsTestHelper<CommonFormatTraceLog>(string.Empty);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
            try
            {
                SaveAsTestHelper<CommonFormatTraceLog>(Path.GetTempFileName());
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
        }

        public void SaveTestHelper<T>()
            where T : class, IFileContextData, new()
        {
            FileContext<T> target = new FileContext<T>();
            target.Save();
        }

        [TestMethod()]
        public void SaveTest()
        {
            try
            {
                SaveAsTestHelper<CommonFormatTraceLog>(string.Empty);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
            try
            {
                SaveAsTestHelper<CommonFormatTraceLog>(Path.GetTempFileName());
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
        }

        [TestMethod()]
        public void CloseTest()
        {
            FileContext<CommonFormatTraceLog> target = new FileContext<CommonFormatTraceLog>(); // TODO: 適切な値に初期化してください
            target.Close();
            Assert.AreEqual(target.IsOpened, false);
            Assert.AreEqual(target.IsSaved, false);
            Assert.AreEqual(target.Data, null);
            Assert.AreEqual(target.Path, string.Empty);
        }
    }
}
