using System;
using System.Collections.Generic;
using System.Web;

namespace XK.Common {
    public class CookieHepler {

        public static string EncodeVal(string val) {
            return HttpUtility.UrlEncode(val);
        }

        public static string DecodeVal(string val) {
            return HttpUtility.UrlDecode(val);
        }

        /// <summary>
        /// 添加一个单值cookie 
        /// 如果已存在则覆盖相同键的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        public static void Add(string name, string value, int days) {
            HttpCookie acookie = new HttpCookie(name);
            acookie.Value = EncodeVal(value);
            acookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.Cookies.Add(acookie);
        }
        /// <summary>
        /// 添加一个多值cookie
        /// 如果已存在则覆盖相同键的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dicItems"></param>
        /// <param name="days"></param>
        public static void AddRange(string name, Dictionary<string, string> dicItems, int days) {
            HttpCookie cookie = new HttpCookie(name);
            foreach (var item in dicItems) {
                cookie.Values[item.Key] = EncodeVal(item.Value);
            }
            cookie.Expires = DateTime.Now.AddDays(days);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 获取一个单值cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(string name) {
            string cookieVal = string.Empty;
            if (HttpContext.Current.Request.Cookies[name] != null) {
                cookieVal = HttpContext.Current.Request.Cookies[name].Value;
            }
            return DecodeVal(cookieVal);
        }
        /// <summary>
        /// 获取一个多值cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRange(string name, List<string> keys) {
            if (HttpContext.Current.Request.Cookies[name] != null) {
                Dictionary<string, string> dicItems = new Dictionary<string, string>();
                foreach (var key in keys) {
                    var valEncode = EncodeVal(HttpContext.Current.Request.Cookies[name][key]);
                    dicItems.Add(key, valEncode);
                }
                return dicItems;
            }
            return null;
        }
        /// <summary>
        /// 修改一个cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Update(string name,string value) {
            //获取客户端的Cookie对象
            HttpCookie cok = HttpContext.Current.Request.Cookies[name];

            if (cok != null) {
                //修改Cookie的两种方法
                cok.Value = EncodeVal(value);

                HttpContext.Current.Response.AppendCookie(cok);
            }      
        }
        /// <summary>
        /// 修改多个键值的 cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dicItems"></param>
        public static void UpdateRange(string name, Dictionary<string, string> dicItems) {
            HttpCookie cok = HttpContext.Current.Request.Cookies[name];
            if (cok != null) {
                //修改Cookie的两种方法
                foreach (var item in dicItems) {
                    cok[item.Key] = EncodeVal(item.Value);
                }

                HttpContext.Current.Response.AppendCookie(cok);
            }
        }

        /// <summary>
        /// 删除cookie，即使之过期
        /// </summary>
        /// <param name="name"></param>
        public static void Delete(string name) {
            HttpCookie cok = HttpContext.Current.Request.Cookies[name];
            if (cok != null) {

                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(ts); //删除整个Cookie，只要把过期时间设置为现在
                HttpContext.Current.Response.AppendCookie(cok);
            }
        }
    }
}
