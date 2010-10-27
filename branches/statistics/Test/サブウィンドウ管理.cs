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
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Third;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Test
{
    /// <summary>
    /// サブウィンドウ管理ユースケース郡のテストケース
    /// </summary>
    [TestClass]
    public class サブウィンドウ管理
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
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void サブウィンドウの追加()
        {
            WindowManager wm = new WindowManager();
            wm.Parent = new Control();
            wm.MainPanel = new Control();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "サブウィンドウ1" },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "サブウィンドウ2" },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "サブウィンドウ3" },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "サブウィンドウ4", Visible = false },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5" },
            };

            wm.AddSubWindow(sws);

            Assert.AreEqual(5, wm.SubWindowCount);
            Assert.IsTrue(wm.ContainSubWindow("sb1"));
            Assert.IsTrue(wm.ContainSubWindow("sb2"));
            Assert.IsTrue(wm.ContainSubWindow("sb3"));
            Assert.IsTrue(wm.ContainSubWindow("sb4"));
            Assert.IsTrue(wm.ContainSubWindow("sb5"));
            Assert.AreEqual(DockState.DockLeft, wm.GetSubWindow("sb1").DockState);
            Assert.AreEqual(DockState.DockRight, wm.GetSubWindow("sb2").DockState);
            Assert.AreEqual(DockState.DockTop, wm.GetSubWindow("sb3").DockState);
            Assert.AreEqual(DockState.DockBottom, wm.GetSubWindow("sb4").DockState);
            Assert.AreEqual(DockState.DockLeft, wm.GetSubWindow("sb5").DockState);
            Assert.AreEqual("サブウィンドウ1", wm.GetSubWindow("sb1").Text);
            Assert.AreEqual("サブウィンドウ2", wm.GetSubWindow("sb2").Text);
            Assert.AreEqual("サブウィンドウ3", wm.GetSubWindow("sb3").Text);
            Assert.AreEqual("サブウィンドウ4", wm.GetSubWindow("sb4").Text);
            Assert.AreEqual("サブウィンドウ5", wm.GetSubWindow("sb5").Text);
            Assert.IsTrue(wm.GetSubWindow("sb1").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb2").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb3").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb4").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb5").Visible);
        }

        [TestMethod]
        public void サブウィンドウの表示()
        {
            WindowManager wm = new WindowManager();
            wm.Parent = new Control();
            wm.MainPanel = new Control();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "サブウィンドウ1" , Visible = false },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "サブウィンドウ2" , Visible = false },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "サブウィンドウ3" , Visible = false },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "サブウィンドウ4", Visible = false },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5" , Visible = false },
            };

            wm.AddSubWindow(sws);
            tsmi.SetWindowManager(wm);

            Assert.IsFalse(wm.GetSubWindow("sb1").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb2").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb3").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb4").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb5").Visible);

            foreach(ToolStripMenuItem item in tsmi.DropDownItems)
            {
                item.PerformClick();
            }

            Assert.IsTrue(wm.GetSubWindow("sb1").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb2").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb3").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb4").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb5").Visible);

        }

        [TestMethod]
        public void サブウィンドウの非表示()
        {
            WindowManager wm = new WindowManager();
            wm.Parent = new Control();
            wm.MainPanel = new Control();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "サブウィンドウ1" , Visible = true },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "サブウィンドウ2" , Visible = true },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "サブウィンドウ3" , Visible = true },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "サブウィンドウ4", Visible = true },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5" , Visible = true },
            };

            wm.AddSubWindow(sws);
            tsmi.SetWindowManager(wm);

            Assert.IsTrue(wm.GetSubWindow("sb1").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb2").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb3").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb4").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb5").Visible);

            foreach(ToolStripMenuItem item in tsmi.DropDownItems)
            {
                item.PerformClick();
            }

            Assert.IsFalse(wm.GetSubWindow("sb1").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb2").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb3").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb4").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb5").Visible);
        }
    }
}
