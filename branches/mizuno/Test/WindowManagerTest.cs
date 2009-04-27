/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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

        #region �ɲäΥƥ���°��
        // 
        //�ƥ��Ȥ��������Ȥ��ˡ������ɲ�°������Ѥ��뤳�Ȥ��Ǥ��ޤ�:
        //
        //���饹�κǽ�Υƥ��Ȥ�¹Ԥ������˥����ɤ�¹Ԥ���ˤϡ�ClassInitialize �����
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //���饹�Τ��٤ƤΥƥ��Ȥ�¹Ԥ�����˥����ɤ�¹Ԥ���ˤϡ�ClassCleanup �����
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //�ƥƥ��Ȥ�¹Ԥ������˥����ɤ�¹Ԥ���ˤϡ�TestInitialize �����
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //�ƥƥ��Ȥ�¹Ԥ�����˥����ɤ�¹Ԥ���ˤϡ�TestCleanup �����
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
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�1" , Visible = false },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "���֥�����ɥ�2" , Visible = false },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "���֥�����ɥ�3" , Visible = false },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "���֥�����ɥ�4", Visible = false },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�5" , Visible = false },
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
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�1" , Visible = true },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "���֥�����ɥ�2" , Visible = true },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "���֥�����ɥ�3" , Visible = true },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "���֥�����ɥ�4", Visible = true },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�5" , Visible = true },
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
