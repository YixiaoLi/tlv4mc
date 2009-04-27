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
using System.Reflection;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class INamedConverter : GeneralConverter<INamed>
	{
		private Stack<Type> _types = new Stack<Type>();

		public override bool CanConvert(Type type)
		{
			if (base.CanConvert(type))
			{
				_types.Push(type);
				return true;
			}
			else
			{
				return false;
			}
		}

		protected override void WriteJson(IJsonWriter writer, INamed obj)
		{
			if (obj.GetType().IsCollection())
			{
				writer.WriteArray(w =>
					{
						foreach (object o in (IList)obj)
						{
							w.WriteValue(o, ApplicationFactory.JsonSerializer);
						}
					});
			}
			else
			{
				writer.WriteObject(w =>
					{
						foreach (PropertyInfo pi in obj.GetType().GetProperties())
						{
							if (pi.Name != "Name" && pi.GetValue(obj, null) != null)
							{
								w.WriteProperty(pi.Name);
								w.WriteValue(pi.GetValue(obj, null), ApplicationFactory.JsonSerializer);
							}
						}
					});
			}
		}

		public override object ReadJson(IJsonReader reader)
		{
			Type type = _types.Pop();
			object obj;

			if (type.IsCollection())
			{
				List<object> list = new List<object>();
				while (reader.TokenType != JsonTokenType.EndArray)
				{
					Type t = getCollectionType(type).GetGenericArguments()[0];
					list.Add(ApplicationFactory.JsonSerializer.Deserialize(reader, t));
					reader.Read();
				}
				obj = Activator.CreateInstance(type);
				foreach(object o in list)
				{
					((IList)obj).Add(o);
				}
			}
			else
			{
				obj = Activator.CreateInstance(type);
				while (reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string key = (string)reader.Value;

						PropertyInfo pi = type.GetProperties().Single<PropertyInfo>(p => p.Name == key);

						object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

						if (pi.CanWrite)
							pi.SetValue(obj, o, null);
					}
					reader.Read();
				}
			}

			return obj;
		}

		private Type getCollectionType(Type type)
		{
			if (type.IsGenericType && typeof(ICollection).IsAssignableFrom(type))
				return type;
			else
				return getCollectionType(type.BaseType);
		}
	}
}
