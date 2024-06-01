namespace Undersoft.SDK.Benchmarks.Instant.Math
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using System.Linq;
    using Undersoft.SDK.Benchmarks.Updating.Models;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Proxies;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Service.Data.Mapper;
    using Undersoft.SDK.Updating;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class UpdaterSeriesBenchmark
    {
        private ProxySeriesCreator factory;
        private IInstantSeries InstantSeries;
        private IInstantSeries InstantSeriesEmpty;
        private ISeries<User> users;
        private ISeries<EmptyUser> emptyUsers;
        private DataMapper mapper;

        public UpdaterSeriesBenchmark()
        {
        }

        [GlobalSetup]
        public void Setup()
        {
            mapper = new DataMapper();
            users = new Listing<User>();

            for (int i = 0; i < 1000 * 100; i++)
            {
                users.Add(new User());
            }
        }

        [IterationSetup]
        public void Prepare()
        {
            emptyUsers = new Listing<EmptyUser>();
            for (int i = 0; i < 1000 * 100; i++)
            {
                emptyUsers.Add(new EmptyUser());
            }
        }

        [Benchmark]
        public void Mapper_Map()
        {
            users.ForEach((m, x) => mapper.Map(m, emptyUsers[x])).Commit();
        }


        [Benchmark]
        public void Updater_Patch()
        {
            users.ForEach((m, x) => m.PatchTo(emptyUsers[x])).Commit();
        }

        [Benchmark]
        public void Updater_Put()
        {
            users.ForEach((m, x) => m.PutTo(emptyUsers[x])).Commit();
        }
    }
}
