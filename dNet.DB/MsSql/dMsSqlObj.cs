#region Bibliotecas
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using dNet;
using dNet.DB.Utils;
using dNet.DB.MsSql;
#endregion

namespace dNet.DB.MsSql
{
	public class dMsSqlObj : IDbObj
	{
        public string ConnectionString { get; set; }

		#region Load
        public bool Load(dModel obj) { return Load(obj, ""); }
        public bool Load(dModel obj, string where)
        {
            bool resp = false;
            string query = "SELECT * FROM " + obj.GetTableName() + " WHERE 1=1 ";
            if (where.Length > 0)
                query += " AND " + where;
			Hashtable htemp = new Hashtable();
            Hashtable pks = obj.GetPKs(); 
            foreach (string key in pks.Keys)
            {
                query += " AND " + key + "=@" + key;
                htemp.Add(key, pks[key]);
            }

			System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();
            SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();
			SqlCommand cmd = new SqlCommand(query, dc);
			dMsSqlUtil.AddHashParameters(cmd, htemp);
			SqlDataReader dr = cmd.ExecuteReader();
			if (dr.Read())
			{
                object[] attributes = null;
                string column = "";
				foreach (System.Reflection.PropertyInfo pi in properties)
				{
                    attributes = pi.GetCustomAttributes(typeof(dNet.DB.ColumnAttribute), false);
                    if (attributes.Length == 0)
                        continue;
                    else
                        column = (attributes[0] as dNet.DB.ColumnAttribute).DbName;

					try
					{
						if (pi.PropertyType == typeof(string))
                            pi.SetValue(obj, dr[column].ToString(), null);
						else if (pi.PropertyType == typeof(int))
                            pi.SetValue(obj, TryConvert.ToInt(dr[column].ToString()), null);
						else if (pi.PropertyType == typeof(decimal))
                            pi.SetValue(obj, TryConvert.ToDecimal(dr[column].ToString()), null);
						else if (pi.PropertyType == typeof(double))
                            pi.SetValue(obj, TryConvert.ToDouble(dr[column].ToString()), null);
						else if (pi.PropertyType == typeof(bool))
                            pi.SetValue(obj, TryConvert.ToBool(dr[column].ToString()), null);
						else if (pi.PropertyType == typeof(DateTime))
						{
							DateTime tmp = DateTime.MinValue;
                            try { tmp = DateTime.Parse(dr[column].ToString()); }
							catch { }
							pi.SetValue(obj, tmp, null);
						}
						else
						{
                            pi.SetValue(obj, dr[column], null);
						}
					}
					catch (Exception exc) { throw new Exception("Erro ao montar obj da Request.", exc); }
				}
				resp = true;
			}
			dr.Close();
			dc.Close();
			return resp;
		}
		#endregion
        #region LoadList
        public List<T> LoadList<T>(string where) where T : dModel { return LoadList<T>("*", where); }
        public List<T> LoadList<T>(string fields, string where) where T : dModel
        {
            T objModel = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(null);
            string query = "SELECT " + fields + " FROM " + objModel.GetTableName() + " WHERE 1=1 ";
            if (where.Length > 0)
                query += " AND " + where;

            string[] arrFields = fields.Split(new char[] { ',' });
            for (int i = 0; i < arrFields.Length; i++)
                arrFields[i] = arrFields[i].Trim();

            SqlConnection dc = new SqlConnection(ConnectionString);
            dc.Open();
            SqlCommand cmd = new SqlCommand(query, dc);
            SqlDataReader dr = cmd.ExecuteReader();
            List<T> arr = new List<T>();
            while (dr.Read())
            {
				T obj = (T)objModel.GetType().GetConstructor(new Type[] { }).Invoke(null);
                System.Reflection.PropertyInfo[] properties = objModel.GetType().GetProperties();
                object[] attributes = null;
                string column = "";
                foreach (System.Reflection.PropertyInfo pi in properties)
                {
                    try
                    {
                        attributes = pi.GetCustomAttributes(typeof(dNet.DB.ColumnAttribute), false);

                        if (attributes.Length > 0)
                        {
                            column = (attributes[0] as dNet.DB.ColumnAttribute).DbName;

                            if (fields.Trim() != "*" && !Util.ArrayContains(arrFields, pi.Name))
                                continue;
                            try { dr.GetOrdinal(column); }
                            catch { continue; }


                            if (pi.PropertyType == typeof(string))
                                pi.SetValue(obj, dr[column].ToString(), null);
                            else if (pi.PropertyType == typeof(int))
                                pi.SetValue(obj, TryConvert.ToInt(dr[column]), null);
                            else if (pi.PropertyType == typeof(int?))
                                pi.SetValue(obj, TryConvert.ToIntN(dr[column]), null);
                            else if (pi.PropertyType == typeof(decimal))
                                pi.SetValue(obj, TryConvert.ToDecimal(dr[column]), null);
                            else if (pi.PropertyType == typeof(decimal?))
                                pi.SetValue(obj, TryConvert.ToDecimalN(dr[column]), null);
                            else if (pi.PropertyType == typeof(double))
                                pi.SetValue(obj, TryConvert.ToDouble(dr[column]), null);
                            else if (pi.PropertyType == typeof(double?))
                                pi.SetValue(obj, TryConvert.ToDoubleN(dr[column]), null);
                            else if (pi.PropertyType == typeof(bool))
                                pi.SetValue(obj, TryConvert.ToBool(dr[column]), null);
                            else if (pi.PropertyType == typeof(bool?))
                                pi.SetValue(obj, TryConvert.ToBoolN(dr[column]), null);
                            else if (pi.PropertyType == typeof(DateTime?))
                                pi.SetValue(obj, TryConvert.ToDateTimeN(dr[pi.Name]), null);
                            else if (pi.PropertyType == typeof(DateTime))
                            {
                                DateTime tmp = DateTime.MinValue;
                                try { tmp = DateTime.Parse(dr[column].ToString()); }
                                catch { }
                                pi.SetValue(obj, tmp, null);
                            }
                            else
                            {
                                pi.SetValue(obj, dr[column], null);
                            }
                        }
                    }
                    catch (Exception exc) { throw new Exception("Model error.", exc); }
                }
                arr.Add(obj);
            }
            dr.Close();
            dc.Close();

            return arr;
        }
        #endregion
        #region LoadProp
        public bool LoadProp(dModel obj)
        {
            bool resp = false;
            Hashtable htemp = new Hashtable();
            System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();

            string query = "SELECT * FROM " + obj.GetTableName() + " WHERE 1=1 ";
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                System.Reflection.PropertyInfo pitmp = obj.GetType().GetProperty(pi.Name + "Modified");
                if (pitmp != null && TryConvert.ToBool(pitmp.GetValue(obj, null)))
                {
                    query += " AND " + pi.Name + "=@" + pi.Name;
                    htemp.Add(pi.Name, pi.GetValue(obj, null));
                }
            }

