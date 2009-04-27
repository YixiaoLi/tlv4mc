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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ������֥�˴ؤ�����̾���ϰʲ���°�����åȤ�Ȥ��������椵��ޤ���
// ������֥�˴�Ϣ�դ����Ƥ��������ѹ�����ˤϡ�
// ������°���ͤ��ѹ����Ƥ���������
[assembly: AssemblyTitle("TraceLogVisualizer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Embedded and Real-Time Systems Laboratory, Graduate School of Information Science, Nagoya Univ., JAPAN")]
[assembly: AssemblyProduct("TraceLogVisualizer")]
[assembly: AssemblyCopyright("Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible �� false �����ꤹ��ȡ����η��Ϥ��Υ�����֥���� COM ����ݡ��ͥ�Ȥ��� 
// �����Բ�ǽ�ˤʤ�ޤ���COM ���餳�Υ�����֥���η��˥�������������ϡ�
// ���η��� ComVisible °���� true �����ꤷ�Ƥ���������
[assembly: ComVisible(false)]

// ���� GUID �ϡ����Υץ������Ȥ� COM �˸����������Ρ�typelib �� ID �Ǥ�
[assembly: Guid("848f9c7f-9f48-4993-ab7f-4dd714ac7be6")]

// ������֥�ΥС���������ϡ��ʲ��� 4 �Ĥ��ͤǹ�������Ƥ��ޤ�:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// ���٤Ƥ��ͤ���ꤹ�뤫�����Τ褦�� '*' ��Ȥäƥӥ�ɤ���ӥ�ӥ�����ֹ�� 
// �����ͤˤ��뤳�Ȥ��Ǥ��ޤ�:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
