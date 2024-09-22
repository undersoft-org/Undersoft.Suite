namespace Undersoft.SDK.Instant.Sql
{
    using Microsoft.Data.SqlClient;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series;

    public class InstantSqlDb
    {
        private SqlAccessor _accessor;
        private SqlDelete _delete;
        private InstantSqlOptions _options;
        private SqlInsert _insert;
        private SqlMapper _mapper;
        private SqlMutator _mutator;
        private SqlConnection _sqlcn;
        private SqlUpdate _update;

        public InstantSqlDb(SqlConnection SqlDbConnection) : this(SqlDbConnection.ConnectionString) { }

        public InstantSqlDb(InstantSqlOptions sqlIdentity)
        {
            _options = sqlIdentity;
            _sqlcn = new SqlConnection(cnString);
            Initialization();
        }

        public InstantSqlDb(string SqlConnectionString)
        {
            _options = new InstantSqlOptions();
            cnString = SqlConnectionString;
            _sqlcn = new SqlConnection(cnString);
            Initialization();
        }

        private string cnString
        {
            get => _options.ConnectionString;
            set => _options.ConnectionString = value;
        }

        public IInstantSeries Get(string sqlQry, string tableName, ISeries<string> keyNames = null)
        {
            return _accessor.Get(cnString, sqlQry, tableName, keyNames);
        }

        public ISeries<ISeries<IInstant>> Add(IInstantSeries cards)
        {
            return _mutator.Set(cnString, cards, false);
        }

        public ISeries<ISeries<IInstant>> BatchDelete(IInstantSeries cards, bool buildMapping)
        {
            if (_delete == null)
                _delete = new SqlDelete(_sqlcn);
            return _delete.BatchDelete(cards, buildMapping);
        }

        public ISeries<ISeries<IInstant>> BatchInsert(IInstantSeries cards, bool buildMapping)
        {
            if (_insert == null)
                _insert = new SqlInsert(_sqlcn);
            return _insert.BatchInsert(cards, buildMapping);
        }

        public ISeries<ISeries<IInstant>> BatchUpdate(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null
        )
        {
            if (_update == null)
                _update = new SqlUpdate(_sqlcn);
            return _update.BatchUpdate(cards, keysFromDeck, buildMapping, updateKeys, updateExcept);
        }

        public ISeries<ISeries<IInstant>> BulkDelete(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            if (_delete == null)
                _delete = new SqlDelete(_sqlcn);
            return _delete.Delete(cards, keysFromDeck, buildMapping, tempType);
        }

        public ISeries<ISeries<IInstant>> BulkInsert(
            IInstantSeries cards,
            bool keysFromDeck = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            if (_insert == null)
                _insert = new SqlInsert(_sqlcn);
            return _insert.Insert(cards, keysFromDeck, buildMapping, false, null, tempType);
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
            if (_update == null)
                _update = new SqlUpdate(_sqlcn);
            return _update.BulkUpdate(
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
            return _mutator.Delete(cnString, cards);
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
            SqlCommand cmd = _sqlcn.CreateCommand();
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
            _mapper = new SqlMapper(cards, keysFromDeck, dbTableNames, tablePrefix);
            return _mapper.CardsMapped;
        }

        public ISeries<ISeries<IInstant>> Put(IInstantSeries cards)
        {
            return _mutator.Set(cnString, cards, true);
        }

        public int SimpleDelete(IInstantSeries cards)
        {
            if (_delete == null)
                _delete = new SqlDelete(_sqlcn);
            return _delete.SimpleDelete(cards);
        }

        public int SimpleDelete(IInstantSeries cards, bool buildMapping)
        {
            if (_delete == null)
                _delete = new SqlDelete(_sqlcn);
            return _delete.SimpleDelete(cards, buildMapping);
        }

        public int SimpleInsert(IInstantSeries cards)
        {
            if (_insert == null)
                _insert = new SqlInsert(_sqlcn);
            return _insert.SimpleInsert(cards);
        }

        public int SimpleInsert(IInstantSeries cards, bool buildMapping)
        {
            if (_insert == null)
                _insert = new SqlInsert(_sqlcn);
            return _insert.SimpleInsert(cards, buildMapping);
        }

        public int SimpleUpdate(
            IInstantSeries cards,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null
        )
        {
            if (_update == null)
                _update = new SqlUpdate(_sqlcn);
            return _update.SimpleUpdate(cards, buildMapping, updateKeys, updateExcept);
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
            string dbName = _sqlcn.Database;
            SqlSchemaBuild SchemaBuild = new SqlSchemaBuild(_sqlcn);
            SchemaBuild.SchemaPrepare();
            _sqlcn.ChangeDatabase("tempdb");
            SchemaBuild.SchemaPrepare(BuildDbSchemaType.Temp);
            _sqlcn.ChangeDatabase(dbName);
            _accessor = new SqlAccessor();
            _mutator = new SqlMutator(this);
        }
    }

    public class SqlException : Exception
    {
        public SqlException(string message) : base(message) { }
    }
}
