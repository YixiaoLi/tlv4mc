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

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class JsonConverter : GeneralConverter<Json>
	{
		public override object ReadJson(IJsonReader reader)
		{
			Stack<Json> stack = new Stack<Json>();
			Stack<string> keyStack = new Stack<string>();
			Json result = null;
			int objectNest = 0;
			int arrayNest = 0;

			Action<object> jsonValueSet = (object value) =>
			{
				if (result != null)
				{
					if (result.Value is List<Json>)
					{
						((List<Json>)result.Value).Add(new Json(value));
					}
					else if (result.Value is Dictionary<string, Json>)
					{
						((Dictionary<string, Json>)result.Value).Add(keyStack.Pop(), new Json(value));
					}
				}
				else
				{
					result = new Json(value);
				}
			};

			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (result != null)
					stack.Push(result);
				result = new Json(new Dictionary<string, Json>());
				objectNest++;
				keyStack.Push(reader.Value as string);
			}

			do
			{
				switch (reader.TokenType)
				{
					case JsonTokenType.StartArray:
						if (result != null)
							stack.Push(result);
						result = new Json(new List<Json>());
						arrayNest++;
						break;
					case JsonTokenType.StartObject:
						if (result != null)
							stack.Push(result);
						result = new Json(new Dictionary<string, Json>());
						objectNest++;
						break;
					case JsonTokenType.PropertyName:
						keyStack.Push(reader.Value as string);
						break;
					case JsonTokenType.EndArray:
						if (stack.Count != 0)
						{
							Json a = result;
							result = stack.Pop();
							jsonValueSet(a.Value);
						}
						arrayNest--;
						break;
					case JsonTokenType.EndObject:
						if (stack.Count != 0)
						{
							Json o = result;
							result = stack.Pop();
							jsonValueSet(o.Value);
						}
						objectNest--;
						break;
					default:
						jsonValueSet(reader.Value);
						break;
				}
			} while (!(objectNest == 0 && arrayNest == 0) && reader.Read());

			if (objectNest != 0)
				throw new Exception("Json�ε��Ҥ˸�꤬����ޤ���\n{��}���б������Ƥ��ޤ���");

			if (arrayNest != 0)
				throw new Exception("Json�ε��Ҥ˸�꤬����ޤ���\n[��]���б������Ƥ��ޤ���");

			return result;
		}

		protected override void WriteJson(IJsonWriter writer, Json json)
		{
			if (json.Value is Json)
			{
				WriteJson(writer, (Json)json.Value);
			}
			else if (json.Value is List<Json>)
			{
				writer.WriteArray(w =>
					{
						foreach (Json j in json)
						{
							WriteJson(w, j);
						}
					});
			}
			else if (json.Value is Dictionary<string, Json>)
			{
				writer.WriteObject(w =>
				{
					foreach (KeyValuePair<string, Json> sj in json.GetKeyValuePairEnumerator())
					{
						w.WriteProperty( sj.Key);
						WriteJson(w, sj.Value);
					}
				});
			}
			else
			{
				writer.WriteValue(json.Value);
			}
		}
	}
}

