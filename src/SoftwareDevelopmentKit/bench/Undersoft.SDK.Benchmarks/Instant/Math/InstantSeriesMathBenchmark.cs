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
        internal InstantSeriesGenerator generator;
        internal InstantMath<InstantSeriesMathMockModel> math;
        internal IInstantSeries listing;

        public InstantSeriesMathBenchmark()
        {
            generator = new InstantSeriesGenerator<InstantSeriesMathMockModel>(
                "InstantSeriesCreator_InstantSeriesMath_Test", InstantType.Derived, false
            );

            listing = generator.Generate();

            for (int i = 0; i < 5000 * 2000; i++)
            {
                var instant = listing.NewInstant();
                var model = (InstantSeriesMathMockModel)instant;

                model.NetPrice = model.NetPrice + i;
                model.SellFeeRate = model.SellFeeRate / 2;
                listing.Add(i, instant);
            }
        }

        [Benchmark]
        public void Parallel_Instant_Series_Undersoft_SDK_Instant_Math_Engine()
        {

            math = new InstantMath<InstantSeriesMathMockModel>(listing);

            var netMath = math[r => r.SellNetPrice];

            netMath.Formula = netMath[r => r.NetPrice] * (netMath[r => r.SellFeeRate] / 100D) + netMath[r => r.NetPrice];

            var grossMath = math[r => r.SellGrossPrice];

            grossMath.Formula = netMath * grossMath[r => r.TaxRate];

            math.Compute(10);
        }

        [Benchmark]
        public void Parallel_Instant_Series_DotNet_Standard_Math_Expression()
        {
            listing
                .AsParallel()
                .ForAll(
                    instant =>
                    {
                        var m = (InstantSeriesMathMockModel)instant;

                        m.SellNetPrice = (m.NetPrice * (m.SellFeeRate / 100D)) + m.NetPrice;

                        m.SellGrossPrice = m.SellNetPrice * m.TaxRate;
                    }
                );
        }
    }
}
