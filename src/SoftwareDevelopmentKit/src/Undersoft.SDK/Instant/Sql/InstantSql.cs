using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series.Base;

namespace Undersoft.SDK.Instant.Sql
{
    public class InstantSql<T> : ListingBase<IProxy<T>>
    {
        public InstantSql() { }

        public InstantSql(InstantSqlContext sqlcontext)
        {
            Context = sqlcontext;
        }
     
        public IInstantSeries InstantSeriesCreator { get; private set; }

        public InstantSqlContext Context { get; }
    }

    public class Sqlset : ListingBase<IProxy>
    {
        public Sqlset() { }

        public Sqlset(InstantSqlContext sqlcontext)
        {
            Context = sqlcontext;
        }

        public IInstantSeries InstantSeriesCreator { get; private set; }

        public InstantSqlContext Context { get; }
    }
}
