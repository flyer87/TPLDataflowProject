﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace ProjectGraphs
{
    public class TPLDataflowGraphs
    {
        const int INPUT_VALUE = 1;

        static void Main(string[] args)
        {
            StartFramework();           
        }

        public static void StartFramework()
        {
            PrintSystemInfo();

            PerformanceTestFlatGraph();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PerformanceTestLinearGraph();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PerformanceTestPrefixSumGraph();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PerformanceTestManyDependencyPrefixSumGraph();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PerformanceTestRunningAverageGraph();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PerformanceTest3WayCrossGraph();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void PerformanceTestLinearGraph()
        {
            const int CNT_START_NODES = 32;
            const int CNT_END_NODES = 8192;
            const string TEST_STRING = "Performance test: Linear Graph";

            Console.WriteLine(TEST_STRING);
            Console.WriteLine("  Nodes | Creation time, ms | Exec. time(Avg; St.dev), ms");
            for (int cntNodes = CNT_START_NODES; cntNodes <= CNT_END_NODES; cntNodes = cntNodes * 2)
            {
                try
                {
                    string creationTime;
                    string executionTime;
                    var graph = GenerateLinearGraph(cntNodes, LongOperation, out creationTime); // GenerateManyDepPrefixSumGraph(cntNodes, LongOperationArray); // GenerateLinearGraph(1000, LongOperation);
                    //Thread.Sleep(100);
                    ExecuteLinearGraph(graph, out executionTime);
                    Console.WriteLine(String.Format("{0,7} | {1,17} | {2,25}", cntNodes, creationTime, executionTime));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void PerformanceTest3WayCrossGraph()
        {
            const int CNT_START_NODES = 32;
            const int CNT_END_NODES = 8192;
            const string TEST_STRING = "Performance test: 3-way Cross graph";

            Console.WriteLine(TEST_STRING);
            Console.WriteLine("  Nodes | Creation time, ms | Exec. time(Avg; St.dev), ms");
            for (int cntNodes = CNT_START_NODES; cntNodes <= CNT_END_NODES; cntNodes = cntNodes * 2)
            {
                try
                {
                    string creationTime;
                    string executionTime;
                    var graph = Generate3WayCrossGraph(cntNodes, LongOperationArray, out creationTime); 
                    Thread.Sleep(100);
                    Execute3WayCrossGraph(graph, out executionTime);  
                    Console.WriteLine(String.Format("{0,7} | {1,17} | {2,25}", cntNodes, creationTime, executionTime));
                }
                catch (Exception)
                {                   
                }
            }            
        }

        public static void PerformanceTestFlatGraph()
        {
            const int START_NODES_CNT = 32;
            const int END_NODES_CNT = 8192;
            const string TEST_STRING = "Performance test: Flat graph";

            Console.WriteLine(TEST_STRING);
            Console.WriteLine("  Nodes | Creation time, ms | Execution time(Avg; St.dev), ms");
            for (int cntNodes = START_NODES_CNT; cntNodes <= END_NODES_CNT; cntNodes = cntNodes * 2)
            {
                try
                {
                    string creationTime;
                    string executionTime;
                    var graph = GenerateFlatGraph(cntNodes, LongOperationArray, out creationTime); 
                    Thread.Sleep(100);
                    ExecuteFlatGraph(graph, out executionTime); 
                    Console.WriteLine(String.Format("{0,7} | {1,17} | {2,25}", cntNodes, creationTime, executionTime));
                }
                catch (Exception)
                {                    
                }
            }
        }

        public static void PerformanceTestPrefixSumGraph()
        {
            const int START_NODES_CNT = 32;
            const int END_NODES_CNT = 8192;
            const string TEST_STRING = "Performance test: Prefix sum graph";

            Console.WriteLine(TEST_STRING);
            Console.WriteLine("  Nodes | Creation time, ms | Execution time(Avg; St.dev), ms");
            for (int cntNodes = START_NODES_CNT; cntNodes <= END_NODES_CNT; cntNodes = cntNodes * 2)
            {
                try
                {
                    string creationTime;
                    string executionTime;
                    var graph = GeneratePrefixSumGraph(cntNodes, LongOperation, LongOperationArray, out creationTime);
                    Thread.Sleep(100);
                    ExecutePrefixSumGraph(graph, out executionTime); 
                    Console.WriteLine(String.Format("{0,7} | {1,17} | {2,25}", cntNodes, creationTime, executionTime));
                }
                catch (Exception)
                {                    
                }
            }
        }

        public static void PerformanceTestManyDependencyPrefixSumGraph()
        {
            const int CNT_START_NODES = 32;
            const int CNT_END_NODES = 8192;
            const string TEST_STRING = "Performance test: Many-dependency prefix graph";

            Console.WriteLine(TEST_STRING);
            Console.WriteLine("  Nodes | Creation time, ms | Execution time(Avg; St.dev), ms");
            for (int cntNodes = CNT_START_NODES; cntNodes <= CNT_END_NODES; cntNodes = cntNodes * 2)
            {
                try
                {
                    string creationTime;
                    string executionTime;
                    var graph = GenerateManyDepPrefixSumGraph(cntNodes, LongOperationArray, out creationTime); 
                    Thread.Sleep(100);
                    ExecuteManyDepPrefixSumGraph(graph, out executionTime);
                    Console.WriteLine(String.Format("{0,7} | {1,17} | {2,25}", cntNodes, creationTime, executionTime));
                }
                catch (Exception)
                {                   
                }
            }
        }

        public static void PerformanceTestRunningAverageGraph()
        {
            const int START_NODES_CNT = 32;
            const int END_NODES_CNT = 8192;
            const string TEST_STRING = "Performance test: Running average graph";

            Console.WriteLine(TEST_STRING);
            Console.WriteLine("  Nodes | Creation time, ms | Execution time(Avg; St.dev), ms");
            for (int cntNodes = START_NODES_CNT; cntNodes <= END_NODES_CNT; cntNodes = cntNodes * 2)
            {
                try
                {
                    string creationTime;
                    string executionTime;
                    var graph = GenerateRunningAverageGraph(cntNodes, LongOperationArray, out creationTime); 
                    Thread.Sleep(100);
                    ExecuteRunningAverageGraph(graph, out executionTime); 
                    Console.WriteLine(String.Format("{0,7} | {1,17} | {2,25}", cntNodes, creationTime, executionTime));
                }
                catch (Exception)
                {
                    //Console.WriteLine("Exception caugth");
                }
            }
        }

        public static Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> GenerateManyDepPrefixSumGraph(int width,
            Func<long[], long> f, out string creationTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            // create
            var firstLayer = new BroadcastBlock<long>[width];
            var secondLayer = new IPropagatorBlock<long, long>[width];
            for (int i = 0; i < width; i++)
            {
                firstLayer[i] = new BroadcastBlock<long>(null);
                secondLayer[i] = CreateManyDepPrefixSumNode(i + 1, f);
            }

            // linking
            for (int i = 0; i < width; i++)
            {
                for (int k = i; k < width; k++)
                {
                    firstLayer[i].LinkTo(secondLayer[k]);
                }
            }

            sw.Stop();            
            const string format = "{0,10:F3}";
            creationTime = string.Format(format, sw.Elapsed.Milliseconds);

            return new Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]>(firstLayer, secondLayer);
        }

        public static long ExecuteManyDepPrefixSumGraph(Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> graph,
            out string executionTime, bool isTestForCorrectness = false)
        {          
            executionTime = "";
            long result = 0;
            if (isTestForCorrectness)
            {
                int cntLayerNodes = graph.Item1.Length;
                for (int nodeId = 0; nodeId < cntLayerNodes; nodeId++)
                {
                    graph.Item1[nodeId].Post(1);
                }

                for (int nodeId = 0; nodeId < cntLayerNodes; nodeId++)
                {
                    result += graph.Item2[nodeId].Receive();
                }
            }
            else                
                executionTime = BenchMark("Many-dependency prefix sum graph", (x) =>
                {
                    int cntLayerNodes = graph.Item1.Length;          
                    for (int nodeId = 0; nodeId < cntLayerNodes; nodeId++)
                    {
                        graph.Item1[nodeId].Post(1);
                    }

                    for (int nodeId = 0; nodeId < cntLayerNodes; nodeId++)
                    {
                        graph.Item2[nodeId].Receive();
                    }

                    return 1;
                });

            return result;
        }

        public static Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> GenerateRunningAverageGraph(int width,
            Func<long[], long> f, out string creationTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            // create nodes
            IPropagatorBlock<long, long>[] firstLayer = new IPropagatorBlock<long, long>[width];
            for (int i = 0; i < width; i++)
            {
                firstLayer[i] = new BroadcastBlock<long>(x => x);
            }

            IPropagatorBlock<long, long>[] secondLayer = new IPropagatorBlock<long, long>[width - 2];
            for (int i = 0; i < width - 2; i++)
            {
                secondLayer[i] = CreateRunningAverageNode(f);
            }

            // link nodes - iterate over the 2nd layer
            for (int i = 0; i < width - 2; i++)
            {
                firstLayer[i].LinkTo(secondLayer[i]);
                firstLayer[i + 1].LinkTo(secondLayer[i]);
                firstLayer[i + 2].LinkTo(secondLayer[i]);
            }

            sw.Stop();
            //Console.WriteLine("Creation: {0}", sw.Elapsed.Milliseconds);
            const string format = "{0,10:F3}";
            creationTime = string.Format(format, sw.Elapsed.Milliseconds);

            return new Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]>(firstLayer, secondLayer);
        }

        public static long ExecuteRunningAverageGraph(Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> graph,
             out string executionTime, bool isTestForCorrectness = false)
        {
            executionTime = "";
            long result = 0;
            if (isTestForCorrectness)
            {
                int depth = graph.Item1.Length;
                for (int col = 0; col < depth; col++)
                {
                    graph.Item1[col].Post(1);
                }

                for (int col = 0; col < depth - 2; col++)
                {
                    result += graph.Item2[col].Receive();
                }
            }
            else
                executionTime = BenchMark("Running average graph", x =>
                {
                    int width = graph.Item1.Length;
                                        
                    for (int col = 0; col < width; col++)
                    {
                        graph.Item1[col].Post(x);                                                
                    }
                    
                    for (int col = 0; col < width - 2; col++)
                    {                        
                        graph.Item2[col].Receive();
                    }

                    return x;
                });

            return result;
        }

        public static Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> GeneratePrefixSumGraph(int depth,
            Func<long> f1, Func<long[], long> f2, out string creationTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            const int WIDTH = 2;
            IPropagatorBlock<long, long>[,] graph = new IPropagatorBlock<long, long>[depth, WIDTH];

            // creation
            for (int row = 0; row < depth; row++)
            {
                if (row == 0) // first row
                {
                    graph[row, 0] = new BroadcastBlock<long>(x => x);
                    graph[row, 1] = new BroadcastBlock<long>(x => x);
                }
                else // other rows 
                {
                    graph[row, 0] = CreatePrefixSumNode_1(f1);
                    graph[row, 1] = CreatePrefixSumNode_2(f2);
                }
            }

            // linking
            for (int row = 0; row < depth - 1; row++)
            {
                graph[row, 0].LinkTo(graph[row + 1, 0]);
                graph[row, 0].LinkTo(graph[row + 1, 1]);

                graph[row, 1].LinkTo(graph[row + 1, 1]);
            }

            // return the first and the last layer
            IPropagatorBlock<long, long>[] firstLayer = new BroadcastBlock<long>[WIDTH];
            firstLayer[0] = graph[0, 0]; // only copies the ref.!!!
            firstLayer[1] = graph[0, 1]; // only copies the ref.!!!

            IPropagatorBlock<long, long>[] lastLayer = new IPropagatorBlock<long, long>[WIDTH];
            lastLayer[0] = graph[depth - 1, 0]; // only copies the ref.!!!
            lastLayer[1] = graph[depth - 1, 1]; // only copies the ref.!!!

            sw.Stop();
            //Console.WriteLine("Creation {0}", sw.Elapsed.Milliseconds);
            const string format = "{0,10:F3}";
            creationTime = string.Format(format, sw.Elapsed.Milliseconds);

            return new Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]>(firstLayer, lastLayer);
        }

        public static long ExecutePrefixSumGraph(Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> graph,
            out string executionTime, bool isTestForCorrectness = false)
        {

            executionTime = "";
            long result = 0;
            if (isTestForCorrectness)
            {
                graph.Item1[0].Post(1);
                graph.Item1[1].Post(1);

                result += graph.Item2[0].Receive(); // should finish first
                result += graph.Item2[1].Receive(); // should take longer creationTime
            }
            else
                executionTime = BenchMark("Prefix sum graph", x =>
                {
                    graph.Item1[0].Post(1);
                    graph.Item1[1].Post(1);

                    long res = 0;
                    res += graph.Item2[0].Receive(); // should finish first
                    res += graph.Item2[1].Receive(); // should take longer creationTime
                    //Console.WriteLine(res);
                    return res;
                });

            return result;
        }

        public static Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> Generate3WayCrossGraph(int cntNodes,
            Func<long[], long> f, out string creationTime)
        {
            int cntRows = cntNodes;
            int cntCols = cntNodes;
            IPropagatorBlock<long, long>[,] matrix = new IPropagatorBlock<long, long>[cntRows, cntCols];

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // creation
            for (int row = 0; row < cntRows; row++)
            {
                if (row == 0)
                {
                    for (int col = 0; col < cntCols; col++)
                    {
                        matrix[row, col] = new BroadcastBlock<long>(x => x);
                    }
                }

                if (row > 0)
                {
                    for (int col = 0; col < cntCols; col++)
                    {
                        if (col == 0 || col == cntCols - 1)
                        {
                            matrix[row, col] = Create3WayCrossNode(2, f);
                        }
                        else
                        {
                            matrix[row, col] = Create3WayCrossNode(3, f);
                        }
                    }
                }
            }

            // linking
            for (int row = 0; row < cntRows - 1; row++)
            {
                for (int col = 0; col < cntCols; col++)
                {
                    if (col == 0)
                    {
                        matrix[row, col].LinkTo(matrix[row + 1, col]);
                        matrix[row, col].LinkTo(matrix[row + 1, col + 1]);
                    }

                    if (col > 0 && col < cntCols - 1)
                    {
                        matrix[row, col].LinkTo(matrix[row + 1, col - 1]);
                        matrix[row, col].LinkTo(matrix[row + 1, col]);
                        matrix[row, col].LinkTo(matrix[row + 1, col + 1]);
                    }

                    if (col == cntCols - 1)
                    {
                        matrix[row, col].LinkTo(matrix[row + 1, col - 1]);
                        matrix[row, col].LinkTo(matrix[row + 1, col]);
                    }
                }
            }

            // first layer
            var firstLayer = new IPropagatorBlock<long, long>[cntCols];
            for (int i = 0; i < cntCols; i++)
            {
                firstLayer[i] = matrix[0, i];
            }

            //last layer
            var lastLayer = new IPropagatorBlock<long, long>[cntCols];
            for (int i = 0; i < cntCols; i++)
            {
                lastLayer[i] = matrix[cntRows - 1, i];
            }

            sw.Stop();
            //Console.WriteLine("Creation creationTime: {0}", sw.Elapsed);
            const string format = "{0,10:F3}";
            creationTime = string.Format(format, sw.Elapsed.Milliseconds);

            return new Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]>(firstLayer, lastLayer);
        }

        public static long Execute3WayCrossGraph(Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long, long>[]> graph,
            out string executionTime, bool isTestForCorrectness = false)
        {
            int cntNodes = graph.Item1.Count();
            //int cntAttempts = 50;
            long result = 0;
            //double elapsedTimeSum = 0;
            
            executionTime = "";
            if (isTestForCorrectness)
            {
                for (int i = 0; i < graph.Item1.Length; i++)
                {
                    graph.Item1[i].Post(1);
                }

                foreach (var node in graph.Item2)
                {
                    result += node.Receive();
                }
            }
            else
                executionTime = BenchMark("3way cross graph", x =>
                {
                    for (int i = 0; i < graph.Item1.Length; i++)
                    {
                        graph.Item1[i].Post(1);
                    }


                    long res = 0;
                    foreach (var node in graph.Item2)
                    {
                        res += node.Receive();
                    }

                    return (int)res / 5;
                });
            
            return result;
        }

        public static long ExecuteFlatGraph(Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long[], long>> graph,
            out string executionTime, bool isTestForCorrectness = false)
        {
            executionTime = "";
            long result = 0;
            if (isTestForCorrectness)
            {
                foreach (var node in graph.Item1)
                {
                    node.Post(1);
                }

                result = graph.Item2.Receive();
            }
            else
                executionTime = BenchMark("Flat graph", (x) =>
                {                    
                    foreach (var node in graph.Item1)
                    {                 
                        node.Post(x);
                    }
                    
                    var res = graph.Item2.Receive();

                    return x;
                });

            return result;
        }

        public static Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long[], long>> GenerateFlatGraph(int cntNodes,
            Func<long[], long> f, out string creationTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            // creation
            IPropagatorBlock<long, long>[] nodes = new BufferBlock<long>[cntNodes];
            for (int i = 0; i < cntNodes; i++)
            {
                nodes[i] = new BufferBlock<long>();
            }

            BatchBlock<long> batcher = new BatchBlock<long>(cntNodes, new GroupingDataflowBlockOptions() { Greedy = true });
            IPropagatorBlock<long[], long> transformer = new TransformBlock<long[], long>(x => f(x));  // f = LongOperationArray

            // linking
            foreach (var item in nodes)
            {
                item.LinkTo(batcher);
            }
            batcher.LinkTo(transformer);
            //Console.WriteLine("Creation creationTime: {0}", sw.Elapsed.Milliseconds);
            const string format = "{0,10:F3}";
            creationTime = string.Format(format, sw.Elapsed.Milliseconds); 

            return new Tuple<IPropagatorBlock<long, long>[], IPropagatorBlock<long[], long>>(nodes, transformer);
        }

        public static long ExecuteLinearGraph(Tuple<IPropagatorBlock<long, long>, IPropagatorBlock<long, long>> graph,
            out string executionTime, bool isTestForCorrectness = false)
        {
            executionTime = "";
            long result = 0;
            if (isTestForCorrectness)
            {
                graph.Item1.Post(1);
                result = graph.Item2.Receive();                
            }
            else
            {
                executionTime = BenchMark("Linear graph", (x) =>
                {
                    graph.Item1.Post(x);
                    long res = graph.Item2.Receive();
                    return res;
                });
            }
            return result;
        }

        public static Tuple<IPropagatorBlock<long, long>, IPropagatorBlock<long, long>> GenerateLinearGraph(int cntNodes,
            Func<long> f, out string creationTime)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // creation
            IPropagatorBlock<long, long>[] vertNodes = new IPropagatorBlock<long, long>[cntNodes];
            vertNodes[0] = new BufferBlock<long>();
            for (int i = 1; i < cntNodes; i++)
            {
                vertNodes[i] = new TransformBlock<long, long>(x => f()); // f = LongOperation() or some other
            }

            var linkOpt = new DataflowLinkOptions() { PropagateCompletion = true };

            // connections
            for (int i = 1; i < cntNodes; i++)
            {
                vertNodes[i - 1].LinkTo(vertNodes[i], linkOpt);
            }

            sw.Stop();
            //Console.WriteLine("Creation creationTime: " + sw.Elapsed);
            const string format = "{0,10:F3}";
            creationTime = string.Format(format, sw.Elapsed.Milliseconds);

            return new Tuple<IPropagatorBlock<long, long>, IPropagatorBlock<long, long>>(vertNodes[0], vertNodes[vertNodes.Length - 1]);
        }

        public static long LongOperation()
        {
            return isPrime(89) ? 42 : 37;
        }

        public static long LongOperationSum(long[] inpArray)
        {
            long res = 0;
            for (int i = 0; i < inpArray.Length; i++)
            {
                res += inpArray[i];
            }
            return res;
        }

        public static long LongOperationArray(long[] inpArray)
        {
            long res = 0;
            for (int i = 0; i < inpArray.Length; i++)
            {
                res += LongOperation();
            }
            return res;
        }

        public static bool isPrime(long n)
        {
            int k = 2;
            while (k * k <= n && n % k != 0)
                k++;
            return n >= 2 && k * k > n;
        }

        private static IPropagatorBlock<long, long> Create3WayCrossNode(int batchSize, Func<long[], long> f)
        {
            // creation
            IPropagatorBlock<long, long[]> target = new BatchBlock<long>(batchSize, new GroupingDataflowBlockOptions() { Greedy = true });
            IPropagatorBlock<long[], long> middle = new TransformBlock<long[], long>(x => f(x)); // f =  LongOperationArray            
            IPropagatorBlock<long, long> source = new BroadcastBlock<long>(x => x);

            // connection
            target.LinkTo(middle);
            middle.LinkTo(source);

            // completion
            target.Completion.ContinueWith(completion =>
            {
                if (completion.IsFaulted)
                    ((IDataflowBlock)middle).Fault(completion.Exception);
                else
                    middle.Complete();
            });

            middle.Completion.ContinueWith(completion =>
            {
                if (completion.IsFaulted)
                    ((IDataflowBlock)source).Fault(completion.Exception);
                else
                    source.Complete();
            });

            return DataflowBlock.Encapsulate(target, source);
        }

        private static IPropagatorBlock<long, long> CreatePrefixSumNode_1(Func<long> f)
        {
            var target = new TransformBlock<long, long>(x => f()); // f = LongOperation
            var source = new BroadcastBlock<long>(x => x);

            // linking
            target.LinkTo(source);

            // completion            
            target.Completion.ContinueWith(completion =>
            {
                if (completion.IsFaulted)
                    ((IDataflowBlock)source).Fault(completion.Exception);
                else
                    source.Complete();
            });

            return DataflowBlock.Encapsulate(target, source);
        }

        private static IPropagatorBlock<long, long> CreatePrefixSumNode_2(Func<long[], long> f)
        {
            const int BATCHSIZE = 2;
            // creation
            var target = new BatchBlock<long>(BATCHSIZE, new GroupingDataflowBlockOptions() { Greedy = true });
            var source = new TransformBlock<long[], long>(x => f(x)); // f = LongOperationArray

            // linking
            target.LinkTo(source);

            // completion            
            target.Completion.ContinueWith(completion =>
            {
                if (completion.IsFaulted)
                    ((IDataflowBlock)source).Fault(completion.Exception);
                else
                    source.Complete();
            });

            return DataflowBlock.Encapsulate(target, source);
        }

        private static IPropagatorBlock<long, long> CreateRunningAverageNode(Func<long[], long> f)
        {
            const int BATCH_SIZE = 3;
            // create
            var target = new BatchBlock<long>(BATCH_SIZE);
            var source = new TransformBlock<long[], long>(x => f(x)); // f = LongOperationArray

            // link
            target.LinkTo(source);

            // completion            
            target.Completion.ContinueWith(completion =>
            {
                if (completion.IsFaulted)
                    ((IDataflowBlock)source).Fault(completion.Exception);
                else
                    source.Complete();
            });

            return DataflowBlock.Encapsulate(target, source);
        }

        private static IPropagatorBlock<long, long> CreateManyDepPrefixSumNode(int batchSize, Func<long[], long> f)
        {
            // create
            var target = new BatchBlock<long>(batchSize);
            var source = new TransformBlock<long[], long>(x => f(x)); // f = LongOperationArray

            // link
            target.LinkTo(source);

            // completion            
            target.Completion.ContinueWith(completion =>
            {
                if (completion.IsFaulted)
                    ((IDataflowBlock)source).Fault(completion.Exception);
                else
                    source.Complete();
            });

            return DataflowBlock.Encapsulate(target, source);
        }

        private static string BenchMark(String msg, Func<long, long> f)
        {
            int n = 50;
            double dummy = 0.0, st = 0.0, sst = 0.0, totalTime = 0.0;
            int iterations = 0;

            int i = 0;
            while (i < n && totalTime < 15000)
            {                
                Stopwatch sw = Stopwatch.StartNew();
                long res = f(i);
                dummy += res;                
                sw.Stop();
                i++;

                //Thread.Sleep(5);
                double time = sw.ElapsedMilliseconds;
                totalTime += time;                
                if (i < 3) continue;

                iterations++;
                st += time;
                sst += time * time;
            }
            
            double mean = st / iterations, sdev = Math.Sqrt((sst - mean * mean * iterations) / (iterations - 1));
            const string format = "{1,7:F3} | {2,7:F3}";
            string formatedStr = string.Format(format, msg, mean, sdev);            
            return formatedStr;
        }

        private static void PrintSystemInfo()
        {
            Console.WriteLine("System info:");
            Console.WriteLine("OS: {0}", Environment.OSVersion);
            Console.WriteLine("OS is x64: {0}", Environment.Is64BitOperatingSystem);
            Console.WriteLine("Processors count: {0}", Environment.ProcessorCount);
            Console.WriteLine("Total RAM: {0} MB",
                new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024);
            Console.WriteLine("Available physical memory: {0} MB", new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1024 / 1024);
            Console.WriteLine("Common language runtime: {0}", Environment.Version);
        }
    }
}
