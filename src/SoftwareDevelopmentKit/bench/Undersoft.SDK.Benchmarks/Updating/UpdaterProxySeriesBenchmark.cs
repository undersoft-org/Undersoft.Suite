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
    public class UpdaterProxySeriesBenchmark
    {
        private ProxySeriesCreator emptyUsersFactory;
        private ProxySeriesCreator usersFactory;
        private IInstantSeries ProxySeries;
        private IInstantSeries ProxySeriesEmpty;
        private DataMapper mapper;
        private ISeries<User> users;
        private ISeries<EmptyUser> emptyUsers;

        public UpdaterProxySeriesBenchmark()
        {
        }

        [GlobalSetup]
        public void Setup()
        {
            mapper = new DataMapper();
            usersFactory = new ProxySeriesCreator<User>(false);
            emptyUsersFactory = new ProxySeriesCreator<EmptyUser>(false);
            users = new Listing<User>();
            ProxySeries = usersFactory.Create();

            for (int i = 0; i < 1000 * 1000; i++)
            {
                ProxySeries.Add(i, emptyUsersFactory.CreateProxy());
                users.Add(new User());
            }
        }

        [IterationSetup]
        public void Prepare()
        {
            ProxySeriesEmpty = emptyUsersFactory.Create();
            emptyUsers = new Listing<EmptyUser>();
            for (int i = 0; i < 1000 * 1000; i++)
            {
                ProxySeriesEmpty.Add(i, emptyUsersFactory.CreateProxy());
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
            ProxySeries.ForEach((m, x) => m.PatchTo(ProxySeriesEmpty[x])).Commit();
        }

        [Benchmark]
        public void Updater_Put()
        {
            ProxySeries.ForEach((m, x) => m.PutTo(ProxySeriesEmpty[x])).Commit();
        }
    }
}
