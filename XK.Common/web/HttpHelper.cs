using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace XK.Common.web {
    public class HttpHelper {
        private const string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies) {
            if (string.IsNullOrEmpty(url)) {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent)) {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue) {
                request.Timeout = timeout.Value;
            }
            if (cookies != null) {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieContainer cookies) {
            if (string.IsNullOrEmpty(url)) {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null) {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase)) {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            request.UserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : DefaultUserAgent;

            if (timeout.HasValue) {
                request.Timeout = timeout.Value;
            }
            if (cookies != null) {
                request.CookieContainer = cookies;
                //request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0)) {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys) {
                    buffer.AppendFormat(i > 0 ? "&{0}={1}" : "{0}={1}", key, parameters[key]);
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream()) {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {
            return true; //总是接受  
        }

        /// <summary>
        /// 获取源代码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="PointStr"></param>
        /// <param name="Ec"></param>
        /// <returns></returns>
        public static string GetPage(string url,Encoding Ec)
        {
            HttpWebRequest request = null;
            HttpWebResponse webResponse = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                request.Timeout = 5000;
                request.ReadWriteTimeout = 5000;
                //request.KeepAlive = true;
                request.Method = "GET";
                //request.ImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Anonymous;
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                webResponse = (HttpWebResponse)request.GetResponse();
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (StreamReader reader2 = new StreamReader(stream, Ec))
                    {
                        return reader2.ReadToEnd();
                    }
                }
            }
            catch
            {
                //Main.St.Add(url+"----"+ex.ToString());
                return "";
            }
            finally
            {
                if (request != null)
                {
                    request = null;
                }
                if (webResponse != null)
                {
                    webResponse.Close();
                }
            }
        }

        /// <summary>
        /// 保存网络文件
        /// </summary>
        /// <param name="sUrl"></param>
        /// <param name="sSavePath"></param>
        /// <param name="fileExtension">exp: .jpg</param>
        /// <returns></returns>
        public static string GetHttpFile(string sUrl, string sSavePath,string fileExtension) {

            var savePath = HttpContext.Current.Request.MapPath(sSavePath);
            string fullName = Guid.NewGuid().ToString("n");
            string fileName = savePath + fullName + fileExtension;
            string bRslt;
            WebResponse oWebRps = null;
            WebRequest oWebRqst = WebRequest.Create(sUrl);
            oWebRqst.Timeout = 50000;
            try {
                oWebRps = oWebRqst.GetResponse();
            }
            catch (WebException e) {
            }
            catch (Exception) {
            }
            finally {
                var ifExisit = oWebRps != null && oWebRps.ResponseUri.LocalPath.ToLower().Contains("error");
                if (!ifExisit) {
                    BinaryReader oBnyRd;
                    if (oWebRps != null)
                        using (
                            oBnyRd =
                            new BinaryReader(oWebRps.GetResponseStream(), Encoding.GetEncoding("GB2312"))) {
                            int iLen = Convert.ToInt32(oWebRps.ContentLength);
                            try {
                                var fullPath = HttpContext.Current.Request.MapPath(sSavePath);
                                if (!Directory.Exists(fullPath)) {
                                    Directory.CreateDirectory(fullPath);
                                }
                                FileStream oFileStream =
                                    File.Exists(fullPath + fullName)
                                        ? File.OpenWrite(fileName)
                                        : File.Create(fileName);
                                oFileStream.SetLength(iLen);
                                oFileStream.Write(oBnyRd.ReadBytes(iLen), 0, iLen);
                                oFileStream.Close();
                                bRslt = "1";
                            }
                            catch (Exception ex) {
                                bRslt = "0";
                            }
                            finally {
                                oBnyRd.Close();
                                oWebRps.Close();
                            }
                        }
                    else {
                        bRslt = "null";
                    }
                }
                else {
                    bRslt = "null";
                }
            }
            return bRslt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlStr"></param>
        /// <param name="postStr"></param>
        /// <returns></returns>
        public static string PostData(string urlStr, string postStr) {
            try {

                HttpWebRequest webRequest = null;
                StreamWriter requestWriter = null;

                string postString = postStr;
                string responseData = "";
                webRequest = WebRequest.Create(urlStr) as HttpWebRequest;
                webRequest.Method = "POST";
                webRequest.ServicePoint.Expect100Continue = false;
                webRequest.Timeout = 1000 * 60;
                // webRequest.ContentType = "application/x-www-form-urlencoded";
                //POST the data. 
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                try {
                    requestWriter.Write(postString);
                }
                catch (Exception ex2) {
                    return "连接错误";
                }
                finally {
                    requestWriter.Close();
                }
                responseData = WebResponseGet(webRequest);
                return responseData;
            }
            catch {
                return "未知错误";
            }
        }

        /// <summary>
        /// 获取网络请求数据 返回string
        /// </summary>
        /// <param name="reqJonUrl"></param>
        /// <returns></returns>
        public static string GetWebString(string reqJonUrl) {
            string str = "";
            StreamReader streamReader = null;
            try {
                Uri uri = new Uri(reqJonUrl);
                var stream = System.Net.WebRequest.Create(uri).GetResponse().GetResponseStream();
                if (stream != null) streamReader = new StreamReader(stream, true);
                if (streamReader != null) {
                    str = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex) {
                if (streamReader != null) streamReader.Close();
            }
            return str;
        }

        /// <summary> 
        /// Process the web response. 
        /// </summary> 
        /// <param name="webRequest">The request object.</param> 
        /// <returns>The response data.</returns> 
        private static string WebResponseGet(HttpWebRequest webRequest) {
            StreamReader responseReader = null;
            string responseData = "";
            try {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch {
                return "连接错误";
            }
            finally {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
            }
            return responseData;
        }
    }
}
