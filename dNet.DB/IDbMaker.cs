using System.Collections;
using System.Data;

namespace dNet.DB
{
    public interface IDbMaker
    {
        string ConnectionString { get; set; }

        //######### MAKE SQL ##########
        int MakeInsert(string table, Hashtable param);
        int MakeUpdate(string table, Hashtable param, string IdName);
        int MakeUpdateWhere(string table, Hashtable param, string where);
        int MakeDelete(string table, Hashtable param);
    }
}