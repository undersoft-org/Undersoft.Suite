namespace System.Series.Tests
{
    using Undersoft.SDK.Series.Complex;
    using Undersoft.SDK.Tests.Mocks.Models.Agreements;

    public static class PrepareTestGraphs
    {
        public static Plot<Agreement> PrepareTestDirectedPlot()
        {
            var plot = new Plot<Agreement>();
            return FillPlot(plot);
        }

        public static Plot<Agreement> PrepareTestUndirectedPlot()
        {
            var plot = new Plot<Agreement>([new Metric(MetricKind.Time, "Seconds")], false, true);
            return FillPlot(plot);
        }

        private static Plot<Agreement> FillPlot(Plot<Agreement> plot)
        {
            for (int i = 0; i < 11; i++)
            {
                plot.Add(new Place<Agreement>(new Agreement()));
            }
            Random rand = new Random();
            IList<Place<Agreement>> plotList = plot;

            plot.AddRoute(plotList[2].Value, plotList[5].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[0].Value, plotList[6].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[5].Value, plotList[1].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[6].Value, plotList[9].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[7].Value, plotList[8].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[5].Value, plotList[4].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));            
            plot.AddRoute(plotList[4].Value, plotList[0].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));            
            plot.AddRoute(plotList[0].Value, plotList[3].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[3].Value, plotList[6].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[8].Value, plotList[9].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[8].Value, plotList[10].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[9].Value, plotList[10].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[1].Value, plotList[2].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[3].Value, plotList[4].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[2].Value, plotList[3].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[2].Value, plotList[7].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
            plot.AddRoute(plotList[2].Value, plotList[6].Value, new Metrics([new Metric(MetricKind.Time, "Seconds", rand.NextDouble() * 10)]));
          

            return plot;
        }
    }
}
