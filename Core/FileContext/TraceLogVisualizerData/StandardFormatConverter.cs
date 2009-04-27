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
using System.Linq;
using System.Threading;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	/// <summary>
	/// 共通形式トレースログへ変換するためのコンバータ
	/// </summary>
	public class StandardFormatConverter
	{
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public ResourceData ResourceData { get; private set; }
		public TraceLogData TraceLogData { get; private set; }
		public VisualizeData VisualizeData { get; private set; }
		public SettingData SettingData { get; private set; }

		public StandardFormatConverter(string resourceFilePath, string traceLogFilePath, string[] visualizeRuleFilePaths, Action<int, string> ConstructProgressReport)
		{
			_constructProgressReport = ConstructProgressReport;

			progressUpdate(10);
			generateData(
				() => { ResourceData = getResourceData(resourceFilePath); },
				"リソースデータを生成中",
				"リソースデータの生成に失敗しました。\nリソースファイルの記述に誤りがある可能性があります。");

			progressUpdate(20);
			generateData(() => { SettingData = getSettingData(); },
				"設定データを生成中",
				"設定データの生成に失敗しました。");

			progressUpdate(30);
			generateData(
				() => { VisualizeData = getVisualizeData(visualizeRuleFilePaths); },
				"可視化データを生成中",
				"可視化データの生成に失敗しました。\n可視化ルールファイルの記述に誤りがある可能性があります。");

			progressUpdate(99);
			generateData(
				() => { TraceLogData = getTraceLogData(traceLogFilePath); },
				"トレースログデータを生成中",
				"トレースログデータの生成に失敗しました。\nトレースログ変換ルールファイルの記述に誤りがある可能性があります。");

			if (_constructProgressReport != null)
				_constructProgressReport(_progressFrom, "初期化中");
		}

		private void progressUpdate(int progressTo)
		{
			_progressFrom = _progressTo;
			_progressTo = progressTo;
		}

		private void generateData(Action action, string message, string exceptionMessage)
		{
			if (_constructProgressReport != null)
				_constructProgressReport(_progressFrom, message);
			try
			{
				action();
			}
			catch (Exception _e)
			{
				throw new Exception(exceptionMessage + "\n" + _e.Message);
			}
		}
		private ResourceData getResourceData(string resourceFilePath)
		{
			ResourceData resData = ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(resourceFilePath));

			// 未記述の属性についてデフォルト値で追加する
			foreach (ResourceType resType in resData.ResourceHeaders.ResourceTypes)
			{
				foreach (Resource res in resData.Resources.Where<Resource>(r=>r.Type == resType.Name))
				{
					foreach(AttributeType attrType in resType.Attributes)
					{
						if (!res.Attributes.ContainsKey(attrType.Name))
						{
							res.Attributes.Add(attrType.Name, attrType.Default);
						}
					}
				}
			}

			return resData;
		}
		private VisualizeData getVisualizeData(string[] visualizeRuleFilePaths)
		{
			VisualizeData visualizeData = new VisualizeData();
			visualizeData.VisualizeRules = new GeneralNamedCollection<VisualizeRule>();
			visualizeData.Shapes = new GeneralNamedCollection<Shapes>();

			string[] target = ResourceData.VisualizeRules.ToArray();

			// ファイルが複数ある場合を想定している
			foreach (string s in visualizeRuleFilePaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (target.Contains(j.Key))
					{
						string d = ApplicationFactory.JsonSerializer.Serialize(j.Value);
						VisualizeData vizData = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(d);

						if (vizData.VisualizeRules != null)
							foreach (VisualizeRule vizRule in vizData.VisualizeRules)
							{
								visualizeData.VisualizeRules.Add(vizRule);
							}
						if (vizData.Shapes != null)
							foreach (Shapes sp in vizData.Shapes)
							{
								visualizeData.Shapes.Add(sp);
							}
					}
				}
			}

			return visualizeData;
		}
		private TraceLogData getTraceLogData(string traceLogFilePath)
		{
			return new TraceLogGenerator(traceLogFilePath, ResourceData, _constructProgressReport, _progressFrom, _progressTo).Generate();
		}
		private SettingData getSettingData()
		{
			SettingData settingData = new SettingData();
			return settingData;
		}
	}

}