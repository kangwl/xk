using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XK.Common;

namespace XK.WeiXin.Main.Logic {
    public class Image : IMessageLogic {

       public Image(XmlDocument xmlRecieve) {
            XmlDoc = xmlRecieve;
        }

       /// <summary>
       /// 接收到的消息
       /// </summary>
       public XmlDocument XmlDoc { get; set; }

       private readonly Dictionary<string, Func<string>> keywordFuncs = new Dictionary<string, Func<string>>();
       /// <summary>
       /// 逻辑处理后返回给发送者消息
       /// </summary>
       /// <returns></returns>
       public string ResponseMessage() {
           string msg = CreateSendMsg();
           return msg;
       }

        public string CreateSendMsg() {
           string ToUserName = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//ToUserName");
           string FromUserName = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//FromUserName");
           string MediaId=XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//MediaId");
           string msg = string.Format(sendXml, FromUserName, ToUserName,
               TimeConvert.GetDateTimeStamp(DateTime.Now), MediaId);
           return msg;
       }

       private string recieveXml = @" <xml>
                                 <ToUserName><![CDATA[toUser]]></ToUserName>
                                 <FromUserName><![CDATA[fromUser]]></FromUserName>
                                 <CreateTime>1348831860</CreateTime>
                                 <MsgType><![CDATA[image]]></MsgType>
                                 <PicUrl><![CDATA[this is a url]]></PicUrl>
                                 <MediaId><![CDATA[media_id]]></MediaId>
                                 <MsgId>1234567890123456</MsgId>
                                 </xml>";


       private string sendXml = @"<xml>
                                <ToUserName><![CDATA[{0}]]></ToUserName>
                                <FromUserName><![CDATA[{1}]]></FromUserName>
                                <CreateTime>{2}</CreateTime>
                                <MsgType><![CDATA[image]]></MsgType>
                                <Image>
                                <MediaId><![CDATA[{3}]]></MediaId>
                                </Image>
                                </xml>";
 



    }
}
