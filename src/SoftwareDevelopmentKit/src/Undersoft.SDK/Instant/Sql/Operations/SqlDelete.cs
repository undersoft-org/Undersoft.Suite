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

    public class SqlDeleteException : Exception
    {
        public SqlDeleteException(string message) : base(message) { }
    }

    internal class SqlDelete
    {
        private SqlConnection _cn;

        public SqlDelete(SqlConnection cn)
        {
            _cn = cn;
        }

        public SqlDelete(string cnstring)
        {
            _cn = new SqlConnection(cnstring);
        }

        public ISeries<ISeries<IInstant>> BatchDelete(
            IInstantSeries table,
            bool buildMapping,
            int batchSize = 1000
        )
        {
            try
            {
                IInstantSeries tab = table;
                RubricSqlMappings nMaps = new RubricSqlMappings();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                ISeries<ISeries<IInstant>> nSet = new Catalog<ISeries<IInstant>>();
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchDeleteQuery(ir, nMap.DbTableName, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        var bIInstantSeries = afad.ExecuteDelete(sb.ToString(), tab);
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

                var rIInstantSeries = afad.ExecuteDelete(sb.ToString(), tab);

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
                throw new SqlDeleteException(ex.ToString());
            }
        }

        public ISeries<ISeries<IInstant>> BatchDelete(IInstantSeries table, int batchSize = 1000)
        {
            try
            {
                IInstantSeries tab = table;
                RubricSqlMappings nMaps = new RubricSqlMappings();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                ISeries<ISeries<IInstant>> nSet = new Catalog<ISeries<IInstant>>();
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchDeleteQuery(ir, nMap.DbTableName, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        var bIInstantSeries = afad.ExecuteDelete(sb.ToString(), tab);
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

                var rIInstantSeries = afad.ExecuteDelete(sb.ToString(), tab);

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
                throw new SqlDeleteException(ex.ToString());
            }
        }

        public StringBuilder BatchDeleteQuery(IInstant card, string tableName, MemberRubric[] keys)
        {
            StringBuilder sb = new StringBuilder();
            string tName = tableName;
            IInstant ir = card;
            object[] ia = ((IValueArray)ir).ValueArray;
            MemberRubric[] ik = keys;

            sb.AppendLine(@"    ");
            sb.Append("DELETE FROM " + tableName + " OUTPUT deleted.* ");
            string delim = "";
            int c = 0;

            delim = "";
            c = 0;
            for (int i = 0; i < keys.Length; i++)
            {
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

        public ISeries<ISeries<IInstant>> BulkDelete(
            IInstantSeries table,
            bool keysFromDeckis = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            try
            {
                IInstantSeries tab = table;
                if (tab.Any())
                {
                    RubricSqlMappings nMaps = new RubricSqlMappings();
                    if (buildMapping)
                    {
                        SqlMapper imapper = new SqlMapper(tab, keysFromDeckis);
                    }
                    nMaps = tab.Rubrics.Mappings;
                    string dbName = _cn.Database;
                    SqlAdapter adapter = new SqlAdapter(_cn);
                    adapter.DataBulk(tab, tab.InstantType.Name, tempType, BulkDbType.TempDB);
                    _cn.ChangeDatabase(dbName);
                    ISeries<ISeries<IInstant>> nSet = new Catalog<ISeries<IInstant>>();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(@"    ");
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        sb.AppendLine(@"    ");

                        string qry = BulkDeleteQuery(dbName, tab.InstantType.Name, nMap.DbTableName)
                            .ToString();
                        sb.Append(qry);

                        sb.AppendLine(@"    ");
                    }
                    sb.AppendLine(@"    ");

                    ISeries<ISeries<IInstant>> bIInstantSeries = adapter.ExecuteDelete(
                        sb.ToString(),
                        tab,
                        true
                    );

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
                throw new SqlDeleteException(ex.ToString());
            }
        }

        public StringBuilder BulkDeleteQuery(string DBName, string buforName, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            string bName = buforName;
            string tName = tableName;
            string dbName = DBName;
            sb.AppendLine(@"  ");
            sb.AppendFormat(
                @"DELETE FROM [{0}].[dbo].[" + tName + "] OUTPUT deleted.* WHERE EXISTS (",
                dbName
            );
            sb.AppendFormat("SELECT * FROM [tempdb].[dbo].[{0}] AS S)", bName);
            sb.AppendLine("");
            sb.AppendLine(@"    ");
            return sb;
        }

        public ISeries<ISeries<IInstant>> Delete(
            IInstantSeries table,
            bool keysFromDeckis = false,
            bool buildMapping = false,
            BulkPrepareType tempType = BulkPrepareType.Trunc
        )
        {
            return BulkDelete(table, keysFromDeckis, buildMapping, tempType);
        }

        public int SimpleDelete(IInstantSeries table, bool buildMapping, int batchSize = 1000)
        {
            try
            {
                IInstantSeries tab = table;
                RubricSqlMappings nMaps = new RubricSqlMappings();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                int intSqlset = 0;
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchDeleteQuery(ir, nMap.DbTableName, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");

                        intSqlset += afad.ExecuteDelete(sb.ToString());

                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                intSqlset += afad.ExecuteDelete(sb.ToString());
                return intSqlset;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlDeleteException(ex.ToString());
            }
        }

        public int SimpleDelete(IInstantSeries table, int batchSize = 1000)
        {
            try
            {
                IInstantSeries tab = table;
                RubricSqlMappings nMaps = new RubricSqlMappings();
                SqlAdapter afad = new SqlAdapter(_cn);
                StringBuilder sb = new StringBuilder();
                int intSqlset = 0;
                sb.AppendLine(@"    ");
                int count = 0;
                foreach (IInstant ir in tab)
                {
                    foreach (RubricSqlMapping nMap in nMaps)
                    {
                        MemberRubric[] ik = tab.Rubrics
                            .AsValues()
                            .Where(c => nMap.KeyOrdinal.Contains(c.FieldId))
                            .ToArray();

                        string qry = BatchDeleteQuery(ir, nMap.DbTableName, ik).ToString();
                        sb.Append(qry);
                        count++;
                    }
                    if (count >= batchSize)
                    {
                        sb.AppendLine(@"    ");
                        intSqlset += afad.ExecuteDelete(sb.ToString());

                        sb.Clear();
                        sb.AppendLine(@"    ");
                        count = 0;
                    }
                }
                sb.AppendLine(@"    ");

                intSqlset += afad.ExecuteDelete(sb.ToString());

                return intSqlset;
            }
            catch (SqlException ex)
            {
                _cn.Close();
                throw new SqlDeleteException(ex.ToString());
            }
        }
    }
}
