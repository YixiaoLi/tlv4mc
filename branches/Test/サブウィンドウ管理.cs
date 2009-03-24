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
