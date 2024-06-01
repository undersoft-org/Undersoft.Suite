using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Running;
using System;
using Undersoft.SDK.Benchmarks.Instant.Math;
using Undersoft.SDK.Benchmarks.Series;

namespace Undersoft.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance)
                .WithOptions(ConfigOptions.JoinSummary)
                .WithOptions(ConfigOptions.DisableLogFile);
            config.AddExporter(CsvMeasurementsExporter.Default);
            config.AddExporter(RPlotExporter.Default);

            var summary = BenchmarkRunner.Run(
                new[]
                {
                    BenchmarkConverter.TypeToBenchmarks(typeof(UpdaterProxySeriesBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(AddOrUpdateBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(AddBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ContainsValueBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(GetByIndexBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(GetByKeyBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(GetOrAddBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(IndexOfBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(InsertBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(IterationBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(RemoveBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(SetByIndexBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(SetByKeyBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentRemoveBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentAddBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentGetByKeyBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentSetByKeyBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentAddOrUpdateBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentContainsKeyBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentIterationBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentGetOrAddBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(ConcurrentTryGetByKeyBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(InstantProxiesMathBenchmark), config),
                    BenchmarkConverter.TypeToBenchmarks(typeof(InstantSeriesMathBenchmark), config),
                }
            );

            Console.ReadLine();
        }
    }

    // Execute all benchmarks from given assembly example
    //
    //        BenchmarkRunner.Run(
    //        typeof(MyBenchmark).Assembly,
    //        ManualConfig
    //            .Create(DefaultConfig.Instance)
    //            .With(ConfigOptions.JoinSummary)
    //            .With(ConfigOptions.DisableLogFile));
}
