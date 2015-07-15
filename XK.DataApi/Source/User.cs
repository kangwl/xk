using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XK.DataApi.Logic;

namespace XK.DataApi.Source {
    public class User {

        private static Act.UserAct act;
        /// <summary>
        /// user的方法体
        /// </summary>
        public static Act.UserAct ActMethod {
            get { return act ?? (act = new Act.UserAct()); }
        }

        //根据act添加对应的方法
        private static readonly Dictionary<string, Func<HttpRequest, string>> dicJsonRes =
            new Dictionary<string, Func<HttpRequest, string>> {
                {"add", ActMethod.Add},
                {"list", ActMethod.List}
            };

        /// <summary>
        /// 根据act初始调用对应的方法
        /// </summary>
        /// <param name="_act"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string Init(string _act, HttpRequest request) {
            Func<HttpRequest, string> actFunc = dicJsonRes.FirstOrDefault(dic => dic.Key == _act).Value;
            return actFunc(request);
        }



    }


}
