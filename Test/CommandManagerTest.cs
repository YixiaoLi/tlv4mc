/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
