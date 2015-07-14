using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using XK.Common.json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
 
    public static class StringExtension {
        /// <summary>
        /// 判读是否是IP地址
        /// </summary>
        /// <param name="in_str"></param>
        /// <returns></returns>
        public static bool IsIPStr(this string in_str) {
            IPAddress ip;
            return IPAddress.TryParse(in_str, out ip);
        }

        #region zs

        ///// <summary>
        ///// 判断是否是数字
        ///// </summary>
        ///// <param name="strNumber"></param>
        ///// <returns></returns>
        //public static bool IsNumber(this string strNumber) {

        //    Regex objNotNumberPattern = new Regex("[^0-9.-]");
        //    Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        //    Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        //    const string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        //    const string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
        //    Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
        //    return !objNotNumberPattern.IsMatch(strNumber) &&
        //           !objTwoDotPattern.IsMatch(strNumber) &&
        //           !objTwoMinusPattern.IsMatch(strNumber) &&
        //           objNumberPattern.IsMatch(strNumber);
        //}

        #endregion

        /// <summary>
        /// 判断字符串中是否包含汉字,有返回true 否则为false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsExistHanZi(this string str) {
            Regex reg = new Regex(@"[\u4e00-\u9fa5]");//正则表达式
            if (reg.IsMatch(str)) {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsEmail(this string strEmail) {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }
        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(this string strUrl) {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(this string str) {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
        /// <summary>
        /// 检查是否是日期格式
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string dateStr) {
            DateTime dt;
            bool success = DateTime.TryParse(dateStr, out dt);
            return success;
        }
        /// <summary>
        /// 获取Email HostName 例 xxxx@gmail.com   获取出来时@gmail.com
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static string EmailHostName(this string strEmail) {
            if (strEmail.IndexOf("@", StringComparison.Ordinal) < 0) {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@", StringComparison.Ordinal)).ToLower();
        }
        /// <summary>
        /// 截取string字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="hasEllipsis">是否显示...</param>
        /// <returns></returns>
        public static string SubStringByLength(this string str, int length, bool hasEllipsis = true) {
            Char[] strChar = str.ToCharArray();
            int intLen = str.Length;
            if ("中文".Length != 4) {
                for (int i = 0; i < strChar.Length; i++) {
                    if (Convert.ToInt32(strChar[i]) > 255) {
                        intLen++;
                    }
                }
            }
            if (intLen > length) {
                for (int i = 0; i < length; i++) {
                    if (Convert.ToInt32(strChar[i]) > 255) {
                        --length;
                    }
                }
                string ellipsis = String.Empty;
                if (hasEllipsis) {
                    ellipsis = "...";
                }
                return str.Substring(0, length) + ellipsis;
            }
            return str;
        }
        /// <summary>
        /// 切割object类型的字符串
        /// </summary>
        /// <param name="objStr"></param>
        /// <param name="length"></param>
        /// <param name="hasEllipsis">是否显示...</param>
        /// <returns></returns>
        public static string SubStringByLength(this object objStr, int length, bool hasEllipsis = true) {
            if (objStr == null) {
                return "";
            }
            return SubStringByLength(objStr.ToString(), length, hasEllipsis);
        }

        /// <summary>
        /// 移除字符串中所有空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAllSpaces(this string str) {
            var arr = str.Split(' ');
            string s = string.Join("", arr);
            return s;
        }
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumer(this string strNumber) {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            const string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            const string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="objInt">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ToInt(this object objInt, int defValue=0) {
            if (objInt != null)
                return ToInt(objInt.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int ToInt(this string str, int defValue = 0) {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11)
                return defValue;

            int rv;
            if (int.TryParse(str, out rv)) {
                return rv;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为decimal类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal ToDecimal(this object strValue, int defValue = 0)
        {
            if (strValue != null)
                return ToDecimal(strValue.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        /// 将对象转换为decimal类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的decimal类型结果</returns>
        public static decimal ToDecimal(this string str, int defValue = 0)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11)
                return defValue;

            decimal rv;
            if (decimal.TryParse(str, out rv))
            {
                return rv;
            }
            return defValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ToFloat(this object strValue, float defValue = 0) {
            if ((strValue == null))
                return defValue;

            return ToFloat(strValue.ToString(), defValue);
        }

        public static string ToStringEXT(this object strValue) {
            try {
                if (strValue == null) {
                    return "";
                }
                return strValue.ToString();
            }
            catch (Exception) {
                return "";
            }
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float ToFloat(this string strValue, float defValue = 0) {
            if (string.IsNullOrEmpty(strValue))
                return defValue;

            float floatVal;

            bool success = float.TryParse(strValue, out floatVal);
            if (success) {
                return floatVal;
            }
            return defValue;
        }

        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime StrToDateTime(this string str, DateTime defValue) {
            if (!string.IsNullOrEmpty(str)) {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                    return dateTime;
                return defValue;
            }
            return defValue;
        }


        /// <summary>
        /// 将对象转换为日期时间类型
        /// </summary>
        /// <param name="objDateTime">要转换的对象</param>
        /// <param name="defValue">默认时间</param>
        /// <returns>转换后的int类型结果</returns>
        public static DateTime ToDateTime(object objDateTime,DateTime defValue) {
            if (objDateTime == null) {
                return defValue;
            }
            return StrToDateTime(objDateTime.ToString(), defValue);
        }

        public static string ToShortDateTime(this object objDateTime, string defValue = "") {
            if (objDateTime == null) {
                return defValue;
            }
            string shortDatetime = StrToDateTime(objDateTime.ToString(), DateTime.MaxValue).ToString("yyyy-MM-dd");
            return shortDatetime;
        }

        public static string ToShortDateTimeByMaxVal(this object objDateTime, string defValue = "") {
            if (objDateTime == null) {
                return defValue;
            }
            string dtStr = objDateTime.ToString();
            if (!string.IsNullOrEmpty(dtStr)) {
                DateTime dateTime;
                if (DateTime.TryParse(dtStr, out dateTime)) {
                    if (dateTime.ToShortDateString() == DateTime.MaxValue.ToShortDateString()) {
                        return defValue;
                    }
                    return dateTime.ToShortDateString();
                }
            }
            return defValue;
        }
        /// <summary>
        /// byte[] => string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToString(this byte[] bytes) {
            string strSignalData = BitConverter.ToString(bytes).Replace("-", ",");
            string[] hexValuesSplit = strSignalData.Split(',');
            // string[] stringValues = new string[hexValuesSplit.Length];
            string strValues = "";
            for (int i = 0; i < hexValuesSplit.Length; i++) {
                int value = Convert.ToInt32(hexValuesSplit[i], 16);
                string stringValue = Char.ConvertFromUtf32(value);
                // stringValues[i] = stringValue;
                strValues = strValues + stringValue;
            }
            return strValues;
        }

        /// <summary>
        /// hex array => string
        /// </summary>
        /// <param name="hexArr"></param>
        /// <returns></returns>
        public static string HexValueArray2String(this string[] hexArr) {
            string strValues = "";
            char[] chars = new char[hexArr.Length];
            for (int i = 0; i < hexArr.Length; i++) {
                chars[i] = (Char)Convert.ToInt32(hexArr[i], 16);
                // int value = Convert.ToInt32(hexArr[i], 16);
                //  string stringValue = Char.ConvertFromUtf32(value);
                // stringValues[i] = stringValue;
                //   strValues = strValues + stringValue;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(chars);

            strValues = Encoding.UTF8.GetString(bytes);

            return strValues;
        }

        public static int TrueLength(this string strValue, int defValue = 0)
        {
            if (string.IsNullOrEmpty(strValue))
                return defValue;
            // str 字符串
            // return 字符串的字节长度
            int lenTotal = 0;
            int n = strValue.Length;
            string strWord = "";
            int asc;
            for (int i = 0; i < n; i++)
            {
                strWord = strValue.Substring(i, 1);
                asc = Convert.ToChar(strWord);
                if (asc < 0 || asc > 127)
                    lenTotal = lenTotal + 2;
                else
                    lenTotal = lenTotal + 1;
            }

            return lenTotal;
        }
        /// <summary>
        /// 根据key获取HttpRequest的value
        /// 直接调用即可，不用区分POST/GET
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetVal(this HttpRequest request, string key) {
            string method = request.HttpMethod.ToUpper();
            string val = "";
            if (method == "POST") {
                val = request.Form[key].ToStringEXT();
            }
            else if (method == "GET") {
                val = request.QueryString[key].ToStringEXT();
            }
            return val.Trim();
        }

    }

    public static class DateTimeExtension {
        /// <summary>  
        /// GMT时间转成本地时间  
        /// </summary>  
        /// <param name="gmt">字符串形式的GMT时间</param>  
        /// <returns></returns>  
        public static DateTime ToLocal(this string gmt) {
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
        public static long ToTimeStamp(this DateTime dt) {
            long epoch;
            try {
                epoch = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            }
            catch (Exception) {
                epoch = 0;
            }
            return epoch;
        }

        /// <summary>
        /// Unix时间戳转换成DateTime(转换失败返回 DateTime.MinValue)
        /// EXP:1389775079
        /// </summary>
        /// <param name="timeStamp">Unix时间戳</param>
        public static DateTime StampToDateTime(this long timeStamp) {
            DateTime dt;
            try {
                long ticks = timeStamp * 10000000 + 621355968000000000;
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
        public static DateTime StampToDateTime(this string timeStamp) {
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
    }

    /// <summary>
    /// 数字类的扩展
    /// </summary>
    public static class NumberExtension {
        /// <summary>
        /// int 转化为指定位数的 string
        /// EXP:00054
        /// </summary>
        /// <param name="val"></param>
        /// <param name="leng"></param>
        /// <returns></returns>
        public static string Format2Str(this int val, int leng) {
            string placeHolder = "";
            for (int i = 0; i < leng; i++) {
                placeHolder += "0";
            }
            return val.ToString(placeHolder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="leng">小数点后面位数</param>
        /// <returns></returns>
        public static string Format2Point(this double val, int leng = 2) {
            return val.ToString("F" + leng);
        }

        /// <summary>
        /// 百分比 val*100%
        /// </summary>
        /// <param name="val"></param>
        /// <param name="leng"></param>
        /// <returns></returns>
        public static string Format2Percent(this double val, int leng = 2) {
            return val.ToString("P" + leng);
        }
    }

    /// <summary>
	/// List扩展
	/// </summary>
    public static class ListExtension {
        /// <summary>
        /// 转化为json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToJson<T>(this List<T> list) where T :class {
            //JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return jsonStr;
        }

        /// <summary>
        /// 转化为带jsonname的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="total">总数量，用于分页</param>
        /// <returns></returns>
        public static string ToJsonCreater<T>(this List<T> list, int total) {
            JsonCreator jsonCreator = new JsonCreator();
            return jsonCreator.Create(list, total);
        }

        public static string ToJsonCreater<T>(this List<T> list, int total, string jsonName, string totalName) {
            JsonCreator jsonCreator = new JsonCreator(jsonName, totalName);
            return jsonCreator.Create(list, total);
        }
        /// <summary>
        /// 把list字符串转化为string
        /// </summary>
        /// <param name="listString"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public static string ToStr<T>(this List<T> listString, string joinString = ",") {
            string joinStr = string.Format(" {0} ", joinString);
            return string.Join(joinStr, listString);
        }
    }
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtension {
        /// <summary>
        /// DataTable=>json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt) {
     
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            return jsonStr;
        }
        public static string ToJsonCreater(this DataTable dt, int total) {
            JsonCreator jsonCreator = new JsonCreator();
            return jsonCreator.Create(dt, total);
        }
        public static string ToJsonCreater(this DataTable dt, int total,string jsonName,string totalName) {
            JsonCreator jsonCreator = new JsonCreator(jsonName,totalName);
            return jsonCreator.Create(dt, total);
        }
    }

    /// <summary>
    /// Dictionary扩展
    /// </summary>
    public static class DictionaryExtension {
        /// <summary>
        /// DataTable=>json
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string ToJson<T>(this Dictionary<string, T> dic) where T : class {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
            return jsonStr;
        }

        public static string ToJsonCreater<T>(this Dictionary<string, T> dic, int total) {
            JsonCreator jsonCreator = new JsonCreator();
            return jsonCreator.Create(dic, total);
        }

        public static string ToJsonCreater<T>(this Dictionary<string, T> dic, int total, string jsonName, string totalName) {
            JsonCreator jsonCreator = new JsonCreator(jsonName, totalName);
            return jsonCreator.Create(dic, total);
        }

    }

 
