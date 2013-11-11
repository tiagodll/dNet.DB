#region Bibliotecas
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using dNet.DB.Utils;
#endregion

namespace dNet.DB.Utils
{
	public class Util
	{
        static public bool ArrayContains(string[] arr, string key)
        {
            bool resp = false;
            if (arr == null) return false;

            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == key)
                    resp = true;
            return resp;
        }

		#region DataRow2Hash
		static public Hashtable DataRow2Hash(DataRow dr)
		{
			Hashtable ht = new Hashtable();
			foreach (DataColumn col in dr.Table.Columns)
			{
				switch(col.ColumnMapping.ToString())
				{
					case "DateTime": ht.Add(col.ColumnName, TryConvert.ToDateTime(dr[col.ColumnName]).ToString("dd/MM/yyyy")); break;
					default: ht.Add(col.ColumnName, dr[col.ColumnName].ToString()); break;
				}
			}
			return ht;
		}
		#endregion
	}
}
