using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dNet.DB
{
    public interface IDbObj
    {
        string ConnectionString { get; set; }

        bool Load(dModel obj);
        bool Load(dModel obj, string where);
        bool LoadProp(dModel obj);
        bool Save(dModel obj);
        bool Insert(dModel obj);
        bool Update(dModel obj);
        bool Delete(dModel obj);
        List<T> LoadList<T>(string where) where T:dModel;
        List<T> LoadList<T>(string fields, string where) where T:dModel;

        bool MakeId(dModel obj);
    }
}
