/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
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
                        string key = keyStack.Pop();
                        try
                        {
                            ((Dictionary<string, Json>)result.Value).Add(key, new Json(value));
                        }
                        catch (ArgumentException e)
                        {
                            e.Data.Add("jsonValueSet key", key);
                            throw e;
                        }
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
                        try
                        {
                            jsonValueSet(reader.Value);
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.Data);
                            throw e;
                        }
						break;
				}
			} while (!(objectNest == 0 && arrayNest == 0) && reader.Read());

			if (objectNest != 0)
				throw new Exception("Jsonの記述に誤りがあります。\n{と}の対応が取れていません。");

			if (arrayNest != 0)
				throw new Exception("Jsonの記述に誤りがあります。\n[と]の対応が取れていません。");

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

