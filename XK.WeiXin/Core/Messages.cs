using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using XK.Common;
using XK.WeiXin.Main.Logic;


namespace XK.WeiXin.Core {
    public class Messages {
        private readonly Dictionary<string, Func<XmlDocument, string>> dicFuncMessage = new Dictionary<string, Func<XmlDocument, string>>();
        public Messages() {
            dicFuncMessage.Add(MessageTypeEnum.text, ResponseTextMsg);
            dicFuncMessage.Add(MessageTypeEnum.image, ResponseArticleMsg);
            dicFuncMessage.Add(MessageTypeEnum.events, ResponseEventMsg);
        }

        /// <summary>
        /// 消息桥接
        /// 根据不同消息类型，经过不同的处理返回不同的结果
        /// </summary>
        /// <param name="xmlStream"></param>
        /// <returns></returns>
        public string GetResponseMsg(Stream xmlStream) {
            string resMsg = "";
            XmlDocument xmlDoc = XmlHelper.GetXmlDoc(xmlStream);
            string msgType = XmlHelper.GetXmlNodeTextByXpath(xmlDoc, "//MsgType");

            Func<XmlDocument, string> funcMessage = dicFuncMessage.FirstOrDefault(d => d.Key == msgType).Value;
            if (funcMessage != null) {
                resMsg = funcMessage(xmlDoc);
            }
            return resMsg;
        }

        //经过文本消息的逻辑处理后，输出
        private string ResponseTextMsg(XmlDocument xmlDoc) {

            Main.Logic.Text text = new Text(xmlDoc);
            string txtMsg = text.ResponseMessage();
            return txtMsg;
        }

        private string ResponseImageMsg(XmlDocument xmlDoc) {
            Main.Logic.Image image = new Image(xmlDoc);

            string imgMsg = image.ResponseMessage();
            return imgMsg;
        }

        private string ResponseArticleMsg(XmlDocument xmlDoc) {
            Main.Logic.Article article = new Article(xmlDoc);
            string message = article.ResponseMessage();
            return message;
        }

        private string ResponseEventMsg(XmlDocument xmlDoc) {
            Main.Logic.Event eEvent = new Event(xmlDoc);
            return eEvent.ResponseMessage();
        }


    }
}
