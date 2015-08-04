using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XK.Common;
using XK.Common.help;
using XK.Model;
using XK.MongoDBUtil;


namespace ConsoleAppTest {
    class Program {
        private static void Main(string[] args) {
            //TestM testM = new TestM();
            //Console.WriteLine(testM.GetResult("3"));
            //Console.WriteLine(testM.GetResultBridge("3"));
            //Console.WriteLine("end");
            //Console.Read();

            //       string xmlSend = @"<xml>
            //                  <ToUserName><![CDATA[123]]></ToUserName>
            //                  <FromUserName><![CDATA[2]]></FromUserName>
            //                  <CreateTime>123</CreateTime>
            //                  <MsgType><![CDATA[text]]></MsgType>
            //                  <Content><![CDATA[333]]></Content>
            //                  </xml>";
            //  XmlDocument xmldoc = new XmlDocument();
            //  xmldoc.LoadXml(xmlSend);
            //  string a = XK.Common.XmlHelper.GetXmlNodeTextByXpath(xmldoc, "//ToUserName");
            //  Console.WriteLine(a);
            //  Console.WriteLine("end");
            //  Console.Read();

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
           // XK.Dal.Mongo.Query query = new Query();
           // bool success = query.InsertUserModel();
           // Console.WriteLine(success);
           //XK.Model.User_Model user= query.TestMongoDB();
           // Console.WriteLine(user.Name);
            //XK.Model.User_Model user = query.GetUer();
            //if (user == null) {
            //    Console.WriteLine("user not exist");
            //    Console.Read();
            //    return;
            //}
            //Console.WriteLine(user.Name);
            //Console.WriteLine("edb");

            MongoDBHelper<XK.Model.User_Model> dbHelper=new MongoDBHelper<User_Model>("User");
            //User_Model user = new User_Model() {
            //    _id =Guid.NewGuid(),
            //    AddDateTime = DateTime.Now,
            //    Age = 12,
            //    Email = "kangwl2009@163.com",
            //    MobilePhone = "1223334",
            //    Name = "k1199",
            //    Sex = 1,
            //    UpdateDateTime = DateTime.Now,
            //    UserID = "kwl2011",
            //    UserPassword = "abc123",
            //    UserType = 2
            //};
            //Task t = dbHelper.DbExcute.Add(user);
            //Console.WriteLine(t.IsCompleted);
            //t.Wait();
            //Console.WriteLine(t.IsCompleted);

            //Task<User_Model> usertTask = dbHelper.DbExcute.Get(U => U.Name == "k555");
            //usertTask.Wait();
            //Console.WriteLine(usertTask.Result.Name);
            //list
            //Task<List<User_Model>> userModelsTask = dbHelper.DbExcute.GetList(u => u.Age < 220);
            //userModelsTask.Result.ForEach(u => Console.WriteLine(u.Name));

           // dbHelper.DbExcute.GetListAction(u => u.Age < 222, (userm) => Console.WriteLine(userm.Name));

            //Task<List<User_Model>> userModelsTask = dbHelper.DbExcute.GetPaged(u => u.Age < 222, 2, 2, m => m.Name, false);
            //userModelsTask.Result.ForEach(u => Console.WriteLine(u.Name));

            //Task<User_Model> user1 = dbHelper.DbExcute.Get(u => u.Name == "k1199");
            //user1.Wait();

            //Console.WriteLine(user1.Result == null ? "" : user1.Result.Name);

            //List<User_Model> users=new List<User_Model>();
            //users.Add(user);
            //bool success = dbHelper.DbExcute.Update(u => u.Name == "k77711", users);
            //Console.WriteLine(success);

            //Console.WriteLine("main thread start");
            //Task<string> task = GetName();
           
            //Console.WriteLine("main thread do some");
            //Console.WriteLine("name thread start");

            //Console.WriteLine(task.Result);

            //Console.WriteLine("end");

            List<string> ips=new List<string> {
                "98.126.50.92",
                "100.43.170.19",
                "98.126.46.227",
                "98.126.63.251",
                "174.139.1.11",
                "174.139.176.35",
                "174.139.248.124",
                "103.228.92.203",
                "103.228.92.204",
                "103.228.92.205",
                "103.228.92.210",
                "103.228.92.211"
            };
            Dictionary<string, long> dicIP = new Dictionary<string, long>();

            ips.ForEach(ip => {
                long millsecond = XK.Common.NetHelper.PingTime(ip, 1000*5);
                dicIP.Add(ip, millsecond);
            });

            //找出最快的
            var dicIPSort = dicIP.OrderBy(pair => pair.Value);
            dicIPSort.ToList().ForEach(pair => Console.WriteLine(pair.Key + "---" + pair.Value));

         

            Console.WriteLine("red");
            Console.Read();
        }



        public static async Task<string> GetName() {
            string result = await Task<string>.Factory.StartNew(() => "kangwl");
            return result;
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
