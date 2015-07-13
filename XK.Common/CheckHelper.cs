using System;
using System.Net;
using System.Text.RegularExpressions;

namespace XK.Common {
   public static class CheckHelper {
       /// <summary>
       /// 判读是否是IP地址
       /// </summary>
       /// <param name="in_str"></param>
       /// <returns></returns>
       public static bool IsIPStr(string in_str) {
           IPAddress ip;
           return IPAddress.TryParse(in_str, out ip);
       }

       /// <summary>
       /// 判断是否是数字
       /// </summary>
       /// <param name="strNumber"></param>
       /// <returns></returns>
       public static bool IsNumber(string strNumber) {

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
       /// 判断字符串中是否包含汉字,有返回true 否则为false
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static bool IsExistHanZi(string str) {
           Regex reg = new Regex(@"[\u4e00-\u9fa5]");//正则表达式
           if (reg.IsMatch(str)) {
               return true;
           }
           return false;
       }


       /// <summary>
       /// 返回文件是否存在
       /// </summary>
       /// <param name="filename">文件名</param>
       /// <returns>是否存在</returns>
       public static bool IsFileExists(string filename) {
           return System.IO.File.Exists(filename);
       }


       /// <summary>
       /// 检测是否符合email格式
       /// </summary>
       /// <param name="strEmail">要判断的email字符串</param>
       /// <returns>判断结果</returns>
       public static bool IsValidEmail(string strEmail) {
           return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
       }

       /// <summary>
       /// 检测是否是正确的Url
       /// </summary>
       /// <param name="strUrl">要验证的Url</param>
       /// <returns>判断结果</returns>
       public static bool IsURL(string strUrl) {
           return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
       }

       /// <summary>
       /// 判断是否为base64字符串
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static bool IsBase64String(string str) {
           //A-Z, a-z, 0-9, +, /, =
           return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
       }

       /// <summary>
       /// 检测是否有Sql危险字符
       /// </summary>
       /// <param name="str">要判断字符串</param>
       /// <returns>判断结果</returns>
       public static bool IsSafeSqlString(string str) {
           return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
       }
   }
}
