using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Web.SessionState;

namespace dNet.DB
{
    static public class Factory
    {
        public static string ConnectionString { get; set; }
        public static TypeOfDb DbType { get; set; }

        #region Db
        public static IDbSql GetDbSql()
        {
            return GetDbSql(DbType, ConnectionString);
        }
        public static IDbSql GetDbSql(TypeOfDb tipo, string connectionstring)
        {
            IDbSql db = null;
            switch (tipo)
            {
                case TypeOfDb.MsSql:
                    db = new MsSql.dMsSqlSql();
                    break;
				
				case TypeOfDb.MySql:
                    db = new MySql.dMySqlSql();
                    break;

                /*case TypeOfDb.SqlCe:
                    db = new SqlCe.dSqlCeSql();
                    break;*/
            }
            db.ConnectionString = connectionstring ?? ConnectionString;
            return db;
        } 
        #endregion

        #region DbObj
        public static IDbObj GetDbObj()
        {
            return GetDbObj(DbType, ConnectionString);
        }
        public static IDbObj GetDbObj(TypeOfDb tipo, string connectionString)
        {
            IDbObj db = null;
            switch (tipo)
            {
                case TypeOfDb.MsSql: 
                    db = new MsSql.dMsSqlObj(); 
                    break;
				
				case TypeOfDb.MySql: 
                    db = new MySql.dMySqlObj(); 
                    break;

                /*case TypeOfDb.SqlCe:
                    db = new SqlCe.dSqlCeObj();
                    break;*/
            }
            db.ConnectionString = connectionString ?? ConnectionString;
            return db;
        } 
        #endregion

        #region DbMaker
        public static IDbMaker GetDbMaker()
        {
            return GetDbMaker(DbType, ConnectionString);
        }
        public static IDbMaker GetDbMaker(TypeOfDb type, string connectionString)
        {
            IDbMaker db = null;
            switch (type)
            {
                case TypeOfDb.MsSql:
                    db = new MsSql.dMsSqlMaker();
                    break;
				
				case TypeOfDb.MySql:
                    db = new MySql.dMySqlMaker();
                    break;
				
                /*case TypeOfDb.SqlCe:
                    db = new SqlCe.dSqlCeMaker();
                    break;*/
            }
            db.ConnectionString = connectionString ?? ConnectionString;
            return db;
        } 
        #endregion

        #region DbUtil
        public static IDbUtil GetDbUtil()
        {
            return GetDbUtil(DbType, ConnectionString);
        }
        public static IDbUtil GetDbUtil(TypeOfDb type, string connectionString)
        {
            IDbUtil db = null;
            switch (type)
            {
                case TypeOfDb.MsSql:
                    db = new MsSql.dMsSqlUtil();
                    break;
				
				case TypeOfDb.MySql:
                    db = new MySql.dMySqlUtil();
                    break;

                /*case TypeOfDb.SqlCe:
                    db = new SqlCe.dSqlCeUtil();
                    break;*/
            }
            return db;
        } 
        #endregion
    }
}
