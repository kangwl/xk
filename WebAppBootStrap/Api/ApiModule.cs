using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBS.Api {
    public class ApiModule:IHttpModule {
        public void Init(HttpApplication context) {
            context.BeginRequest += context_BeginRequest;
        }

        private void context_BeginRequest(object sender, EventArgs e) {
            HttpApplication application = sender as HttpApplication;
            var arr = application.Request.Url.AbsolutePath.Split('/');
            if (arr.Length < 4) {
                application.Response.Write("err");
                application.Response.End();
                return;
            }
            if (arr[1] == "api") {
                application.Context.RewritePath("/Api/Data.ashx");
            }
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}