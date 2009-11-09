using ParserExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    
    
    /// <summary>
    ///StreamTest のテスト クラスです。すべての
    ///StreamTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class StreamTest
    {
        private Stream<char> stream;

        [TestInitialize()]
        public void SetupStream()
        {
            this.stream = new Stream<char>(new char[]{'a','b','c'});
        }

        [TestMethod()]
        public void ReadTest() {
            Assert.AreEqual('a', stream.Read());
            Assert.AreEqual('b', stream.Read());
        }

        [TestMethod()]
        public void PeekTest() {
            Assert.AreEqual('a', stream.Peek());
            Assert.AreEqual('a', stream.Peek());
        }

        [TestMethod()]
        public void IsEmptyTest() {
            Assert.AreEqual(false, stream.IsEmpty());
        }

        [TestMethod()]
        public void EmptyTest() {
            var empty = new Stream<char>(new char[]{});
            Assert.AreEqual(true, empty.IsEmpty());
            Assert.AreEqual(null, empty.Peek());
        }

        [TestMethod()]
        public void SaveRestoreTest() {
            var state = stream.Save();
            Assert.AreEqual('a', stream.Read());
            Assert.AreEqual('b', stream.Read());
            stream.Restore(state);

            Assert.AreEqual('a', stream.Read());
        }

        /*

        /// <summary>
        ///Save のテスト
        ///</summary>
        public void SaveTestHelper<T>()
            where T : struct
        {
            Stream_Accessor<T> target = new Stream_Accessor<T>(); // TODO: 適切な値に初期化してください
            Stream_Accessor<T>.State expected = null; // TODO: 適切な値に初期化してください
            Stream_Accessor<T>.State actual;
            actual = target.Save();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        [TestMethod()]
        [DeploymentItem("ParserExample.exe")]
        public void SaveTest()
        {
            Assert.Inconclusive("T の型の制約を満たす適切な型パラメータが見つかりません。適切な型パラメータで SaveTestHelper<T>() を呼び出してください。");
        }

        /// <summary>
        ///Restore のテスト
        ///</summary>
        public void RestoreTestHelper<T>()
            where T : struct
        {
            // Restore のプライベート アクセサが見つかりません。プライベート アクセサを含むプロジェクトをリビルドするか、または、手動で Publicize.exe を実行してください。
            Assert.Inconclusive("Restore のプライベート アクセサが見つかりません。プライベート アクセサを含むプロジェクトをリビルドするか、または、手動で Publicize.exe を" +
                    "実行してください。");
        }

        [TestMethod()]
        [DeploymentItem("ParserExample.exe")]
        public void RestoreTest()
        {
            Assert.Inconclusive("T の型の制約を満たす適切な型パラメータが見つかりません。適切な型パラメータで RestoreTestHelper<T>() を呼び出してください。");
        }

        /// <summary>
        ///Read のテスト
        ///</summary>
        public void ReadTestHelper<T>()
            where T : struct
        {
            Stream_Accessor<T> target = new Stream_Accessor<T>(); // TODO: 適切な値に初期化してください
            T expected = new T(); // TODO: 適切な値に初期化してください
            T actual;
            actual = target.Read();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        [TestMethod()]
        [DeploymentItem("ParserExample.exe")]
        public void ReadTest()
        {
            Assert.Inconclusive("T の型の制約を満たす適切な型パラメータが見つかりません。適切な型パラメータで ReadTestHelper<T>() を呼び出してください。");
        }

        /// <summary>
        ///Peek のテスト
        ///</summary>
        public void PeekTestHelper<T>()
            where T : struct
        {
            Stream_Accessor<T> target = new Stream_Accessor<T>(); // TODO: 適切な値に初期化してください
            Nullable<T> expected = new Nullable<T>(); // TODO: 適切な値に初期化してください
            Nullable<T> actual;
            actual = target.Peek();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        [TestMethod()]
        [DeploymentItem("ParserExample.exe")]
        public void PeekTest()
        {
            Assert.Inconclusive("T の型の制約を満たす適切な型パラメータが見つかりません。適切な型パラメータで PeekTestHelper<T>() を呼び出してください。");
        }

        /// <summary>
        ///IsEmpty のテスト
        ///</summary>
        public void IsEmptyTestHelper<T>()
            where T : struct
        {
            Stream_Accessor<T> target = new Stream_Accessor<T>(); // TODO: 適切な値に初期化してください
            bool expected = false; // TODO: 適切な値に初期化してください
            bool actual;
            actual = target.IsEmpty();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("このテストメソッドの正確性を確認します。");
        }

        [TestMethod()]
        [DeploymentItem("ParserExample.exe")]
        public void IsEmptyTest()
        {
            Assert.Inconclusive("T の型の制約を満たす適切な型パラメータが見つかりません。適切な型パラメータで IsEmptyTestHelper<T>() を呼び出してください。");
        }

        /// <summary>
        ///Stream`1 コンストラクタ のテスト
        ///</summary>
        public void StreamConstructorTestHelper<T>()
            where T : struct
        {
            T[] xs = null; // TODO: 適切な値に初期化してください
            Stream_Accessor<T> target = new Stream_Accessor<T>(xs);
            Assert.Inconclusive("TODO: ターゲットを確認するためのコードを実装してください");
        }

        [TestMethod()]
        [DeploymentItem("ParserExample.exe")]
        public void StreamConstructorTest()
        {
            Assert.Inconclusive("T の型の制約を満たす適切な型パラメータが見つかりません。適切な型パラメータで StreamConstructorTestHelper<T>() を呼び出してくだ" +
                    "さい。");
        }*/
    }
}
