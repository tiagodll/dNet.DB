using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dNet.DB
{
    public class dModelSync : dModel
    {
        public TypeOfSyncAction SyncActionEnum { get; set; }

        public enum TypeOfSyncAction
        {
            Nothing = 0,
            Insert = 1,
            Update = 2,
            Delete = 3
        }
    }
}
