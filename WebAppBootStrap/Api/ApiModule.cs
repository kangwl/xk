using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBS.Api {
    public class ApiModule:IHttpModule {
        public void Init(HttpApplication context) {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e) {
            HttpApplication application = sender as HttpApplication;
            string absPath = application.Request.Url.AbsolutePath.ToLower();
            var arr = absPath.Split('/');
            int a = 0;
            if (arr.Contains("api")) {
                a = 12;
                application.Context.RewritePath("/view/user/list.aspx");
            }
            string b = a.ToString();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}