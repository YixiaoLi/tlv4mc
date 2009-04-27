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
using NU.OJL.MPRTOS.TLV.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class JsonReader : IJsonReader
	{
		private Newtonsoft.Json.JsonReader _reader;

		public Newtonsoft.Json.JsonReader Reader { get { return _reader; } }

		public JsonReader(Newtonsoft.Json.JsonReader reader)
		{
			_reader = reader;
		}

		public bool Read()
		{
			return _reader.Read();
		}

		public  NU.OJL.MPRTOS.TLV.Base.JsonTokenType TokenType
		{
			get
			{
				switch (_reader.TokenType)
				{
					case JsonToken.StartObject:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartObject;
					case JsonToken.StartArray:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartArray;
					case JsonToken.EndObject:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndObject;
					case JsonToken.EndArray:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndArray;
					case JsonToken.StartConstructor:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartConstructor;
					case JsonToken.EndConstructor:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndConstructor;
					case JsonToken.PropertyName:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.PropertyName;
					case JsonToken.Integer:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Integer;
					case JsonToken.Float:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Float;
					case JsonToken.String:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.String;
					case JsonToken.Boolean:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Boolean;
					case JsonToken.Null:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Null;
					case JsonToken.Comment:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Comment;
					case JsonToken.Date:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Date;
					case JsonToken.None:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.None;
					case JsonToken.Raw:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Raw;
					case JsonToken.Undefined:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Undefined;
					default:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Undefined;
				}
			}
		}

		public object Value
		{
			get { return _reader.Value; }
		}

		public void Skip()
		{
			_reader.Skip();
		}

	}
}
