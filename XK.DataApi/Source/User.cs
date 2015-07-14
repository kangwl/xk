using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XK.DataApi.ApiInfo;

namespace XK.DataApi.Source {
    public class User {

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
                return "add";
            }

            public string GetList(HttpRequest request) {
                ApiException exception = new ApiException(2, "list err");
                string extjson = Common.json.JsonHelper<ApiException>.Serialize2Object(exception);
                return extjson;
            }

            public string Delete(HttpRequest request) {
                return "delete";
            }

            public string Update(HttpRequest request) {
                return "update";
            }
        }
    }


}
