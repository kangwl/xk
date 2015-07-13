using System.Xml;

namespace XK.WeiXin.Main.Logic {
    public interface IMessageLogic {
        XmlDocument XmlDoc { get; set; }
        string ResponseMessage();
        string CreateSendMsg();
    }
}