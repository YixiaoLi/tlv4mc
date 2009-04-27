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

namespace Test
{
    
    
    /// <summary>
    ///DecimalExtensionTest �Υƥ��� ���饹�Ǥ������٤Ƥ�
    ///DecimalExtensionTest ñ�Υƥ��Ȥ򤳤��˴ޤ�ޤ�
    ///</summary>
	[TestClass()]
	public class DecimalExtensionTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///���ߤΥƥ��Ȥμ¹ԤˤĤ��Ƥξ��󤪤�ӵ�ǽ��
		///�󶡤���ƥ��� ����ƥ����Ȥ�����ޤ������ꤷ�ޤ���
		///</summary>
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

		private void test(string s, decimal v, int r)
		{
			Assert.AreEqual(v.ToString(r), s);
		}

		/// <summary>
		///ToString �Υƥ���
		///</summary>
		[TestMethod()]
		public void ToStringTest()
		{
			test("1", 1m, 10);
			test("10", 10m, 10);
			test("0.1", 0.1m, 10);
			test("0.001", 0.001m, 10);
			test("a", 10m, 16);
			test("b", 11m, 16);
			test("af", 175m, 16);
			test("64", 100m, 16);
			test("144", 100m, 8);
			test("10201", 100m, 3);
			test("1100100", 100m, 2);
			test("a.4", 10.25m, 16);
			test("12.2", 10.25m, 8);
			test("1010.01", 10.25m, 2);
			test("0.5", 0.625m, 8);
			test("0.a", 0.625m, 16);
			test("20ca819", 23424324m, 15);
			test("3f", 123m, 36);
			test("10", 36m, 36);
			test("g", 16m, 36);
			string s = (0.1m).ToString(16);
		}
	}
}
