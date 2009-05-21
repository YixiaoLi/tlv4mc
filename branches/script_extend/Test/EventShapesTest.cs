using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    
    
    /// <summary>
    ///EventShapesTest のテスト クラスです。すべての
    ///EventShapesTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class EventShapesTest
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
        public void EmptyToJsonTest()
        {
            EventShapes target = new EventShapes();
            string expected = "{}";
            string actual;
            actual = target.ToJson();
            Assert.AreEqual(expected, actual);
        }


        private Event makeEvent(string rule, string name) {
            Event evnt = new Event();
            evnt.SetVisualizeRuleName("sample");
            evnt.Name = "a";
            return evnt; 
        }

        private Shape makeShape(ShapeType t) {
            Shape shape = new Shape();
            shape.Type = t;
            return shape; 
        }

        [TestMethod()]
        public void ToJsonTest()
        {
            EventShape a = new EventShape(new Time("0", 10),
                new Time("10", 10),
               Shape.Default,
                makeEvent("sample", "a"));

            EventShapes target = new EventShapes();
            target.Add(a);

            string expected =   "{\r\n  \"sample:a\": [\r\n    {\r\n      \"From\": \"0(10)\",\r\n      \"To\": \"10(10)\",\r\n      \"Shape\": {\r\n        \"Alpha\": 255,\r\n        \"Area\": [\r\n          \"0,0\",\r\n          \"100%,100%\"\r\n        ],\r\n        \"Arc\": [\r\n          0,\r\n          90\r\n        ],\r\n        \"Fill\": \"ffffffff\",\r\n        \"Font\": {\r\n          \"Family\": \"Microsoft Sans Serif\",\r\n          \"Style\": \"Regular\",\r\n          \"Color\": \"ff000000\",\r\n          \"Size\": 8,\r\n          \"Align\": \"MiddleCenter\"\r\n        },\r\n        \"Location\": \"0,0\",\r\n        \"Offset\": \"0,0\",\r\n        \"Pen\": {\r\n          \"Color\": \"ff000000\",\r\n          \"Alpha\": 255,\r\n          \"Width\": 1,\r\n          \"DashStyle\": \"Solid\",\r\n          \"DashPattern\": [\r\n            1,\r\n            1\r\n          ],\r\n          \"DashCap\": \"Flat\"\r\n        },\r\n        \"Points\": [\r\n          \"0,0\",\r\n          \"100%,0\",\r\n          \"100%,100%\",\r\n          \"0,100%\"\r\n        ],\r\n        \"Size\": \"100%,100%\",\r\n        \"Text\": \"\",\r\n        \"Type\": \"Undefined\"\r\n      },\r\n      \"Event\": {\r\n        \"DisplayName\": \"a\"\r\n      }\r\n    }\r\n  ]\r\n}";
            string actual;
            actual = target.ToJson();
           Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseTest()
        {
            EventShape a = new EventShape(new Time("0", 10),
                new Time("10", 10),
                Shape.Default,
                makeEvent("sample", "a"));

            EventShapes target = new EventShapes();
            target.Add(a);

            string json = target.ToJson();
            EventShapes actual = target.Parse(json);
            Assert.AreEqual(1, actual.List.Count);
            Assert.AreEqual(true, actual.List.ContainsKey("sample:a"));
            Assert.AreEqual(1,actual.List["sample:a"].Count);
           // todo: and more 
        } 
    }
}
