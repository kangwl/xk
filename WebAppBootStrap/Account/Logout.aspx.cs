using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Authentication;

namespace WebAppBootStrap.Account {
    public partial class Logout : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            DealLogout();
        }

        private void DealLogout() {
            LoginLogic.LogOut();
            //其他逻辑
            Response.Redirect(LoginLogic.LoginUrl);
        }
    }
}