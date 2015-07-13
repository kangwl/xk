using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Common;
using XK.WeiXin;

namespace WebAppBS.weixin {
    public partial class Index : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //Log log=new Log();
           // log.WriteLog("11111111111111111111");
            //log.WriteLog("ready");
            XK.WeiXin.Enter enter = new Enter(XK.WeiXin.Author.AppConfig.Instance.Token, Request, Response);
           // log.WriteLog("start");
            enter.StartWeiXin();
        }
    }
}