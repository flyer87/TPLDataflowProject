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
            var graph = TPLDataflowGraphs.Generate3CrossGraph(3, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.Execute3CrossGraph(graph, true, out executionTime);
            Assert.AreEqual(17, res);
        }

        [TestMethod]
        public void TestVectorGraph()
        {
            string creationTime;
            string executionTime;
            var graph = TPLDataflowGraphs.GenerateVerticalGraph(1000, TPLDataflowGraphs.LongOperation, out creationTime);
            TPLDataflowGraphs.ExecuteVerticalGraph(graph, out executionTime); // no result returned in vertical graph
        }

        [TestMethod]
        public void TestSumGraph()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 5000;
            var graph = TPLDataflowGraphs.GenerateSumGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteSumGraph(graph, true, out executionTime);
            Assert.AreEqual(5000, res);
        }

        [TestMethod]
        public void Test3AverageGraph()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 10;
            var graph = TPLDataflowGraphs.Generate3AverageGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.Execute3AverageGraph(graph, true, out executionTime);
            Assert.AreEqual(24, res);
        }

        [TestMethod]
        public void TestSumSoFarGraph()
        {            
            string creationTime;
            string executionTime;
            int cnt_nodes = 4;
            var graph = TPLDataflowGraphs.GenerateSumSoFarGraph(cnt_nodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteSumSoFarGraph(graph, true, out executionTime);
            Assert.AreEqual(10, res);
        }

        [TestMethod]
        public void Test2CrossGraph()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 4;
            Func<long> f = () => 2;
            var graph = TPLDataflowGraphs.Generate2CrossGraph(cntNodes, f, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.Execute2CrossGraph(graph, true, out executionTime);
            Assert.AreEqual(8, res);
        }
    }
}
