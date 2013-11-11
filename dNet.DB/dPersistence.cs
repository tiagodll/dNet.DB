using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dNet.DB
{
    public class dPersistence : IDisposable
    {
        public IDbObj DbObj { get; set; }

        /*public dPersistence() 
        {
            DbObj = dNet.DB.Factory.GetDbObj();
        }*/

        public dPersistence(dNet.DB.IDbObj obj)
        {
            DbObj = obj;
        }

        public void Dispose()
        {
            DbObj = null;
        }

        /// <summary>
        /// Carrega os elementos para uma lista de Generics. Ex: List&lt;dNet.dModel&gt; o = m.LoadList();
        /// </summary>
        /// <returns>Lista de Generics</returns>
        public List<T> LoadList<T>() where T : dModel { return DbObj.LoadList<T>("*", ""); }
        /// <summary>
        /// Carrega os elementos para uma lista de Generics. Ex: List&ls;dNet.dModel&gt; o = m.LoadList();
        /// </summary>
        /// <returns>Lista de Generics</returns>
        public List<T> LoadList<T>(string where) where T : dModel { return DbObj.LoadList<T>("*", where); }
        /// <summary>
        /// Carrega os elementos (apenas os campos selecionados) para uma lista de Generics. Ex: List&lt;dNet.dModel&gt; o = m.LoadList();
        /// </summary>
        /// <returns>Lista de Generics</returns>
        public List<T> LoadListFields<T>(string fields) where T : dModel { return DbObj.LoadList<T>(fields, ""); }
        /// <summary>
        /// Carrega os elementos (apenas os campos selecionados) para uma lista de Generics. Ex: List&ls;dNet.dModel&gt; o = m.LoadList();
        /// </summary>
        /// <returns>Lista de Generics</returns>
        public List<T> LoadListFields<T>(string fields, string where) where T : dModel { return DbObj.LoadList<T>(fields, where); }

        public bool Load(dModel obj) { return DbObj.Load(obj); }
        public bool Load(dModel obj, string where) { return DbObj.Load(obj, where); }
        public bool LoadProp(dModel obj) { return DbObj.LoadProp(obj); }
        public bool Save(dModel obj) { return DbObj.Save(obj); }
        public bool Insert(dModel obj) { return DbObj.Insert(obj); }
        public bool Update(dModel obj) { return DbObj.Update(obj); }
        public bool Delete(dModel obj) { return DbObj.Delete(obj); }
    }
}
