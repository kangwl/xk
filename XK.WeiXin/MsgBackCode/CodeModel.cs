namespace XK.WeiXin.MsgBackCode {
    /// <summary>
    /// 错误码等信息 
    /// JSON {"errcode":40013,"errmsg":"invalid appid"}
    /// </summary>
    public class CodeModel {
        /// <summary>
        /// 错误码
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误信息 
        /// </summary>
        public string errmsg { get; set; }
    }
}
