using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using XK.DataApi.Logic;

namespace XK.DataApi.Source.Act {
    /// <summary>
    /// 操作用户的一些方法
    /// </summary>
    public class UserAct : IAct {

        public string Add(HttpRequest request) {

            Logic.JsonTpl<string> jsonTemplate = new JsonTpl<string>();
            jsonTemplate.info = new ApiInfo(-11, "添加失败");
            jsonTemplate.data = "";
            bool addSucess = true;
            if (addSucess) {
                jsonTemplate.info = new ApiInfo(1, "添加成功");
            }
            string jsonResult = Common.json.JsonHelper<JsonTpl<string>>.Serialize2Object(jsonTemplate);
            return jsonResult;
        }

        public string List(HttpRequest request) {
            Logic.JsonTpl<List<Logic.Model.User>> json = new JsonTpl<List<Logic.Model.User>>();
            json.info = new ApiInfo(1, "操作成功");
            json.data = new List<Logic.Model.User>() {
                new Logic.Model.User() {ID = 1, Name = "k1", Email = "kangwl2009@163.com", Sex = "男"},
                new Logic.Model.User() {ID = 2, Name = "k2", Email = "kangwl2009@163.com", Sex = "男"}
            };
            string extjson = Common.json.JsonHelper<JsonTpl<List<Logic.Model.User>>>.Serialize2Object(json);
            return extjson;
        }

        public string Delete(HttpRequest request) {
            return "delete";
        }

        public string Update(HttpRequest request) {
            return "update";
        }

        public string GetOne(HttpRequest request) {
            return "getone";
        }
    }
}
