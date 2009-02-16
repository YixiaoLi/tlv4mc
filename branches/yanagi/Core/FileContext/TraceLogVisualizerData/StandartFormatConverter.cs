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
	public class StandartFormatConverter
	{
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public ResourceData ResourceData { get; private set; }
		public TraceLogData TraceLogData { get; private set; }
		public VisualizeData VisualizeData { get; private set; }
		public SettingData SettingData { get; private set; }

		public StandartFormatConverter(string resourceFilePath, string traceLogFilePath, string[] visualizeRuleFilePaths, Action<int, string> ConstructProgressReport)
		{
			_constructProgressReport = ConstructProgressReport;

			progressUpdate(10);
			generateData(() => { ResourceData = getResourceData(resourceFilePath); },
				"リソースデータを生成中",
				"リソースデータの生成に失敗しました。\nリソースファイルの記述に誤りがある可能性があります。");

			progressUpdate(20);
			generateData(() => { SettingData = getSettingData(); },
				"設定データを生成中",
				"設定データの生成に失敗しました。");

			progressUpdate(30);
			generateData(() => { VisualizeData = getVisualizeData(visualizeRuleFilePaths); },
				"可視化データを生成中",
				"可視化データの生成に失敗しました。\n可視化ルールファイルの記述に誤りがある可能性があります。");

			progressUpdate(99);
			generateData(() => { TraceLogData = getTraceLogData(traceLogFilePath); },
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

			//foreach (string visualizeRuleFilePath in visualizeRuleFilePaths)
			//{
			//    VisualizeData vizData = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(visualizeRuleFilePath));

			//    if (vizData.VisualizeRules != null)
			//        foreach (VisualizeRule vizRule in vizData.VisualizeRules)
			//        {
			//            visualizeData.VisualizeRules.Add(vizRule.Name, vizRule);
			//        }
			//    if (vizData.Shapes != null)
			//        foreach (Shapes sp in vizData.Shapes)
			//        {
			//            visualizeData.Shapes.Add(sp.Name, sp);
			//        }
			//}

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