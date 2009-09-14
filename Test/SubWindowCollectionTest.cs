
using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class SubWindowCollectionTest
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
        public void ItemTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            SubWindow actual = new SubWindow("test", new System.Windows.Forms.Control(), DockState.DockBottom);
            target.Add(actual);
            Assert.AreEqual(actual, target[actual.Name]);
        }

        [TestMethod()]
        public void CountTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.AreEqual(5, target.Count);
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.IsTrue(target.Contains("test2"));
            target.Remove("test2");
            Assert.IsFalse(target.Contains("test2"));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            SubWindow sb = new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom);
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(sb);
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.IsTrue(target.Contains("test2"));
            target.Remove(sb);
            Assert.IsFalse(target.Contains("test2"));
        }

        [TestMethod()]
        public void ContainsTest1()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            SubWindow sb = new SubWindow("test", new System.Windows.Forms.Control(), DockState.DockBottom);
            Assert.IsFalse(target.Contains("test"));
            target.Add(sb);
            Assert.IsTrue(target.Contains("test"));
        }

        [TestMethod()]
        public void ContainsTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            SubWindow sb = new SubWindow("test", new System.Windows.Forms.Control(), DockState.DockBottom);
            Assert.IsFalse(target.Contains(sb));
            target.Add(sb);
            Assert.IsTrue(target.Contains(sb));
        }

        [TestMethod()]
        public void ClearTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.AreEqual(5, target.Count);
            target.Clear();
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod()]
        public void AddTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: 適切な値に初期化してください
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.AreEqual(5, target.Count);
            Assert.IsTrue(target.Contains("test1"));
            Assert.IsTrue(target.Contains("test2"));
            Assert.IsTrue(target.Contains("test3"));
            Assert.IsTrue(target.Contains("test4"));
            Assert.IsTrue(target.Contains("test5"));
        }
    }
}
