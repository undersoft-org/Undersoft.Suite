namespace Undersoft.SDK.Benchmarks.Instant.Math
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using System.Linq;
    using Undersoft.SDK.Benchmarks.Instant.Math.Mocks;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Math;
    using Undersoft.SDK.Instant.Series;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class InstantSeriesMathBenchmark
    {
        private InstantCreator figure;
        private InstantSeriesCreator factory;
        private InstantMath seriesMathA;
        private InstantMath<InstantSeriesMathMockModel> seriesMathB;
        private IInstantSeries InstantSeries;

        public InstantSeriesMathBenchmark()
        {
            factory = new InstantSeriesCreator<InstantSeriesMathMockModel>(
                "InstantSeriesCreator_InstantSeriesMath_Test", InstantType.Reference, false
            );

            InstantSeries = factory.Create();

            InstantSeriesMathMockModel fom = new InstantSeriesMathMockModel();

            for (int i = 0; i < 5000 * 2000; i++)
            {
                IInstant f = InstantSeries.NewInstant();

                f["NetPrice"] = (double)f["NetPrice"] + i;
                f["SellFeeRate"] = (double)f["SellFeeRate"] / 2;
                InstantSeries.Add(i, f);
            }

            seriesMathB = new InstantMath<InstantSeriesMathMockModel>(InstantSeries);

            var mathsetB0 = seriesMathB[r => r.SellNetPrice];

            mathsetB0.Formula = mathsetB0[r => r.NetPrice] * (mathsetB0[r => r.SellFeeRate] / 100D) + mathsetB0[r => r.NetPrice];

            var mathsetB1 = seriesMathB[r => r.SellGrossPrice];

            mathsetB1.Formula = mathsetB0 * mathsetB1[r => r.TaxRate];
        }

        [Benchmark]
        public void Parallel_Instant_Series_Undersoft_SDK_Instant_Math_Engine()
        {
            seriesMathB.Compute(10);
        }

        [Benchmark]
        public void Parallel_Instant_Series_DotNet_Standard_Math_Expression()
        {
            InstantSeries
                .AsParallel()
                .ForEach(
                    (c) =>
                    {
                        c["SellNetPrice"] = (double)c["NetPrice"] * ((double)c["SellFeeRate"] / 100D) + (double)c["NetPrice"];

                        c["SellGrossPrice"] = (double)c["SellNetPrice"] * (double)c["TaxRate"];
                    }
                );
        }
    }
}
