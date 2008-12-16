using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class CommandManagerTest
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
        public void UndoTextTest()
        {
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            target.Do(new GeneralCommand("UndoTextTest", () => { }, () => { }));
            Assert.AreEqual("「UndoTextTest」を", target.UndoText);
        }

        [TestMethod()]
        public void RedoTextTest()
        {
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            target.Do(new GeneralCommand("RedoTextTest", () => { }, () => { }));
            target.Undo();
            Assert.AreEqual("「RedoTextTest」を", target.RedoText);
        }

        [TestMethod()]
        public void CanUndoTest()
        {
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            Assert.IsFalse(target.CanUndo);
            target.Do(new GeneralCommand("CanUndoTest", () => { }, () => { }));
            Assert.IsTrue(target.CanUndo);

            target.CanUndo = false;
            Assert.IsFalse(target.CanUndo);
        }

        [TestMethod()]
        public void CanRedoTest()
        {
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            Assert.IsFalse(target.CanRedo);
            target.Do(new GeneralCommand("CanRedoTest", () => { }, () => { }));
            Assert.IsFalse(target.CanRedo);
            target.Undo();
            Assert.IsTrue(target.CanRedo);

            target.CanRedo = false;
            Assert.IsFalse(target.CanRedo);
        }

        [TestMethod()]
        public void UndoTest()
        {
            string str = "";
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            target.Do(new GeneralCommand("CanRedoTest", () => { str = "CanRedoTest"; }, () => { str = ""; }));
            Assert.AreEqual("CanRedoTest", str);
            target.Undo();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void RedoTest()
        {
            string str = "";
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            target.Do(new GeneralCommand("CanRedoTest", () => { str = "CanRedoTest"; }, () => { str = ""; }));
            Assert.AreEqual("CanRedoTest", str);
            target.Undo();
            Assert.AreEqual("", str);
            target.Redo();
            Assert.AreEqual("CanRedoTest", str);
        }

        [TestMethod()]
        public void DoneTest()
        {
            string str = "";
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            target.Done(new GeneralCommand("CanRedoTest", () => { str = "CanRedoTest"; }, () => { str = ""; }));
            Assert.AreEqual("", str);
            target.Undo();
            Assert.AreEqual("", str);
            target.Redo();
            Assert.AreEqual("CanRedoTest", str);
        }

        [TestMethod()]
        public void DoTest()
        {
            string str = "";
            CommandManager target = new CommandManager(); // TODO: 適切な値に初期化してください
            target.Do(new GeneralCommand("CanRedoTest", () => { str = "CanRedoTest"; }, () => { str = ""; }));
            Assert.AreEqual("CanRedoTest", str);
            target.Undo();
            Assert.AreEqual("", str);
            target.Redo();
            Assert.AreEqual("CanRedoTest", str);
        }

    }
}
