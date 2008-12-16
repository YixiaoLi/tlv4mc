using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Test
{
    
    
    /// <summary>
    ///JsonTest のテスト クラスです。すべての
    ///JsonTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class JsonTest
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
        ///Value のテスト
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
          Json target = new Json();
          target.Value = 42;
          Assert.AreEqual(target.Value, 42);

          target.Value = "hoge";
          Assert.AreEqual(target.Value, "hoge");

          target.Value = "{,,,}";
          Assert.AreEqual(target.Value, "{,,,}");
        }


        /// <summary>
        /// obj["foo"]によるアクセスのテスト
        ///</summary>
        [TestMethod()]
        public void HashAccessTest()
        {
            Json target = new Json(new Dictionary<string,Json>());
            target["foo"] = new Json(42);
            target["bar"] = new Json("thanks for fish");
            Assert.AreEqual(42, target["foo"].Value);
            Assert.AreEqual("thanks for fish", target["bar"].Value);

            // Json empty = new Json();
            // target["baz"] = empty;
            // Assert.AreEqual(empty, target["baz"].Value);

        }

        /// <summary>
        /// IndexAccess のテスト
        ///</summary>
        [TestMethod()]
        public void IndexAccessTest()
        {
            Json target = new Json(new List<Json>());
            target.Add("foo");
            target.Add("bar");
            target.Add("baz");

            Assert.AreEqual("foo", target[0].Value);
            Assert.AreEqual("bar", target[1].Value);
            Assert.AreEqual("baz", target[2].Value);

            target[0] = new Json("xyzzy");
            Assert.AreEqual("xyzzy", target[0].Value);
        }

        /// <summary>
        ///IsObject のテスト
        ///</summary>
        [TestMethod()]
        public void IsObjectTest()
        {
            Assert.AreEqual(true, (new Json(new Dictionary<String, Json>())).IsObject);
            Assert.AreEqual(false, (new Json(new List<Json>())).IsObject);
            Assert.AreEqual(false, (new Json(42).IsObject));
        }

        /// <summary>
        ///IsArray のテスト
        ///</summary>
        [TestMethod()]
        public void IsArrayTest()
        {
            Assert.AreEqual(false, (new Json(new Dictionary<String, Json>())).IsArray);
            Assert.AreEqual(true, (new Json(new List<Json>())).IsArray);
            Assert.AreEqual(false, (new Json(42).IsArray));
        }

        /// <summary>
        ///Count のテスト
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            Assert.AreEqual(0, (new Json(new List<Json>(0))).Count);
            Json array = new Json(new List<Json>());
            array.Add(""); array.Add(""); array.Add("");

            Assert.AreEqual(3, array.Count);

            // etc
            Assert.AreEqual(0, (new Json(new Dictionary<String, Json>())).Count);
            Assert.AreEqual(0, (new Json(42).Count));

        }

        /// <summary>
        ///ToString のテスト
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("foo", (new Json("foo")).ToString());
            Assert.AreEqual("42", (new Json(42)).ToString());

            Json array = new Json(new List<Json>());
            array.Add("foo");
            array.Add("bar");
            array.Add("baz");
            Assert.IsNotNull(array.ToString());

            Json hash = new Json(new Dictionary<String,Json>());
            hash.Add("foo", 42);
            hash.Add("bar", 42);
            hash.Add("baz", 42);
            Assert.IsNotNull(hash.ToString());
        }

        /// <summary>
        ///System.Collections.IEnumerable.GetEnumerator のテスト
        ///</summary>
        [TestMethod()]
        [DeploymentItem("NU.OJL.MPRTOS.TLV.Base.dll")]
        public void GetEnumeratorTest1()
        {
            Json array = new Json(new List<Json>());
            array.Add("foo");
            array.Add("bar");
            array.Add("baz");
            int i = 0;
            foreach (Json json in array)
            {
               Assert.AreEqual(array[i], json); 
               ++i;
            }
            Assert.AreEqual(3, i);

           // ---
           Json hash = new Json(new Dictionary<string,Json>());
           hash.Add("foo", 42);
           hash.Add("bar", 43);
           hash.Add("baz", 44);
           int j = 0;
           foreach (KeyValuePair<string, Json> pair in hash.GetKeyValuePairEnumerator()){
               switch (j) { 
                   case 0:
                       Assert.AreEqual("foo", pair.Key);
                       Assert.AreEqual(42, pair.Value.Value);
                       break;
                   case 1:
                       Assert.AreEqual("bar", pair.Key);
                       Assert.AreEqual(43, pair.Value.Value);
                       break;
                   case 2:
                       Assert.AreEqual("baz", pair.Key);
                       Assert.AreEqual(44, pair.Value.Value);
                       break;
               }
               ++j;
           }
           Assert.AreEqual(3, j);
        }

        /// <summary>
        ///op_Implicit のテスト
        ///</summary>
        [TestMethod()]
        public void op_StringTest()
        {
            Json json = new Json("foo");
            Assert.AreEqual("foo", (string)json);
        }

        /// <summary>
        ///op_Implicit のテスト
        ///</summary>
        [TestMethod()]
        public void op_DecimalTest()
        {
            Json json = new Json((decimal)10);
            Assert.AreEqual((decimal)10, (decimal)json);
        }

        /// <summary>
        ///op_Implicit のテスト
        ///</summary>
        [TestMethod()]
        public void op_BoolTest()
        {
            Assert.AreEqual(true, (bool)(new Json(true)));
            Assert.AreEqual(false, (bool)(new Json(false)));
        }

        /// <summary>
        ///op_Implicit のテスト
        ///</summary>
        [TestMethod()]
        public void op_ListTest()
        {
            List<Json> list = new List<Json>();
            list.Add(new Json(10));
            list.Add(new Json(11));
            list.Add(new Json(12));

            Json jsonValue = new Json(list);
            Assert.AreEqual(list,(List<Json>)jsonValue);
        }

        /// <summary>
        ///op_Implicit のテスト
        ///</summary>
        [TestMethod()]
        public void op_DictTest()
        {
            Dictionary<string, Json> dict = new Dictionary<string, Json>();
            dict.Add("foo", new Json(42));
            dict.Add("bar", new Json("x"));
            dict.Add("baz", new Json("y"));
            Json jsonValue = new Json(dict);

            Assert.AreEqual(dict, (Dictionary<string, Json>)jsonValue);
        }

        /// <summary>
        ///IndexOf のテスト
        ///</summary>
        [TestMethod()]
        public void IndexOfTest()
        {
            Json foo = new Json("foo");
            Json bar = new Json("bar");
            Json baz = new Json("baz");
            List<Json> list = new List<Json>();
            list.Add(foo);
            list.Add(bar);
            list.Add(baz);
            list.Add(bar);

            Json target = new Json(list);
            target.Add("foo");
            target.Add("bar");
            target.Add("baz");
            target.Add("bar");

            Assert.AreEqual(0, target.IndexOf(foo));
            Assert.AreEqual(1, target.IndexOf(bar));
        }

        /// <summary>
        ///ContainsKey のテスト
        ///</summary>
        [TestMethod()]
        public void ContainsKeyHashTest()
        {
            Json target = new Json(new Dictionary<string,Json>());
            target.Add("foo", 0);
            target.Add("bar", 0);
            target.Add("baz", 0);
            Assert.AreEqual(true, target.ContainsKey("foo"));
            Assert.AreEqual(true, target.ContainsKey("baz"));

        }

        /// <summary>
        ///ContainsKey のテスト
        ///</summary>
        [TestMethod()]
        public void ContainsKeyListTest()
        {
            Json target = new Json(new List<Json>());
            target.Add("foo");
            target.Add("bar");
            Assert.AreEqual(true, target.ContainsKey(0));
            // See ticket#14
            Assert.AreEqual(false, target.ContainsKey(2));

        }

        /// <summary>
        ///AddObject のテスト
        ///</summary>
        [TestMethod()]
        public void AddObjectTest()
        {
            Json target = new Json(new Dictionary<string, Json>());
            target.AddObject("foo");
            Assert.IsTrue(target["foo"].IsObject);
        }

        /// <summary>
        ///AddArray のテスト
        ///</summary>
        [TestMethod()]
        public void AddArrayTest()
        {
            Json target = new Json(new Dictionary<string, Json>());
            target.AddArray("foo");
            Assert.IsTrue(target["foo"].IsArray);
        }

        /// <summary>
        ///Add のテスト
        ///</summary>
        [TestMethod()]
        public void AddTest1()
        {
            Json target = new Json(new List<Json>());
            target.Add("foo");
            Assert.AreEqual("foo", target[0].Value); 
        }

        /// <summary>
        ///Add のテスト
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            Json target = new Json(new Dictionary<string,Json>());
            target.Add("foo", 42);

            Assert.AreEqual(42, target["foo"].Value); 
        }

        /// <summary>
        ///Json コンストラクタ のテスト
        ///</summary>
        [TestMethod()]
        public void JsonConstructorTest1()
        {
            Json target = new Json();
            Assert.IsNull(target.Value); 
        }

        /// <summary>
        ///Json コンストラクタ のテスト
        ///</summary>
        [TestMethod()]
        public void JsonConstructorTest()
        {
            Json target = new Json(42);
            Assert.AreEqual(42, target.Value);
        }
    }
}
