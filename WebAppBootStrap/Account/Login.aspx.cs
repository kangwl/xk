﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Authentication;
using XK.Bll;

namespace WebAppBootStrap.Account {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            //test
            if (LoginLogic.HasLogin) {
                Response.Redirect(LoginLogic.DefaultUrl);
            }
        }

        public string RetutnUrl { get { return Request.QueryString["returl"]; } } 
    }
}