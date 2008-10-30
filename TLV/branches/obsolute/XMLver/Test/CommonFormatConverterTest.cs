using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class CommonFormatConverterTest
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


        [TestMethod()]
        public void GetInstanceTest()
        {
            string convertDirPath = @"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\rules\convertRules\ITRON4\";
            CommonFormatConverter actual = CommonFormatConverter.GetInstance(convertDirPath);
            Assert.AreEqual("ITRON4", actual.Name);
            Assert.AreEqual("ITRON4.0", actual.Description);
            Assert.AreEqual(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\rules\convertRules\ITRON4\", actual.Path);
            Assert.AreEqual(File.ReadAllText(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\TLV\bin\Debug\rules\convertRules\ITRON4\ITRON4.xsd"), actual.ResourceXsd);
            Assert.AreEqual(File.ReadAllText(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\TLV\bin\Debug\rules\convertRules\ITRON4\ITRON4.xslt"), actual.ResourceXslt);
            Assert.AreEqual(File.ReadAllText(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\TLV\bin\Debug\rules\convertRules\ITRON4\ITRON4.lcnv"), actual.TraceLogConvertRule);
        }

        [TestMethod()]
        public void ConvertTraceLogFileTest()
        {
            string convertDirPath = @"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\rules\convertRules\ITRON4\";
            CommonFormatConverter actual = CommonFormatConverter.GetInstance(convertDirPath);
            Assert.AreEqual(File.ReadAllText(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRON\SampleITRON.log"), actual.ConvertTraceLogFile(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRONTraceLog.log"));
        }

        [TestMethod()]
        public void ConvertResourceFileTest()
        {
            // TODO とても面倒なので飛ばす
        }
    }
}
