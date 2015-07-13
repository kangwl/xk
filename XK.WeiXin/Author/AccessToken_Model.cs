namespace XK.WeiXin.Author {
    /// <summary>
    /// 获取到的凭证
    /// access_token的有效期目前为2个小时，需定时刷新，重复获取将导致上次获取的access_token失效。
    /// </summary>
    public class AccessToken_Model {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }
    }
}
