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

namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class GeneralCommandTest
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
        public void UndoActionTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action _do = () => { str = "done"; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action undo = () => {str = "";}; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action expected = undo; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action actual;
            target.UndoAction = expected;
            actual = target.UndoAction;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TextTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action _do = () => { str = "done"; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action undo = () => { str = ""; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            string expected = text; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DoActionTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action _do = () => { str = "done"; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action undo = () => { str = ""; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action expected = _do; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action actual;
            target.DoAction = expected;
            actual = target.DoAction;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void UndoTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action _do = () => { str = "done"; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action undo = () => { str = ""; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������

            Assert.AreEqual("", str);
            target.Do();
            Assert.AreEqual("done", str);
            target.Undo();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void DoTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action _do = () => { str = "done"; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action undo = () => { str = ""; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������

            Assert.AreEqual("", str);
            target.Do();
            Assert.AreEqual("done", str);
            target.Undo();
            Assert.AreEqual("", str);
        }

        [TestMethod()]
        public void GeneralCommandConstructorTest()
        {
            string str = "";
            string text = "UndoActionTest"; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action _do = () => { str = "done"; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Action undo = () => { str = ""; }; // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            GeneralCommand target = new GeneralCommand(text, _do, undo); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            Assert.AreEqual(target.Text, text);
        }
    }
}
