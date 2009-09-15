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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLog : ICloneable
	{
		public string Time { get; private set; }
		public string Object { get; private set; }
		public string ObjectName { get; private set; }
		public string ObjectType { get; private set; }
		public string Behavior { get; private set; }
		public string Attribute { get; private set; }
		public string Value { get; private set; }
		public string Arguments { get; private set; }
		public bool HasTime { get; private set; }
		public bool HasObjectName { get; private set; }
		public bool HasObjectType { get; private set; }
		public TraceLogType Type { get; private set; }

		private string _log;

		public TraceLog(string log)
		{
			Match m;

			_log = Regex.Replace(log, @"\s","");

			m = Regex.Match(_log, @"\[(?<time>[0-9a-zA-Z]+(\.[0-9a-zA-Z]*)?)\]");
			if (m.Success)
				Time = m.Groups["time"].Value;
			HasTime = m.Success;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?(?<object>[^\[\]\(\)\.]+(\([^\)]+\))?)(\.[^\s]+)?$");
			if (m.Success)
				Object = m.Groups["object"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?(?<objectName>[^\[\]\(\)\.]+)(\.[^\s]+)?$");
			if (m.Success)
				ObjectName = m.Groups["objectName"].Value;
			HasObjectName = m.Success;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?(?<objectType>[^\[\]\(\)\.]+)\([^\)]+\)(\.[^\s]+)?$");
			if (m.Success)
				ObjectType = m.Groups["objectType"].Value;
			HasObjectType = m.Success;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.(?<behavior>[^\(\s]+)\([^\)]*\)$");
			if (m.Success)
				Behavior = m.Groups["behavior"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.(?<attribute>[^=!<>\(\)]+)([=!<>].*)?$");
			if (m.Success)
				Attribute = m.Groups["attribute"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.[^=!<>\(\s]+(>=|>|<|<=|==|!=|=)(?<value>[^\s$]+)$");
			if (m.Success)
				Value = m.Groups["value"].Value;

			m = Regex.Match(_log, @"^(\[[^\]]+\])?[^\[\]\(\)\.]+(\([^\)]+\))?\.[^=!<>\(\s]+\((?<args>[^\)]*)\)$");
			if (m.Success)
				Arguments = m.Groups["args"].Value;

			if (Behavior != null && Attribute != null)
				throw new Exception("不正なトレースログです。\n" + _log);
			else if (Behavior == null && Attribute != null)
				Type = TraceLogType.AttributeChange;
			else if (Behavior != null && Attribute == null)
				Type = TraceLogType.BehaviorHappen;


		}

		public static implicit operator string(TraceLog stdlog)
		{
			return stdlog.ToString();
		}
		public static implicit operator TraceLog(string str)
		{
			return new TraceLog(str);
		}

		public override string ToString()
		{
			return _log;
		}

		public Json GetValue(ResourceData resourceData)
		{
			if (!HasObjectType || HasObjectName)
				ObjectType = resourceData.Resources[ObjectName].Type;

			switch (resourceData.ResourceHeaders[ObjectType].Attributes[Attribute].VariableType)
			{
				case JsonValueType.String:
					return new Json(Value);
				case JsonValueType.Number:
					return new Json(Convert.ToDecimal(Value));
				case JsonValueType.Boolean:
					return new Json(Convert.ToBoolean(Value));
				default:
					return null;
			}
		}

		public ArgumentList GetArguments(ResourceData resourceData)
		{
			if (!HasObjectType || HasObjectName)
				ObjectType = resourceData.Resources[ObjectName].Type;

			if (Arguments == string.Empty)
				return new ArgumentList();

			string[] args = Arguments.Split(',');
			ArgumentList argList = new ArgumentList();

			int i = 0;
			foreach (ArgumentType argType in resourceData.ResourceHeaders[ObjectType].Behaviors[Behavior].Arguments)
			{
				if (args[i] == string.Empty)
					argList.Add(null);
				else
					switch (argType.Type)
					{
						case JsonValueType.String:
							argList.Add(new Json(args[i]));
							break;
						case JsonValueType.Number:
							argList.Add(new Json(Convert.ToDecimal(args[i])));
							break;
						case JsonValueType.Boolean:
							argList.Add(new Json(Convert.ToBoolean(args[i])));
							break;
						default:
							argList.Add(null);
							break;
					}

				i++;
				if (args.Length <= i)
					break;
			}

			return argList;
		}

		public object Clone()
		{
			TraceLog log = new TraceLog(_log);
			return log;
		}
	}
}
