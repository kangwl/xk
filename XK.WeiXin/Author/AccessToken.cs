using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.WeiXin.Author {
    public class AccessToken {
        private static readonly AccessToken _instance = new AccessToken();

        private AccessToken() { }

        static AccessToken() { }

        public static AccessToken Instance { get { return _instance; } }

        /// <summary>
        /// token的值
        /// </summary>
        public string Value { get { return CacheAccessToken(); } }


        private static string CacheAccessToken() {
            string accessToken = "";
            string _cache_token_key = "AccessToken_Cache";
            var obj = Common.CacheHelper.Get(_cache_token_key);
            AccessToken_Model accessTokemModel = null;
            if (obj == null) {
                accessTokemModel = new AppTicket().GetAccessToken(AppConfig.Instance.AppID,
                    AppConfig.Instance.AppSecret);
                int expireTime = accessTokemModel.expires_in;
                int storeMin = (expireTime / 60) - 10;
                Common.CacheHelper.Insert(_cache_token_key, accessTokemModel, DateTime.Now.AddMinutes(storeMin));
            }
            else {
                accessTokemModel = obj as AccessToken_Model;
            }
            if (accessTokemModel != null) {
                accessToken = accessTokemModel.access_token;
            }
            return accessToken;
        }
    }
}
