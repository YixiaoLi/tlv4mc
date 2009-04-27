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
using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace Test
{
    
    
    /// <summary>
    ///LogDataEnumeableTest �Υƥ��� ���饹�Ǥ������٤Ƥ�
    ///LogDataEnumeableTest ñ�Υƥ��Ȥ򤳤��˴ޤ�ޤ�
    ///</summary>
	[TestClass()]
	public class LogDataEnumeableTest
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


		/// <summary>
		///op_Addition �Υƥ���
		///</summary>
		[TestMethod()]
		public void op_AdditionTest()
		{
			LogDataEnumeable left = new LogDataEnumeable(
				new LogData[]{
					new LogData(new Time("100",10), new Resource(){ Name = "a"}){ Id = 1},
					new LogData(new Time("200",10), new Resource(){ Name = "b"}){ Id = 3},
					new LogData(new Time("300",10), new Resource(){ Name = "c"}){ Id = 5},
					new LogData(new Time("400",10), new Resource(){ Name = "d"}){ Id = 7},
					new LogData(new Time("500",10), new Resource(){ Name = "e"}){ Id = 9},
					new LogData(new Time("600",10), new Resource(){ Name = "f"}){ Id = 11}
				}); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
			LogDataEnumeable right = new LogDataEnumeable(
				new LogData[]{
					new LogData(new Time("100",10), new Resource(){ Name = "a"}){ Id = 1},
					new LogData(new Time("200",10), new Resource(){ Name = "B"}){ Id = 4},
					new LogData(new Time("300",10), new Resource(){ Name = "c"}){ Id = 5},
					new LogData(new Time("450",10), new Resource(){ Name = "D"}){ Id = 8},
					new LogData(new Time("500",10), new Resource(){ Name = "e"}){ Id = 9},
					new LogData(new Time("650",10), new Resource(){ Name = "F"}){ Id = 12}
				}); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
			LogDataEnumeable expected = new LogDataEnumeable(
				new LogData[]{
					new LogData(new Time("100",10), new Resource(){ Name = "a"}){ Id = 1},
					new LogData(new Time("200",10), new Resource(){ Name = "b"}){ Id = 3},
					new LogData(new Time("200",10), new Resource(){ Name = "B"}){ Id = 4},
					new LogData(new Time("300",10), new Resource(){ Name = "c"}){ Id = 5},
					new LogData(new Time("400",10), new Resource(){ Name = "d"}){ Id = 7},
					new LogData(new Time("450",10), new Resource(){ Name = "D"}){ Id = 8},
					new LogData(new Time("500",10), new Resource(){ Name = "e"}){ Id = 9},
					new LogData(new Time("600",10), new Resource(){ Name = "f"}){ Id = 11},
					new LogData(new Time("650",10), new Resource(){ Name = "F"}){ Id = 12}
				}); // TODO: Ŭ�ڤ��ͤ˽�������Ƥ�������
			LogDataEnumeable actual;
			actual = (left + right);

			for (int i = 0; i < expected.List.Count; i++ )
			{
				Assert.AreEqual(expected.List[i].Id, actual.List[i].Id);
			}
		}


	}
}
