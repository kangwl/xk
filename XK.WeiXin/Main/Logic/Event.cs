using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XK.Common;

namespace XK.WeiXin.Main.Logic {
    public class Event : IMessageLogic {

        public Event(XmlDocument recieveXml) {
            XmlDoc = recieveXml;
            AddKeyWordFunc();
        }
        //1.todo: 用户未关注时，进行关注后的事件推送
        private string guanzhuXml = @"<xml><ToUserName><![CDATA[toUser]]></ToUserName>
                                        <FromUserName><![CDATA[FromUser]]></FromUserName>
                                        <CreateTime>123456789</CreateTime>
                                        <MsgType><![CDATA[event]]></MsgType>
                                        <Event><![CDATA[subscribe]]></Event>
                                        <EventKey><![CDATA[qrscene_123123]]></EventKey>
                                        <Ticket><![CDATA[TICKET]]></Ticket>
                                        </xml>";

        //ToUserName	开发者微信号
        //FromUserName	发送方帐号（一个OpenID）
        //CreateTime	消息创建时间 （整型）
        //MsgType	消息类型，event
        //Event	事件类型，subscribe
        //EventKey	事件KEY值，qrscene_为前缀，后面为二维码的参数值
        //Ticket	二维码的ticket，可用来换取二维码图片







        //todo:2. 用户已关注时的事件推送

        //推送XML数据包示例：

        //<xml>
        //<ToUserName><![CDATA[toUser]]></ToUserName>
        //<FromUserName><![CDATA[FromUser]]></FromUserName>
        //<CreateTime>123456789</CreateTime>
        //<MsgType><![CDATA[event]]></MsgType>
        //<Event><![CDATA[SCAN]]></Event>
        //<EventKey><![CDATA[SCENE_VALUE]]></EventKey>
        //<Ticket><![CDATA[TICKET]]></Ticket>
        //</xml>
        //参数说明：

        //参数	描述
        //ToUserName	开发者微信号
        //FromUserName	发送方帐号（一个OpenID）
        //CreateTime	消息创建时间 （整型）
        //MsgType	消息类型，event
        //Event	事件类型，SCAN
        //EventKey	事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        //Ticket	二维码的ticket，可用来换取二维码图片

        private string Content { get { return XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//Event"); } }



        public System.Xml.XmlDocument XmlDoc { get; set; }

        public string ResponseMessage() {
            ReturnText = Content;
            Func<string> keywordFunc = null;
            keywordFunc = keywordFuncs.FirstOrDefault(d => d.Key == Content.ToLower()).Value;
            if (keywordFunc == null) {
                Log log=new Log();
                log.WriteLog("Event:"+XmlDoc.InnerText);
                return "";
            }
            return keywordFunc();

        }

        private string ReturnText { get; set; }

        private readonly Dictionary<string, Func<string>> keywordFuncs = new Dictionary<string, Func<string>>();

        private void AddKeyWordFunc() {
            keywordFuncs.Add("subscribe", CreateSendMsg);
            keywordFuncs.Add("scan", CreateSendMsg); 
        }

        public string CreateSendMsg() {
            StringBuilder sb = new StringBuilder();
            sb.Append("欢迎关注大侠我\n");
            sb.Append("指令如下:\n");
            sb.Append("添加股票的指令：添加股票600372\n");
            sb.Append("查询已添加的股票：股票\n");
            sb.Append("删除某个股票:删除股票600372\n");
            sb.Append("删除全部的股票:删除全部股票\n");
            sb.Append("自由查询股票:查询股票600372\n");

            string ToUserName = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//ToUserName");
            string FromUserName = XmlHelper.GetXmlNodeTextByXpath(XmlDoc, "//FromUserName");

            string msg = string.Format(Text.sendXml, FromUserName, ToUserName,
                TimeConvert.GetDateTimeStamp(DateTime.Now), sb.ToString());

            return msg;
        }
    }
}
