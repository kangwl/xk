﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Authentication;
using XK.Bll;

namespace WebAppBS.Account {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //log4net.ILog log = log4net.LogManager.GetLogger(typeof(Login));
            //log.Error(DateTime.Now);
            //int.Parse("sd");
            //test
            if (LoginLogic.HasLogin) {
                Response.Redirect(LoginLogic.DefaultUrl);
            }
        }

    }
}