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

    public class SqlUpdate
    {
        private SqlConnection _cn;

        public SqlUpdate(SqlConnection cn)
        {
            _cn = cn;
        }

        public SqlUpdate(string cnstring)
        {
            _cn = new SqlConnection(cnstring);
        }

        public ISeries<ISeries<IInstant>> BatchUpdate(
            IInstantSeries table,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            int batchSize = 250
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
                foreach (IInstant ir in table)
                {
                    if (ir.GetType().DeclaringType != tab.InstantType)
                    {
                        if (buildMapping)
                        {
                            SqlMapper imapper = new SqlMapper(tab, keysFromDeck);
                        }
                        nMaps = tab.Rubrics.Mappings;
                    }

                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        ISeries<int> co = nMap.ColumnOrdinal;
                        ISeries<int> ko = nMap.KeyOrdinal;
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

                        string qry = BatchUpdateQuery(ir, nMap.DbTableName, ic, ik, updateKeys)
                            .ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        ISeries<ISeries<IInstant>> bIInstantSeries = afad.ExecuteUpdate(sb.ToString(), tab);
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
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                ISeries<ISeries<IInstant>> rIInstantSeries = afad.ExecuteUpdate(sb.ToString(), tab, true);

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
                throw new SqlUpdateException(ex.ToString());
            }
        }

        public StringBuilder BatchUpdateQuery(
            IInstant card,
            string tableName,
            MemberRubric[] columns,
            MemberRubric[] keys,
            bool updateKeys = true
        )
        {
            StringBuilder sb = new StringBuilder();
            string tName = tableName;
            IInstant ir = card;
            object[] ia = ((IValueArray)ir).ValueArray;
            MemberRubric[] ic = columns;
            MemberRubric[] ik = keys;

            sb.AppendLine(@"    ");
            sb.Append("UPDATE " + tName + " SET ");
            bool isUpdateCol = false;
            string delim = "";
            int c = 0;
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].RubricName.ToLower() == "updated")
                    isUpdateCol = true;
                if (ia[columns[i].FieldId] != DBNull.Value)
                {
                    if (c > 0)
                        delim = ",";
                    sb.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0}[{1}] = {2}{3}{2}",
                        delim,
                        columns[i].RubricName,
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
                sb.AppendFormat(CultureInfo.InvariantCulture, ", [updated] = '{0}'", DateTime.Now);

            if (updateKeys)
            {
                if (columns.Length > 0)
                    delim = ",";
                else
                    delim = "";
                c = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (ia[keys[i].FieldId] != DBNull.Value)
                    {
                        if (c > 0)
                            delim = ",";
                        sb.AppendFormat(
                            CultureInfo.InvariantCulture,
                            @"{0}[{1}] = {2}{3}{2}",
                            delim,
                            keys[i].RubricName,
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
            }
            sb.AppendLine(" OUTPUT inserted.*, deleted.* ");
            delim = "";
            c = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (i > 0)
                    delim = " AND ";
                else
                    delim = " WHERE ";

                if (ia[keys[i].FieldId] != DBNull.Value)
                {
                    if (c > 0)
                        delim = " AND ";
                    else
                        delim = " WHERE ";

                    sb.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0} [{1}] = {2}{3}{2}",
                        delim,
                        keys[i].RubricName,
                        (
                            keys[i].RubricType == typeof(string)
                            || keys[i].RubricType == typeof(DateTime)
                        )
                            ? "'"
                            : "",
                        (ia[keys[i].FieldId] != DBNull.Value)
                            ? (keys[i].RubricType != typeof(string))
                                ? Convert.ChangeType(ia[keys[i].FieldId], keys[i].RubricType)
                                : ia[keys[i].FieldId].ToString().Replace("'", "''")
                            : ""
                    );
                    c++;
                }
            }
            sb.AppendLine("");
            sb.AppendLine(@"    ");
            return sb;
        }

        public ISeries<ISeries<IInstant>> BulkUpdate(
            IInstantSeries table,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            try
            {
                IInstantSeries tab = table;
                if (tab.Count > 0)
                {
                    IList<RubricSqlMapping> nMaps = new List<RubricSqlMapping>();
                    if (buildMapping)
                    {
                        SqlMapper imapper = new SqlMapper(tab, keysFromDeck);
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

                        string qry = BulkUpdateQuery(
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

                    var bIInstantSeries = afad.ExecuteUpdate(sb.ToString(), tab, true);

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
                throw new SqlUpdateException(ex.ToString());
            }
        }

        public StringBuilder BulkUpdateQuery(
            string DBName,
            string buforName,
            string tableName,
            MemberRubric[] columns,
            MemberRubric[] keys,
            bool updateKeys = true
        )
        {
            StringBuilder sb = new StringBuilder();
            string bName = buforName;
            string tName = tableName;
            MemberRubric[] ic = columns;
            MemberRubric[] ik = keys;
            string dbName = DBName;
            sb.AppendLine(@"    ");
            sb.AppendFormat(@"UPDATE [{0}].[dbo].[" + tName + "] SET ", dbName);
            string delim = "";
            int c = 0;
            for (int i = 0; i < columns.Length; i++)
            {

                if (c > 0)
                    delim = ",";
                sb.AppendFormat(
                    CultureInfo.InvariantCulture,
                    @"{0}[{1}] =[S].[{2}]",
                    delim,
                    columns[i].RubricName,
                    columns[i].RubricName
                );
                c++;
            }

            if (updateKeys)
            {
                if (columns.Length > 0)
                    delim = ",";
                else
                    delim = "";
                c = 0;
                for (int i = 0; i < keys.Length; i++)
                {
                    if (c > 0)
                        delim = ",";
                    sb.AppendFormat(
                        CultureInfo.InvariantCulture,
                        @"{0}[{1}] = [S].[{2}]",
                        delim,
                        keys[i].RubricName,
                        keys[i].RubricName
                    );
                    c++;
                }
            }
            sb.AppendLine(" OUTPUT inserted.*, deleted.* ");
            sb.AppendFormat(
                " FROM [tempdb].[dbo].[{0}] AS S INNER JOIN [{1}].[dbo].[{2}] AS T ",
                bName,
                dbName,
                tName
            );
            delim = "";
            c = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (i > 0)
                    delim = " AND ";
                else
                    delim = " ON ";

                sb.AppendFormat(
                    CultureInfo.InvariantCulture,
                    @"{0} [T].[{1}] = [S].[{2}]",
                    delim,
                    keys[i].RubricName,
                    keys[i].RubricName
                );
                c++;
            }
            sb.AppendLine("");
            sb.AppendLine(@"    ");
            return sb;
        }

        public int SimpleUpdate(
            IInstantSeries table,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            int batchSize = 500
        )
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
                foreach (IInstant ir in table)
                {
                    if (ir.GetType().DeclaringType != tab.InstantType)
                    {
                        if (buildMapping)
                        {
                            SqlMapper imapper = new SqlMapper(tab);
                        }
                        nMaps = tab.Rubrics.Mappings;
                    }

                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        ISeries<int> co = nMap.ColumnOrdinal;
                        ISeries<int> ko = nMap.KeyOrdinal;
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

                        string qry = BatchUpdateQuery(ir, nMap.DbTableName, ic, ik, updateKeys)
                            .ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        intSqlset += afad.ExecuteUpdate(sb.ToString());
                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                intSqlset += afad.ExecuteUpdate(sb.ToString());
                return intSqlset;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlUpdateException(ex.ToString());
            }
        }

        public ISeries<ISeries<IInstant>> Update(
            IInstantSeries table,
            bool keysFromDeck = false,
            bool buildMapping = false,
            bool updateKeys = false,
            string[] updateExcept = null,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            return BulkUpdate(
                table,
                keysFromDeck,
                buildMapping,
                updateKeys,
                updateExcept,
                tempType
            );
        }

        public class SqlUpdateException : Exception
        {
            public SqlUpdateException(string message) : base(message) { }
        }
    }
}
