using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using Microsoft.Win32;

namespace WebAppBootStrap.ViewHandler {
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class User : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            DealRequest(context);
            context.Response.End();
        }

        private void CheckLogin() {
            if (XK.Authentication.LoginLogic.HasLogin) {
 
                 
            }
        }
        
        private void DealRequest(HttpContext context) {
            XK.Core.ListJson.User usersJson = new XK.Core.ListJson.User(context.Request);
            string json = usersJson.GetDataJson();
            context.Response.Write(json);
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
  
}