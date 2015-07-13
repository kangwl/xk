using System;

namespace XK.Common {
   public class TimeConvert {
        /// <summary>  
        /// GMT时间转成本地时间  
        /// </summary>  
        /// <param name="gmt">字符串形式的GMT时间</param>  
        /// <returns></returns>  
        public static DateTime GMT2Local(string gmt) {
            DateTime dt = DateTime.MinValue;
            try {
                string pattern = "";
                if (gmt.IndexOf("+0", StringComparison.Ordinal) != -1) {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd-MMM-yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT", StringComparison.Ordinal) != -1) {
                    pattern = "ddd, dd-MMM-yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "") {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch {
            }
            return dt;
        }

       /// <summary>
       /// DateTime转换成Unix时间戳(转换失败返回 0)
       /// </summary>
       public static long GetDateTimeStamp(DateTime dt) {
           long epoch;
           try {
               epoch = (dt.ToUniversalTime().Ticks - 621355968000000000)/10000000;
           }
           catch (Exception) {
               epoch = 0;
           }
           return epoch;
       }

       /// <summary>
       /// Unix时间戳转换成DateTime(转换失败返回 DateTime.MinValue)
       /// </summary>
       /// <param name="timeStamp">Unix时间戳</param>
       public static DateTime TimeStampToDateTime(long timeStamp = 1389775079) {
           DateTime dt;
           try {
               long ticks = timeStamp*10000000 + 621355968000000000;
               dt = new DateTime(ticks, DateTimeKind.Utc);
               dt = dt.AddHours(8);
           }
           catch (Exception) {
               dt = DateTime.MinValue;
           }
           return dt;
       }

       /// <summary>
       /// 时间戳转为C#格式时间 如果转换失败就返回 DateTime.MinValue
       /// </summary>
       /// <param name="timeStamp"></param>
       /// <returns></returns>
       public static DateTime GetTime(string timeStamp) {
           DateTime retDateTime = DateTime.MinValue;
           try {
               DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
               long lTime = long.Parse(timeStamp + "0000000");
               TimeSpan toNow = new TimeSpan(lTime);
               retDateTime = dtStart.Add(toNow);
           }
           catch (Exception) {
           }
           return retDateTime;
       }

       /// <summary>
       /// DateTime时间格式转换为Unix时间戳格式 转换失败就返回 0
       /// </summary>
       /// <param name="time"></param>
       /// <returns></returns>
       public static double ConvertDateTimeInt(System.DateTime time) {
           double sjc = 0;
           try {
               System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
               sjc = (time - startTime).TotalSeconds;
           }
           catch (Exception) {
               sjc = 0;
           }
           return sjc;
       }

   }
}
