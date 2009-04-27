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
using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
namespace NU.OJL.MPRTOS.TLV.Test
{


    [TestClass()]
    public class FileContextTest
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


        public void SaveAsTestHelper<T>(string path)
            where T : class, IFileContextData, new()
        {
            FileContext<T> target = new FileContext<T>();
            target.SaveAs(path);
        }

        [TestMethod()]
        public void SaveAsTest()
        {
            try
            {
                SaveAsTestHelper<TraceLogVisualizerData>(string.Empty);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
            try
            {
                SaveAsTestHelper<TraceLogVisualizerData>(Path.GetTempFileName());
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
        }

        public void SaveTestHelper<T>()
            where T : class, IFileContextData, new()
        {
            FileContext<T> target = new FileContext<T>();
            target.Save();
        }

        [TestMethod()]
        public void SaveTest()
        {
            try
            {
                SaveAsTestHelper<TraceLogVisualizerData>(string.Empty);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
            try
            {
                SaveAsTestHelper<TraceLogVisualizerData>(Path.GetTempFileName());
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(FilePathUndefinedException));
            }
        }

        [TestMethod()]
        public void CloseTest()
        {
            FileContext<TraceLogVisualizerData> target = new FileContext<TraceLogVisualizerData>(); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
            target.Close();
            Assert.AreEqual(target.IsOpened, false);
            Assert.AreEqual(target.IsSaved, false);
            Assert.AreEqual(target.Data, null);
            Assert.AreEqual(target.Path, string.Empty);
        }
    }
}
