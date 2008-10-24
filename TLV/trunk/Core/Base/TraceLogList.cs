using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// トレースログリスト
    /// </summary>
    public class TraceLogList : IEnumerable<TraceLog>
    {
        private List<TraceLog> _list = new List<TraceLog>();
        private string _traceLogData = string.Empty;
        public int Count { get { return _list.Count; } }

        public TraceLogList(string traceLogData)
        {
            _traceLogData = traceLogData;
            generateTraceLogs();
        }

        private void generateTraceLogs()
        {
            // 共通形式トレースログをパースしTraceLogクラスのインスタンスを生成、_listに格納する
            foreach (Match m in Regex.Matches(_traceLogData, ApplicationDatas.CommonFormatTraceLogRegex, RegexOptions.Multiline))
            {
                _list.Add(new TraceLog(long.Parse(m.Groups["T"].Value), m.Groups["S"].Value, m.Groups["O"].Value, m.Groups["B"].Value));
            }
        }

        public IEnumerator<TraceLog> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public override string ToString()
        {
            return _traceLogData;
        }
    }
}
