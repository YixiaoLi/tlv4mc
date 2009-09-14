
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
