namespace Undersoft.SDK.Benchmarks.Instant.Math
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using System.Linq;
    using Undersoft.SDK.Benchmarks.Updating.Models;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Service.Data.Mapper;
    using Undersoft.SDK.Updating;

    [MemoryDiagnoser]
    [RankColumn]
    [RPlotExporter]
    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    public class UpdaterInstantSeriesBenchmark
    {
        private InstantSeriesCreator usersFactory;
        private InstantSeriesCreator emptyUsersFactory;
        private IInstantSeries InstantSeries;
        private IInstantSeries InstantSeriesEmpty;
        private DataMapper mapper;
        private ISeries<User> users;
        private ISeries<EmptyUser> emptyUsers;

        public UpdaterInstantSeriesBenchmark()
        {
        }

        [GlobalSetup]
        public void Setup()
        {
            mapper = new DataMapper();
            usersFactory = new InstantSeriesCreator<User>(InstantType.Derived, false);
            emptyUsersFactory = new InstantSeriesCreator<EmptyUser>(InstantType.Derived, false);
            users = new Listing<User>();
            InstantSeries = usersFactory.Create();

            User fom = new User();

            for (int i = 0; i < 1000 * 100; i++)
            {
                InstantSeries.Add(i, InstantSeries.NewInstant());
                users.Add(new User());
            }
        }

        [IterationSetup]
        public void Prepare()
        {
            InstantSeriesEmpty = emptyUsersFactory.Create();
            emptyUsers = new Listing<EmptyUser>();
            for (int i = 0; i < 1000 * 100; i++)
            {
                InstantSeriesEmpty.Add(i, InstantSeriesEmpty.NewInstant());
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
            InstantSeries.ForEach((m, x) => m.PatchTo(InstantSeriesEmpty[x])).Commit();
        }

        [Benchmark]
        public void Updater_Put()
        {
            InstantSeries.ForEach((m, x) => m.PutTo(InstantSeriesEmpty[x])).Commit();
        }
    }
}
