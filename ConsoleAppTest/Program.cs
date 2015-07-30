﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XK.Common.help;
using XK.Dal.Mongo;
using XK.Model;


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
            //XK.Common.web.HttpWebHelper webHelper = new HttpWebHelper("http://sd");
            //string res = webHelper.GetResponseStr();
            //int firstIndex = res.IndexOf('{');
            //string s = res.Substring(firstIndex).TrimEnd(')');

            //JObject jo = (JObject)JsonConvert.DeserializeObject(s);
            //string zone = jo["items"]["7"].ToString();
            //Newtonsoft.Json.JsonReader reader=new JTokenReader();

            //List<XK.Common.help.Where> wheres = new List<Where> {
            //    new Where("1", "=", "1")
            //};
            //DataTable dt = XK.Bll.User_Bll.GetDataTable(wheres, 100);
            //Console.WriteLine(dt.Rows.Count);

            //List<XK.Common.help.WhereItem> wheres = new List<WhereItem> {
            //    new WhereItem("ID", "=", "6")
            //};
            //Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            //dic.Add("Name", "kwl110");
            //bool success = XK.Bll.User_Bll.Update(wheres, dic);
            //Console.WriteLine(success);
            //List<WhereItem> whereItems=new List<WhereItem> {
            //    new WhereItem("ID","=","6")
            //};
            //bool success = XK.Bll.User_Bll.Delete(whereItems);
            //Console.WriteLine(success);

            //XK.Model.User_Model userModel = XK.Bll.User_Bll.GetModel(5);
            //Console.WriteLine(userModel.Name);

            //List<XK.Common.help.WhereItem> whereItems = new List<WhereItem>();
            //whereItems.Add(new WhereItem("ID", "<=", 10));
            //List<XK.Model.User_Model> userModels;
            //userModels = XK.Bll.User_Bll.GetModels(whereItems, 1, 3, "ID ASC");
            //userModels.ForEach(u => Console.WriteLine(u.Name));
            //XK.Model.User_Model user = XK.Bll.User_Bll.GetModel(5);
            //Console.WriteLine(user.UserPassword);

            //List<XK.Model.User_Model> userModels = new List<User_Model>() {
            //    new User_Model(){AddDateTime = DateTime.Now,Age = 12,Email = "kangwl2009@163.com",MobilePhone = "1223334",Name = "k5",Sex = 1,UpdateDateTime = DateTime.Now,UserID = "kwl2011",UserPassword = "abc123",UserType = 2},
            //    new User_Model(){AddDateTime = DateTime.Now,Age = 13,Email = "kangwl2010@163.com",MobilePhone = "122356",Name = "k4",Sex = 1,UpdateDateTime = DateTime.Now,UserID = "kwl2012",UserPassword = "abc123",UserType = 2},
            //    new User_Model(){AddDateTime = DateTime.Now,Age = 16,Email = "kangwl2011@163.com",MobilePhone = "122321",Name = "k3",Sex = 2,UpdateDateTime = DateTime.Now,UserID = "kwl2013",UserPassword = "abc123",UserType = 2},
            //    new User_Model(){AddDateTime = DateTime.Now,Age = 12,Email = "kangwl2012@163.com",MobilePhone = "122677",Name = "k2",Sex = 1,UpdateDateTime = DateTime.Now,UserID = "kwl2014",UserPassword = "abc123",UserType = 2}
            //};

            //int retIndex = XK.Bll.User_Bll.InsertTran(userModels);
            //Console.WriteLine(retIndex);
            XK.Dal.Mongo.Query query = new Query();
           // bool success = query.InsertUserModel();
           // Console.WriteLine(success);
           //XK.Model.User_Model user= query.TestMongoDB();
           // Console.WriteLine(user.Name);
            XK.Model.User_Model user = query.GetUer();
            if (user == null) {
                Console.WriteLine("user not exist");
                Console.Read();
                return;
            }
            Console.WriteLine(user.Name);
            Console.WriteLine("edb");
            Console.Read();
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
