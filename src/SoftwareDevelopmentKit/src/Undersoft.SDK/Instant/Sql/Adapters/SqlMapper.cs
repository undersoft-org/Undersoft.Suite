namespace Undersoft.SDK.Instant.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Series;

    public class SqlMapper
    {
        public SqlMapper(
            IInstantSeries table,
            bool keysFromDeck = false,
            string[] dbTableNames = null,
            string tablePrefix = ""
        )
        {
            try
            {
                bool mixedMode = false;
                string tName = "",
                    dbtName = "",
                    prefix = tablePrefix;
                List<string> dbtNameMixList = new List<string>();
                if (dbTableNames != null)
                {
                    foreach (string dbTableName in dbTableNames)
                        if (DbHand.Schema.DataDbTables.Have(dbTableName))
                            dbtNameMixList.Add(dbTableName);
                    if (dbtNameMixList.Count > 0)
                        mixedMode = true;
                }
                IInstantSeries t = table;
                tName = t.InstantType.Name;
                if (!mixedMode)
                {
                    if (!DbHand.Schema.DataDbTables.Have(tName))
                    {
                        if (DbHand.Schema.DataDbTables.Have(prefix + tName))
                            dbtName = prefix + tName;
                    }
                    else
                        dbtName = tName;
                    if (!string.IsNullOrEmpty(dbtName))
                    {
                        if (!keysFromDeck)
                        {
                            Chain<int> colOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DataDbColumns.Have(
                                                c.RubricName
                                            )
                                            && !DbHand.Schema.DataDbTables[dbtName].DbPrimaryKey
                                                .Select(pk => pk.ColumnName)
                                                .Contains(c.RubricName)
                                    )
                                    .Select(o => o.FieldId)
                            );
                            Chain<int> keyOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DbPrimaryKey
                                                .Select(pk => pk.ColumnName)
                                                .Contains(c.RubricName)
                                    )
                                    .Select(o => o.FieldId)
                            );
                            RubricSqlMapping iSqlsetMap = new RubricSqlMapping(
                                dbtName,
                                keyOrdinal,
                                colOrdinal
                            );
                            if (t.Rubrics.Mappings == null)
                                t.Rubrics.Mappings = new RubricSqlMappings();
                            t.Rubrics.Mappings.Add(iSqlsetMap.DbTableName, iSqlsetMap);
                        }
                        else
                        {
                            Chain<int> colOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DataDbColumns.Have(
                                                c.RubricName
                                            ) && !c.IsKey
                                    )
                                    .Select(o => o.FieldId)
                            );
                            Chain<int> keyOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DataDbColumns.Have(
                                                c.RubricName
                                            ) && c.IsKey
                                    )
                                    .Select(o => o.FieldId)
                            );
                            RubricSqlMapping iSqlsetMap = new RubricSqlMapping(
                                dbtName,
                                keyOrdinal,
                                colOrdinal
                            );
                            if (t.Rubrics.Mappings == null)
                                t.Rubrics.Mappings = new RubricSqlMappings();
                            t.Rubrics.Mappings.Add(iSqlsetMap.DbTableName, iSqlsetMap);
                        }
                    }
                }
                else
                {
                    if (!keysFromDeck)
                    {
                        foreach (string dbtNameMix in dbtNameMixList)
                        {
                            dbtName = dbtNameMix;
                            Chain<int> colOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DataDbColumns.Have(
                                                c.RubricName
                                            )
                                            && !DbHand.Schema.DataDbTables[dbtName].DbPrimaryKey
                                                .Select(pk => pk.ColumnName)
                                                .Contains(c.RubricName)
                                    )
                                    .Select(o => o.FieldId)
                            );
                            Chain<int> keyOrdinal = new Chain<int>(
                                (
                                    t.Rubrics
                                        .AsValues()
                                        .Where(
                                            c =>
                                                DbHand.Schema.DataDbTables[dbtName].DbPrimaryKey
                                                    .Select(pk => pk.ColumnName)
                                                    .Contains(c.RubricName)
                                        )
                                        .Select(o => o.FieldId)
                                )
                            );
                            if (keyOrdinal.Count == 0)
                                keyOrdinal = new Chain<int>(
                                    t.Rubrics.KeyRubrics
                                        .AsValues()
                                        .Where(
                                            c =>
                                                DbHand.Schema.DataDbTables[
                                                    dbtName
                                                ].DataDbColumns.Have(c.RubricName)
                                        )
                                        .Select(o => o.FieldId)
                                );
                            RubricSqlMapping iSqlsetMap = new RubricSqlMapping(
                                dbtName,
                                keyOrdinal,
                                colOrdinal
                            );
                            if (t.Rubrics.Mappings == null)
                                t.Rubrics.Mappings = new RubricSqlMappings();
                            t.Rubrics.Mappings.Add(iSqlsetMap.DbTableName, iSqlsetMap);
                        }
                    }
                    else
                    {
                        foreach (string dbtNameMix in dbtNameMixList)
                        {
                            dbtName = dbtNameMix;
                            Chain<int> colOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DataDbColumns.Have(
                                                c.RubricName
                                            ) && !c.IsKey
                                    )
                                    .Select(o => o.FieldId)
                            );
                            Chain<int> keyOrdinal = new Chain<int>(
                                t.Rubrics
                                    .AsValues()
                                    .Where(
                                        c =>
                                            DbHand.Schema.DataDbTables[dbtName].DataDbColumns.Have(
                                                c.RubricName
                                            ) && c.IsKey
                                    )
                                    .Select(o => o.FieldId)
                            );
                            if (keyOrdinal.Count == 0)
                                keyOrdinal = new Chain<int>(
                                    t.Rubrics.KeyRubrics
                                        .AsValues()
                                        .Where(
                                            c =>
                                                DbHand.Schema.DataDbTables[
                                                    dbtName
                                                ].DataDbColumns.Have(c.RubricName)
                                        )
                                        .Select(o => o.FieldId)
                                );
                            RubricSqlMapping iSqlsetMap = new RubricSqlMapping(
                                dbtName,
                                keyOrdinal,
                                colOrdinal
                            );
                            if (t.Rubrics.Mappings == null)
                                t.Rubrics.Mappings = new RubricSqlMappings();
                            t.Rubrics.Mappings.Add(iSqlsetMap.DbTableName, iSqlsetMap);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SqlMapperException(ex.ToString());
            }
            CardsMapped = table;
        }

        public IInstantSeries CardsMapped { get; set; }

        public class SqlMapperException : Exception
        {
            public SqlMapperException(string message) : base(message) { }
        }
    }
}
