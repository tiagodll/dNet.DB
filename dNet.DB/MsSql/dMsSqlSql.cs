#region Bibliotecas
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#endregion

namespace dNet.DB.MsSql
{
	public class dMsSqlSql : IDbSql
	{
        public string ConnectionString { get; set; }
        public TypeOfDb DbType { get { return TypeOfDb.MsSql; } }

        #region Select
        public DataTable Select(string query) { return Select(query, new Hashtable()); }
        public DataTable Select(string query, Hashtable parametros)
        {
            DataSet ds = new DataSet();
            using (SqlConnection dc = new SqlConnection(ConnectionString))
            {
                dc.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, dc);
                dMsSqlUtil.AddHashParameters(da, parametros);
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
            using (SqlConnection dc = new SqlConnection(ConnectionString))
            {
                dc.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, dc);
                dMsSqlUtil.AddHashParameters(da, parametros);
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
            using (SqlConnection dc = new SqlConnection(ConnectionString))
            {
                dc.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, dc);
                dMsSqlUtil.AddHashParameters(da, parametros);
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
            using (SqlConnection dc = new SqlConnection(ConnectionString))
            {
                dc.Open();
                SqlCommand cmd = new SqlCommand(query, dc);
                dMsSqlUtil.AddHashParameters(cmd, parametros);
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
            using (SqlConnection dc = new SqlConnection(ConnectionString))
            {
                dc.Open();
                SqlCommand cmd = new SqlCommand(query, dc);
                dMsSqlUtil.AddHashParameters(cmd, parametros);
                n = cmd.ExecuteNonQuery();
                dc.Close();
            }
            return n;
        }
        #endregion
	}
}