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
		private ResourceData _resourceData;
		private VisualizeData _visualizeData;
		private TraceLogData _traceLogData;
		private SettingData _settingData;
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public ResourceData ResourceData
		{
			get
			{
				return _resourceData;
			}
		}
		public TraceLogData TraceLogData
		{
			get
			{
				return _traceLogData;
			}
		}
		public VisualizeData VisualizeData
		{
			get { return _visualizeData; }
		}
		public SettingData SettingData
		{
			get { return _settingData; }
		}

		/// <summary>
		/// <c>CommonFormatConverter</c>のコンストラクタ
		/// </summary>
		public StandartFormatConverter(string resourceFilePath, string traceLogFilePath, string[] visualizeRuleFilePaths, Action<int, string> ConstructProgressReport)
		{
			_constructProgressReport = ConstructProgressReport;

			progressUpdate(10);
			generateData(() => { _resourceData = getResourceData(resourceFilePath); },
				"リソースデータを生成中",
				"リソースデータの生成に失敗しました。\nリソースファイルの記述に誤りがある可能性があります。");

			progressUpdate(20);
			generateData(() => { _settingData = getSettingData(); },
				"設定データを生成中",
				"設定データの生成に失敗しました。");

			progressUpdate(30);
			generateData(() => { _visualizeData = getVisualizeData(visualizeRuleFilePaths); },
				"可視化データを生成中",
				"可視化データの生成に失敗しました。\n可視化ルールファイルの記述に誤りがある可能性があります。");

			progressUpdate(99);
			generateData(() => { _traceLogData = getTraceLogData(traceLogFilePath); },
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

			foreach(ResourceType resType in resData.ResourceHeader.ResourceTypes)
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

			foreach (string visualizeRuleFilePath in visualizeRuleFilePaths)
			{
				VisualizeData vizData = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(visualizeRuleFilePath));

				foreach (VisualizeRule vizRule in vizData.VisualizeRules)
				{
					bool flag = false;
					foreach (ResourceType resh in _resourceData.ResourceHeader)
					{
						bool f = false;
						foreach (AttributeType attr in resh.Attributes)
						{
							if (vizRule.Name == attr.VisualizeRule)
							{
								f = true;
								break;
							}
						}
						foreach (Behavior bhvr in resh.Behaviors)
						{
							if (vizRule.Name == bhvr.VisualizeRule)
							{
								f = true;
								break;
							}
						}
						if (f)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						visualizeData.VisualizeRules.Add(vizRule.Name, vizRule);
					}
				}
				foreach (Shapes sp in vizData.Shapes)
				{
					bool flag = false;
					foreach (VisualizeRule vizRule in visualizeData.VisualizeRules)
					{
						bool f = false;
						if (vizRule.IsMapped)
						{
							foreach (KeyValuePair<string, string> v in vizRule)
							{
								if (sp.Name == v.Value)
								{
									f = true;
									break;
								}
							}
						}
						else
						{
							if (sp.Name == vizRule)
							{
								f = true;
							}
						}
						if (f)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						visualizeData.Shapes.Add(sp.Name, sp);
					}
				}
			}
			return visualizeData;
		}
		private TraceLogData getTraceLogData(string traceLogFilePath)
		{
			return new TraceLogGenerator(traceLogFilePath, _resourceData, _constructProgressReport, _progressFrom, _progressTo).Generate();
		}
		private SettingData getSettingData()
		{
			SettingData settingData = new SettingData();
			return settingData;
		}
	}

}