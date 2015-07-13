using System;
using System.Data;
using System.Web;
using System.Web.Security;

namespace XK.Authentication {
    public class LoginLogic {


        /// <summary>
        /// 登出
        /// </summary>
        public static void LogOut() {
            FormsAuthentication.SignOut();
        }


        /// <summary>
        /// 已登录为true
        /// </summary>
        public static bool HasLogin {
            get { return !string.IsNullOrEmpty(GetCurrentUser()); }
        }

        /// <summary>
        /// 系统登录页
        /// webconfig 配置
        /// </summary>
        public static string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }

        /// <summary>
        /// 系统默认首页
        /// webconfig 配置
        /// </summary>
        public static string DefaultUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }

        /// <summary>
        /// 获取登录用户账号
        /// </summary>
        public static string UserName { get { return GetCurrentUser(); } }

        /// <summary>
        /// 用户登录成功后记录ticket
        /// </summary>
        /// <param name="userID">用户账号</param>
        /// <param name="userData">用户数据（比如个人权限）</param>
        /// <param name="expireDateTime">过期时间</param>
        /// <param name="remember"></param>
        public static void RecordLogined(string userID, string userData, DateTime expireDateTime, bool remember = false) {
            //ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, userID, DateTime.Now, expireDateTime, remember, userData, "/");

            //encrypt
            string hashTicket = FormsAuthentication.Encrypt(ticket);
            //create cookie
            HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            userCookie.HttpOnly = true; //不允许客户端访问
            userCookie.Expires = ticket.Expiration;
            HttpContext.Current.Response.Cookies.Add(userCookie);
        }

        /// <summary>
        /// 获取登录用户账号
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUser() {
            string userid = HttpContext.Current.User.Identity.Name;
            return userid;
        }
  
    }
}
