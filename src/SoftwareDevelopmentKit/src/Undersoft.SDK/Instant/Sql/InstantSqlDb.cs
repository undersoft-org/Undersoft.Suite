namespace Undersoft.SDK.Instant.Sql
{
    using Microsoft.Data.SqlClient;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series;

    public class InstantSqlDb
    {
        private SqlAccessor accessor;
        private SqlDelete delete;
        private InstantSqlOptions identity;
        private SqlInsert insert;
        private SqlMapper mapper;
        private SqlMutator mutator;
        private SqlConnection sqlcn;
        private SqlUpdate update;

        public InstantSqlDb(SqlConnection SqlDbConnection) : this(SqlDbConnection.ConnectionString) { }

        public InstantSqlDb(InstantSqlOptions sqlIdentity)
        {
            identity = sqlIdentity;
            sqlcn = new SqlConnection(cnString);
            Initialization();
        }

        public InstantSqlDb(string SqlConnectionString)
        {
            identity = new InstantSqlOptions();
            cnString = SqlConnectionString;
            sqlcn = new SqlConnection(cnString);
            Initialization();
        }

        private string cnString
        {
            get => identity.ConnectionString;
            set => identity.ConnectionString = value;
        }

        public IInstantSeries Get(string sqlQry, string tableName, ISeries<string> keyNames = null)
        {
            return accessor.Get(cnString, sqlQry, tableName, keyNames);
        }

        public ISeries<ISeries<IInstant>> Add(IInstantSeries cards)
        {
            return mutator.Set(cnString, cards, false);
        }

        public ISeries<ISeries<IInstant>> BatchDelete(IInstantSeries cards, bool buildMapping)
        {
            if (delete == null)
                delete = new SqlDelete(sqlcn);
            return delete.BatchDelete(cards, buildMapping);
        }

        public ISeries<ISeries<IInstant>> BatchInsert(IInstantSeries cards, bool buildMapping)
        {
            if (insert == null)
                insert = new SqlInsert(sqlcn);
            return insert.BatchInsert(cards, buildMapping);
        }

        public ISeries<ISeries<IInstant>> BatchUpdate(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null
        )
        {
            if (update == null)
                update = new SqlUpdate(sqlcn);
            return update.BatchUpdate(cards, keysFromDeck, buildMapping, updateKeys, updateExcept);
        }

        public ISeries<ISeries<IInstant>> BulkDelete(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            if (delete == null)
                delete = new SqlDelete(sqlcn);
            return delete.Delete(cards, keysFromDeck, buildMapping, tempType);
        }

        public ISeries<ISeries<IInstant>> BulkInsert(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            if (insert == null)
                insert = new SqlInsert(sqlcn);
            return insert.Insert(cards, keysFromDeck, buildMapping, false, null, tempType);
        }

        public ISeries<ISeries<IInstant>> BulkUpdate(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            if (update == null)
                update = new SqlUpdate(sqlcn);
            return update.BulkUpdate(
                cards,
                keysFromDeck,
                buildMapping,
                updateKeys,
                updateExcept,
                tempType
            );
        }

        public ISeries<ISeries<IInstant>> Delete(IInstantSeries cards)
        {
            return mutator.Delete(cnString, cards);
        }

        public ISeries<ISeries<IInstant>> Delete(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            return BulkDelete(cards, keysFromDeck, buildMapping, tempType);
        }

        public int Execute(string query)
        {
            SqlCommand cmd = sqlcn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            return cmd.ExecuteNonQuery();
        }

        public ISeries<ISeries<IInstant>> Insert(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            return BulkInsert(cards, keysFromDeck, buildMapping, tempType);
        }

        public IInstantSeries Mapper(
            IInstantSeries cards,
            bool keysFromDeck = false,
            string[] dbTableNames = null,
            string tablePrefix = ""
        )
        {
            mapper = new SqlMapper(cards, keysFromDeck, dbTableNames, tablePrefix);
            return mapper.CardsMapped;
        }

        public ISeries<ISeries<IInstant>> Put(IInstantSeries cards)
        {
            return mutator.Set(cnString, cards, true);
        }

        public int SimpleDelete(IInstantSeries cards)
        {
            if (delete == null)
                delete = new SqlDelete(sqlcn);
            return delete.SimpleDelete(cards);
        }

        public int SimpleDelete(IInstantSeries cards, bool buildMapping)
        {
            if (delete == null)
                delete = new SqlDelete(sqlcn);
            return delete.SimpleDelete(cards, buildMapping);
        }

        public int SimpleInsert(IInstantSeries cards)
        {
            if (insert == null)
                insert = new SqlInsert(sqlcn);
            return insert.SimpleInsert(cards);
        }

        public int SimpleInsert(IInstantSeries cards, bool buildMapping)
        {
            if (insert == null)
                insert = new SqlInsert(sqlcn);
            return insert.SimpleInsert(cards, buildMapping);
        }

        public int SimpleUpdate(
            IInstantSeries cards,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null
        )
        {
            if (update == null)
                update = new SqlUpdate(sqlcn);
            return update.SimpleUpdate(cards, buildMapping, updateKeys, updateExcept);
        }

        public ISeries<ISeries<IInstant>> Update(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            return BulkUpdate(
                cards,
                keysFromDeck,
                buildMapping,
                updateKeys,
                updateExcept,
                tempType
            );
        }

        private void Initialization()
        {
            string dbName = sqlcn.Database;
            SqlSchemaBuild SchemaBuild = new SqlSchemaBuild(sqlcn);
            SchemaBuild.SchemaPrepare();
            sqlcn.ChangeDatabase("tempdb");
            SchemaBuild.SchemaPrepare(BuildDbSchemaType.Temp);
            sqlcn.ChangeDatabase(dbName);
            accessor = new SqlAccessor();
            mutator = new SqlMutator(this);
        }
    }

    public class SqlException : Exception
    {
        public SqlException(string message) : base(message) { }
    }
}
