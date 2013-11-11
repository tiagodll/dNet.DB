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
	public class dMsSqlMaker : IDbMaker
	{
        public string ConnectionString { get; set; }

		#region MakeInsert
		public int MakeInsert(string table, Hashtable param)
		{
			SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();

			string p1 = "";
            string p2 = "";
			foreach (string key in param.Keys)
			{
				p1 += ", " + key;
				p2 += ", @" + key;
			}
            if (p1.Length > 2)
                p1 = p1.Substring(2);
            if (p2.Length > 2)
                p2 = p2.Substring(2);

			SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, p1, p2), dc);
			foreach (string key in param.Keys)
			{
                dMsSqlUtil.AddParameter(cmd, "@" + key, param[key]);
			}
			int n = cmd.ExecuteNonQuery();
			dc.Close();
            return n;
		}
		#endregion

		#region MakeUpdate - ID
		public int MakeUpdate(string table, Hashtable param, string IdName)
		{
			SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();
			
			string p1 = "";
			foreach (string key in param.Keys)
			{
				p1 += string.Format("{0}=@{0}, ", key);
			}
			p1 = p1.Substring(0, p1.Length - 2);

			SqlCommand cmd = new SqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2}=@{2}", table, p1, IdName), dc);
            //dMsSqlUtil.AddParameter(cmd, "@" + IdName, param[IdName]);
			foreach (string key in param.Keys)
			{
                dMsSqlUtil.AddParameter(cmd, "@" + key, param[key]);
			}
			int n = cmd.ExecuteNonQuery();

			dc.Close();
            return n;
		}
		#endregion
		#region MakeUpdate - WHERE
        public int MakeUpdateWhere(string table, Hashtable param, string where)
		{
			SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();

			string p1 = "";
			foreach (string key in param.Keys)
			{
				p1 += string.Format("{0}=@{0}, ", key);
			}
			p1 = p1.Substring(0, p1.Length - 2);

			SqlCommand cmd = new SqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2}", table, p1, where), dc);
			foreach (string key in param.Keys)
			{
                dMsSqlUtil.AddParameter(cmd, "@" + key, param[key]);
			}
			int n = cmd.ExecuteNonQuery();

			dc.Close();
            return n;
		}
		#endregion

        #region MakeDelete
        public int MakeDelete(string table, Hashtable param)
        {
            SqlConnection dc = new SqlConnection(ConnectionString);
            dc.Open();

            string p1 = "";
            foreach (string key in param.Keys)
            {
                p1 += string.Format(" AND {0}=@{0}", key);
            }
            p1 = p1.Substring(4);

            SqlCommand cmd = new SqlCommand(string.Format("DELETE FROM {0} WHERE {1}", table, p1), dc);
            dMsSqlUtil.AddHashParameters(cmd, param);
            int n = cmd.ExecuteNonQuery();

            dc.Close();
            return n;
        }
        #endregion
	}
}