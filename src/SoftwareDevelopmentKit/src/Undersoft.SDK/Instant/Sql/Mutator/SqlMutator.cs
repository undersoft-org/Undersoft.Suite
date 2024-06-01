namespace Undersoft.SDK.Instant.Sql
{
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series;

    public class SqlMutator
    {
        private InstantSqlDb _sqldb;

        public SqlMutator() { }

        public SqlMutator(InstantSqlDb sqldb)
        {
            _sqldb = sqldb;
        }

        public ISeries<ISeries<IInstant>> Delete(string SqlConnectString, IInstantSeries series)
        {
            try
            {
                if (_sqldb == null)
                    _sqldb = new InstantSqlDb(SqlConnectString);
                try
                {
                    if (series.Count > 0)
                    {
                        BulkPrepareType prepareType = BulkPrepareType.Drop;
                        return _sqldb.Delete(series, true, true, prepareType);
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ISeries<ISeries<IInstant>> Set(string SqlConnectString, IInstantSeries series, bool renew)
        {
            try
            {
                if (_sqldb == null)
                    _sqldb = new InstantSqlDb(SqlConnectString);
                try
                {
                    if (series.Count > 0)
                    {
                        BulkPrepareType prepareType = BulkPrepareType.Drop;

                        if (renew)
                            prepareType = BulkPrepareType.Trunc;

                        var result = _sqldb.Update(series, true, true, true, null, prepareType);
                        if (result != null)
                        {
                            IInstantSeries postseries = (IInstantSeries)Instances.New(series.GetType());
                            postseries.Rubrics = series.Rubrics;
                            postseries.InstantType = series.InstantType;
                            postseries.InstantSize = series.InstantSize;
                            postseries.Add(result["Failed"].AsValues());
                            return _sqldb.Insert(postseries, true, false, prepareType);
                        }
                        else
                            return null;
                    }
                    return null;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
