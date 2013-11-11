using System.Collections;
using System.Data;

namespace dNet.DB
{
    public interface IDbSql
    {
        string ConnectionString { get; set; }

        //######### DEFAULT QUERIES ##########
        DataTable Select(string query);
        DataTable Select(string query, Hashtable parametros);
        DataRow SelectOne(string query);
        DataRow SelectOne(string query, Hashtable parametros);
        Hashtable SelectOneHash(string query);
        Hashtable SelectOneHash(string query, Hashtable parametros);
        object SelectScalar(string query);
        object SelectScalar(string query, Hashtable parametros);
        int NonQuery(string query);
        int NonQuery(string query, Hashtable parametros);
    }

    public enum TypeOfDb
    {
        MySql,
        MsSql,
        SqlCe
    }
}