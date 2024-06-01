﻿namespace Undersoft.SDK.Instant.Sql
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Undersoft.SDK.Instant.Series;
    using Undersoft.SDK.Rubrics;
    using Undersoft.SDK.Series;

    public enum BulkPrepareType
    {
        Trunc,
        Drop,
        None
    }

    public enum BulkDbType
    {
        TempDB,
        Origin,
        None
    }

    public class SqlAdapter
    {
        private SqlCommand _cmd;
        private SqlConnection _cn;

        public SqlAdapter(SqlConnection cn)
        {
            _cn = cn;
        }

        public SqlAdapter(string cnstring)
        {
            _cn = new SqlConnection(cnstring);
        }

        public bool DataBulk(
            InstantSeriesItem[] cards,
            string buforTable,
            BulkPrepareType prepareType = BulkPrepareType.None,
            BulkDbType dbType = BulkDbType.TempDB
        )
        {
            try
            {
                IInstantSeries deck = null;
                if (cards.Any())
                {
                    deck = cards.ElementAt(0).InstantSeries;
                    if (_cn.State == ConnectionState.Closed)
                        _cn.Open();
                    try
                    {
                        if (dbType == BulkDbType.TempDB)
                            _cn.ChangeDatabase("tempdb");
                        if (
                            !DbHand.Temp.DataDbTables.Have(buforTable)
                            || prepareType == BulkPrepareType.Drop
                        )
                        {
                            string createTable = "";
                            if (prepareType == BulkPrepareType.Drop)
                                createTable += "Drop table if exists [" + buforTable + "] \n";
                            createTable += "Create Table [" + buforTable + "] ( ";
                            foreach (MemberRubric column in deck.Rubrics.AsValues())
                            {
                                string sqlTypeString = "varchar(200)";
                                List<string> defineStr = new List<string>()
                                {
                                    "varchar",
                                    "nvarchar",
                                    "ntext",
                                    "varbinary"
                                };
                                List<string> defineDec = new List<string>()
                                {
                                    "decimal",
                                    "numeric"
                                };
                                int colLenght = column.RubricSize;
                                sqlTypeString = SqlNetType.NetTypeToSql(column.RubricType);
                                string addSize =
                                    (colLenght > 0)
                                        ? (defineStr.Contains(sqlTypeString))
                                            ? (string.Format(@"({0})", colLenght))
                                            : (defineDec.Contains(sqlTypeString))
                                                ? (string.Format(@"({0}, {1})", colLenght - 6, 6))
                                                : ""
                                        : "";
                                sqlTypeString += addSize;
                                createTable +=
                                    " [" + column.RubricName + "] " + sqlTypeString + ",";
                            }
                            createTable = createTable.TrimEnd(new char[] { ',' }) + " ) ";
                            SqlCommand createcmd = new SqlCommand(createTable, _cn);
                            createcmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new SqlInsertException(ex.ToString());
                    }
                    if (prepareType == BulkPrepareType.Trunc)
                    {
                        string deleteData = "Truncate Table [" + buforTable + "]";
                        SqlCommand delcmd = new SqlCommand(deleteData, _cn);
                        delcmd.ExecuteNonQuery();
                    }

                    try
                    {
                        DataReader ndr = new DataReader(cards);
                        SqlBulkCopy bulkcopy = new SqlBulkCopy(_cn);
                        bulkcopy.DestinationTableName = "[" + buforTable + "]";
                        bulkcopy.WriteToServer(ndr);
                    }
                    catch (SqlException ex)
                    {
                        throw new SqlInsertException(ex.ToString());
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw new SqlInsertException(ex.ToString());
            }
        }

        public bool DataBulk(
            IInstantSeries deck,
            string buforTable,
            BulkPrepareType prepareType = BulkPrepareType.None,
            BulkDbType dbType = BulkDbType.TempDB
        )
        {
            try
            {
                if (_cn.State == ConnectionState.Closed)
                    _cn.Open();
                try
                {
                    if (dbType == BulkDbType.TempDB)
                        _cn.ChangeDatabase("tempdb");
                    if (
                        !DbHand.Schema.DataDbTables.Have(buforTable)
                        || prepareType == BulkPrepareType.Drop
                    )
                    {
                        string createTable = "";
                        if (prepareType == BulkPrepareType.Drop)
                            createTable += "Drop table if exists [" + buforTable + "] \n";
                        createTable += "Create Table [" + buforTable + "] ( ";
                        foreach (MemberRubric column in deck.Rubrics.AsValues())
                        {
                            string sqlTypeString = "varchar(200)";
                            List<string> defineOne = new List<string>()
                            {
                                "varchar",
                                "nvarchar",
                                "ntext",
                                "varbinary"
                            };
                            List<string> defineDec = new List<string>() { "decimal", "numeric" };
                            int colLenght = column.RubricSize;
                            sqlTypeString = SqlNetType.NetTypeToSql(column.RubricType);
                            string addSize =
                                (colLenght > 0)
                                    ? (defineOne.Contains(sqlTypeString))
                                        ? (string.Format(@"({0})", colLenght))
                                        : (defineDec.Contains(sqlTypeString))
                                            ? (string.Format(@"({0}, {1})", colLenght - 6, 6))
                                            : ""
                                    : "";
                            sqlTypeString += addSize;
                            createTable += " [" + column.RubricName + "] " + sqlTypeString + ",";
                        }
                        createTable = createTable.TrimEnd(new char[] { ',' }) + " ) ";
                        SqlCommand createcmd = new SqlCommand(createTable, _cn);
                        createcmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new SqlInsertException(ex.ToString());
                }
                if (prepareType == BulkPrepareType.Trunc)
                {
                    string deleteData = "Truncate Table [" + buforTable + "]";
                    SqlCommand delcmd = new SqlCommand(deleteData, _cn);
                    delcmd.ExecuteNonQuery();
                }

                try
                {
                    DataReader ndr = new DataReader(deck);
                    SqlBulkCopy bulkcopy = new SqlBulkCopy(_cn);
                    bulkcopy.DestinationTableName = "[" + buforTable + "]";
                    bulkcopy.WriteToServer(ndr);
                }
                catch (SqlException ex)
                {
                    throw new SqlInsertException(ex.ToString());
                }
                return true;
            }
            catch (SqlException ex)
            {
                throw new SqlInsertException(ex.ToString());
            }
        }

        public int ExecuteDelete(string sqlqry, bool disposeCmd = false)
        {
            if (_cmd == null)
                _cmd = _cn.CreateCommand();
            SqlCommand cmd = _cmd;
            cmd.CommandText = sqlqry;
            SqlTransaction tr = _cn.BeginTransaction();
            cmd.Transaction = tr;
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            int i = cmd.ExecuteNonQuery();
            tr.Commit();
            if (disposeCmd)
                cmd.Dispose();
            return i;
        }

        public ISeries<ISeries<IInstant>> ExecuteDelete(
            string sqlqry,
            IInstantSeries cards,
            bool disposeCmd = false
        )
        {
            if (_cmd == null)
                _cmd = _cn.CreateCommand();
            SqlCommand cmd = _cmd;
            cmd.CommandText = sqlqry;
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            IDataReader sdr = cmd.ExecuteReader();
            SqlReader<IInstantSeries> dr = new SqlReader<IInstantSeries>(sdr);
            var _is = dr.DeleteRead(cards);
            sdr.Dispose();
            if (disposeCmd)
                cmd.Dispose();
            return _is;
        }

        public IInstantSeries ExecuteLoad(string sqlqry, string tableName = null)
        {
            SqlCommand cmd = new SqlCommand(sqlqry, _cn);
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            IDataReader sdr = cmd.ExecuteReader();
            SqlReader<IInstantSeries> dr = new SqlReader<IInstantSeries>(sdr);
            IInstantSeries it = dr.ReadLoaded(tableName);
            sdr.Dispose();
            cmd.Dispose();
            return it;
        }

        public IInstantSeries ExecuteLoad(
            string sqlqry,
            string tableName,
            ISeries<string> keyNames = null
        )
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sqlqry, _cn);
                cmd.Prepare();
                if (_cn.State == ConnectionState.Closed)
                    _cn.Open();
                IDataReader sdr = cmd.ExecuteReader();
                SqlReader<IInstantSeries> dr = new SqlReader<IInstantSeries>(sdr);
                IInstantSeries it = dr.ReadLoaded(tableName, keyNames);
                sdr.Dispose();
                cmd.Dispose();
                return it;
            }
            catch (Exception ex)
            {
                throw new SqlException(ex.ToString());
            }
        }

        public int ExecuteInsert(string sqlqry, bool disposeCmd = false)
        {
            if (_cmd == null)
                _cmd = _cn.CreateCommand();
            SqlCommand cmd = _cmd;
            cmd.CommandText = sqlqry;
            SqlTransaction tr = _cn.BeginTransaction();
            cmd.Transaction = tr;
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            int i = cmd.ExecuteNonQuery();
            tr.Commit();
            if (disposeCmd)
                cmd.Dispose();
            return i;
        }

        public ISeries<ISeries<IInstant>> ExecuteInsert(
            string sqlqry,
            IInstantSeries cards,
            bool disposeCmd = false
        )
        {
            if (_cmd == null)
                _cmd = _cn.CreateCommand();
            SqlCommand cmd = _cmd;
            cmd.CommandText = sqlqry;
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            IDataReader sdr = cmd.ExecuteReader();
            SqlReader<IInstantSeries> dr = new SqlReader<IInstantSeries>(sdr);
            var _is = dr.InsertRead(cards);
            sdr.Dispose();
            if (disposeCmd)
                cmd.Dispose();
            return _is;
        }

        public int ExecuteUpdate(string sqlqry, bool disposeCmd = false)
        {
            if (_cmd == null)
                _cmd = _cn.CreateCommand();
            SqlCommand cmd = _cmd;
            cmd.CommandText = sqlqry;
            SqlTransaction tr = _cn.BeginTransaction();
            cmd.Transaction = tr;
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            int i = cmd.ExecuteNonQuery();
            tr.Commit();
            if (disposeCmd)
                cmd.Dispose();
            return i;
        }

        public ISeries<ISeries<IInstant>> ExecuteUpdate(
            string sqlqry,
            IInstantSeries cards,
            bool disposeCmd = false
        )
        {
            if (_cmd == null)
                _cmd = _cn.CreateCommand();
            SqlCommand cmd = _cmd;
            cmd.CommandText = sqlqry;
            cmd.Prepare();
            if (_cn.State == ConnectionState.Closed)
                _cn.Open();
            IDataReader sdr = cmd.ExecuteReader();
            SqlReader<IInstantSeries> dr = new SqlReader<IInstantSeries>(sdr);
            var _is = dr.UpdateRead(cards);
            sdr.Dispose();
            if (disposeCmd)
                cmd.Dispose();
            return _is;
        }
    }
}
