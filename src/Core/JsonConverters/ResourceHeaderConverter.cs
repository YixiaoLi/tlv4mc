/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
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
				throw new Exception("ResourceHeader記述の文法が間違っています。");
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
