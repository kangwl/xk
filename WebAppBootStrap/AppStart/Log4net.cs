using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace WebAppBS.AppStart {
    public class Log4net {

        private Log4net(){}
        static Log4net(){}

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("mylogger");

        public static void Init() {
            string log4config_xml = HttpContext.Current.Server.MapPath("/Config/log4net_n.xml");
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(log4config_xml));
        }

        public static ILog logger {
            get {
                return log;
            }
        }
    }
}