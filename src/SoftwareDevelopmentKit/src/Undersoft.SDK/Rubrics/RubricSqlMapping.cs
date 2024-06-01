namespace Undersoft.SDK.Rubrics
{
    using Undersoft.SDK.Series;

    [Serializable]
    public class RubricSqlMapping
    {
        public RubricSqlMapping(string dbDeckName) : this(dbDeckName, new Catalog<int>(), new Catalog<int>())
        { }

        public RubricSqlMapping(string dbDeckName, ISeries<int> keyOrdinal, ISeries<int> columnOrdinal)
        {
            KeyOrdinal = keyOrdinal;
            ColumnOrdinal = columnOrdinal;
            DbTableName = dbDeckName;
        }

        public ISeries<int> ColumnOrdinal { get; set; }

        public string DbTableName { get; set; }

        public ISeries<int> KeyOrdinal { get; set; }
    }
}
