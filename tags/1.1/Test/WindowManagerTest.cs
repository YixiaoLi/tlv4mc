/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
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
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Test
{

    [TestClass()]
    public class WindowManagerTest
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
        public void SubWindowsTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft),
                new SubWindow("sb2", new Control(), DockState.DockRight),
                new SubWindow("sb3", new Control(), DockState.DockTop),
                new SubWindow("sb4", new Control(), DockState.DockBottom),
                new SubWindow("sb5", new Control(), DockState.DockLeft),
            };

            target.AddSubWindow(sws);

            IEnumerable<SubWindow> actual;
            actual = target.SubWindows;

            List<SubWindow> list = new List<SubWindow>(sws);

            foreach(SubWindow sw in actual)
            {
                Assert.IsTrue(list.Contains(sw));
            }
        }

        [TestMethod()]
        public void SubWindowCountTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft),
                new SubWindow("sb2", new Control(), DockState.DockRight),
                new SubWindow("sb3", new Control(), DockState.DockTop),
                new SubWindow("sb4", new Control(), DockState.DockBottom),
                new SubWindow("sb5", new Control(), DockState.DockLeft),
            };

            int expected = sws.Length;

            target.AddSubWindow(sws);

            Assert.AreEqual(expected, target.SubWindowCount);
        }

        [TestMethod()]
        public void ParentTest()
        {
            WindowManager target = new WindowManager();
            Control expected = new Control() { Name = "test"};
            Control actual;
            target.Parent = expected;
            actual = target.Parent;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MainPanelTest()
        {
            WindowManager target = new WindowManager();
            Control expected = new Control();
            Control actual;
            target.MainPanel = expected;
            actual = target.MainPanel;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ShowSubWindowTest()
        {
            WindowManager target = new WindowManager();

            target.AddSubWindow(new SubWindow("sb1", new Control(), DockState.DockLeft) { Visible = false });

            Assert.IsFalse(target.GetSubWindow("sb1").Visible);

            target.ShowSubWindow("sb1");

            Assert.IsTrue(target.GetSubWindow("sb1").Visible);
        }

        [TestMethod()]
        public void ShowAllSubWindowsTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "サブウィンドウ1" , Visible = false },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "サブウィンドウ2" , Visible = false },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "サブウィンドウ3" , Visible = false },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "サブウィンドウ4", Visible = false },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5" , Visible = false },
            };

            target.AddSubWindow(sws);

            foreach (SubWindow sb in target.SubWindows)
            {
                Assert.IsFalse(sb.Visible);
            }

            target.ShowAllSubWindows();

            foreach(SubWindow sb in target.SubWindows)
            {
                Assert.IsTrue(sb.Visible);
            }
        }

        [TestMethod()]
        public void HideSubWindowTest()
        {
            WindowManager target = new WindowManager();

            target.AddSubWindow(new SubWindow("sb1", new Control(), DockState.DockLeft) { Visible = true });

            string name = "sb1";

            Assert.IsTrue(target.GetSubWindow("sb1").Visible);

            target.HideSubWindow(name);

            Assert.IsFalse(target.GetSubWindow("sb1").Visible);
        }

        [TestMethod()]
        public void HideAllSubWindowsTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "サブウィンドウ1" , Visible = true },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "サブウィンドウ2" , Visible = true },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "サブウィンドウ3" , Visible = true },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "サブウィンドウ4", Visible = true },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "サブウィンドウ5" , Visible = true },
            };

            target.AddSubWindow(sws);

            foreach (SubWindow sb in target.SubWindows)
            {
                Assert.IsTrue(sb.Visible);
            }

            target.HideAllSubWindows();

            foreach (SubWindow sb in target.SubWindows)
            {
                Assert.IsFalse(sb.Visible);
            }
        }

        [TestMethod()]
        public void GetSubWindowTest()
        {
            WindowManager target = new WindowManager();

            SubWindow sb1 = new SubWindow("sb1", new Control(), DockState.DockLeft) { Visible = true };

            target.AddSubWindow(sb1);

            Assert.AreEqual(sb1, target.GetSubWindow("sb1"));
        }

        [TestMethod()]
        public void ContainSubWindowTest()
        {
            WindowManager target = new WindowManager();

            SubWindow sb1 = new SubWindow("sb1", new Control(), DockState.DockLeft) { Visible = true };

            target.AddSubWindow(sb1);

            Assert.IsTrue(target.ContainSubWindow("sb1"));
        }

        [TestMethod()]
        public void AutoHideSubWindowTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft),
                new SubWindow("sb2", new Control(), DockState.DockRight),
                new SubWindow("sb3", new Control(), DockState.DockTop),
                new SubWindow("sb4", new Control(), DockState.DockBottom),
            };

            target.AddSubWindow(sws);

            Assert.AreEqual(DockState.DockLeft, target.GetSubWindow("sb1").DockState);
            Assert.AreEqual(DockState.DockRight, target.GetSubWindow("sb2").DockState);
            Assert.AreEqual(DockState.DockTop, target.GetSubWindow("sb3").DockState);
            Assert.AreEqual(DockState.DockBottom, target.GetSubWindow("sb4").DockState);

            target.AutoHideSubWindow("sb1");
            target.AutoHideSubWindow("sb2");
            target.AutoHideSubWindow("sb3");
            target.AutoHideSubWindow("sb4");

            Assert.AreEqual(DockState.DockLeftAutoHide, target.GetSubWindow("sb1").DockState);
            Assert.AreEqual(DockState.DockRightAutoHide, target.GetSubWindow("sb2").DockState);
            Assert.AreEqual(DockState.DockTopAutoHide, target.GetSubWindow("sb3").DockState);
            Assert.AreEqual(DockState.DockBottomAutoHide, target.GetSubWindow("sb4").DockState);
        }

        [TestMethod()]
        public void AutoHideAllSubWindowsTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft),
                new SubWindow("sb2", new Control(), DockState.DockRight),
                new SubWindow("sb3", new Control(), DockState.DockTop),
                new SubWindow("sb4", new Control(), DockState.DockBottom),
            };

            target.AddSubWindow(sws);

            Assert.AreEqual(DockState.DockLeft, target.GetSubWindow("sb1").DockState);
            Assert.AreEqual(DockState.DockRight, target.GetSubWindow("sb2").DockState);
            Assert.AreEqual(DockState.DockTop, target.GetSubWindow("sb3").DockState);
            Assert.AreEqual(DockState.DockBottom, target.GetSubWindow("sb4").DockState);

            target.AutoHideAllSubWindows();

            Assert.AreEqual(DockState.DockLeftAutoHide, target.GetSubWindow("sb1").DockState);
            Assert.AreEqual(DockState.DockRightAutoHide, target.GetSubWindow("sb2").DockState);
            Assert.AreEqual(DockState.DockTopAutoHide, target.GetSubWindow("sb3").DockState);
            Assert.AreEqual(DockState.DockBottomAutoHide, target.GetSubWindow("sb4").DockState);
        }

        [TestMethod()]
        public void AddSubWindowTest()
        {
            WindowManager target = new WindowManager();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft),
                new SubWindow("sb2", new Control(), DockState.DockRight),
                new SubWindow("sb3", new Control(), DockState.DockTop),
                new SubWindow("sb4", new Control(), DockState.DockBottom),
            };

            target.AddSubWindow(sws);

            Assert.AreEqual(sws.Length, target.SubWindowCount);
            Assert.IsTrue(target.ContainSubWindow("sb1"));
            Assert.IsTrue(target.ContainSubWindow("sb2"));
            Assert.IsTrue(target.ContainSubWindow("sb3"));
            Assert.IsTrue(target.ContainSubWindow("sb4"));
        }
    }
}
