using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using XK.DataApi.Logic;

namespace XK.DataApi.Source {
    public class News {
        private static Act act;

        public static Act ActMethod
        {
            get { return act ?? (act = new Act()); }
        }

        public static string Init(string _act, HttpRequest request) {
            Func<HttpRequest, string> actFunc = dicJsonRes.FirstOrDefault(dic => dic.Key == _act).Value;
            return actFunc(request);
        }

        private static readonly Dictionary<string, Func<HttpRequest, string>> dicJsonRes =
            new Dictionary<string, Func<HttpRequest, string>> {
                {"add", ActMethod.Add},
                {"list", ActMethod.GetList}
            };

        public class Act {
            public string Add(HttpRequest request) {
               ApiInfo info=new ApiInfo();
                return "news add";
            }

            public string GetList(HttpRequest request) {
                ApiInfo exception = new ApiInfo(2, "news list err");
                string extjson = Common.json.JsonHelper<ApiInfo>.Serialize2Object(exception);
                return extjson;
            }

            public string Delete(HttpRequest request) {
                return "news delete";
            }

            public string Update(HttpRequest request) {
                return "news update";
            }
        }
    }

}

