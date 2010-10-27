/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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

        [TestMethod()]
        public void EnabledTest()
        {
            string name = "test"; // TODO: 適切な値に初期化してください
            Control control = new Control(); // TODO: 適切な値に初期化してください
            DockState dockState = DockState.DockLeft; // TODO: 適切な値に初期化してください
            SubWindow target = new SubWindow(name, control, dockState); // TODO: 適切な値に初期化してください
            target.Enabled = true;
            Assert.AreEqual(true, target.Enabled);
            Assert.AreEqual(true, target.Visible);
            target.Enabled = false;
            Assert.AreEqual(false, target.Enabled);
            Assert.AreEqual(false, target.Visible);
            target.Enabled = true;
            Assert.AreEqual(true, target.Enabled);
            Assert.AreEqual(false, target.Visible);
        }
    }
}
