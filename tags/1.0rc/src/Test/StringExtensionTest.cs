using NU.OJL.MPRTOS.TLV.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    
    
    /// <summary>
    ///StringExtensionTest のテスト クラスです。すべての
    ///StringExtensionTest 単体テストをここに含めます
    ///</summary>
	[TestClass()]
	public class StringExtensionTest
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

		public void test(string st, decimal de, int radix)
		{
			Assert.AreEqual(de, st.ToDecimal(radix));
		}


		/// <summary>
		///ToDecimal のテスト
		///</summary>
		[TestMethod()]
		public void ToDecimalTest()
		{
			test("1", 1m, 10);
			test("10", 10m, 10);
			test("0.1", 0.1m, 10);
			test("0.001", 0.001m, 10);
			test("a", 10m, 16);
			test("b", 11m, 16);
			test("af", 175m, 16);
			test("64", 100m, 16);
			test("144", 100m, 8);
			test("10201", 100m, 3);
			test("1100100", 100m, 2);
			test("A.4", 10.25m, 16);
			test("12.2", 10.25m, 8);
			test("1010.01", 10.25m, 2);
			test("0.5", 0.625m, 8);
			test("0.A", 0.625m, 16);
			test("20ca819", 23424324m, 15);
			test("3f", 123m, 36);
			test("10", 36m, 36);
			test("g", 16m, 36);
		}
	}
}
