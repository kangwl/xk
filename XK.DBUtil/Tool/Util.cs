using System;
using System.Data;
using System.Data.SqlClient;

namespace XK.DBUtil.Tool
{
   public static class SqlReader
   {

       public static object GetObjectEXT(this SqlDataReader reader, string key) {
           object retVal = null;

           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null &&
               !string.IsNullOrEmpty(reader[key].ToString())) {
               retVal = reader[key];
           }

           return retVal;
       }

       public static int GetIntEXT(this SqlDataReader reader, string key) {
           int retVal = 0;

           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null &&
               !string.IsNullOrEmpty(reader[key].ToString())) {
               int.TryParse(reader[key].ToString(), out retVal);
           }

           return retVal;
       }

       public static string GetStringEXT(this SqlDataReader reader, string key) {
           string retVal = string.Empty;

           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null &&
               !string.IsNullOrEmpty(reader[key].ToString())) {
               retVal = reader[key].ToString().Trim();
           }

           return retVal;
       }

       public static decimal GetDecimalEXT(this SqlDataReader reader, string key) {
           decimal retVal = 0m;

           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null &&
               !string.IsNullOrEmpty(reader[key].ToString())) {
               decimal.TryParse(reader[key].ToString(), out retVal);
           }

           return retVal;
       }

       public static DateTime GetDateTimeEXT(this SqlDataReader reader, string key) {
           DateTime retVal = DateTime.MaxValue;

           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null &&
               !string.IsNullOrEmpty(reader[key].ToString())) {
               DateTime.TryParse(reader[key].ToString(), out retVal);
           }

           return retVal;
       }

       public static float GetFloatEXT(this SqlDataReader reader, string key) {
           float retVal = 0f;
           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null &&
               !string.IsNullOrEmpty(reader[key].ToString())) {
               float.TryParse(reader[key].ToString(), out retVal);
           }
           return retVal;
       }

       public static double GetDoubleEXT(this SqlDataReader reader, string key) {
           double retVal = 0d;
           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null && !string.IsNullOrEmpty(reader[key].ToString())) {
               double.TryParse(reader[key].ToString(), out retVal);
           }
           return retVal;
       }
       public static bool GetBitBoolEXT(this SqlDataReader reader, string key)
       {
           bool retBool = false;
           if ((!DBNull.Value.Equals(reader[key])) && reader[key] != null && !string.IsNullOrEmpty(reader[key].ToString()))
           {
               bool converOk = bool.TryParse(reader[key].ToString(), out retBool);
               if (!converOk) {
                   return false;
               }
           }
           return retBool;
       }
   }

   public static class SqlRow
   {

       public static int GetInt(this DataRow row, string key)
       {
           int retVal = 0;
           if (!row.IsNull(key))
           {
               if (row[key] != null && !string.IsNullOrEmpty(row[key].ToString()))
               {
                   int.TryParse(row[key].ToString(), out retVal);
               }
           }
           return retVal;
       }

       public static string GetString(this DataRow row, string key)
       {
           string retVal = string.Empty;
           if (!row.IsNull(key))
           {
               if (row[key] != null && !string.IsNullOrEmpty(row[key].ToString()))
               {
                   retVal = row[key].ToString();
               }
           }
           return retVal;
       }

       public static decimal GetDecimal(this DataRow row, string key)
       {
           decimal retVal = 0m;
           if (!row.IsNull(key))
           {
               if (row[key] != null && !string.IsNullOrEmpty(row[key].ToString()))
               {
                   decimal.TryParse(row[key].ToString(), out retVal);
               }
           }
           return retVal;
       }

       public static DateTime GetDateTime(this DataRow row, string key)
       {
           DateTime retVal = DateTime.MinValue;
           if (!row.IsNull(key))
           {
               if (row[key] != null && !string.IsNullOrEmpty(row[key].ToString()))
               {
                   DateTime.TryParse(row[key].ToString(), out retVal);
               }
           }
           return retVal;
       }

       public static float GetFloat(this DataRow row, string key) {
           float retVal = 0f;
           if (!row.IsNull(key)) return retVal;
           if (row[key] != null && !string.IsNullOrEmpty(row[key].ToString()))
           {
               float.TryParse(row[key].ToString(), out retVal);
           }
           return retVal;
       }
   }
}
