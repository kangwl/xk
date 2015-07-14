using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace XK.DataApi {
    public class Enter {
    
        private readonly Dictionary<string, Func<string,HttpRequest, string>> dicReq =
            new Dictionary<string, Func<string,HttpRequest, string>> {
                {"user",Source.User.Init},
                {"news",Source.News.Init}
            };

        public string Init(string source, string act, HttpRequest request) {
            Func<string, HttpRequest,string> sourceCall = dicReq.FirstOrDefault(dic => dic.Key == source).Value;
            string json = sourceCall(act,request);
            return json;
        }


    }
}
