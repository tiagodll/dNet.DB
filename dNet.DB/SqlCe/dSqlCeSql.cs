#region Bibliotecas
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#endregion

namespace dNet.DB.SqlCe
{
	public class dSqlCeSql : IDbSql
	{
        public string ConnectionString { get; set; }
        public TypeOfDb DbType { get { return TypeOfDb.SqlCe; } }

        #region Select
        public DataTable Select(string query) { return Select(query, new Hashtable()); }
        public DataTable Select(string query, Hashtable parametros)
        {
            DataSet ds = new DataSet();
            using (SqlCeConnection dc = new SqlCeConnection(ConnectionString))
            {
                dc.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(query, dc);
                dSqlCeUtil.AddHashParameters(da, parametros);
                da.Fill(ds);
                dc.Close();
            }
            return (ds.Tables.Count > 0 ? ds.Tables[0] : null);
        }
        #endregion
        
        #region SelectOne
        public DataRow SelectOne(string query) { return SelectOne(query, new Hashtable()); }
        public DataRow SelectOne(string query, Hashtable parametros)
        {
            DataSet ds = new DataSet();
            using (SqlCeConnection dc = new SqlCeConnection(ConnectionString))
            {
                dc.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(query, dc);
                dSqlCeUtil.AddHashParameters(da, parametros);
                da.Fill(ds);
                dc.Close();
            }
            return (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null);
        }
        #endregion

        #region SelectOneHash
        public Hashtable SelectOneHash(string query) { return SelectOneHash(query, new Hashtable()); }
        public Hashtable SelectOneHash(string query, Hashtable parametros)
        {
            DataSet ds = new DataSet();
            using (SqlCeConnection dc = new SqlCeConnection(ConnectionString))
            {
                dc.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter(query, dc);
                dSqlCeUtil.AddHashParameters(da, parametros);
                da.Fill(ds);
                dc.Close();
            }
            if (ds.Tables.Count <= 0 && ds.Tables[0].Rows.Count <= 0)
                return null;
            else
                return Utils.Util.DataRow2Hash(ds.Tables[0].Rows[0]);
        }
        #endregion

        #region SelectScalar
        public object SelectScalar(string query) { return SelectScalar(query, new Hashtable()); }
        public object SelectScalar(string query, Hashtable parametros)
        {
            object obj = null;
            using (SqlCeConnection dc = new SqlCeConnection(ConnectionString))
            {
                dc.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, dc);
                dSqlCeUtil.AddHashParameters(cmd, parametros);
                obj = cmd.ExecuteScalar();
                dc.Close();
            }
            return obj;
        }
        #endregion

        #region NonQuery
        public int NonQuery(string query) { return NonQuery(query, new Hashtable()); }
        public int NonQuery(string query, Hashtable parametros)
        {
            int n = 0;
            using (SqlCeConnection dc = new SqlCeConnection(ConnectionString))
            {
                dc.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, dc);
                dSqlCeUtil.AddHashParameters(cmd, parametros);
                n = cmd.ExecuteNonQuery();
                dc.Close();
            }
            return n;
        }
        #endregion
	}
}