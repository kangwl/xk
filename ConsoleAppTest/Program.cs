using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XK.Common.web;

namespace ConsoleAppTest {
    class Program {
        private static void Main(string[] args) {
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
            //XK.Common.web.HttpWebHelper webHelper = new HttpWebHelper("http://d.10jqka.com.cn/v2/realhead/hs_600372/last.js");
            //string res = webHelper.GetResponseStr();
            //int firstIndex = res.IndexOf('{');
            //string s = res.Substring(firstIndex).TrimEnd(')');

            //JObject jo = (JObject)JsonConvert.DeserializeObject(s);
            //string zone = jo["items"]["7"].ToString();
            //Newtonsoft.Json.JsonReader reader=new JTokenReader();

            var role = "user";
            var operation = new FileOper();
            // 可以正常调用Read
            
            OperationInvoker.Invoke(operation, role, "Read", null);
            // 但是不能调用Write
            //OperationInvoker.Invoke(operation, role, "Write", null);

            Console.WriteLine("\u4e2d\u822a\u7535\u5b50");
            Console.Read();
        }
      
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PowerAttribute : Attribute {
        public string Role { get; set; }

        public PowerAttribute(string role) {
            Role = role;
        }

    }

    public class FileOper {
        [Power("user")]
        public void Read() {

        }
        [Power("admin")]
        public void Write() {

        }
    }

    /// <summary>
    /// 调用操作的工具类
    /// </summary>
    public static class OperationInvoker {
        public static void Invoke(object target, string role, string operationName, object[] parameters) {
            var targetType = target.GetType();
            var methodInfo = targetType.GetMethod(operationName);

            if (methodInfo.IsDefined(typeof(PowerAttribute), false)) {
                // 读取出所有权限相关的标记
                var permissons = methodInfo
                    .GetCustomAttributes(typeof(PowerAttribute), false)
                    .OfType<PowerAttribute>();
                // 如果其中有满足的权限
                if (permissons.Any(p => p.Role == role)) {
                    methodInfo.Invoke(target, parameters);
                }
                else {
                    throw new Exception(string.Format("角色{0}没有访问操作{1}的权限！", role, operationName));
                }
            }
        }
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
