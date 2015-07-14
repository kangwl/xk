using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace WebAppBS.Api {
    /// <summary>
    /// Data 的摘要说明
    /// </summary>
    public class Data : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            context.Response.ContentType = "text/plain";
            ResponseJson(context);
            context.Response.End();
        }

        //http://localhost/api/user/add
        //http://localhost/api/user/delete
        //http://localhost/api/user/list
        //http://localhost/api/user/getone
        private void ResponseJson(HttpContext context) {
            string json = "";
            try {

                string url = context.Request.RawUrl;

                string[] urlitm = url.Split('/');

                string source = urlitm[2]; //对应处理的类（XK.DataApi.Source 中）
                string act = urlitm[3]; //对应处理的类中的方法

                json = new XK.DataApi.Enter().Init(source, act, context.Request);
            }
            catch (Exception ex) {
                json = Newtonsoft.Json.JsonConvert.SerializeObject(new XK.DataApi.ApiInfo.ApiException(-1, ex.ToString()));
            }
            context.Response.Write(json);
        }


        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}