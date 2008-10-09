using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class SubWindowTest
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
        public void VisibleTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow target = new SubWindow(name, control, dockState); // TODO: 適切な値に初期化してください
            bool expected = true; // TODO: 適切な値に初期化してください
            bool actual;
            target.Visible = expected;
            actual = target.Visible;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TextTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow target = new SubWindow(name, control, dockState); // TODO: 適切な値に初期化してください
            string expected = "text test"; // TODO: 適切な値に初期化してください
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void NameTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow target = new SubWindow(name, control, dockState); // TODO: 適切な値に初期化してください
            string expected = "name test"; // TODO: 適切な値に初期化してください
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DockStateTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow target = new SubWindow(name, control, dockState); // TODO: 適切な値に初期化してください
            DockState expected = DockState.DockBottom; // TODO: 適切な値に初期化してください
            DockState actual;
            target.DockState = expected;
            actual = target.DockState;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DeploymentItem("NU.OJL.MPRTOS.TLV.Base.dll")]
        public void ControlTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow_Accessor target = new SubWindow_Accessor(name, control, dockState); // TODO: 適切な値に初期化してください
            Control expected = new Control("control test"); // TODO: 適切な値に初期化してください
            Control actual;
            target.Control = expected;
            actual = target.Control;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SubWindowConstructorTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow target = new SubWindow(name, control, dockState); // TODO: 適切な値に初期化してください
            Assert.AreEqual(target.Name, name);
            Assert.AreEqual(target.Control, control);
            Assert.AreEqual(target.DockState, dockState);
        }
    }
}
