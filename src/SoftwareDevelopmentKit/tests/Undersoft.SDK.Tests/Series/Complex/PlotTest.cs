namespace Undersoft.SDK.Tests.Series
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;
    using System.Series.Tests;
    using Undersoft.SDK.Series.Complex;
    using Undersoft.SDK.Tests.Mocks.Models.Agreements;

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
        public void DirectedPlot_QuickPath_Integrated_Test()
        {
            Plot<Agreement> directedPlot = PrepareTestGraphs.PrepareTestDirectedPlot();

            var minPath = directedPlot.QuickPath(((IList<Place<Agreement>>)directedPlot)[5], ((IList<Place<Agreement>>)directedPlot)[10], MetricKind.Time, new MetricRange(0, 10));
         
            var minSum = minPath.Sum(m => m.Metrics[MetricKind.Time].Value);

            Assert.IsTrue(minSum > 0);
        }

        [TestMethod]
        public void UndirectedPlot_QuickPath_Integrated_Test()
        {
            Plot<Agreement> undirectedPlot = PrepareTestGraphs.PrepareTestUndirectedPlot();

            var minPath = undirectedPlot.QuickPath(((IList<Place<Agreement>>)undirectedPlot)[5], ((IList<Place<Agreement>>)undirectedPlot)[10], MetricKind.Time, new MetricRange(0, 10));

            var minSum = minPath.Sum(m => m.Metrics[MetricKind.Time].Value);

            Assert.IsTrue(minSum > 0);
        }
    }
}
