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
    /// ���֥�����ɥ������桼�����������Υƥ��ȥ�����
    /// </summary>
    [TestClass]
    public class ���֥�����ɥ�����
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
        // �ƥ��Ȥ��������ݤˤϡ������ɲ�°������ѤǤ��ޤ�:
        //
        // ���饹��Ǻǽ�Υƥ��Ȥ�¹Ԥ������ˡ�ClassInitialize ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ���饹��Υƥ��Ȥ򤹤٤Ƽ¹Ԥ����顢ClassCleanup ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // �ƥƥ��Ȥ�¹Ԥ������ˡ�TestInitialize ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // �ƥƥ��Ȥ�¹Ԥ�����ˡ�TestCleanup ����Ѥ��ƥ����ɤ�¹Ԥ��Ƥ�������
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ���֥�����ɥ����ɲ�()
        {
            WindowManager wm = new WindowManager();
            wm.Parent = new Control();
            wm.MainPanel = new Control();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�1" },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "���֥�����ɥ�2" },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "���֥�����ɥ�3" },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "���֥�����ɥ�4", Visible = false },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�5" },
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
            Assert.AreEqual("���֥�����ɥ�1", wm.GetSubWindow("sb1").Text);
            Assert.AreEqual("���֥�����ɥ�2", wm.GetSubWindow("sb2").Text);
            Assert.AreEqual("���֥�����ɥ�3", wm.GetSubWindow("sb3").Text);
            Assert.AreEqual("���֥�����ɥ�4", wm.GetSubWindow("sb4").Text);
            Assert.AreEqual("���֥�����ɥ�5", wm.GetSubWindow("sb5").Text);
            Assert.IsTrue(wm.GetSubWindow("sb1").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb2").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb3").Visible);
            Assert.IsFalse(wm.GetSubWindow("sb4").Visible);
            Assert.IsTrue(wm.GetSubWindow("sb5").Visible);
        }

        [TestMethod]
        public void ���֥�����ɥ���ɽ��()
        {
            WindowManager wm = new WindowManager();
            wm.Parent = new Control();
            wm.MainPanel = new Control();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�1" , Visible = false },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "���֥�����ɥ�2" , Visible = false },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "���֥�����ɥ�3" , Visible = false },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "���֥�����ɥ�4", Visible = false },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�5" , Visible = false },
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
        public void ���֥�����ɥ�����ɽ��()
        {
            WindowManager wm = new WindowManager();
            wm.Parent = new Control();
            wm.MainPanel = new Control();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();

            SubWindow[] sws = new[]
            {
                new SubWindow("sb1", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�1" , Visible = true },
                new SubWindow("sb2", new Control(), DockState.DockRight) { Text = "���֥�����ɥ�2" , Visible = true },
                new SubWindow("sb3", new Control(), DockState.DockTop) { Text = "���֥�����ɥ�3" , Visible = true },
                new SubWindow("sb4", new Control(), DockState.DockBottom) { Text = "���֥�����ɥ�4", Visible = true },
                new SubWindow("sb5", new Control(), DockState.DockLeft) { Text = "���֥�����ɥ�5" , Visible = true },
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
