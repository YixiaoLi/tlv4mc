using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class GeneralCommandTest
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
        public void UndoActionTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: 適切な値に初期化してください
            Action _do = () => { str = "done"; }; // TODO: 適切な値に初期化してください
            Action undo = () => {str = "";}; // TODO: 適切な値に初期化してください
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: 適切な値に初期化してください
            Action expected = undo; // TODO: 適切な値に初期化してください
            Action actual;
            target.UndoAction = expected;
            actual = target.UndoAction;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TextTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: 適切な値に初期化してください
            Action _do = () => { str = "done"; }; // TODO: 適切な値に初期化してください
            Action undo = () => { str = ""; }; // TODO: 適切な値に初期化してください
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: 適切な値に初期化してください
            string expected = text; // TODO: 適切な値に初期化してください
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DoActionTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: 適切な値に初期化してください
            Action _do = () => { str = "done"; }; // TODO: 適切な値に初期化してください
            Action undo = () => { str = ""; }; // TODO: 適切な値に初期化してください
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: 適切な値に初期化してください
            Action expected = _do; // TODO: 適切な値に初期化してください
            Action actual;
            target.DoAction = expected;
            actual = target.DoAction;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UndoTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: 適切な値に初期化してください
            Action _do = () => { str = "done"; }; // TODO: 適切な値に初期化してください
            Action undo = () => { str = ""; }; // TODO: 適切な値に初期化してください
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: 適切な値に初期化してください

            Assert.AreEqual("", str);
            target.Do();
            Assert.AreEqual("done", str);
            target.Undo();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void DoTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: 適切な値に初期化してください
            Action _do = () => { str = "done"; }; // TODO: 適切な値に初期化してください
            Action undo = () => { str = ""; }; // TODO: 適切な値に初期化してください
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: 適切な値に初期化してください

            Assert.AreEqual("", str);
            target.Do();
            Assert.AreEqual("done", str);
            target.Undo();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void GeneralCommandConstructorTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: 適切な値に初期化してください
            Action _do = () => { str = "done"; }; // TODO: 適切な値に初期化してください
            Action undo = () => { str = ""; }; // TODO: 適切な値に初期化してください
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: 適切な値に初期化してください
            Assert.AreEqual(target.Text, text);
        }
    }
}
