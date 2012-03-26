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
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TLVFunction
	{
		public static readonly Dictionary<string, Func<string[], ResourceData, TraceLogData, string>> TLVFunctions = new Dictionary<string, Func<string[], ResourceData, TraceLogData, string>>()
		{
			{
				"COUNT", (args, resData, logData) =>
				{
					return logData.GetObject(args[0]).Count().ToString();
				}
			},
			{
				"EXIST", (args, resData, logData) =>
				{
					return logData.GetObject(args[0]).Count() != 0 ? "True" : "False";
				}
			},
			{
				"ATTR", (args, resData, logData) =>
				{
					return logData.GetAttributeValue(args[0]).ToString();
				}
			},
			{
				"RES_NAME", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length != 1)
                    {
                        throw new ResNotFoundException(args[0]);
                    }
				    return i[0].Name;
				}
			},
			{
				"RES_DISPLAYNAME", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME で 指定した条件のリソースは複数存在します。\n" + args[0]);
                    else if(i.Length == 0)
                        throw new ResNotFoundException(args[0]);
                    else
    					return i[0].DisplayName;
				}
			},
			{
				"RES_COLOR", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME で 指定した条件のリソースは複数存在します。\n" + args[0]);
                    else if(i.Length == 0)
                        throw new ResNotFoundException(args[0]);
                    else
					    return i[0].Color.Value.ToHexString();
				}
			}
		};

		public static TraceLog Apply(TraceLog value, ResourceData resData, TraceLogData logData)
		{
			return new TraceLog(Apply(value.ToString(), resData, logData));
		}

		public static string Apply(string value, ResourceData resData, TraceLogData logData)
		{
			if (value == null)
				return value;

			value = value.Replace("\\{", "___START_BIG_BRACKET___");
			value = value.Replace("\\}", "___END_BIG_BRACKET___");
			foreach (Match m in Regex.Matches(value, @"\$(?<func>[^{]+){(?<args>[^}]+)}"))
			{
				try
				{
					value = Regex.Replace(value, Regex.Escape(m.Value), apply(m.Groups["func"].Value, m.Groups["args"].Value, resData, logData));
				}
				catch (ConvertException e)
				{
                    e.rule = value;
					throw e;
				}
			}
			value = value.Replace("___START_BIG_BRACKET___", "\\{");
			value = value.Replace("___END_BIG_BRACKET___", "\\}");
			return value;
		}

		private static string apply(string func, string tlvarguments, ResourceData resData, TraceLogData logData)
		{
			tlvarguments = tlvarguments.Replace("\\,", "___COMMAS___");
			tlvarguments = tlvarguments.Replace("\\\"", "___DOUBLE_QUOTE___");
			string[] tlvargs = tlvarguments.Split(',');
			for (int i=0;i<tlvargs.Length;i++)
			{
				tlvargs[i] = tlvargs[i].Replace("___COMMAS___", "\\,");
				tlvargs[i] = tlvargs[i].Replace("___DOUBLE_QUOTE___", "\\\"");
			}

			string result;

			if (TLVFunctions.ContainsKey(func))
				result = TLVFunctions[func](tlvargs, resData, logData);
			else
				throw new Exception("未定義の関数です。\n" + "\"" + func + "\"");

			return result;
		}

	}
}