            SqlConnection dc = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(query, dc);
            dMsSqlUtil.AddHashParameters(cmd, htemp);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                object[] attributes = null;
                string column = "";
                foreach (System.Reflection.PropertyInfo pi in properties)
                {
                        try
                        {
                            attributes = pi.GetCustomAttributes(typeof(dNet.DB.ColumnAttribute), false);
                            if (attributes.Length == 0)
                                continue;
                            else
                                column = (attributes[0] as dNet.DB.ColumnAttribute).DbName;

                            if (pi.PropertyType == typeof(string))
                                pi.SetValue(obj, dr[column].ToString(), null);
                            else if (pi.PropertyType == typeof(int))
                                pi.SetValue(obj, TryConvert.ToInt(dr[column].ToString()), null);
                            else if (pi.PropertyType == typeof(decimal))
                                pi.SetValue(obj, TryConvert.ToDecimal(dr[column].ToString()), null);
                            else if (pi.PropertyType == typeof(double))
                                pi.SetValue(obj, TryConvert.ToDouble(dr[column].ToString()), null);
                            else if (pi.PropertyType == typeof(bool))
                                pi.SetValue(obj, TryConvert.ToBool(dr[column].ToString()), null);
                            else if (pi.PropertyType == typeof(DateTime))
                            {
                                DateTime tmp = DateTime.MinValue;
                                try { tmp = DateTime.Parse(dr[column].ToString()); }
                                catch { }
                                pi.SetValue(obj, tmp, null);
                            }
                            else
                            {
                                pi.SetValue(obj, dr[column], null);
                            }
                        }
                        catch (Exception exc) { throw new Exception("Erro ao montar obj da Request.", exc); }
                }
                resp = true;
            }
            dr.Close();
            dc.Close();
            return resp;
        }
        #endregion

        #region Save
        public bool Save(dModel obj)
        {
            string query = "SELECT COUNT(1) FROM " + obj.GetTableName() + " WHERE 1=1 ";
            Hashtable htemp = new Hashtable();
            Hashtable pks = obj.GetPKs();
            foreach (string key in pks.Keys)
            {
                query += " AND " + key + "=@" + key;
                htemp.Add(key, pks[key]);
            }

            if (pks.Count == 0 || TryConvert.ToInt(new dMsSqlSql() { ConnectionString=this.ConnectionString }.SelectScalar(query, htemp)) == 0)
                return Insert(obj);
            else
                return Update(obj);
        }
        #endregion
		#region Insert
		public bool Insert(dModel obj)
		{
            Hashtable pks = obj.GetPKs();
            SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();
			StringBuilder sb1 = new StringBuilder("INSERT INTO " + obj.GetTableName() + "(");
			StringBuilder sb2 = new StringBuilder(") VALUES(");
			Hashtable htemp = new Hashtable();
			System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();
			object[] attributes = null;
            System.Reflection.PropertyInfo identity = null;
            string column = "";
            foreach (System.Reflection.PropertyInfo pi in properties)
			{
                attributes = pi.GetCustomAttributes(typeof(dNet.DB.ColumnAttribute), false);
                if (attributes.Length > 0)
                {
                    if(!(attributes[0] as dNet.DB.ColumnAttribute).IsAutoIncrement)
                    {
                        System.Reflection.PropertyInfo pinfo = obj.GetType().GetProperty(pi.Name + "Modified");
                        if (TryConvert.ToBool(pinfo.GetValue(obj, null)))
                        {
                            column = (attributes[0] as dNet.DB.ColumnAttribute).DbName;
                            if (pks.ContainsKey(column) && pks[column] == null)
                                pi.SetValue(obj, TryConvert.ToDecimal(new SqlCommand("SELECT MAX(" + pks[column] + ") AS N FROM " + obj.GetTableName(), dc).ExecuteScalar()) + 1, null);

                            sb1.Append(column + ", ");
                            sb2.Append("@" + column + ", ");

                            if (pi.PropertyType == typeof(DateTime))
                            {
                                if ((pi.GetValue(obj, null) as DateTime?).Value == DateTime.MinValue)
                                    htemp.Add(column, DBNull.Value);
                                else
                                    htemp.Add(column, (pi.GetValue(obj, null) as DateTime?).Value.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            else if (pi.PropertyType == typeof(int) && ((pi.GetValue(obj, null) as int?).Value == int.MinValue))
                            {
                                htemp.Add(column, DBNull.Value);
                            }
                            else if (pi.PropertyType == typeof(decimal) && ((pi.GetValue(obj, null) as decimal?).Value == decimal.MinValue))
                            {
                                htemp.Add(column, DBNull.Value);
                            }
                            else
                            {
                                htemp.Add(column, pi.GetValue(obj, null));
                            }
                        }
                    }
                    else
                    {
                        identity = pi;
                    }
                }

			}
			sb1.Remove(sb1.Length - 2, 2);
			sb2.Remove(sb2.Length - 2, 2);
			sb1.Append(sb2);
			sb1.Append("); ");
            if(identity != null)
                sb1.Append("SELECT SCOPE_IDENTITY()");

			SqlCommand cmd = new SqlCommand(sb1.ToString(), dc);
            dMsSqlUtil.AddHashParameters(cmd, htemp);
            decimal n = 0;
            if(identity == null)
            {
                n = cmd.ExecuteNonQuery();
            }
            else
            {
                n = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0);
                identity.SetValue(obj, n, null);
            }
			dc.Close();
			return (n > 0);
		}
		#endregion
		#region Update
		public bool Update(dModel obj)
		{
            List<string> pks = obj.GetPKColumns();
            SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();

			StringBuilder sb1 = new StringBuilder("UPDATE " + obj.GetTableName() + " SET ");
			Hashtable htemp = new Hashtable();
			System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();
            object[] attributes = null;
            string column = "";
			foreach (System.Reflection.PropertyInfo pi in properties)
			{
                attributes = pi.GetCustomAttributes(typeof(dNet.DB.ColumnAttribute), false);
                if (attributes.Length > 0)
                {
                    column = (attributes[0] as dNet.DB.ColumnAttribute).DbName;
                    if (!pks.Contains(column))
                    {
                        System.Reflection.PropertyInfo pinfo = obj.GetType().GetProperty(pi.Name + "Modified");
                        if (TryConvert.ToBool(pinfo.GetValue(obj, null)))
                            sb1.Append(column + "=@" + column + ", ");
                    }

                    if (pi.PropertyType == typeof(DateTime))
                    {
                        if ((pi.GetValue(obj, null) as DateTime?).Value == DateTime.MinValue)
                            htemp.Add(column, DBNull.Value);
                        else
                            htemp.Add(column, (pi.GetValue(obj, null) as DateTime?).Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else if (pi.PropertyType == typeof(int) && ((pi.GetValue(obj, null) as int?).Value == int.MinValue))
                    {
                        htemp.Add(column, DBNull.Value);
                    }
                    else if (pi.PropertyType == typeof(decimal) && ((pi.GetValue(obj, null) as decimal?).Value == decimal.MinValue))
                    {
                        htemp.Add(column, DBNull.Value);
                    }
                    else
                    {
                        htemp.Add(column, pi.GetValue(obj, null));
                    }
                }
			}
			sb1.Remove(sb1.Length - 2, 2);
            sb1.Append(" WHERE 1=1 ");
            foreach(string col in pks)
                sb1.Append(" AND " + col + "=@" + col);
			SqlCommand cmd = new SqlCommand(sb1.ToString(), dc);
            dMsSqlUtil.AddHashParameters(cmd, htemp);
			int n = cmd.ExecuteNonQuery();
			dc.Close();
			return (n > 0);
		}
		#endregion
		#region Delete
		public bool Delete(dModel obj)
		{
            Hashtable pks = obj.GetPKs();
			Hashtable htemp = new Hashtable();
            string query = "DELETE FROM " + obj.GetTableName() + " WHERE 1=1";
            foreach (string key in pks.Keys)
            {
                query += " AND " + key + "=@" + key;
                htemp.Add(key, pks[key]);
            }
            SqlConnection dc = new SqlConnection(ConnectionString);
			dc.Open();
			SqlCommand cmd = new SqlCommand(query, dc);
            dMsSqlUtil.AddHashParameters(cmd, htemp);
			int n = cmd.ExecuteNonQuery();
			dc.Close();
			return n>0;
		}
		#endregion

        #region MakeId
        public bool MakeId(dModel obj)
        {
            List<string> pkcolumns = obj.GetPKColumns();
            if (pkcolumns.Count == 0 || pkcolumns.Count > 1)
            {
                throw new Exception("é necessário definir uma chave para poder gerar o próximo id do objeto");
            }
            else
            {
                Hashtable htparam = new Hashtable();
                System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo pi in properties)
                {
                    if (pi.Name == pkcolumns[0])
                    {
                        htparam.Add(pi.Name, pi.GetValue(obj, null));
                        pi.SetValue(obj, TryConvert.ToDecimalN(dNet.DB.Factory.GetDbSql().SelectScalar("select max(" + pkcolumns[0] + ") as n from " + obj.GetTableName(), htparam)) + 1, null);
                        return false;
                    }
                }
            }
            return false;
        }
        #endregion
	}
}