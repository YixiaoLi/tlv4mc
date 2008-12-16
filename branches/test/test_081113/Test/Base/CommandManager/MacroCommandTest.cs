using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Test
{
    
    
    /// <summary>
    ///MacroCommandTest のテスト クラスです。すべての
    ///MacroCommandTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class MacroCommandTest
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
        /// Textのテスト
        /// </summary>
        [TestMethod()]

        public void TextTest() {
            IEnumerable<ICommand> commands = new List<ICommand>() {
              new GeneralCommand("A", () => { }, () => { }),
              new GeneralCommand("B", () => { }, () => { }),
              new GeneralCommand("C", () => { }, () => { })
            };
            MacroCommand target = new MacroCommand(commands);
            Assert.AreEqual("A から C までの一連の動作", target.Text);
            
            target.Text = "hoge";
            Assert.AreEqual("hoge", target.Text);
        }

        /// <summary>
        ///Do のテスト
        ///</summary>
        [TestMethod()]
        public void DoTest()
        {
            int count=0;
            int a = -1, b = -1, c = -1;
            IEnumerable<ICommand> commands = new List<ICommand>() {
              new GeneralCommand("A", () => { a = count++; }, () => { Assert.Fail(); }),
              new GeneralCommand("B", () => { b = count++; }, () => { Assert.Fail(); }),
              new GeneralCommand("C", () => { c = count++; }, () => { Assert.Fail(); })
            };

            MacroCommand target = new MacroCommand(commands); 
            target.Do();
            Assert.AreEqual(a, 0);
            Assert.AreEqual(b, 1);
            Assert.AreEqual(c, 2);

        }

        /// <summary>
        ///Undo のテスト
        ///</summary>
        [TestMethod()]
        public void UndoTest()
        {
            int count = 0;
            int a = -1, b = -1, c = -1;
            IEnumerable<ICommand> commands = new List<ICommand>() {
              new GeneralCommand("A", () => { Assert.Fail(); } , () => { a = count++; }),
              new GeneralCommand("B", () => { Assert.Fail(); } , () => { b = count++; }),
              new GeneralCommand("C", () => { Assert.Fail(); } , () => { c = count++; })
            };
            MacroCommand target = new MacroCommand(commands);
            target.Undo();

            Assert.AreEqual(a, 2);
            Assert.AreEqual(b, 1);
            Assert.AreEqual(c, 0);
        }


        [TestMethod()]
        public void CanUndoTest()
        {
            IEnumerable<ICommand> commands = new List<ICommand>() {
              new GeneralCommand("A", () => { } , () => { }),
              new GeneralCommand("B", () => { } , () => { }),
              new GeneralCommand("C", () => { } , () => { })
            };
            MacroCommand target = new MacroCommand(commands);

            Assert.IsTrue(target.CanUndo);
            target.CanUndo = false;
            Assert.IsFalse(target.CanUndo);
        }
    }
}
