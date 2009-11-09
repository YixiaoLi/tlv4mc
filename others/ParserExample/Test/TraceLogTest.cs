using ParserExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    
    
    /// <summary>
    ///TraceLogTest のテスト クラスです。すべての
    ///TraceLogTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class TraceLogTest
    {
        [TestMethod()]
        public void ObjectNameTest()
        {
            TraceLog target = new TraceLog("Task1");
            Assert.AreEqual("Task1", target.Object);
            Assert.AreEqual("Task1", target.ObjectName);
            Assert.AreEqual(null, target.ObjectType);
        }
        
        [TestMethod()]
        public void ObjectTypeTest() {
            TraceLog target = new TraceLog("TASK(id=1)");
            Assert.AreEqual("TASK(id=1)",target.Object);
            Assert.AreEqual(null, target.ObjectName);
            Assert.AreEqual("TASK(id=1)", target.ObjectType);
        }
    }
}
