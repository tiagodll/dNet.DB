#region Bibliotecas
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace dNet.DB.Utils
{
	public class TryConvert
	{
		#region TryToString
		static public string ToString(object obj) { return ToString(obj, ""); }
		static public string ToString(object obj, string inCase)
		{
			try
			{
				if (obj == null) return inCase;
				else if (obj == DBNull.Value) return inCase;
				else return obj.ToString();
			}
			catch
			{
				return inCase;
			}
		}
		#endregion

		#region ToInt
		public static int ToInt(object obj) { return ToInt(obj, int.MinValue); }
		public static int ToInt(object obj, int inCase)
		{
			try
			{
				if (obj == DBNull.Value)
					return inCase;
				return Convert.ToInt32(obj);
			}
			catch
			{
				return inCase;
			}
		}
		#endregion

		#region ToDecimal
		public static decimal ToDecimal(object obj) { return ToDecimal(obj, decimal.MinValue); }
		public static decimal ToDecimal(object obj, decimal inCase)
		{
			try
			{
				if (obj == DBNull.Value)
					return inCase;
				return Convert.ToDecimal(obj);
			}
			catch
			{
				return inCase;
			}
		}
		#endregion

		#region ToDouble
		public static double ToDouble(object obj) { return ToDouble(obj, double.MinValue); }
		public static double ToDouble(object obj, double inCase)
		{
			try
			{
				if (obj == DBNull.Value)
					return inCase;
				return Convert.ToDouble(obj);
			}
			catch
			{
				return inCase;
			}
		}
		#endregion

		#region ToDateTime
		public static DateTime ToDateTime(object obj) { return ToDateTime(obj, DateTime.MinValue); }
		public static DateTime ToDateTime(object obj, DateTime inCase)
		{
			try
			{
				if (obj == DBNull.Value)
					return inCase;
				return Convert.ToDateTime(obj);
			}
			catch
			{
				return inCase;
			}
		}
		#endregion

		#region ToBool
		static public bool ToBool(object obj) { return ToBool(obj, false); }
		static public bool ToBool(object obj, bool inCase)
		{
			try
			{
                if (obj == DBNull.Value)
                    return inCase;
                else if (obj.ToString() == "1")
                    return true;
                else if (obj.ToString().ToLower() == "true")
                    return true; 
                return Convert.ToBoolean(obj);
			}
			catch
			{
				return inCase;
			}
		}
		#endregion

        //######################## Nullable ########################

        #region ToIntN
        public static int? ToIntN(object obj) { return ToIntN(obj, null); }
        public static int? ToIntN(object obj, int? inCase)
        {
            try
            {
                if (obj == DBNull.Value)
                    return inCase;
                return (int?)Convert.ToInt32(obj);
            }
            catch
            {
                return inCase;
            }
        }
        #endregion

        #region ToDecimalN
        public static decimal? ToDecimalN(object obj) { return ToDecimalN(obj, null); }
        public static decimal? ToDecimalN(object obj, decimal? inCase)
        {
            try
            {
                if (obj == DBNull.Value)
                    return inCase;
                return (decimal?)Convert.ToDecimal(obj);
            }
            catch
            {
                return inCase;
            }
        }
        #endregion

        #region ToDoubleN
        public static double? ToDoubleN(object obj) { return ToDoubleN(obj, null); }
        public static double? ToDoubleN(object obj, double? inCase)
        {
            try
            {
                if (obj == DBNull.Value)
                    return inCase;
                return (double?)Convert.ToDouble(obj);
            }
            catch
            {
                return inCase;
            }
        }
        #endregion

        #region ToDateTimeN
        public static DateTime? ToDateTimeN(object obj) { return ToDateTimeN(obj, null); }
        public static DateTime? ToDateTimeN(object obj, DateTime? inCase)
        {
            try
            {
                if (obj == DBNull.Value)
                    return inCase;
                return (DateTime?)Convert.ToDateTime(obj);
            }
            catch
            {
                return inCase;
            }
        }
        #endregion

        #region ToBoolN
        static public bool? ToBoolN(object obj) { return ToBoolN(obj, null); }
        static public bool? ToBoolN(object obj, bool? inCase)
        {
            try
            {
                if (obj == DBNull.Value)
                    return inCase;
                if (TryConvert.ToInt(obj, 0) > 0)
                    return true;
                return (bool?)Convert.ToBoolean(obj);
            }
            catch
            {
                return inCase;
            }
        }
        #endregion

        //######################## List ########################
        static public List<TO_TYPE> ToListType<FROM_TYPE, TO_TYPE>(List<FROM_TYPE> listToCopyFrom) where TO_TYPE : FROM_TYPE
        {
            List<TO_TYPE> listToCopyTo = new List<TO_TYPE>();

            // loop through the list to copy, and  
            foreach (FROM_TYPE item in listToCopyFrom)
            {
                TO_TYPE obj = (TO_TYPE)item;
                // add items to the copy tolist  
                listToCopyTo.Add(obj);
            }

            // return the copy to list  
            return listToCopyTo;
        } 
	}
}
