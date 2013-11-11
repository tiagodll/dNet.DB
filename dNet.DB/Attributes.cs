using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dNet.DB
{
    public class DatabaseAttribute : Attribute
    {
        public string Name { get; set; }
    }
    public class ColumnAttribute : Attribute
    {
        public string DbName { get; set; }
        //public string DbType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsDescription { get; set; }
        public bool IsAutoIncrement { get; set; }
    }
}
