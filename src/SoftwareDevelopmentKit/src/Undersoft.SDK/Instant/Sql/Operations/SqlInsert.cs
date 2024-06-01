namespace Undersoft.SDK.Instant.Sql
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Series;

    public class SqlInsert
    {
        private SqlConnection _cn;

        public SqlInsert(SqlConnection cn)
        {
            _cn = cn;
        }

        public SqlInsert(string cnstring)
        {
            _cn = new SqlConnection(cnstring);
        }

        public ISeries<ISeries<IInstant>> BatchInsert(
            IInstantSeries table,
            bool buildMapping,
            int batchSize = 1000
        )
        {
            try
            {
                IInstantSeries tab = table;
                IList<RubricSqlMapping> nMaps = new List<RubricSqlMapping>();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                ISeries<ISeries<IInstant>> nSet = new Catalog<ISeries<IInstant>>();
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ic = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.ColumnOrdinal.Contains(c.FieldId))
                            .ToArray();
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchInsertQuery(ir, nMap.DbTableName, ic, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        var bIInstantSeries = afad.ExecuteInsert(sb.ToString(), tab);
                        if (nSet.Count == 0)
                            nSet = bIInstantSeries;
                        else
                            foreach (Catalog<IInstant> its in bIInstantSeries.AsValues())
                            {
                                if (nSet.Contains(its))
                                {
                                    nSet[its].Put(its.AsValues());
                                }
                                else
                                    nSet.Add(its);
                            }
                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                var rIInstantSeries = afad.ExecuteInsert(sb.ToString(), tab);

                if (nSet.Count == 0)
                    nSet = rIInstantSeries;
                else
                    foreach (ISeries<IInstant> its in rIInstantSeries.AsValues())
                    {
                        if (nSet.Contains(its))
                        {
                            nSet[its].Put(its.AsValues());
                        }
                        else
                            nSet.Add(its);
                    }

                return nSet;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlInsertException(ex.ToString());
            }
        }

        public ISeries<ISeries<IInstant>> BatchInsert(IInstantSeries table, int batchSize = 1000)
        {
            try
            {
                IInstantSeries tab = table;
                IList<RubricSqlMapping> nMaps = new List<RubricSqlMapping>();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                ISeries<ISeries<IInstant>> nSet = new Catalog<ISeries<IInstant>>();
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ic = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.ColumnOrdinal.Contains(c.FieldId))
                            .ToArray();
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchInsertQuery(ir, nMap.DbTableName, ic, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        var bIInstantSeries = afad.ExecuteInsert(sb.ToString(), tab);
                        if (nSet.Count == 0)
                            nSet = bIInstantSeries;
                        else
                            foreach (Catalog<IInstant> its in bIInstantSeries.AsValues())
                            {
                                if (nSet.Contains(its))
                                {
                                    nSet[its].Put(its.AsValues());
                                }
                                else
                                    nSet.Add(its);
                            }
                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                var rIInstantSeries = afad.ExecuteInsert(sb.ToString(), tab);

                if (nSet.Count == 0)
                    nSet = rIInstantSeries;
                else
                    foreach (ISeries<IInstant> its in rIInstantSeries.AsValues())
                    {
                        if (nSet.Contains(its))
                        {
                            nSet[its].Put(its.AsValues());
                        }
                        else
                            nSet.Add(its);
                    }

                return nSet;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlInsertException(ex.ToString());
            }
        }

        public StringBuilder BatchInsertQuery(
            IInstant card,
            string tableName,
            MemberRubric[] columns,
            MemberRubric[] keys,
            bool updateKeys = true
        )
        {
            StringBuilder sbCols = new StringBuilder(),
                sbVals = new StringBuilder(),
                sbQry = new StringBuilder();
            string tName = tableName;
            IInstant ir = card;
            object[] ia = ((IValueArray)ir).ValueArray;
            MemberRubric[] ic = columns;
            MemberRubric[] ik = keys;

            sbCols.AppendLine(@"    ");
            sbCols.Append("INSERT INTO " + tableName + " (");
            sbVals.Append(@") OUTPUT inserted.* VALUES (");
            bool isUpdateCol = false;
            string delim = "";
            int c = 0;
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].RubricName.ToLower() == "updated")
                    isUpdateCol = true;
                if (ia[columns[i].FieldId] != DBNull.Value && !columns[i].IsIdentity)
                {
                    if (c > 0)
                        delim = ",";
                    sbCols.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0}[{1}]",
                        delim,
                        columns[i].RubricName
                    );
                    sbVals.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0} {1}{2}{1}",
                        delim,
                        (
                            columns[i].RubricType == typeof(string)
                            || columns[i].RubricType == typeof(DateTime)
                        )
                            ? "'"
                            : "",
                        (columns[i].RubricType != typeof(string))
                            ? Convert.ChangeType(ia[columns[i].FieldId], columns[i].RubricType)
                            : ia[columns[i].FieldId].ToString().Replace("'", "''")
                    );
                    c++;
                }
            }

            if (DbHand.Schema.DataDbTables[tableName].DataDbColumns.Have("updated") && !isUpdateCol)
            {
                sbCols.AppendFormat(CultureInfo.InvariantCulture, ", [updated]", DateTime.Now);
                sbVals.AppendFormat(CultureInfo.InvariantCulture, ", '{0}'", DateTime.Now);
            }
            if (columns.Length > 0)
                delim = ",";
            else
                delim = "";
            c = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (ia[keys[i].FieldId] != DBNull.Value && !keys[i].IsIdentity)
                {
                    if (c > 0)
                        delim = ",";
                    sbCols.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0}[{1}]",
                        delim,
                        keys[i].RubricName
                    );
                    sbVals.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0} {1}{2}{1}",
                        delim,
                        (
                            keys[i].RubricType == typeof(string)
                            || keys[i].RubricType == typeof(DateTime)
                        )
                            ? "'"
                            : "",
                        (keys[i].RubricType != typeof(string))
                            ? Convert.ChangeType(ia[keys[i].FieldId], keys[i].RubricType)
                            : ia[keys[i].FieldId].ToString().Replace("'", "''")
                    );
                    c++;
                }
            }
            sbQry.Append(sbCols.ToString() + sbVals.ToString() + ") ");
            sbQry.AppendLine(@"    ");
            return sbQry;
        }

        public ISeries<ISeries<IInstant>> BulkInsert(
            IInstantSeries table,
            bool keysFromDeckis = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            try
            {
                IInstantSeries tab = table;
                if (tab.Any())
                {
                    IList<RubricSqlMapping> nMaps = new List<RubricSqlMapping>();
                    if (buildMapping)
                    {
                        SqlMapper imapper = new SqlMapper(tab, keysFromDeckis);
                    }
                    nMaps = tab.Rubrics.Mappings;
                    string dbName = _cn.Database;
                    SqlAdapter afad = new SqlAdapter(_cn);
                    afad.DataBulk(tab, tab.InstantType.Name, tempType, BulkDbType.TempDB);
                    _cn.ChangeDatabase(dbName);
                    ISeries<ISeries<IInstant>> nSet = new Catalog<ISeries<IInstant>>();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(@"    ");
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        sb.AppendLine(@"    ");

                        MemberRubric[] ic = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.ColumnOrdinal.Contains(c.FieldId))
                            .ToArray();
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        if (updateExcept != null)
                        {
                            ic = ic.Where(c => !updateExcept.Contains(c.RubricName)).ToArray();
                            ik = ik.Where(c => !updateExcept.Contains(c.RubricName)).ToArray();
                        }

                        string qry = BulkInsertQuery(
                                dbName,
                                tab.InstantType.Name,
                                nMap.DbTableName,
                                ic,
                                ik,
                                updateKeys
                            )
                            .ToString();
                        sb.Append(qry);
                        sb.AppendLine(@"    ");
                    }
                    sb.AppendLine(@"    ");

                    ISeries<ISeries<IInstant>> bIInstantSeries = afad.ExecuteInsert(sb.ToString(), tab, true);

                    if (nSet.Count == 0)
                        nSet = bIInstantSeries;
                    else
                        foreach (ISeries<IInstant> its in bIInstantSeries.AsValues())
                        {
                            if (nSet.Contains(its))
                            {
                                nSet[its].Put(its.AsValues());
                            }
                            else
                                nSet.Add(its);
                        }
                    sb.Clear();

                    return nSet;
                }
                else
                    return null;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlInsertException(ex.ToString());
            }
        }

        public StringBuilder BulkInsertQuery(
            string DBName,
            string buforName,
            string tableName,
            MemberRubric[] columns,
            MemberRubric[] keys,
            bool updateKeys = true
        )
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbv = new StringBuilder();
            string bName = buforName;
            string tName = tableName;
            MemberRubric[] rubrics = keys.Concat(columns).ToArray();
            string dbName = DBName;
            sb.AppendLine(@"  ");
            sb.AppendFormat(@"INSERT INTO [{0}].[dbo].[" + tName + "] (", dbName);
            sbv.Append(@"SELECT ");
            string delim = "";
            int c = 0;
            for (int i = 0; i < rubrics.Length; i++)
            {
                if (c > 0)
                    delim = ",";
                sb.AppendFormat(
                    CultureInfo.InvariantCulture,
                    @"{0}[{1}]",
                    delim,
                    rubrics[i].RubricName
                );
                sbv.AppendFormat(
                    CultureInfo.InvariantCulture,
                    @"{0}[S].[{1}]",
                    delim,
                    rubrics[i].RubricName
                );
                c++;
            }
            sb.AppendFormat(
                CultureInfo.InvariantCulture,
                @") OUTPUT inserted.* {0}",
                sbv.ToString()
            );
            sb.AppendFormat(" FROM [tempdb].[dbo].[{0}] AS S ", bName, dbName, tName);
            sb.AppendLine("");
            sb.AppendLine(@"    ");
            sbv.Clear();
            return sb;
        }

        public ISeries<ISeries<IInstant>> Insert(
            IInstantSeries table,
            bool keysFromDeckis = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            return BulkInsert(
                table,
                keysFromDeckis,
                buildMapping,
                updateKeys,
                updateExcept,
                tempType
            );
        }

        public int SimpleInsert(IInstantSeries table, bool buildMapping, int batchSize = 1000)
        {
            try
            {
                IInstantSeries tab = table;
                IList<RubricSqlMapping> nMaps = new List<RubricSqlMapping>();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                int intSqlset = 0;
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        ;
                        MemberRubric[] ic = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.ColumnOrdinal.Contains(c.FieldId))
                            .ToArray();
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchInsertQuery(ir, nMap.DbTableName, ic, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");

                        intSqlset += afad.ExecuteInsert(sb.ToString());

                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                intSqlset += afad.ExecuteInsert(sb.ToString());
                return intSqlset;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlInsertException(ex.ToString());
            }
        }

        public int SimpleInsert(IInstantSeries table, int batchSize = 1000)
        {
            try
            {
                IInstantSeries tab = table;
                IList<RubricSqlMapping> nMaps = new List<RubricSqlMapping>();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                int intSqlset = 0;
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ic = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.ColumnOrdinal.Contains(c.FieldId))
                            .ToArray();
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchInsertQuery(ir, nMap.DbTableName, ic, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        intSqlset += afad.ExecuteInsert(sb.ToString());

                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                intSqlset += afad.ExecuteInsert(sb.ToString());

                return intSqlset;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlInsertException(ex.ToString());
            }
        }
    }

    public class SqlInsertException : Exception
    {
        public SqlInsertException(string message) : base(message) { }
    }
}
