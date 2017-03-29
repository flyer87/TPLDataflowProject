using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectGraphs;

namespace TPLDataflowUnitTests
{
    [TestClass]
    public class TPLDataflowCorrectnessTests
    {
        [TestMethod]
        public void Test3CrossGraph()
        {
            string creationTime;
            string executionTime;
            var graph = TPLDataflowCheck.Generate3CrossGraph(3, TPLDataflowCheck.LongOperationSum, out creationTime);
            var res = TPLDataflowCheck.Execute3CrossGraph(graph, true, out executionTime);
            Assert.AreEqual(17, res);
        }

        [TestMethod]
        public void TestVectorGraph()
        {
            string creationTime;
            string executionTime;
            var graph = TPLDataflowCheck.GenerateVerticalGraph(1000, TPLDataflowCheck.LongOperation, out creationTime);
            TPLDataflowCheck.ExecuteVerticalGraph(graph, out executionTime); // no result returned in vertical graph
        }

        [TestMethod]
        public void TestSumGraph()
        {
            var graph = TPLDataflowCheck.GenerateSumGraph(5000, TPLDataflowCheck.LongOperationSum);
            var res = TPLDataflowCheck.ExecuteSumGraph(graph, true);
            Assert.AreEqual(5000, res);
        }

        [TestMethod]
        public void Test3AverageGraph()
        {
            var graph = TPLDataflowCheck.Generate3AverageGraph(10, TPLDataflowCheck.LongOperationSum);
            var res = TPLDataflowCheck.Execute3AverageGraph(graph, true);
            Assert.AreEqual(24, res);
        }

        [TestMethod]
        public void TestSumSoFarGraph()
        {            
            var graph = TPLDataflowCheck.GenerateSumSoFarGraph(4, TPLDataflowCheck.LongOperationSum);
            var res = TPLDataflowCheck.ExecuteSumSoFarGraph(graph, true);
            Assert.AreEqual(10, res);
        }

        [TestMethod]
        public void Test2CrossGraph()
        {
            var graph = TPLDataflowCheck.Generate2CrossGraph(4, () => 2, TPLDataflowCheck.LongOperationSum);
            var res = TPLDataflowCheck.Execute2CrossGraph(graph, true);
            Assert.AreEqual(8, res);
        }
    }
}
