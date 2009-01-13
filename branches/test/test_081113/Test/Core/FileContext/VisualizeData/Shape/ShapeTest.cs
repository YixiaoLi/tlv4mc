using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Test
{
    
    
    /// <summary>
    ///ShapeTest のテスト クラスです。すべての
    ///ShapeTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ShapeTest
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
        ///Value のテスト
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            Shape target = new Shape();
            string expected = "foo";
            string actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Type のテスト
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            Shape target = new Shape();
            string expected = "foo";
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Pen のテスト
        ///</summary>
        [TestMethod()]
        public void PenTest()
        {
            Shape target = new Shape();
            NU.OJL.MPRTOS.TLV.Core.Pen expected = new NU.OJL.MPRTOS.TLV.Core.Pen();
            NU.OJL.MPRTOS.TLV.Core.Pen actual;
            target.Pen = expected;
            actual = target.Pen;
            Assert.AreEqual(expected.Color, actual.Color);
            Assert.AreEqual(expected.DashCap, actual.DashCap);
            Assert.AreEqual(expected.DashPattern, actual.DashPattern);
            Assert.AreEqual(expected.DashStyle, actual.DashStyle);
            Assert.AreEqual(expected.EndCap, actual.EndCap);
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.StartCap, actual.StartCap);
        }

        /// <summary>
        ///Offset のテスト
        ///</summary>
        [TestMethod()]
        public void OffsetTest()
        {
            Shape target = new Shape();
            Coordinate expected = new Coordinate("1,2");
            Coordinate actual;
            target.Offset = expected;
            actual = target.Offset;
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        /// <summary>
        ///Fill のテスト
        ///</summary>
        [TestMethod()]
        public void FillTest()
        {
            Shape target = new Shape();
            Color expected = Color.Black;
            Color actual;
            target.Fill = expected;
            actual = target.Fill;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Coordinates のテスト
        ///</summary>
        [TestMethod()]
        public void CoordinatesTest()
        {
            Shape target = new Shape();
            CoordinateList expected = new CoordinateList();
            expected.Add(new Coordinate("1,2"));
            expected.Add(new Coordinate("3,4"));
            CoordinateList actual;
            target.Coordinates = expected;
            actual = target.Coordinates;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Area のテスト
        ///</summary>
        [TestMethod()]
        public void AreaTest()
        {
            Shape target = new Shape();
            Coordinate location = new Coordinate("1,2");
            NU.OJL.MPRTOS.TLV.Core.Size size = new NU.OJL.MPRTOS.TLV.Core.Size("3,4");
            Area expected = new Area(location, size);
            Area actual;
            target.Area = expected;
            actual = target.Area;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Align のテスト
        ///</summary>
        [TestMethod()]
        public void AlignTest()
        {
            Shape target = new Shape();
            ContentAlignment expected = ContentAlignment.TopLeft;
            ContentAlignment actual;
            target.Align = expected;
            actual = target.Align;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Arc のテスト
        ///</summary>
        [TestMethod()]
        public void ArcTest()
        {
            Shape target = new Shape();
            Arc expected = new Arc(1.41f,1.73f);
            Arc actual;
            target.Arc = expected;
            actual = target.Arc;
            Assert.AreEqual(expected, actual);
        }
    }
}
