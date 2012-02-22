/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public	class NewtonsoftJson :IJsonSerializer
	{
		private JsonSerializer _serializer = new JsonSerializer();

		private Dictionary<Type, IJsonConverter> _converterList = new Dictionary<Type, IJsonConverter>();

		public NewtonsoftJson()
		{
			_serializer.Converters.Add(new IsoDateTimeConverter());
			_serializer.Converters.Add(new EnumConverter());
			_serializer.NullValueHandling = NullValueHandling.Ignore;
			_serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
			_serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			_serializer.ObjectCreationHandling = ObjectCreationHandling.Auto;
			_serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
		}

		public T Deserialize<T>(IJsonReader reader)
		{
			return (T)Deserialize(reader, typeof(T));
		}
		public T Deserialize<T>(string json)
		{
			return (T)Deserialize(json, typeof(T));
		}
		public object Deserialize(IJsonReader reader, Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				type = type.GetGenericArguments()[0];

			object obj = _serializer.Deserialize(((JsonReader)reader).Reader, type);
			return obj;
		}
		public object Deserialize(string json, Type type)
		{
			return Deserialize(new JsonReader(new JsonTextReader(new StringReader(json))), type);
		}

		public void Serialize(IJsonWriter writer, object obj)
		{
			_serializer.Serialize(((JsonWriter)writer).Writer, obj);
		}
		public string Serialize(object obj)
		{
			StringBuilder sb = new StringBuilder();
			Serialize(new JsonWriter(new JsonTextWriter(new StringWriter(sb)) { Formatting = Formatting.Indented }), obj);
			return sb.ToString();
		}

		public void AddConverter(IJsonConverter converter)
		{
			GeneralConverter cnvtr = new GeneralConverter(converter);

			_serializer.Converters.Add(cnvtr);

			_converterList.Add(converter.Type, converter);
		}
		public bool HasConverter(Type type)
		{
			return _converterList.ContainsKey(type);
		}
		public IJsonConverter GetConverter(Type type)
		{
			return _converterList[type];
		}

	}
}
