namespace XK.WeiXin.Core {
    /// <summary>
    /// 判断发送过来的消息类型
    /// </summary>
    public class MessageTypeEnum {
        /// <summary>
        /// 文本消息
        /// </summary>
        public const string text = "text";
        /// <summary>
        /// 图片消息
        /// </summary>
        public const string image = "image"; 
        /// <summary>
        /// 事件消息
        /// </summary>
        public const string events = "event";
    }
}