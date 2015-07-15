using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace XK.DataApi {
    /// <summary>
    /// api总入口
    /// 由网站ApiModule调用
    /// </summary>
    public class Enter {

        //根据Source文件夹下
        private readonly Dictionary<string, Func<string,HttpRequest, string>> dicReq =
            new Dictionary<string, Func<string,HttpRequest, string>> {
                {"user",Source.User.Init},
                {"news",Source.News.Init}
            };
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="source">要操作的对象</param>
        /// <param name="act">要操作对象的方法</param>
        /// <param name="request">HttpRequest</param>
        /// <returns></returns>
        public string Init(string source, string act, HttpRequest request) {
            Func<string, HttpRequest,string> sourceCall = dicReq.FirstOrDefault(dic => dic.Key == source).Value;
            string json = sourceCall(act,request);
            return json;
        }


    }
}
