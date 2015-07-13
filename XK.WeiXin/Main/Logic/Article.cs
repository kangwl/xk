using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XK.Common;

namespace XK.WeiXin.Main.Logic {
   public class Article:IMessageLogic {

       public Article(XmlDocument xmlRecieve) {
           XmlDoc = xmlRecieve;
       }

       /// <summary>
       /// 接收到的消息
       /// </summary>
       public XmlDocument XmlDoc { get; set; }

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

           List<string> itms = new List<string>();
           for (int i = 0; i < 2; i++) {
               string itm = string.Format(itmBase, "这是title", "这是description",
                   "http://bootstrap-4.apphb.com/Content/images/1/005.jpg", "");
               itms.Add(itm);
           }
           string itmStr = string.Join("", itms);
           string sendXml = string.Format(sendXmlBase, FromUserName, ToUserName,
               Common.TimeConvert.GetDateTimeStamp(DateTime.Now), itms.Count, itmStr);
           return sendXml;
       }

       private string _sendXml = @"<xml>
                                    <ToUserName><![CDATA[toUser]]></ToUserName>
                                    <FromUserName><![CDATA[fromUser]]></FromUserName>
                                    <CreateTime>12345678</CreateTime>
                                    <MsgType><![CDATA[news]]></MsgType>
                                    <ArticleCount>2</ArticleCount>
                                    <Articles>
                                        <item>
                                            <Title><![CDATA[title1]]></Title> 
                                            <Description><![CDATA[description1]]></Description>
                                            <PicUrl><![CDATA[picurl]]></PicUrl>
                                            <Url><![CDATA[url]]></Url>
                                        </item>
                                        <item>
                                            <Title><![CDATA[title]]></Title>
                                            <Description><![CDATA[description]]></Description>
                                            <PicUrl><![CDATA[picurl]]></PicUrl>
                                            <Url><![CDATA[url]]></Url>
                                        </item>
                                    </Articles>
                                    </xml> ";


       private string sendXmlBase = @"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[news]]></MsgType>
                                    <ArticleCount>{3}</ArticleCount>
                                    <Articles>
                                        {4}
                                    </Articles>
                                    </xml> ";

       private string itmBase = @"<item>
                                    <Title><![CDATA[{0}]]></Title>
                                    <Description><![CDATA[{1}]]></Description>
                                    <PicUrl><![CDATA[{2}]]></PicUrl>
                                    <Url><![CDATA[{3}]]></Url>
                                 </item>";

    }
}
