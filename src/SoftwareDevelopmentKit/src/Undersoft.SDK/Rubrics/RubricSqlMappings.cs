namespace Undersoft.SDK.Rubrics
{
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Series.Base;
    using Undersoft.SDK.Uniques;

    [Serializable]
    public class RubricSqlMappings : ListingBase<RubricSqlMapping>
    {
        public override ISeriesItem<RubricSqlMapping>[] EmptyVector(int size)
        {
            return new SeriesItem<RubricSqlMapping>[size];
        }

        public override ISeriesItem<RubricSqlMapping> EmptyItem()
        {
            return new SeriesItem<RubricSqlMapping>();
        }

        public override ISeriesItem<RubricSqlMapping>[] EmptyTable(int size)
        {
            return new SeriesItem<RubricSqlMapping>[size];
        }

        public override ISeriesItem<RubricSqlMapping> NewItem(RubricSqlMapping value)
        {
            return new SeriesItem<RubricSqlMapping>(value.DbTableName.UniqueKey(), value);
        }

        public override ISeriesItem<RubricSqlMapping> NewItem(ISeriesItem<RubricSqlMapping> value)
        {
            return new SeriesItem<RubricSqlMapping>(value);
        }

        public override ISeriesItem<RubricSqlMapping> NewItem(object key, RubricSqlMapping value)
        {
            return new SeriesItem<RubricSqlMapping>(key, value);
        }

        public override ISeriesItem<RubricSqlMapping> NewItem(long key, RubricSqlMapping value)
        {
            return new SeriesItem<RubricSqlMapping>(key, value);
        }
    }
}
