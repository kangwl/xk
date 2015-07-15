using System;
using XK.Common.web;

namespace XK.WeiXin.Author {
   public class AppTicket {
       public const string AccessTokenUrl =
           "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

       /// <summary>
       /// 获取请求地址
       /// </summary>
       /// <param name="appID"></param>
       /// <param name="appSecret"></param>
       /// <returns></returns>
       public string GetAccessTokenUrl(string appID, string appSecret) {
           return string.Format(AccessTokenUrl, appID, appSecret);
       }

       public string GetAccessTokenUrl() {
           return string.Format(AccessTokenUrl, AppConfig.Instance.AppID, AppConfig.Instance.AppSecret);
       }

       public string GetAccessTokenJson() {
           string reqUrl = string.Format(GetAccessTokenUrl(AppConfig.Instance.AppID, AppConfig.Instance.AppSecret));
           HttpWebHelper httpWebHelper = new HttpWebHelper(reqUrl);
           string json = httpWebHelper.GetResponseStr();
           return json;
       }

       public string GetAccessTokenJson(string appID, string appSecret) {
           string reqUrl = string.Format(GetAccessTokenUrl(appID, appSecret));
           HttpWebHelper httpWebHelper = new HttpWebHelper(reqUrl);
           string json = httpWebHelper.GetResponseStr();
           return json;
       }

       public AccessToken_Model GetAccessToken() {
           string jsonAccessToken = GetAccessTokenJson();
           AccessToken_Model accessToken =
               Common.json.JsonHelper<AccessToken_Model>.DeserializeFromStr(jsonAccessToken);
           return accessToken;
       }

       public AccessToken_Model GetAccessToken(string appID, string appSecret) {
           string jsonAccessToken = GetAccessTokenJson(appID, appSecret);
           AccessToken_Model accessToken =
               Common.json.JsonHelper<AccessToken_Model>.DeserializeFromStr(jsonAccessToken);
           return accessToken;
       }





   }
}
