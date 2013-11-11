#region Bibliotecas
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#endregion

namespace dNet.DB.MySql
{
	public class dMySqlMaker : IDbMaker
	{
        public string ConnectionString { get; set; }

		#region MakeInsert
		public int MakeInsert(string table, Hashtable param)
		{
			MySqlConnection dc = new MySqlConnection(ConnectionString);
			dc.Open();

			string p1 = "";
            string p2 = "";
			foreach (string key in param.Keys)
			{
				p1 += ", " + key;
				p2 += ", @" + key;
			}

			MySqlCommand cmd = new MySqlCommand(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, p1, p2), dc);
			foreach (string key in param.Keys)
			{
                dMySqlUtil.AddParameter(cmd, "@" + key, param[key]);
			}
			int n = cmd.ExecuteNonQuery();
			dc.Close();
            return n;
		}
		#endregion

		#region MakeUpdate - ID
		public int MakeUpdate(string table, Hashtable param, string IdName)
		{
			MySqlConnection dc = new MySqlConnection(ConnectionString);
			dc.Open();
			
			string p1 = "";
			foreach (string key in param.Keys)
			{
				p1 += string.Format("{0}=@{0}, ", key);
			}
			p1 = p1.Substring(0, p1.Length - 2);

			MySqlCommand cmd = new MySqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2}=@{2}", table, p1, IdName), dc);
            dMySqlUtil.AddParameter(cmd, "@" + IdName, param[IdName]);
			foreach (string key in param.Keys)
			{
                dMySqlUtil.AddParameter(cmd, "@" + key, param[key]);
			}
			int n = cmd.ExecuteNonQuery();

			dc.Close();
            return n;
		}
		#endregion
		#region MakeUpdate - WHERE
        public int MakeUpdateWhere(string table, Hashtable param, string where)
		{
			MySqlConnection dc = new MySqlConnection(ConnectionString);
			dc.Open();

			string p1 = "";
			foreach (string key in param.Keys)
			{
				p1 += string.Format("{0}=@{0}, ", key);
			}
			p1 = p1.Substring(0, p1.Length - 2);

			MySqlCommand cmd = new MySqlCommand(string.Format("UPDATE {0} SET {1} WHERE {2}", table, p1, where), dc);
			foreach (string key in param.Keys)
			{
                dMySqlUtil.AddParameter(cmd, "@" + key, param[key]);
			}
			int n = cmd.ExecuteNonQuery();

			dc.Close();
            return n;
		}
		#endregion

        #region MakeDelete
        public int MakeDelete(string table, Hashtable param)
        {
            MySqlConnection dc = new MySqlConnection(ConnectionString);
            dc.Open();

            string p1 = "";
            foreach (string key in param.Keys)
            {
                p1 += string.Format(" AND {0}=?{0}", key);
            }
            p1 = p1.Substring(4);

            MySqlCommand cmd = new MySqlCommand(string.Format("DELETE FROM {0} WHERE {1}", table, p1), dc);
            dMySqlUtil.AddHashParameters(cmd, param);
            int n = cmd.ExecuteNonQuery();

            dc.Close();
            return n;
        }
        #endregion
	}
}