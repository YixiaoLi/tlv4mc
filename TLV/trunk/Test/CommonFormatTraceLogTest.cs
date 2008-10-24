using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class CommonFormatTraceLogTest
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
        public void SerializeTest()
        {
            string convertDirPath = @"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\rules\convertRules\ITRON4\";
            CommonFormatConverter actual = CommonFormatConverter.GetInstance(convertDirPath);
            CommonFormatTraceLog target = new CommonFormatTraceLog(
                actual.ConvertResourceFile(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRONResources.res"),
                actual.ConvertTraceLogFile(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRONTraceLog.log"));
            string path = Path.GetTempFileName();
            target.Serialize(path);
            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }

        [TestMethod()]
        public void DeserializeTest()
        {
            string convertDirPath = @"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\rules\convertRules\ITRON4\";
            CommonFormatConverter actual = CommonFormatConverter.GetInstance(convertDirPath);
            CommonFormatTraceLog target = new CommonFormatTraceLog(
                actual.ConvertResourceFile(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRONResources.res"),
                actual.ConvertTraceLogFile(@"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRONTraceLog.log"));
            string path = @"C:\Junji\Work\OJL\TLV\フェーズ2\trunk\sampleFiles\SampleITRON.tlv";
            target.Deserialize(path);
            Assert.IsTrue(target.ResourceList.Count != 0);
            Assert.IsTrue(target.TraceLogList.Count != 0);
        }
    }
}
