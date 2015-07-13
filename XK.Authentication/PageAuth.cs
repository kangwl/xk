using System;

namespace XK.Authentication {
    /// <summary>
    /// 让页面继承此类，检查用户登录、权限
    /// </summary>
    public class PageAuth:System.Web.UI.Page {
        protected override void OnLoad(EventArgs e) {
            string userid = LoginLogic.GetCurrentUser();
            if (string.IsNullOrWhiteSpace(userid)) {
                var retUrl = Request.Url.PathAndQuery;
                Response.Redirect(LoginLogic.LoginUrl + "?returl=" + retUrl, false);
            }
        }


        /// <summary>
        /// 带参数的url
        /// </summary>
        public string PathAndQuery{get { return Request.Url.PathAndQuery; }}
        /// <summary>
        /// 不带参数的 url 绝对路径
        /// </summary>
        public string AbsolutePath{get { return Request.Url.AbsolutePath; }}

    }
}
