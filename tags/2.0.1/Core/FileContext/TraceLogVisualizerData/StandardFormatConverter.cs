/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)?(4)の条件を満たす場合に限り，本ソフトウェ
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
 */

/**
 * @file  StandardFormatConverter
 * @brief 標準変換を行うクラス
 * @author r-miwa 
 * @date 2011/05/17
 * @version $Id: StandardFormatConverter.cs,v 1.1 2011/05/17 
 *
 */
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
  
/*
 * 標準変換を行うクラス
 * @author r-miwa 
 * @date 2011/05/17
 * @version $Id: StandardFormatConverter.cs,v 1.1 2011/05/17 
 *
 */
  
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
      public Dictionary<string,LogData> lastLogs { get; private set; }
      
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
                    //時刻が全て同一の場合、0除算例外が発生するのでエラーチェック
                    if (TraceLogData.MaxTime == TraceLogData.MinTime)
                      throw new Exception("時刻が全て同一である不正なトレースログです。");
                    
                    setLastLogs();
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
          catch (ConvertException _e) {
            throw _e;
          }
          catch (Exception _e)
            {
              foreach (System.Collections.DictionaryEntry de in _e.Data)
                {
                  exceptionMessage += "\n" + de.Value;
                }
              //throw _e;
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
          //TraceLogData data = new TraceLogGenerator(traceLogFilePath, ResourceData, _constructProgressReport, _progressFrom, _progressTo).Generate();           
          //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();            
          //sw.Start();
          //TraceLogData data = new TraceLogGenerator(traceLogFilePath, ResourceData, _constructProgressReport, _progressFrom, _progressTo).Generate();
          //TraceLogData data = new TraceLogGenerator(traceLogFilePath, ResourceData, _constructProgressReport, _progressFrom, _progressTo).Generate2();
          //sw.Stop();
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
          
          string json = "";
          
          try
            {
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
            }
          finally
            {
              p.Close();
            }
          
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
              this._constructProgressReport((int)((current / count) * (to - from) + from),rule.DisplayName);
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
                          gen.SetData(TraceLogData, VisualizeData, ResourceData, lastLogs);
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
                      gen.SetData(TraceLogData, VisualizeData, ResourceData, lastLogs);
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
      
      //各リソースにおいて、最後に発生したイベントを lastLogs へ記録する
      //チケット#156の解決に各リソースで最後に発生したイベントの情報が
      //必要となったため、 11/25 に作成
      private void setLastLogs()
        {
          lastLogs = new Dictionary<string, LogData>();
          foreach (Resource res in ResourceData.Resources)
            {
              for (int i = TraceLogData.LogDataBase.Count-1; i >= 0; i--)
                {
                  if (!res.Name.Contains("Current") && TraceLogData.LogDataBase[i].Object.Name.Equals(res.Name))
                    {
                      lastLogs.Add(res.Name, TraceLogData.LogDataBase[i]);
                      break;
                    }
                }
            }
        }
    }
  
}