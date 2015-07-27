using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using XK.Authentication;
using XK.Bll; 

namespace WebAppBS.Account {
    /// <summary>
    /// Account 的摘要说明
    /// </summary>
    public class Account : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            DealRequest(context);
            context.Response.End();
        }

        private Action<HttpContext> action;
        private void DealRequest(HttpContext context) {

            action += Login;
            action(context);
        }
        //登录
        private void Login(HttpContext context) {

            HttpRequest request = context.Request;
            string act = request.GetVal("act");
            if (act == "login") {
                string uid = request.GetVal("uid");
                string pwd = request.GetVal("pwd");
                string rem = request.GetVal("rem");
                bool remember = (rem.Trim().ToLower() == "true");
               // try {
                    bool success = false;
       
                    DataTable dt = User_Bll.CheckLogin(uid, pwd, out success);
                    if (success) {
                        //login success
                        DataRow row = dt.Rows[0];
                        int usertype = row["UserType"].ToInt();
                        LoginLogic.RecordLogined(uid, usertype.ToString(), DateTime.Now.AddDays(2), remember);
                    }
                    context.Response.Write(success ? "1" : "登录失败，请检查用户名和密码");
              //  }
             //   catch (Exception) {
             //       context.Response.Write("系统异常");
             //   }
            }
        }


        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}
