using System;
using System.Threading;
using System.Web;
using System.Web.Optimization;

namespace WebAppBS {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
            //绑定资源文件js/css
            AppStart.BundleConfig.BundleResources(BundleTable.Bundles);
            //log4net
            AppStart.Log4net.Init();
        }

        protected void Session_Start(object sender, EventArgs e) {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        //全局异常捕捉
        protected void Application_Error(object sender, EventArgs e) {

            AppStart.Log4net.logger.ErrorFormat("客户机IP：{0},错误地址:{1},信息：{2}", Request.UserHostAddress, Request.Url,
                HttpContext.Current.Error);
           // HttpContext.Current.Server.ClearError();
            //HttpContext.Current.Response.Write("系统出错");
        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {
            
        }
    }
}