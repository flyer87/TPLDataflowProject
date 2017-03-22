using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectGraphs;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var graph = Program.Generate3CrossGraph(3);
            var res = Program.Execute3CrossGraph(graph);
            Assert.AreEqual(17, res);
        }
    }
}
