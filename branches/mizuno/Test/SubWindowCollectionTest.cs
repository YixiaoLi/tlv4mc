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
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class SubWindowCollectionTest
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
        public void ItemTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            SubWindow actual = new SubWindow("test", new System.Windows.Forms.Control(), DockState.DockBottom);
            target.Add(actual);
            Assert.AreEqual(actual, target[actual.Name]);
        }

        [TestMethod()]
        public void CountTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.AreEqual(5, target.Count);
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.IsTrue(target.Contains("test2"));
            target.Remove("test2");
            Assert.IsFalse(target.Contains("test2"));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            SubWindow sb = new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom);
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(sb);
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.IsTrue(target.Contains("test2"));
            target.Remove(sb);
            Assert.IsFalse(target.Contains("test2"));
        }

        [TestMethod()]
        public void ContainsTest1()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            SubWindow sb = new SubWindow("test", new System.Windows.Forms.Control(), DockState.DockBottom);
            Assert.IsFalse(target.Contains("test"));
            target.Add(sb);
            Assert.IsTrue(target.Contains("test"));
        }

        [TestMethod()]
        public void ContainsTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            SubWindow sb = new SubWindow("test", new System.Windows.Forms.Control(), DockState.DockBottom);
            Assert.IsFalse(target.Contains(sb));
            target.Add(sb);
            Assert.IsTrue(target.Contains(sb));
        }

        [TestMethod()]
        public void ClearTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.AreEqual(5, target.Count);
            target.Clear();
            Assert.AreEqual(0, target.Count);
        }

        [TestMethod()]
        public void AddTest()
        {
            SubWindowCollection target = new SubWindowCollection(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Add(new SubWindow("test1", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test2", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test3", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test4", new System.Windows.Forms.Control(), DockState.DockBottom));
            target.Add(new SubWindow("test5", new System.Windows.Forms.Control(), DockState.DockBottom));
            Assert.AreEqual(5, target.Count);
            Assert.IsTrue(target.Contains("test1"));
            Assert.IsTrue(target.Contains("test2"));
            Assert.IsTrue(target.Contains("test3"));
            Assert.IsTrue(target.Contains("test4"));
            Assert.IsTrue(target.Contains("test5"));
        }
    }
}
