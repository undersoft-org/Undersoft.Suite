using Undersoft.SDK.Instant.Series;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Series.Base;

namespace Undersoft.SDK.Instant.Sql
{
    public class InstantSql<T> : ListingBase<IProxy>
    {
        public InstantSql() { }

        public InstantSql(InstantSqlContext sqlcontext)
        {
            Context = sqlcontext;
        }

        public IInstantSeries InstantSeriesGenerator { get; private set; }

        public InstantSqlContext Context { get; }
    }

    public class SqlSet : ListingBase<IProxy>
    {
        public SqlSet() { }

        public SqlSet(InstantSqlContext sqlcontext)
        {
            Context = sqlcontext;
        }

        public IInstantSeries InstantSeriesGenerator { get; private set; }

        public InstantSqlContext Context { get; }
    }
}
