using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectGraphs;

namespace TPLDataflowUnitTests
{
    [TestClass]
    public class TPLDataflowCorrectnessTests
    {
        [TestMethod]
        public void Test3CrossGraph_3nodes()
        {
            string creationTime;
            string executionTime;
            const int cntNodes = 3;
            var graph = TPLDataflowGraphs.Generate3WayCrossGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.Execute3WayCrossGraph(graph, out executionTime, true);
            Assert.AreEqual(17, res);
        }

        [TestMethod]
        public void Test3CrossGraph_5nodes()
        {
            string creationTime;
            string executionTime;
            const int cntNodes = 5;
            var graph = TPLDataflowGraphs.Generate3WayCrossGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.Execute3WayCrossGraph(graph, out executionTime, true);
            Assert.AreEqual(259, res);
        }

        [TestMethod]
        public void TestLinearGraph_1000()
        {
            string creationTime;
            string executionTime;
            const int cntNodes = 1000;
            var graph = TPLDataflowGraphs.GenerateLinearGraph(cntNodes, TPLDataflowGraphs.LongOperation, out creationTime);
            var res = TPLDataflowGraphs.ExecuteLinearGraph(graph, out executionTime, true); 
            Assert.AreEqual(42, res);
        }

        [TestMethod]
        public void TestFlatGraph_1000()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 1000;
            var graph = TPLDataflowGraphs.GenerateFlatGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteFlatGraph(graph, out executionTime, true);
            Assert.AreEqual(1000, res);
        }

        [TestMethod]
        public void TestSumGraph_5000()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 5000;
            var graph = TPLDataflowGraphs.GenerateFlatGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteFlatGraph(graph, out executionTime, true);
            Assert.AreEqual(5000, res);
        }

        [TestMethod]
        public void Test3AverageGraph_10()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 10;
            var graph = TPLDataflowGraphs.GenerateRunningAverageGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteRunningAverageGraph(graph, out executionTime, true);
            Assert.AreEqual(24, res);
        }

        [TestMethod]
        public void Test3AverageGraph_15()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 15;
            var graph = TPLDataflowGraphs.GenerateRunningAverageGraph(cntNodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteRunningAverageGraph(graph, out executionTime, true);
            Assert.AreEqual(39, res);
        }

        [TestMethod]
        public void TestSumSoFarGraph_4()
        {            
            string creationTime;
            string executionTime;
            int cnt_nodes = 4;
            var graph = TPLDataflowGraphs.GenerateManyDepPrefixSumGraph(cnt_nodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteManyDepPrefixSumGraph(graph, out executionTime, true);
            Assert.AreEqual(10, res);
        }

        [TestMethod]
        public void TestSumSoFarGraph_10()
        {
            string creationTime;
            string executionTime;
            int cnt_nodes = 10;
            var graph = TPLDataflowGraphs.GenerateManyDepPrefixSumGraph(cnt_nodes, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecuteManyDepPrefixSumGraph(graph, out executionTime, true);
            Assert.AreEqual(55, res);
        }

        [TestMethod]
        public void Test2CrossGraph_4()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 4;
            Func<long> f = () => 2; // Why ???
            var graph = TPLDataflowGraphs.GeneratePrefixSumGraph(cntNodes, f, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecutePrefixSumGraph(graph, out executionTime, true);
            Assert.AreEqual(8, res);
        }

        [TestMethod]
        public void Test2CrossGraph_10()
        {
            string creationTime;
            string executionTime;
            int cntNodes = 10;
            Func<long> f = () => 2; // Why ???
            var graph = TPLDataflowGraphs.GeneratePrefixSumGraph(cntNodes, f, TPLDataflowGraphs.LongOperationSum, out creationTime);
            var res = TPLDataflowGraphs.ExecutePrefixSumGraph(graph, out executionTime, true);
            Assert.AreEqual(20, res);
        }
    }
}
