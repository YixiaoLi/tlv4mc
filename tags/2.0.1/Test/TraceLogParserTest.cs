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
using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace Test
{
    
    
    /// <summary>
    ///TraceLogParserTest のテスト クラスです。すべての
    ///TraceLogParserTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TraceLogParserTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        

        /// <summary>
        ///Parse のテスト
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            TraceLogParser target = new TraceLogParser(); // TODO: 適切な値に初期化してください
            TestCaseGenerator tcg = new TestCaseGenerator();
            foreach (string s in tcg.GetTestCase())
            {
                char[] input = s.ToCharArray(); // TODO: 適切な値に初期化してください
                target.Parse(input);
                var actual = new TestTraceLogInstance(target);
                using (StreamWriter w = new StreamWriter(@"F:/mydebug/expected.txt", true))  // TODO: あらかじめ空ファイルを用意してフルパスを指定して下さい
                {                                                                            //       
                    w.WriteLine("Log: " + s);
                    w.WriteLine("Time: " + _expected.Time);
                    w.WriteLine("Object: " + _expected.Object);
                    w.WriteLine("ObjectName: " + _expected.ObjectName);
                    w.WriteLine("ObjectType: " + _expected.ObjectType);
                    w.WriteLine("Behavior: " + _expected.Behavior);
                    w.WriteLine("Attribute: " + _expected.Attribute);
                    w.WriteLine("Value: " + _expected.Value);
                    w.WriteLine("Arguments: " + _expected.Arguments);
                    w.WriteLine("HasTime: " + _expected.HasTime);
                    w.WriteLine("HasObjectName: " + _expected.HasObjectName);
                    w.WriteLine("HasObjectType: " + _expected.HasObjectType);
                    w.WriteLine("Type: " + _expected.Type);
                }
                using (StreamWriter w = new StreamWriter(@"F:/mydebug/actual.txt", true))  // TODO: あらかじめ空ファイルを用意してフルパスを指定して下さい
                {
                    w.WriteLine("Log: " + s);
                    w.WriteLine("Time: " + actual.Time);
                    w.WriteLine("Object: " + actual.Object);
                    w.WriteLine("ObjectName: " + actual.ObjectName);
                    w.WriteLine("ObjectType: " + actual.ObjectType);
                    w.WriteLine("Behavior: " + actual.Behavior);
                    w.WriteLine("Attribute: " + actual.Attribute);
                    w.WriteLine("Value: " + actual.Value);
                    w.WriteLine("Arguments: " + actual.Arguments);
                    w.WriteLine("HasTime: " + actual.HasTime);
                    w.WriteLine("HasObjectName: " + actual.HasObjectName);
                    w.WriteLine("HasObjectType: " + actual.HasObjectType);
                    w.WriteLine("Type: " + actual.Type);
                }
                Assert.AreEqual(_expected.Time, actual.Time);
                Assert.AreEqual(_expected.Object, actual.Object);
                Assert.AreEqual(_expected.ObjectName, actual.ObjectName);
                Assert.AreEqual(_expected.ObjectType, actual.ObjectType);
                Assert.AreEqual(_expected.Behavior, actual.Behavior);
                Assert.AreEqual(_expected.Attribute, actual.Attribute);
                Assert.AreEqual(_expected.Value, actual.Value);
                Assert.AreEqual(_expected.Arguments, actual.Arguments);
                Assert.AreEqual(_expected.HasTime, actual.HasTime);
                Assert.AreEqual(_expected.HasObjectName, actual.HasObjectName);
                Assert.AreEqual(_expected.HasObjectType, actual.HasObjectType);
                Assert.AreEqual(_expected.Type, actual.Type);
            }
            
            //Assert.Inconclusive("値を返さないメソッドは確認できません。");
        }


        private static TestTraceLogInstance _expected = null;  // TestCaseGenerator.GetTestCase()内でnew


        // テストケース作成用
        //private readonly string _largeAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //private readonly string _smallAlpha = "abcdefghijklmnopqrstuvwxyz";
        //private readonly string _num = "1234567890";
        private static readonly string TIME = "190.azAZ";
        private static readonly string OBJECT_NAME = "${az_AZ190}";
        private static readonly string OBJECT_TYPE_NAME = "${az_190AZ}";
        private static readonly string BEHAVIOR_NAME = "${AZ_190az}";  // AttributeChangeのAttributeNameにも使用
        private static readonly string ATTRIBUTE_NAME = "${AZ_az190}";
        private static readonly string ACN_ATTRIBUTE_NAME = "${azAZ_190}";  // AttributeCondition内のAttributeName

        private static readonly string COMPARISON_SYMBOL = "=!<>";
        private static readonly string LOGIC_SYMBOL = "&|";
        private static readonly string OTHER_SYMBOL = "+-*/%$_{}^~:;,?#@`'";

        private static readonly string VALUE = TIME + COMPARISON_SYMBOL + LOGIC_SYMBOL + "(" + ")" + OTHER_SYMBOL;

        private static readonly string[] COMPARISON_SYMBOLS = new string[] { "==", "!=", "<", ">", "<=", ">=" };
        private static readonly string[] LOGIC_SYMBOLS = new string[] { "&&", "||" };

        // 3つのArgumentと異なる文字構成　　　// 最前、間、最後に括弧があるケース
        private static readonly string[] ARGUMENTS = new string[] { "", ",", "190_azAZ,${190_azAZ},190(_az()A)Z", "(_az()A)Z190,190(_az()A)Z,190(_az()AZ)", VALUE };

        private static readonly string[] CONPARISON_EXPRESSION_VAULE = new string[] { TIME + COMPARISON_SYMBOL + OTHER_SYMBOL, "(_AZ()a)z190", "190(_AZ()a)z", "190(_AZ()az)" };





        class TestTraceLogInstance
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

            // actual用
            public TestTraceLogInstance(TraceLogParser parser)
            {
                Time = parser.TimeValue;
                Object = parser.ObjectValue;
                ObjectName = parser.ObjectNameValue;
                ObjectType = parser.ObjectTypeValue;
                Behavior = parser.BehaviorValue;
                Attribute = parser.AttributeValue;
                Value = parser.ValueValue;
                Arguments = parser.ArgumentsValue;
                HasTime = parser.HasTimeValue;
                HasObjectName = parser.HasObjectNameValue;
                HasObjectType = parser.HasObjectTypeValue;
                if (Behavior != null && Attribute != null)
                    Assert.Fail("不正なトレースログです。\n");
                else if (Behavior == null && Attribute != null)
                    Type = TraceLogType.AttributeChange;
                else if (Behavior != null && Attribute == null)
                    Type = TraceLogType.BehaviorHappen;
            }


            // expected用
            public TestTraceLogInstance(string log, string acn)
            {
                if (log.Contains("["+TIME+"]")) { Time = TIME; HasTime = true;}
                if (log.Contains(OBJECT_NAME)) { Object = ObjectName = OBJECT_NAME; HasObjectName = true; }
                if (log.Contains(OBJECT_TYPE_NAME)) { ObjectType = OBJECT_TYPE_NAME; HasObjectType = true; }
                if (log.Contains(BEHAVIOR_NAME)) { Behavior = BEHAVIOR_NAME; }
                if (log.Contains(ATTRIBUTE_NAME)) { Attribute = ATTRIBUTE_NAME; }
                if (log.EndsWith(VALUE)) { Value = VALUE; }
                foreach (string s in ARGUMENTS)
                {
                    if (log.EndsWith("(" + s + ")")) { Arguments = s; break; }
                }
                if (acn != null) { Object = OBJECT_TYPE_NAME + "(" + acn +")"; }

                if (Behavior != null && Attribute != null)
                    Assert.Fail("不正なトレースログです。\n" + log);
                else if (Behavior == null && Attribute != null)
                    Type = TraceLogType.AttributeChange;
                else if (Behavior != null && Attribute == null)
                    Type = TraceLogType.BehaviorHappen;
            }

        }

        class TestCaseGenerator
        {
            public IEnumerable<string> GetTestCase()
            {
                foreach (string time in new string[] { "", "[" + TIME +"]" })
                {
                    // ----ObjectName----
                    // --AttributeChange--
                    foreach (string ac in GetAttributeChange(time + OBJECT_NAME))
                    {
                        _expected = new TestTraceLogInstance(ac, null); 
                        yield return ac;
                    }

                    string nobj = time + OBJECT_NAME + "." + BEHAVIOR_NAME;

                    // --BehaviorHappen--
                    foreach (string arg in ARGUMENTS)
                    {
                        _expected = new TestTraceLogInstance(nobj + "(" + arg + ")", null); 
                        yield return nobj + "(" + arg + ")";
                    }

                    // ----ObjectType----                
                    foreach (string cev in CONPARISON_EXPRESSION_VAULE)
                    {
                        foreach (string acn in GetAttributeCondition(cev))
                        {
                            string otype = time + OBJECT_TYPE_NAME + "(" + acn + ")";
                            // --AttributeChange--
                            foreach (string ac in GetAttributeChange(otype))
                            {
                                _expected = new TestTraceLogInstance(ac, acn); 
                                yield return ac;
                            }

                            // --BehaviorHappen--
                            string tobj = otype + "." + BEHAVIOR_NAME;
                            foreach (string arg in ARGUMENTS)
                            {
                                _expected = new TestTraceLogInstance(tobj + "(" + arg + ")", acn);
                                yield return tobj + "(" + arg + ")";
                            }
                        }
                    }
                }
            }


            public IEnumerable<string> GetAttributeChange(string obj)
            {
                yield return obj;
                yield return obj + "." + ATTRIBUTE_NAME;
                yield return obj + "." + ATTRIBUTE_NAME + "=" + VALUE;
            }

            public IEnumerable<string> GetAttributeCondition(string valueAC)
            {
                foreach (string s in COMPARISON_SYMBOLS)
                {
                    yield return ACN_ATTRIBUTE_NAME + s + valueAC;
                }

                yield return "true";
                yield return "false";

                // 順序、組み合わせ、括弧
                yield return ACN_ATTRIBUTE_NAME + "==" + valueAC + "&&true";
                yield return "false||" + ACN_ATTRIBUTE_NAME + "!=" + valueAC;
                yield return "(" + ACN_ATTRIBUTE_NAME + "<" + valueAC + ")&&true";
                yield return "false||(" + ACN_ATTRIBUTE_NAME + ">" + valueAC + ")";
                yield return "(" + ACN_ATTRIBUTE_NAME + "<=" + valueAC + ")&&(true)";
                yield return "((false)||(" + ACN_ATTRIBUTE_NAME + ">=" + valueAC + "))";
            }
        }


    }
}
