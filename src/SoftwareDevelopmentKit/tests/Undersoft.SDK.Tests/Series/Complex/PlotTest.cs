namespace Undersoft.SDK.Tests.Series
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;
    using System.Series.Tests;
    using Undersoft.SDK.Series.Complex;
    using Undersoft.SDK.Tests.Instant;

    [TestClass]
    public class PlotTest
    {
        public PlotTest() : base()
        {
            DefaultTraceListener Logfile = new DefaultTraceListener();
            Logfile.Name = "Logfile";
            Trace.Listeners.Add(Logfile);
            Logfile.LogFileName = $"Lsting__{DateTime.Now.ToFileTime().ToString()}_Test.log";
        }


        [TestMethod]
        public void DirectedPlot_Lowest_Vectors_Path_Integrated_Test()
        {
            Plot<Agreement> directedPlot = PrepareTestGraphs.PrepareTestDirectedPlot();

            var minPath = directedPlot.GetLowestVectors(((IList<Place<Agreement>>)directedPlot)[0], ((IList<Place<Agreement>>)directedPlot)[10], MetricKind.Distance);
         
            var minSum = minPath.Sum(m => m.Metrics.FirstOrDefault().Value);

            Assert.IsTrue(minSum > 0);
        }

        [TestMethod]
        public void UndirectedPlot_Lowest_Vectors_Path_Integrated_Test()
        {
            Plot<Agreement> undirectedPlot = PrepareTestGraphs.PrepareTestUndirectedPlot();

            var minPath = undirectedPlot.GetLowestVectors(((IList<Place<Agreement>>)undirectedPlot)[0], ((IList<Place<Agreement>>)undirectedPlot)[10], MetricKind.Distance);
            double minSum = 0;
            if (minPath.Any())
            {
                minSum = minPath.Sum(m => m.Metrics.FirstOrDefault().Value);
                Assert.IsTrue(minSum > 0);
            }
            else
            {
                Assert.IsTrue(minSum == 0);
            }
        }
    }
}
