namespace Undersoft.SDK.Instant.Sql
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Uniques;

    public interface IDataReader<T>
        where T : class
    {
        ISeries<ISeries<IInstant>> ReadDelete(IInstantSeries toInsertCards);

        IInstantSeries ReadSelect(string tableName, ISeries<string> keyNames = null);

        ISeries<ISeries<IInstant>> ReadInsert(IInstantSeries toInsertCards);

        ISeries<ISeries<IInstant>> ReadUpdate(IInstantSeries toUpdateCards);
    }

    public class SqlReader<T> : IDataReader<T>
        where T : class
    {
        private IDataReader dr;

        public SqlReader(IDataReader _dr)
        {
            dr = _dr;
        }

        public IInstantSeries CreateSeries(
            DataTable table,
            IRubrics keyRubrics,
            bool insAndDel = false
        )
        {
            var _rubrics = GetTableRubrics(table);
            if (insAndDel)
                _rubrics = new MemberRubrics(_rubrics.Take(_rubrics.Count / 2));

            IInstantSeries series = new InstantSeriesGenerator(
                new InstantGenerator(_rubrics, "SchemaInstantGenerator"),
                "Schema"
            ).Generate();

            var dbKeyNames = DbHand
                .Schema.DataDbTables.List.SelectMany(t =>
                    t.GetKeyForDataTable.Select(d => d.RubricName)
                )
                .Distinct()
                .ToArray();
            _rubrics.ForOnly(c => dbKeyNames.Contains(c.RubricName), c => c.IsKey = true).Commit();

            var pKeys = _rubrics.Where(c =>
                dbKeyNames.Contains(c.RubricName) && keyRubrics.ContainsKey(c.RubricName)
            );
            if (pKeys.Any())
                series.Rubrics.KeyRubrics = new MemberRubrics(pKeys);
            series.Rubrics.Update();
            return series;
        }

        public ISeries<ISeries<IInstant>> ReadDelete(IInstantSeries deleteSeries)
        {
            IInstantSeries series = deleteSeries;
            ISeries<IInstant> deleted = new Chain<IInstant>();
            ISeries<IInstant> broken = new Chain<IInstant>();

            int i = 0;
            do
            {
                int count = series.Rubrics.Count;

                if (i == 0 && series.Rubrics.Count == 0)
                {
                    series = CreateSeries(dr.GetSchemaTable(), series.Rubrics.KeyRubrics);
                    count = series.Rubrics.Count;
                }
                object[] itemArray = new object[count];
                int[] keyOrdinals = series.Rubrics.KeyRubrics.Ordinals;
                while (dr.Read())
                {
                    if ((count - 1) != dr.FieldCount)
                    {
                        series = CreateSeries(dr.GetSchemaTable(), series.Rubrics.KeyRubrics);
                        count = series.Rubrics.Count;
                        itemArray = new object[count];
                        keyOrdinals = series.Rubrics.KeyRubrics.Ordinals;
                    }

                    dr.GetValues(itemArray);

                    IInstant instant = series.NewInstant();

                    ((IValueArray)instant).ValueArray = itemArray
                        .ForEach(
                            (a, y) => (a == DBNull.Value) ? itemArray[y] = a.GetType().Default() : a
                        )
                        .ToArray();

                    deleted.Add(instant);
                }

                broken.Add(deleteSeries.Where(u => !deleted.ContainsKey(u)));
            } while (dr.NextResult());

            var result = new Chain<ISeries<IInstant>>();
            result.Add("Failed", broken);
            result.Add("Deleted", deleted);

            return result;
        }

        public IInstantSeries ReadSelect(string tableName, ISeries<string> keyNames = null)
        {
            var rubrics = GetTableRubrics(dr.GetSchemaTable());

            bool takeDbKeys = false;
            if (keyNames != null)
                if (keyNames.Count > 0)
                    foreach (var k in keyNames)
                        if (rubrics.TryGet(k, out MemberRubric rubric))
                            rubric.IsKey = true;
                        else
                            takeDbKeys = true;
                else
                    takeDbKeys = true;

            if (takeDbKeys && DbHand.Schema != null && DbHand.Schema.DataDbTables.List.Count > 0)
            {
                var dbKeyNames = DbHand
                    .Schema.DataDbTables.List.SelectMany(t =>
                        t.GetKeyForDataTable.Select(d => d.RubricName)
                    )
                    .Distinct();
                rubrics
                    .ForOnly(c => dbKeyNames.Contains(c.RubricName), c => c.IsKey = true)
                    .Commit();
            }

            IInstantSeries series = new InstantSeriesGenerator(
                new InstantGenerator(rubrics, tableName),
                tableName + "_InstantSeriesGenerator"
            ).Generate();

            if (dr.Read())
            {
                int columnsCount = dr.FieldCount;
                object[] itemArray = new object[columnsCount];
                int[] keyOrder = series.Rubrics.KeyRubrics.Ordinals;

                do
                {
                    IInstant instant = series.NewInstant();

                    dr.GetValues(itemArray);

                    ((IValueArray)instant).ValueArray = itemArray
                        .ForEach(
                            (a, y) => (a == DBNull.Value) ? itemArray[y] = a.GetType().Default() : a
                        )
                        .ToArray();

                    instant.Id = keyOrder.ForEach(i => itemArray[i]).Commit().UniqueKey64();

                    series.Put(instant);
                } while (dr.Read());

                itemArray = null;
            }
            dr.Dispose();
            return series;
        }

        public ISeries<ISeries<IInstant>> ReadInsert(IInstantSeries insertSeries)
        {
            IInstantSeries series = insertSeries;
            ISeries<IInstant> inserted = new Chain<IInstant>();
            ISeries<IInstant> broken = new Chain<IInstant>();

            int i = 0;
            do
            {
                int count = series.Rubrics.Count;

                if (i == 0 && series.Rubrics.Count == 0)
                {
                    series = CreateSeries(dr.GetSchemaTable(), series.Rubrics.KeyRubrics);
                    count = series.Rubrics.Count;
                }
                object[] itemArray = new object[count];
                int[] keyOrdinals = series.Rubrics.KeyRubrics.Ordinals;
                while (dr.Read())
                {
                    if ((count - 1) != dr.FieldCount)
                    {
                        series = CreateSeries(dr.GetSchemaTable(), series.Rubrics.KeyRubrics);
                        count = series.Rubrics.Count;
                        itemArray = new object[count];
                        keyOrdinals = series
                            .Rubrics.KeyRubrics.AsValues()
                            .Select(k => k.FieldId)
                            .ToArray();
                    }

                    dr.GetValues(itemArray);

                    IInstant instant = series.NewInstant();

                    ((IValueArray)instant).ValueArray = itemArray
                        .ForEach(
                            (a, y) => (a == DBNull.Value) ? itemArray[y] = a.GetType().Default() : a
                        )
                        .ToArray();

                    inserted.Add(instant);
                }
                broken.Add(insertSeries.Where(u => !inserted.ContainsKey(u)));
            } while (dr.NextResult());

            var result = new Chain<ISeries<IInstant>>();

            result.Add("Failed", broken);
            result.Add("Inserted", inserted);

            return result;
        }

        public ISeries<ISeries<IInstant>> ReadUpdate(IInstantSeries updateSeries)
        {
            IInstantSeries series = updateSeries;
            ISeries<IInstant> updated = new Chain<IInstant>();
            ISeries<IInstant> insertSeries = new Chain<IInstant>();

            int i = 0;
            do
            {
                int count = series.Rubrics != null ? series.Rubrics.Count : 0;

                if (i == 0 && count == 0)
                {
                    series = CreateSeries(dr.GetSchemaTable(), series.Rubrics.KeyRubrics, true);
                    count = series.Rubrics.Count;
                }
                object[] itemArray = new object[count];
                int[] keyOrdinals = series.Rubrics.KeyRubrics.Ordinals;
                while (dr.Read())
                {
                    if ((count - 1) != (int)(dr.FieldCount / 2))
                    {
                        series = CreateSeries(dr.GetSchemaTable(), series.Rubrics.KeyRubrics, true);
                        count = series.Rubrics.Count;
                        itemArray = new object[count];
                        keyOrdinals = series
                            .Rubrics.KeyRubrics.AsValues()
                            .Select(k => k.FieldId)
                            .ToArray();
                    }

                    dr.GetValues(itemArray);

                    IInstant instant = series.NewInstant();

                    ((IValueArray)instant).ValueArray = itemArray
                        .ForEach(
                            (a, y) => itemArray[y] = (a == DBNull.Value) ? a.GetType().Default() : a
                        )
                        .ToArray();

                    updated.Add(instant);
                }
                insertSeries.Add(updateSeries.Where(u => !updated.ContainsKey(u)));
            } while (dr.NextResult());

            var result = new Chain<ISeries<IInstant>>();

            result.Add("Failed", insertSeries);
            result.Add("Updated", updated);

            return result;
        }

        private IRubrics GetTableRubrics(DataTable table)
        {
            return new MemberRubrics(
                table
                    .Rows.Cast<DataRow>()
                    .AsEnumerable()
                    .AsQueryable()
                    .ForOnly(
                        n => n["ColumnName"].ToString() != "code",
                        c => new MemberRubric(
                            new FieldRubric(
                                Type.GetType(c["DataType"].ToString()),
                                c["ColumnName"].ToString(),
                                Convert.ToInt32(c["ColumnSize"]),
                                Convert.ToInt32(c["ColumnOrdinal"])
                            )
                            {
                                RubricSize = Convert.ToInt32(c["ColumnSize"]),
                            }
                        )
                        {
                            FieldId = Convert.ToInt32(c["ColumnOrdinal"]),
                            IsIdentity = Convert.ToBoolean(c["IsIdentity"]),
                            IsAutoincrement = Convert.ToBoolean(c["IsAutoincrement"]),
                            IsDBNull = Convert.ToBoolean(c["AllowDBNull"]),
                        }
                    )
            );
        }
    }
}
