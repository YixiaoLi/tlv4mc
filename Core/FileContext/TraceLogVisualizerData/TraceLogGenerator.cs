
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogGenerator
	{
		private string _traceLogFilePath;
		private ResourceData _resourceData;
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public TraceLogGenerator(string traceLogFilePath, ResourceData resourceData, Action<int, string> ConstructProgressReport, int progressFrom, int progressTo)
		{
			_constructProgressReport = ConstructProgressReport;
			_traceLogFilePath = traceLogFilePath;
			_resourceData = resourceData;
			_progressFrom = progressFrom;
			_progressTo = progressTo;
		}

		public TraceLogData Generate()
		{
            Dictionary<string, Json> oldRule = new Dictionary<string, Json>();
            Json newRule = null;

			string[] target = _resourceData.ConvertRules.ToArray();

			string[] convertRulePaths = Directory.GetFiles(ApplicationData.Setting.ConvertRulesDirectoryPath, "*." + Properties.Resources.ConvertRuleFileExtension);
			// トレースログ変換ファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach (string s in convertRulePaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (target.Contains(j.Key))
					{
                        if (j.Value.ContainsKey("$STYLE"))
                        {
                            if (newRule == null)
                            {
                                newRule = j.Value;
                            }
                            else {
                                throw new Exception("複数の新形式変換ルールが存在します");
                            }
                        }
                        else
                        {
                            foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
                            {
                                oldRule.Add(_j.Key, _j.Value);
                            }
                        }
					}
				}
			}
            if (newRule != null && oldRule.Count > 0)
            {
                throw new Exception("新形式変換ルールと旧形式変換ルールは混在させることはできません");
            }
            else if (oldRule.Count > 0)
            {
                return generateByOldRule(oldRule);
            }
            else if (newRule != null)
            {
                return generateByNewRule(newRule);
            }
            else
            {
                throw new Exception("一致する変換ルールが見つかりません。");
            }
		}

        private TraceLogData generateByNewRule(Json rule)
        {
            ProcessStartInfo psi;
            if (rule.ContainsKey("script"))
            {
                string path = Path.GetTempFileName();
                StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));

                string script = rule["script"];
                sw.Write(script);
                sw.Close();
                psi = new ProcessStartInfo(rule["fileName"],
                                           string.Format(rule["arguments"], path));
            }
            else
            {
                psi = new ProcessStartInfo(rule["fileName"], rule["arguments"]);
            }
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;

            Process p = Process.Start(psi);
            string[] logs = File.ReadAllLines(_traceLogFilePath);
            
            foreach(string log in logs){
                p.StandardInput.WriteLine(log);
            }
            p.WaitForExit();


            TraceLogData t = new TraceLogData(_resourceData);
            while (!p.StandardOutput.EndOfStream) {
                t.Add(new TraceLog(p.StandardOutput.ReadLine()));
            }
            t.LogDataBase.SetIds();
            t.Path = this._traceLogFilePath;
            return t;
        }

        private TraceLogData generateByOldRule(Dictionary<string, Json> dic) {
            TraceLogData t = new TraceLogData(_resourceData);

            // トレースログを一行ずつ調べTraceLogクラスに変換しTraceLogListに追加していく
            string[] logs = File.ReadAllLines(_traceLogFilePath);
            float i = 1;
            float max = logs.Length;
            foreach (string s in logs)
            {
                if (_constructProgressReport != null)
                    _constructProgressReport((int)(((i / max) * (float)(_progressTo - _progressFrom)) + (float)_progressFrom), "トレースログを共通形式へ変換中 " + i + "/" + max + " 行目...");

                foreach (KeyValuePair<string, Json> kvp in dic)
                {
                    if (Regex.IsMatch(s, kvp.Key))
                    {
                        addTraceLog(s, kvp.Key, kvp.Value, t);
                    }
                }
                i++;
            }

            t.LogDataBase.SetIds();
            t.Path = this._traceLogFilePath;
            return t;
        }

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="pattern">パターン</param>
		/// <param name="value">変換値がValue（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLog(string log, string pattern, Json value, TraceLogData traceLogData)
		{
			if (value.IsArray)
			{
				addTraceLogAsArray(log, pattern, value, traceLogData);
			}
			else if (value.IsObject)
			{
				addTraceLogAsObject(log, pattern, value, traceLogData);
			}
			else
			{
				// valueがstringのときログを置換して追加
				string s = Regex.Replace(log, pattern, value);
				// 関数を適用
				s = TLVFunction.Apply(s, _resourceData, traceLogData);
				// ログを追加
                try
                {
                    traceLogData.Add(new TraceLog(s));
                }
                catch(Exception e)
                {
                    e.Data.Add("log", log.ToString());
                    e.Data.Add("pattern", pattern.ToString());
                    e.Data.Add("jsonvalue", value.ToString());
                    throw (e);
                }

			}
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="pattern">パターン</param>
		/// <param name="value">変換値がArray（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLogAsArray(string log, string pattern, List<Json> value, TraceLogData traceLogData)
		{
			foreach (Json j in value)
			{
				addTraceLog(log, pattern, j, traceLogData);
			}
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="condition">パターン</param>
		/// <param name="value">変換値がObject（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLogAsObject(string log, string pattern, Dictionary<string, Json> value, TraceLogData traceLogData)
		{
			foreach (KeyValuePair<string, Json> kvp in value)
			{
				string condition = Regex.Replace(log, pattern, kvp.Key);

				// 条件に関数を適用
				condition = TLVFunction.Apply(condition, _resourceData, traceLogData);

				// 条件式を評価
				bool result;
				try
				{
					result = ConditionExpression.Result(condition);
				}
				catch (Exception e)
				{
					throw new Exception("ログ条件式が異常です。\n" + "\"" + kvp.Key + "\"\n" + e.Message);
				}

				// 条件式が真ならトレースログを追加
				if (result)
				{
					addTraceLog(log, pattern, kvp.Value, traceLogData);
				}
			}
		}



	}
}
