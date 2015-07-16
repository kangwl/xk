using System;
using System.Linq;
using System.Text;
using System.Web;
using XK.DataApi.Logic;

namespace XK.DataApi {
    public class ApiModule:IHttpModule {
        public void Init(HttpApplication context) {
            context.BeginRequest += context_BeginRequest;
        }
        /// <summary>
        /// api的url标识
        /// web.config配置
        /// <add key="apisign" value="api"/>
        /// </summary>
        public string ApiSign {
            get {
                string apiConfig = System.Configuration.ConfigurationManager.AppSettings["apisign"];
                return apiConfig;
            }
        }

        private void context_BeginRequest(object sender, EventArgs e) {
            HttpApplication application = sender as HttpApplication;
            if (application != null) {
                string json = "";
                if (application.Request.Url.IsFile) return;
                var arr = application.Request.Url.AbsolutePath.ToLower().Split('/');
                int apiSignIndex = arr.ToList().FindIndex(a => a == ApiSign);
    
                if (apiSignIndex >= 0) {
                    //属于api访问
                    json = arr.Length < 4 ? Common.json.JsonFac.Serialize2Json(new Logic.ApiInfo(2, "参数不对")) : GetDataJson(application.Context);
                    Resposer(application.Context, json);
                }
            }
        }

        private string GetDataJson(HttpContext context) {
            string json = "";
            try {
                //exp:/api/user/list
                string url = context.Request.RawUrl;
                string[] urlitm = url.Split('/');
                int apiSignIndex = urlitm.ToList().FindIndex(a => a == ApiSign);
                string source = urlitm[apiSignIndex + 1]; //对应处理的类（XK.DataApi.Source 中）
                string act = urlitm[apiSignIndex + 2]; //对应处理的类中的方法
                //处理
                json = new XK.DataApi.Enter().Init(source, act, context.Request);
            }
            catch (Exception ex) {
                json = XK.Common.json.JsonFac.Serialize2Json(new ApiInfo(-1, ex.ToString()));
            }
            return json;
        }

        private void Resposer(HttpContext context, string content) {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(content);
            context.Response.End();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}