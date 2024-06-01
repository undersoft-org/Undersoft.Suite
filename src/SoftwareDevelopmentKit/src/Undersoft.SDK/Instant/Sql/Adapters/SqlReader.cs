namespace Undersoft.SDK.Instant.Sql
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Uniques;

    public interface IDataReader<T> where T : class
    {
        ISeries<ISeries<IInstant>> DeleteRead(IInstantSeries toInsertCards);

        IInstantSeries ReadLoaded(string tableName, ISeries<string> keyNames = null);

        ISeries<ISeries<IInstant>> InsertRead(IInstantSeries toInsertCards);

        ISeries<ISeries<IInstant>> UpdateRead(IInstantSeries toUpdateCards);
    }

    public class SqlReader<T> : IDataReader<T> where T : class
    {
        private IDataReader dr;

        public SqlReader(IDataReader _dr)
        {
            dr = _dr;
        }

        public IInstantSeries DeckFromSchema(
            DataTable schema,
            ISeries<MemberRubric> operColumns,
            bool insAndDel = false
        )
        {
            List<MemberRubric> columns = new List<MemberRubric>(
                schema.Rows
                    .Cast<DataRow>()
                    .AsEnumerable()
                    .AsQueryable()
                    .Select(
                        c =>
                            new MemberRubric(
                                new FieldRubric(
                                    Type.GetType(c["DataType"].ToString()),
                                    c["ColumnName"].ToString(),
                                    Convert.ToInt32(c["ColumnSize"]),
                                    Convert.ToInt32(c["ColumnOrdinal"])
                                )
                                {
                                    RubricSize = Convert.ToInt32(c["ColumnSize"])
                                }
                            )
                            {
                                FieldId = Convert.ToInt32(c["ColumnOrdinal"]),
                                IsIdentity = Convert.ToBoolean(c["IsIdentity"]),
                                IsAutoincrement = Convert.ToBoolean(c["IsAutoincrement"]),
                                IsDBNull = Convert.ToBoolean(c["AllowDBNull"])
                            }
                    )
                    .ToList()
            );

            List<MemberRubric> _columns = new List<MemberRubric>();

            if (insAndDel)
                for (int i = 0; i < (int)(columns.Count / 2); i++)
                    _columns.Add(columns[i]);
            else
                _columns.AddRange(columns);

            InstantCreator rt = new InstantCreator(_columns.ToArray(), "SchemaInstantCreator");
            InstantSeriesCreator tab = new InstantSeriesCreator(rt, "Schema");
            IInstantSeries deck = tab.Create();

            List<DbTable> dbtabs = DbHand.Schema.DataDbTables.List;
            MemberRubric[] pKeys = columns
                .Where(
                    c =>
                        dbtabs
                            .SelectMany(t => t.GetKeyForDataTable.Select(d => d.RubricName))
                            .Contains(c.RubricName)
                        && operColumns.Select(o => o.RubricName).Contains(c.RubricName)
                )
                .ToArray();
            if (pKeys.Length > 0)
                deck.Rubrics.KeyRubrics = new MemberRubrics(pKeys);
            deck.Rubrics.Update();
            return deck;
        }

        public ISeries<ISeries<IInstant>> DeleteRead(IInstantSeries toDeleteCards)
        {
            IInstantSeries deck = toDeleteCards;
            ISeries<IInstant> deletedList = new Catalog<IInstant>();
            ISeries<IInstant> brokenList = new Catalog<IInstant>();

            int i = 0;
            do
            {
                int columnsCount = deck.Rubrics.Count;

                if (i == 0 && deck.Rubrics.Count == 0)
                {
                    IInstantSeries tab = DeckFromSchema(dr.GetSchemaTable(), deck.Rubrics.KeyRubrics);
                    deck = tab;
                    columnsCount = deck.Rubrics.Count;
                }
                object[] itemArray = new object[columnsCount];
                int[] keyIndexes = deck.Rubrics.KeyRubrics.Ordinals;
                while (dr.Read())
                {
                    if ((columnsCount - 1) != dr.FieldCount)
                    {
                        IInstantSeries tab = DeckFromSchema(dr.GetSchemaTable(), deck.Rubrics.KeyRubrics);
                        deck = tab;
                        columnsCount = deck.Rubrics.Count;
                        itemArray = new object[columnsCount];
                        keyIndexes = deck.Rubrics.KeyRubrics.Ordinals;
                    }

                    dr.GetValues(itemArray);

                    IInstant row = deck.NewInstant();

                    ((IValueArray)row).ValueArray = itemArray
                        .Select(
                            (a, y) => itemArray[y] = (a == DBNull.Value) ? a.GetType().Default() : a
                        )
                        .ToArray();

                    deletedList.Add(row);
                }

                foreach (IInstant ir in toDeleteCards)
                    if (!deletedList.ContainsKey(ir))
                        brokenList.Add(ir);
            } while (dr.NextResult());

            ISeries<ISeries<IInstant>> iSet = new Catalog<ISeries<IInstant>>();

            iSet.Add("Failed", brokenList);

            iSet.Add("Deleted", deletedList);

            return iSet;
        }

        public IInstantSeries ReadLoaded(string tableName, ISeries<string> keyNames = null)
        {
            DataTable schema = dr.GetSchemaTable();
            List<MemberRubric> columns = new List<MemberRubric>(
                schema.Rows
                    .Cast<DataRow>()
                    .AsEnumerable()
                    .AsQueryable()
                    .Where(n => n["ColumnName"].ToString() != "code")
                    .Select(
                        c =>
                            new MemberRubric(
                                new FieldRubric(
                                    Type.GetType(c["DataType"].ToString()),
                                    c["ColumnName"].ToString(),
                                    Convert.ToInt32(c["ColumnSize"]),
                                    Convert.ToInt32(c["ColumnOrdinal"])
                                )
                                {
                                    RubricSize = Convert.ToInt32(c["ColumnSize"])
                                }
                            )
                            {
                                FieldId = Convert.ToInt32(c["ColumnOrdinal"]),
                                IsIdentity = Convert.ToBoolean(c["IsIdentity"]),
                                IsAutoincrement = Convert.ToBoolean(c["IsAutoincrement"]),
                                IsDBNull = Convert.ToBoolean(c["AllowDBNull"]),
                            }
                    )
                    .ToList()
            );

            bool takeDbKeys = false;
            if (keyNames != null)
                if (keyNames.Count > 0)
                    foreach (var k in keyNames)
                    {
                        columns.Where(c => c.Name == k).Select(ck => ck.IsKey = true).ToArray();
                    }
                else
                    takeDbKeys = true;
            else
                takeDbKeys = true;

            if (takeDbKeys && DbHand.Schema != null && DbHand.Schema.DataDbTables.List.Count > 0)
            {
                List<DbTable> dbtabs = DbHand.Schema.DataDbTables.List;
                MemberRubric[] pKeys = columns
                    .Where(
                        c =>
                            dbtabs
                                .SelectMany(t => t.GetKeyForDataTable.Select(d => d.RubricName))
                                .Contains(c.RubricName)
                    )
                    .ToArray();

                if (pKeys.Length > 0)
                {
                    pKeys.Select(pk => pk.IsKey = true);
                }
            }

            InstantCreator rt = new InstantCreator(columns.ToArray(), tableName);
            InstantSeriesCreator deck = new InstantSeriesCreator(rt, tableName + "_InstantSeriesCreator");
            IInstantSeries tab = deck.Create();

            if (dr.Read())
            {
                int columnsCount = dr.FieldCount;
                object[] itemArray = new object[columnsCount];
                int[] keyOrder = tab.Rubrics.KeyRubrics.Ordinals;

                do
                {
                    IInstant figure = tab.NewInstant();

                    dr.GetValues(itemArray);

                    ((IValueArray)figure).ValueArray = itemArray
                        .Select(
                            (a, y) => itemArray[y] = (a == DBNull.Value) ? a.GetType().Default() : a
                        )
                        .ToArray();

                    figure.Id = keyOrder.Select(i => itemArray[i]).ToArray().UniqueKey64();

                    tab.Put(figure);
                } while (dr.Read());
                itemArray = null;
            }
            dr.Dispose();
            return tab;
        }

        public ISeries<ISeries<IInstant>> InsertRead(IInstantSeries toInsertCards)
        {
            IInstantSeries deck = toInsertCards;
            ISeries<IInstant> insertedList = new Catalog<IInstant>();
            ISeries<IInstant> brokenList = new Catalog<IInstant>();

            int i = 0;
            do
            {
                int columnsCount = deck.Rubrics.Count;

                if (i == 0 && deck.Rubrics.Count == 0)
                {
                    IInstantSeries tab = DeckFromSchema(dr.GetSchemaTable(), deck.Rubrics.KeyRubrics);
                    deck = tab;
                    columnsCount = deck.Rubrics.Count;
                }
                object[] itemArray = new object[columnsCount];
                int[] keyIndexes = deck.Rubrics.KeyRubrics.Ordinals;
                while (dr.Read())
                {
                    if ((columnsCount - 1) != dr.FieldCount)
                    {
                        IInstantSeries tab = DeckFromSchema(dr.GetSchemaTable(), deck.Rubrics.KeyRubrics);
                        deck = tab;
                        columnsCount = deck.Rubrics.Count;
                        itemArray = new object[columnsCount];
                        keyIndexes = deck.Rubrics.KeyRubrics
                            .AsValues()
                            .Select(k => k.FieldId)
                            .ToArray();
                    }

                    dr.GetValues(itemArray);

                    IInstant row = deck.NewInstant();

                    ((IValueArray)row).ValueArray = itemArray
                        .Select(
                            (a, y) => itemArray[y] = (a == DBNull.Value) ? a.GetType().Default() : a
                        )
                        .ToArray();

                    insertedList.Add(row);
                }

                foreach (IInstant ir in toInsertCards)
                    if (!insertedList.ContainsKey(ir))
                        brokenList.Add(ir);
            } while (dr.NextResult());

            ISeries<ISeries<IInstant>> iSet = new Catalog<ISeries<IInstant>>();

            iSet.Add("Failed", brokenList);

            iSet.Add("Inserted", insertedList);

            return iSet;
        }

        public ISeries<ISeries<IInstant>> UpdateRead(IInstantSeries toUpdateCards)
        {
            IInstantSeries deck = toUpdateCards;
            ISeries<IInstant> updatedList = new Catalog<IInstant>();
            ISeries<IInstant> toInsertList = new Catalog<IInstant>();

            int i = 0;
            do
            {
                int columnsCount = deck.Rubrics != null ? deck.Rubrics.Count : 0;

                if (i == 0 && columnsCount == 0)
                {
                    IInstantSeries tab = DeckFromSchema(
                        dr.GetSchemaTable(),
                        deck.Rubrics.KeyRubrics,
                        true
                    );
                    deck = tab;
                    columnsCount = deck.Rubrics.Count;
                }
                object[] itemArray = new object[columnsCount];
                int[] keyOrder = deck.Rubrics.KeyRubrics.Ordinals;
                while (dr.Read())
                {
                    if ((columnsCount - 1) != (int)(dr.FieldCount / 2))
                    {
                        IInstantSeries tab = DeckFromSchema(
                            dr.GetSchemaTable(),
                            deck.Rubrics.KeyRubrics,
                            true
                        );
                        deck = tab;
                        columnsCount = deck.Rubrics.Count;
                        itemArray = new object[columnsCount];
                        keyOrder = deck.Rubrics.KeyRubrics
                            .AsValues()
                            .Select(k => k.FieldId)
                            .ToArray();
                    }

                    dr.GetValues(itemArray);

                    IInstant row = deck.NewInstant();

                    ((IValueArray)row).ValueArray = itemArray
                        .Select(
                            (a, y) => itemArray[y] = (a == DBNull.Value) ? a.GetType().Default() : a
                        )
                        .ToArray();

                    updatedList.Add(row);
                }

                foreach (IInstant ir in toUpdateCards)
                    if (!updatedList.ContainsKey(ir))
                        toInsertList.Add(ir);
            } while (dr.NextResult());

            ISeries<ISeries<IInstant>> iSet = new Catalog<ISeries<IInstant>>();

            iSet.Add("Failed", toInsertList);

            iSet.Add("Updated", updatedList);

            return iSet;
        }
    }
}
