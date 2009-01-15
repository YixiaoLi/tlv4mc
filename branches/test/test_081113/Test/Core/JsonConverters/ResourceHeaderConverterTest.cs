using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace Test
{
    
    
    /// <summary>
    ///ResourceHeaderConverterTest のテスト クラスです。すべての
    ///ResourceHeaderConverterTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class ResourceHeaderConverterTest
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
        private string Write(ResourceHeader rh)
        {
            ResourceHeaderConverter target = new ResourceHeaderConverter(); 
            System.IO.StringWriter sw = new System.IO.StringWriter();
            IJsonWriter writer = new NU.OJL.MPRTOS.TLV.Third.JsonWriter(new Newtonsoft.Json.JsonTextWriter(sw));
            target.WriteJson(writer, rh);
            return sw.ToString();
        }

        private ResourceHeader Read(string s)
        {
            ResourceHeaderConverter target = new ResourceHeaderConverter(); 
            IJsonReader reader = new NU.OJL.MPRTOS.TLV.Third.JsonReader(
               new Newtonsoft.Json.JsonTextReader(new System.IO.StringReader(s)));
            reader.Read();
            return (ResourceHeader)target.ReadJson(reader);
        } 


        /// <summary>
        ///Type のテスト
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            ResourceHeaderConverter target = new ResourceHeaderConverter();
            Assert.AreEqual(typeof(ResourceHeader),target.Type);
        }

        /// <summary>
        ///WriteJson のテスト
        ///</summary>
        [TestMethod()]
        public void WriteJsonSimpleTest()
        {
            ResourceHeader header = new ResourceHeader();
            Assert.AreEqual("{}",Write(header));
        }

        [TestMethod()]
        public void WriteJsonNullException() {
            // AttributeType 
            AttributeType attr1 = new AttributeType();
            attr1.Name = "foo-attr";
            attr1.DisplayName = "foo";
            attr1.VariableType = JsonValueType.Decimal;
            attr1.AllocationType = AllocationType.Dynamic;
            attr1.CanGrouping = true;
            attr1.Default = new Json(42);
            attr1.VisualizeRule = "square";

            AttributeTypeList attrs = new AttributeTypeList();
            attrs.Add(attr1.Name, attr1);

            // ResouceType 
            ResourceType type1 = new ResourceType();
            type1.Name = "foo-type";
            type1.DisplayName = "foo";
            type1.Attributes = attrs;
            type1.Behaviors = new BehaviorList();

            ResourceTypeList types = new ResourceTypeList();
            types.Add(type1.Name, type1);

            ResourceHeader header = new ResourceHeader("x", types);
            header.Name = "hoge";
            Write(header); 
        }

        [TestMethod()]
        public void WriteJsonComplexTest()
        {
            // AttributeType 
            AttributeType attr1 = new AttributeType();
            attr1.Name = "foo-attr";
            attr1.DisplayName = "foo";
            attr1.VariableType = JsonValueType.Decimal;
            attr1.AllocationType = AllocationType.Dynamic;
            attr1.CanGrouping = true;
            attr1.Default = new Json(42);
            attr1.VisualizeRule = "square";
 
            AttributeTypeList attrs = new AttributeTypeList();
            attrs.Add(attr1.Name,attr1);


            // Argument
            ArgumentType arg1 = new ArgumentType();
            arg1.Name = "baz";
            arg1.Type = JsonValueType.Boolean;

            ArgumentType arg2 = new ArgumentType();
            arg1.Name = "xyzzy";
            arg1.Type = JsonValueType.String;

            ArgumentTypeList args = new ArgumentTypeList();
            args.Add(arg1);
            args.Add(arg2);

            // Behavior
            Behavior behavior1 = new Behavior();
            behavior1.Name = "foo-behavior";
            behavior1.DisplayName = "behavior";
            behavior1.Arguments = args;


            BehaviorList beharivors = new BehaviorList();
            beharivors.Add(behavior1.Name, behavior1);

            // ResouceType 
            ResourceType type1 = new ResourceType();
            type1.Name = "foo-type";
            type1.DisplayName = "foo";
            type1.Attributes = attrs;
            type1.Behaviors = beharivors;

            ResourceTypeList types = new ResourceTypeList();
            types.Add(type1.Name, type1);
 
            ResourceHeader header = new ResourceHeader("x", types);
            header.Name = "hoge";

           Assert.AreEqual(header,Read(Write(header))); 
        }

        /// <summary>
        ///ReadJson のテスト
        ///</summary>
        [TestMethod()]
        public void ReadJsonAttributeTest()
        {
            ResourceHeader header1 = Read(@"{""foo"":{""Attributes"":{},""Behaviors"":{}}}");
            Assert.AreEqual(0, header1["foo"].Attributes.Count);
            Assert.AreEqual(0, header1["foo"].Behaviors.Count);

            ResourceHeader header_alloc = Read(@"{""foo"":{""Attributes"":{""bar"":{""AllocationType"":""Static""}},""Behaviors"":{}}}");
            Assert.AreEqual(AllocationType.Static, header_alloc["foo"].Attributes["bar"].AllocationType);

            ResourceHeader header_name = Read(@"{""foo"":{""Attributes"":{""bar"":{""DisplayName"":""baz""}},""Behaviors"":{}}}");
            Assert.AreEqual("baz", header_name["foo"].Attributes["bar"].DisplayName);

            ResourceHeader header_group = Read(@"{""foo"":{""Attributes"":{""bar"":{""CanGrouping"":true}},""Behaviors"":{}}}");
            Assert.AreEqual(true, header_group["foo"].Attributes["bar"].CanGrouping);

            ResourceHeader header_type = Read(@"{""foo"":{""Attributes"":{""bar"":{""VariableType"":""String""}},""Behaviors"":{}}}");
            Assert.AreEqual(JsonValueType.String, header_type["foo"].Attributes["bar"].VariableType);
        }

        [TestMethod()]
        public void ReadJsonBehaviorTest()
        {
        }
    }
}
