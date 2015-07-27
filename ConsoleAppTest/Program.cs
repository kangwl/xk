using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleAppTest
{
    class Program {
        private static void Main(string[] args)
        {
            //TestM testM = new TestM();
            //Console.WriteLine(testM.GetResult("3"));
            //Console.WriteLine(testM.GetResultBridge("3"));
            //Console.WriteLine("end");
            //Console.Read();

            //                 string xmlSend = @"<xml>
            //                            <ToUserName><![CDATA[123]]></ToUserName>
            //                            <FromUserName><![CDATA[2]]></FromUserName>
            //                            <CreateTime>123</CreateTime>
            //                            <MsgType><![CDATA[text]]></MsgType>
            //                            <Content><![CDATA[333]]></Content>
            //                            </xml>";
            //            XmlDocument xmldoc = new XmlDocument();
            //            xmldoc.LoadXml(xmlSend);
            //            string a = XK.Common.XmlHelper.GetXmlNodeTextByXpath(xmldoc, "//ToUserName");
            //            Console.WriteLine(a);
            //            Console.WriteLine("end");
            //            Console.Read();
            //XK.Common.web.HttpWebHelper webHelper = new HttpWebHelper("http://sd");
            //string res = webHelper.GetResponseStr();
            //int firstIndex = res.IndexOf('{');
            //string s = res.Substring(firstIndex).TrimEnd(')');

            //JObject jo = (JObject)JsonConvert.DeserializeObject(s);
            //string zone = jo["items"]["7"].ToString();
            //Newtonsoft.Json.JsonReader reader=new JTokenReader();

            Console.WriteLine(Age);
            Console.Read();
        }
        public static int Age { get; set; } = 2;

    }
 

    public class TestM {

        #region 普通 if else 逻辑判断

        /// <summary>
        /// 普通 if else
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public string GetResult(string act) {
            if (act == "1") {
                return n1();
            }
            else if (act == "2") {
                return n2();
            }
            else if (act == "3") {
                return n3();
            }
            return "unknow";
        }

        #endregion


        #region 不用 if else 用委托进行逻辑判断（免除大量的 if else 分支判断）

        private readonly Dictionary<string, Func<string>> dicFunc = new Dictionary<string, Func<string>>();

        public TestM() {
            dicFunc.Add("1", n1);
            dicFunc.Add("2", n2);
            dicFunc.Add("3", n3);
        }

        public string GetResultBridge(string num) {
            Func<string> methodFunc;
            //bool has = dicFunc.TryGetValue(num, out methodFunc);
            //if (!has) {
            //    return "unknow";
            //}
            methodFunc = dicFunc.FirstOrDefault(d => d.Key == num).Value;
            if (methodFunc == null) {
                return "unknow";
            }
            string res = methodFunc();
            return res;
        } 

        #endregion


        private string n1() {
            return "n1";
        }

        private string n2() {
            return "n2";
        }

        private string n3() {
            return "n3";
        }

    }
 
}
