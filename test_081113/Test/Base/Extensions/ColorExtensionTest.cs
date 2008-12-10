using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Test
{
    
    
    /// <summary>
    ///ColorExtensionTest のテスト クラスです。すべての
    ///ColorExtensionTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ColorExtensionTest
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

        public Color rgb(byte r, byte g, byte b) {
            return Color.FromArgb(r, g, b);
        } 

        /// <summary>
        ///RandomNextColor のテスト
        ///</summary>
        [TestMethod()]
        public void RandomNextColorTest()
        {
            Color color = (new Color()).FromHsv(0,0,50);
            Color actual = ColorExtension.RandomNextColor(color);

            Assert.AreEqual(color.GetBrightness(), actual.GetBrightness());
            Assert.AreEqual(true, color.GetHue() != actual.GetHue() || color.GetSaturation() != actual.GetSaturation());

        }

        /// <summary>
        ///HueRotateNextColor のテスト
        ///</summary>
        [TestMethod()]
        public void HueRotateNextColorTest()
        {
            Color color = new Color(); // TODO: 適切な値に初期化してください
            Color expected = new Color(); // TODO: 適切な値に初期化してください
            Color actual;
            actual = ColorExtension.HueRotateNextColor(color);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        /// <summary>
        ///HueRotateColorReset のテスト
        ///</summary>
        [TestMethod()]
        public void HueRotateColorResetTest()
        {
            Color color = new Color(); // TODO: 適切な値に初期化してください
            ColorExtension.HueRotateColorReset(color);
            Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }

        public void HsvTest(int h,int s,int v) {
            Color hsv = (new Color()).FromHsv(h, s, v);
            Assert.AreEqual(h, hsv.GetHue());
            Assert.AreEqual(s/100.0, hsv.GetSaturation(),0.05);
            Assert.AreEqual(v/100.0, hsv.GetBrightness(),0.05);
 
        }

        /// <summary>
        ///FromHsv のテスト
        ///</summary>
        [TestMethod()]
        public void FromHsvTest()
        {
            // s = 0 
            HsvTest(0, 0, 100);
            HsvTest(0, 0, 50);

            HsvTest(0, 50, 100);
            HsvTest(60, 50, 100);
            HsvTest(120, 50, 100);
            HsvTest(180, 50, 100);
            HsvTest(240, 50, 100);
            HsvTest(300, 50, 100);
/*
            Assert.AreEqual(rgb(255, 255, 255), rgb(0, 0, 0).FromHsv(0, 0, 100));
            Assert.AreEqual(rgb(127, 127, 127), rgb(0, 0, 0).FromHsv(0, 0, 50));

            
            Assert.AreEqual(rgb(255, 127, 127), rgb(0, 0, 0).FromHsv(0, 50, 100));   // H/60 = 0
            Assert.AreEqual(rgb(255, 255, 127), rgb(0, 0, 0).FromHsv(60, 50, 100));  // H/60 = 1
            Assert.AreEqual(rgb(127, 255,127), rgb(0, 0, 0).FromHsv(120, 50, 100));  //  H/60 = 2
            Assert.AreEqual(rgb(127, 255, 255), rgb(0, 0, 0).FromHsv(180, 50, 100)); //  H/60 = 3
            Assert.AreEqual(rgb(127, 127, 255), rgb(0, 0, 0).FromHsv(240, 50, 100)); //  H/60 = 4
            Assert.AreEqual(rgb(255, 127, 255), rgb(0, 0, 0).FromHsv(300, 50, 100)); //  H/60 = 5
            */
        }
    }
}
