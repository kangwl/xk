using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.SessionState;

namespace WebAppBS {
    public class Global : System.Web.HttpApplication {

        log4net.ILog log = log4net.LogManager.GetLogger("mylogger");

        protected void Application_Start(object sender, EventArgs e) {
            AppStart.BundleConfig.BundleResources(BundleTable.Bundles);
            //log4net
            string log4config_xml = Server.MapPath("/Config/log4net1.xml");
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(log4config_xml));

        }

        protected void Session_Start(object sender, EventArgs e) {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {

        }

        protected void Application_Error(object sender, EventArgs e) {
          
            log.ErrorFormat("客户机IP：{0},错误地址:{1},信息：{2}", Request.UserHostAddress, Request.Url, HttpContext.Current.Error);
            HttpContext.Current.Server.ClearError();
            HttpContext.Current.Response.Write("系统出错");

        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}