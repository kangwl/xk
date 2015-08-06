using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppBS.Prox {
    public partial class go : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        /// <summary>
        /// 通过URI读取指向地址的HTML代码
        /// </summary>
        /// <param name="url">URI地址(例如:http://www.wnweixiu.com)</param>
        /// <returns></returns>
        protected string GetHTMLCode(string url) {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //使用Cookie设置AllowAutoRedirect属性为false,是解决“尝试自动重定向的次数太多。”的核心
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = false;
            WebResponse response = (WebResponse)request.GetResponse();
            Stream sm = response.GetResponseStream();
            System.IO.StreamReader streamReader = new System.IO.StreamReader(sm);
            //将流转换为字符串
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            return html;
        }

        protected void btnGetHTML_Click(object sender, EventArgs e) {
            txtPageHTML.Text = GetHTMLCode(txtURL.Text);
        }
    }
}