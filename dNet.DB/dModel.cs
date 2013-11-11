#region Bibliotecas
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using System.Runtime.Serialization.Json;
using dNet.DB;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel;
using dNet.DB.Utils;
#endregion

namespace dNet.DB
{
	public class dModel
	{
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        

		#region MakeId
		/*public bool MakeId() 
		{
            TestDB(); 
			if (_Id==null || _Id=="")
			{
				throw new Exception("É necessário definir uma chave para poder gerar o próximo ID do objeto");
			}
			else
			{
				Hashtable htParam = new Hashtable(); 
				System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();
				foreach (System.Reflection.PropertyInfo pi in properties)
				{
					if (pi.Name == _Id)
					{
						htParam.Add(pi.Name, pi.GetValue(this, null));
                        pi.SetValue(this, TryConvert.ToIntN(_Database.SelectScalar("SELECT MAX(" + _Id + ") AS N FROM " + _Tabela, htParam)) + 1, null);
						return false;
					}
				}
			}
			return false;
		}*/
		#endregion

        #region GetTableName
        public string GetTableName()
        {
            object[] attributes = this.GetType().GetCustomAttributes(typeof(dNet.DB.DatabaseAttribute), false);
            if (attributes.Length > 0)
                return (attributes[0] as dNet.DB.DatabaseAttribute).Name;

            return null;
        } 
        #endregion

        #region GetPKColumn
        public List<string> GetPKColumns()
        {
            List<string> pks = new List<string>();
            System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                object[] attributes = pi.GetCustomAttributes(typeof(dNet.DB.ColumnAttribute), false);
                if (attributes.Length > 0 && (attributes[0] as dNet.DB.ColumnAttribute).IsPrimaryKey)
                    pks.Add(pi.Name);
            }
            return pks;
        } 
        #endregion

        #region GetPKs
        public Hashtable GetPKs()
        {
            Hashtable ht = new Hashtable();
            List<string> pks = GetPKColumns();
            foreach (string pk in pks)
            {
                System.Reflection.PropertyInfo pimod = this.GetType().GetProperty(pk + "Modified");
                if (TryConvert.ToBool(pimod.GetValue(this, null), false))
                    ht.Add(pk, this.GetType().GetProperty(pk).GetValue(this, null));
            }
            return ht;
        } 
        #endregion

        #region MakeId
        /*public bool MakeId()
        {
            List<string> pkcolumns = GetPKColumns();
            if (pkcolumns.Count == 0 || pkcolumns.Count > 1)
            {
                throw new Exception("É necessário definir uma chave para poder gerar o próximo ID do objeto");
            }
            else
            {
                Hashtable htParam = new Hashtable();
                System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo pi in properties)
                {
                    if (pi.Name == pkcolumns[0])
                    {
                        htParam.Add(pi.Name, pi.GetValue(this, null));
                        pi.SetValue(this, TryConvert.ToDecimalN(dNet.DB.Factory.GetDbSql().SelectScalar("SELECT MAX(" + pkcolumns[0] + ") AS N FROM " + GetTableName(), htParam)) + 1, null);
                        return false;
                    }
                }
            }
            return false;
        }*/
        #endregion

		#region ToString
		public override string ToString()
		{
			return this.GetType().Name;
		}
		#endregion
    }
}
