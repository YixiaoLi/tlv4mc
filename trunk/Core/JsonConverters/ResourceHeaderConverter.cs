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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceHeaderConverter : GeneralConverter<ResourceHeader>
	{
		protected override void WriteJson(IJsonWriter writer, ResourceHeader resh)
		{
			writer.WriteObject(w =>
				{
					foreach (ResourceType rt in resh)
					{
						w.WriteProperty(rt.Name);
						w.WriteValue(rt, ApplicationFactory.JsonSerializer);
					}
				});
		}

		public override object ReadJson(IJsonReader reader)
		{
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				GeneralNamedCollection<ResourceType> resTypes = new GeneralNamedCollection<ResourceType>();
				while(reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string name = (string)reader.Value;
						ResourceType resType = ApplicationFactory.JsonSerializer.Deserialize<ResourceType>(reader);
						resType.Name = name;
						resTypes.Add(resType);
					}
					reader.Read();
				}

				ResourceHeader resh = new ResourceHeader(resTypes);
				return resh;
			}
			else if (reader.TokenType == JsonTokenType.StartArray)
			{
				ResourceHeader result = getResourceHeader(reader);
				return result;
			}
			else
			{
				throw new Exception("ResourceHeader���Ҥ�ʸˡ���ְ�äƤ��ޤ���");
			}
		}

		private static ResourceHeader getResourceHeader(IJsonReader reader)
		{

			Json data = new Json(new Dictionary<string, Json>());

			while(reader.TokenType != JsonTokenType.EndArray)
			{
				if (reader.TokenType == JsonTokenType.String)
				{
					string name = (string)reader.Value;

					string[] resourceHeadersPaths = Directory.GetFiles(ApplicationData.Setting.ResourceHeadersDirectoryPath, "*." + Properties.Resources.ResourceHeaderFileExtension);

					foreach (string s in resourceHeadersPaths)
					{
						Json json = new Json().Parse(File.ReadAllText(s));
						foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
						{
							if (j.Key == name)
							{
								foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
								{
									data.Add(_j.Key, _j.Value.Value);
								}
							}
						}
					}
				}
				reader.Read();
			}

			string typesStr = data.ToJsonString();

			GeneralNamedCollection<ResourceType> types = ApplicationFactory.JsonSerializer.Deserialize<GeneralNamedCollection<ResourceType>>(typesStr);
			ResourceHeader result = new ResourceHeader(types);
			return result;
		}
	}
}
