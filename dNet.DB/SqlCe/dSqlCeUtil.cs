#region Bibliotecas
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
#endregion

namespace dNet.DB.SqlCe
{
	public class dSqlCeUtil : IDbUtil
	{
        #region AddParameter
        static public void AddParameter(SqlCeCommand cmd, string name, object value)
        {
            if (value == null) value = DBNull.Value;
            else if (value.GetType() == typeof(string) && value != null && (string)value == "") value = DBNull.Value;
            else if (value.GetType() == typeof(int) && (int)value == int.MinValue) value = DBNull.Value;
            else if (value.GetType() == typeof(double) && (double)value == double.MinValue) value = DBNull.Value;
            else if (value.GetType() == typeof(DateTime))
            {
                if ((DateTime)value == DateTime.MinValue) value = DBNull.Value;
                else value = ((DateTime)value).ToString("yyyy-MM-dd");
            }

            cmd.Parameters.Add(new SqlCeParameter(name, value));
        }
        
        static public void AddParameter(SqlCeDataAdapter da, string name, object value)
        {
            if (value == null) value = DBNull.Value;
            else if (value.GetType() == typeof(string) && value != null && (string)value == "") value = DBNull.Value;
            else if (value.GetType() == typeof(int) && (int)value == int.MinValue) value = DBNull.Value;
            else if (value.GetType() == typeof(double) && (double)value == double.MinValue) value = DBNull.Value;
            else if (value.GetType() == typeof(DateTime))
            {
                if ((DateTime)value == DateTime.MinValue) value = DBNull.Value;
                else value = ((DateTime)value).ToString("yyyy-MM-dd");
            }

            if (da.SelectCommand.CommandText != null)
                da.SelectCommand.Parameters.Add(new SqlCeParameter(name, value));
            else if (da.InsertCommand != null)
                da.InsertCommand.Parameters.Add(new SqlCeParameter(name, value));
            else if (da.DeleteCommand != null)
                da.DeleteCommand.Parameters.Add(new SqlCeParameter(name, value));
            else if (da.UpdateCommand != null)
                da.UpdateCommand.Parameters.Add(new SqlCeParameter(name, value));
        }
        #endregion

        #region AddHashParameters
        static public void AddHashParameters(SqlCeCommand cmd, Hashtable ht)
        {
            foreach (string key in ht.Keys)
            {
                if (key.Contains("@"))
                    AddParameter(cmd, key, ht[key]);
                else
                    AddParameter(cmd, "@" + key, ht[key]);
            }
        }

        static public void AddHashParameters(SqlCeDataAdapter da, Hashtable ht)
        {
            foreach (string key in ht.Keys)
            {
                if (key.Contains("@"))
                    AddParameter(da, key, ht[key]);
                else
                    AddParameter(da, "@" + key, ht[key]);
            }
        }
        #endregion

        #region GetHashValues
        static public Hashtable GetHashValues(SqlCeDataReader dr)
        {
            int n = dr.FieldCount;
            Hashtable hash = new Hashtable(n);
            if (dr.Read())
            {
                for (int i = 0; i < n; i++)
                {
                    hash.Add(dr.GetName(i), dr.GetValue(i));
                }
            }
            return hash;
        }
        #endregion

        #region CreateCompareDateString
        public string CreateCompareDateString(DateTime date, string column)
        {
            return "CAST(FLOOR(CAST(" + column + " AS float)) AS datetime) = '"
                + date.ToString("yyyy-MM-dd") + "'";
        }
        #endregion
	}
}