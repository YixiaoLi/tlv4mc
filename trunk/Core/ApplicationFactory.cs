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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Third;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationFactory
	{
		private static readonly ApplicationBlackBoard _blackBoard = new ApplicationBlackBoard();
		private static readonly StatusManager _statusManager = new StatusManager();
		private static readonly CommandManager _commandManager = new CommandManager();
		private static readonly IZip _zip = new SharpZipLibZip();
		private static readonly IJsonSerializer _json = new NewtonsoftJson();
		private static readonly RotateColorFactory _colorFactory = new RotateColorFactory();
        public static WindowManager WindowManager
        {
            get { return new WeifenLuoWindowManager(); }
        }

        /// <summary>
        /// <c>IZip</c>�Υ��󥹥��󥹡�����ϥ��󥰥�ȥ�ʥ��󥹥��󥹤Ǥ��롣
        /// </summary>
        public static IZip Zip
        {
            get { return _zip; }
        }

		/// <summary>
		/// <c>IJson</c>�Υ��󥹥��󥹡�����ϥ��󥰥�ȥ�ʥ��󥹥��󥹤Ǥ��롣
		/// </summary>
		public static IJsonSerializer JsonSerializer
		{
			get { return _json; }
		}

        /// <summary>
        /// <c>CommandManager</c>�Υ��󥹥��󥹡�����ϥ��󥰥�ȥ�ʥ��󥹥��󥹤Ǥ��롣
        /// </summary>
        public static CommandManager CommandManager
        {
            get { return _commandManager; }
		}

		/// <summary>
		/// <c>ApplicationBlackBoard</c>�Υ��󥹥��󥹡�����ϥ��󥰥�ȥ�ʥ��󥹥��󥹤Ǥ��롣
		/// </summary>
		public static ApplicationBlackBoard BlackBoard
		{
			get { return _blackBoard; }
		}

		public static StatusManager StatusManager
		{
			get { return _statusManager; }
		}

		public static RotateColorFactory ColorFactory
		{
			get { return _colorFactory; }
		}

		static ApplicationFactory()
		{
			JsonSerializer.AddConverter(new TraceLogConverter());
			JsonSerializer.AddConverter(new ArcConverter());
			JsonSerializer.AddConverter(new AreaConverter());
			JsonSerializer.AddConverter(new ColorConverter());
			JsonSerializer.AddConverter(new PointConverter());
			JsonSerializer.AddConverter(new ResourceHeaderConverter());
			JsonSerializer.AddConverter(new SizeConverter());
			JsonSerializer.AddConverter(new JsonConverter());
			JsonSerializer.AddConverter(new FontFamilyConverter());
			JsonSerializer.AddConverter(new ArgumentTypeConverter());
			JsonSerializer.AddConverter(new ShapesConverter());
			JsonSerializer.AddConverter(new FiguresConverter());
			JsonSerializer.AddConverter(new ShapeConverter());
			JsonSerializer.AddConverter(new TimeConverter());
			JsonSerializer.AddConverter(new TimeLineConverter());

			JsonSerializer.AddConverter(new ClassHavingNullablePropertyConverter());
			JsonSerializer.AddConverter(new INamedConverter());
			JsonSerializer.AddConverter(new GeneralNamedCollectionConverter());
			//JsonSerializer.AddConverter(new ObservableNamedCollectionConverter());
		}
    }
}
