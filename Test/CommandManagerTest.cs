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
        public void UndoTextTest()
        {
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Do(new GeneralCommand("UndoTextTest", () => { }, () => { }));
            Assert.AreEqual("��UndoTextTest�פ�", target.UndoText);
        }

        [TestMethod()]
        public void RedoTextTest()
        {
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Do(new GeneralCommand("RedoTextTest", () => { }, () => { }));
            target.Undo();
            Assert.AreEqual("��RedoTextTest�פ�", target.RedoText);
        }

        [TestMethod()]
        public void CanUndoTest()
        {
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Assert.IsFalse(target.CanUndo);
            target.Do(new GeneralCommand("CanUndoTest", () => { }, () => { }));
            Assert.IsTrue(target.CanUndo);
        }

        [TestMethod()]
        public void CanRedoTest()
        {
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
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
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Do(new GeneralCommand("CanRedoTest", () => { str = "CanRedoTest"; }, () => { str = ""; }));
            Assert.AreEqual("CanRedoTest", str);
            target.Undo();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void RedoTest()
        {
            string str = "";
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
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
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
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
            CommandManager target = new CommandManager(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Do(new GeneralCommand("CanRedoTest", () => { str = "CanRedoTest"; }, () => { str = ""; }));
            Assert.AreEqual("CanRedoTest", str);
            target.Undo();
            Assert.AreEqual("", str);
            target.Redo();
            Assert.AreEqual("CanRedoTest", str);
        }

    }
}
