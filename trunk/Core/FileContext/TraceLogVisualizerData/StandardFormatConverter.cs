
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;
using System.Linq;
using System.Threading;
using System.Drawing;
using System.Diagnostics;

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
        public VisualizeShapeData VisualizeShapeData { get; private set; }

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

            progressUpdate(40);
			generateData(
				() => { TraceLogData = getTraceLogData(traceLogFilePath); },
				"トレースログデータを生成中",
				"トレースログデータの生成に失敗しました。\nトレースログ変換ルールファイルの記述に誤りがある可能性があります。");
            progressUpdate(50);
            generateData(
               () => { VisualizeShapeData = getVisualizeShapeData(50,99); },
              "図形データを生成中",
             "図形データの生成に失敗しました。\n可視化ルールファイルの記述に誤りがある可能性があります。" );
            progressUpdate(99);
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
                foreach (System.Collections.DictionaryEntry de in _e.Data)
                {
                    exceptionMessage += "\n" + de.Value;
                }
#if DEBUG
                throw new Exception(exceptionMessage + "\n" + _e.Message + "\n" + _e.StackTrace);
#else
                throw new Exception(exceptionMessage + "\n" + _e.Message);
#endif
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
            resData.Path = resourceFilePath;
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

        private EventShapes generateByNewRule(VisualizeRule rule)
        {
            ProcessStartInfo psi;
            if (rule.Script != null)
            {
                string path = Path.GetTempFileName();
                StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));

                string script = rule.Script;
                sw.Write(script);
                sw.Close();
                psi = new ProcessStartInfo(rule.FileName,
                                           string.Format(rule.Arguments, path));
            }
            else
            {
                psi = new ProcessStartInfo(rule.FileName, rule.Arguments);
            }
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            Process p = new Process();
            p.StartInfo = psi;
            string AppPath = System.Windows.Forms.Application.StartupPath;
            p.StartInfo.WorkingDirectory = AppPath;

            string json= "";
   
            p.Start();
            p.StandardInput.WriteLine(this.ResourceData.ToJson());
            p.StandardInput.WriteLine("---");
            foreach (TraceLog log in TraceLogData.TraceLogs)
            {
                p.StandardInput.WriteLine(log.ToString());
            }


            p.StandardInput.Close();

            while (!(p.HasExited && p.StandardOutput.EndOfStream))
            {
                json += p.StandardOutput.ReadLine();
            }

            if (p.ExitCode != 0)
            {
                string error = "";
                while (!p.StandardError.EndOfStream)
                {
                    error += p.StandardError.ReadLine() + "\n";
                } 
                throw new Exception(error);
            }
            p.Close();

            EventShapes es = new EventShapes();
            List<EventShape> shapes = ApplicationFactory.JsonSerializer.Deserialize<List<EventShape>>(json);

            // FIXME: 古いルールと整合性を保つためにrule.Shapesを作成する
            // Shapesに代入されたEventはNameしか使われないと仮定してる
            rule.Shapes = new GeneralNamedCollection<Event>();
            foreach (EventShape shape in shapes)
            {
                shape.Event.SetVisualizeRuleName(rule.Name);
                es.Add(shape);
                if (!rule.Shapes.Keys.Contains(shape.Event.Name))
                {
                    rule.Shapes.Add(shape.Event);
                }
            }
            return es;
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {

            throw new NotImplementedException();
        }

        private VisualizeShapeData getVisualizeShapeData(int from,int to)
        {
            VisualizeShapeData vizData = new VisualizeShapeData();
            int count = this.VisualizeData.VisualizeRules.Count;
            double current = 0.0;
            foreach (VisualizeRule rule in this.VisualizeData.VisualizeRules)
            {
                this._constructProgressReport((int)((current / count) * (to - from) + from),
rule.DisplayName);
                current += 1.0;
                if (rule.IsBelongedTargetResourceType())
                {
                    foreach (Resource res in this.ResourceData.Resources.Where<Resource>(r => r.Type == rule.Target))
                    {
                        if (rule.Style != "Script")
                        {
                            foreach (Event e in rule.Shapes)
                            {
                                e.SetVisualizeRuleName(rule.Name);
                            }
                            var gen = new EventShapesGenerator(rule, res);
                            gen.SetData(TraceLogData, VisualizeData, ResourceData);
                            vizData.Add(rule, res, gen.GetEventShapes());
                        }
                        else
                        {
                            vizData.Add(rule, res, this.generateByNewRule(rule));
                        }
                    }
                }
                else
                {
                    if (rule.Style != "Script")
                    {
                        foreach (Event e in rule.Shapes)
                        {
                            e.SetVisualizeRuleName(rule.Name);
                        }
                        var gen = new EventShapesGenerator(rule);
                        gen.SetData(TraceLogData, VisualizeData, ResourceData);
                        vizData.Add(rule, gen.GetEventShapes());
                    }
                    else
                    {
                        vizData.Add(rule,this.generateByNewRule(rule));
                    }
                }
            }

            return vizData;
        }
	}

}