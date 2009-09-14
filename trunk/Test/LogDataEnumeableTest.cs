
using NU.OJL.MPRTOS.TLV.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace Test
{
    
    
    /// <summary>
    ///LogDataEnumeableTest のテスト クラスです。すべての
    ///LogDataEnumeableTest 単体テストをここに含めます
    ///</summary>
	[TestClass()]
	public class LogDataEnumeableTest
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
		///op_Addition のテスト
		///</summary>
		[TestMethod()]
		public void op_AdditionTest()
		{
			LogDataEnumeable left = new LogDataEnumeable(
				new LogData[]{
					new LogData(new Time("100",10), new Resource(){ Name = "a"}){ Id = 1},
					new LogData(new Time("200",10), new Resource(){ Name = "b"}){ Id = 3},
					new LogData(new Time("300",10), new Resource(){ Name = "c"}){ Id = 5},
					new LogData(new Time("400",10), new Resource(){ Name = "d"}){ Id = 7},
					new LogData(new Time("500",10), new Resource(){ Name = "e"}){ Id = 9},
					new LogData(new Time("600",10), new Resource(){ Name = "f"}){ Id = 11}
				}); // TODO: 適切な値に初期化してください
			LogDataEnumeable right = new LogDataEnumeable(
				new LogData[]{
					new LogData(new Time("100",10), new Resource(){ Name = "a"}){ Id = 1},
					new LogData(new Time("200",10), new Resource(){ Name = "B"}){ Id = 4},
					new LogData(new Time("300",10), new Resource(){ Name = "c"}){ Id = 5},
					new LogData(new Time("450",10), new Resource(){ Name = "D"}){ Id = 8},
					new LogData(new Time("500",10), new Resource(){ Name = "e"}){ Id = 9},
					new LogData(new Time("650",10), new Resource(){ Name = "F"}){ Id = 12}
				}); // TODO: 適切な値に初期化してください
			LogDataEnumeable expected = new LogDataEnumeable(
				new LogData[]{
					new LogData(new Time("100",10), new Resource(){ Name = "a"}){ Id = 1},
					new LogData(new Time("200",10), new Resource(){ Name = "b"}){ Id = 3},
					new LogData(new Time("200",10), new Resource(){ Name = "B"}){ Id = 4},
					new LogData(new Time("300",10), new Resource(){ Name = "c"}){ Id = 5},
					new LogData(new Time("400",10), new Resource(){ Name = "d"}){ Id = 7},
					new LogData(new Time("450",10), new Resource(){ Name = "D"}){ Id = 8},
					new LogData(new Time("500",10), new Resource(){ Name = "e"}){ Id = 9},
					new LogData(new Time("600",10), new Resource(){ Name = "f"}){ Id = 11},
					new LogData(new Time("650",10), new Resource(){ Name = "F"}){ Id = 12}
				}); // TODO: 適切な値に初期化してください
			LogDataEnumeable actual;
			actual = (left + right);

			for (int i = 0; i < expected.List.Count; i++ )
			{
				Assert.AreEqual(expected.List[i].Id, actual.List[i].Id);
			}
		}


	}
}
