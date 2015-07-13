using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace XK.Common.json {
    /// <summary>
    /// json生成
    /// 默认格式：{"data":{},"total":1555}
    /// </summary>
    public class JsonCreator {
        /// <summary>
        /// 默认json 名字
        /// </summary>
        private const string _defaultJsonName = "data";
        /// <summary>
        /// 默认总数量，用于分页
        /// </summary>
        private const string _defaultTotalName = "total";
        /// <summary>
        /// 默认时间格式
        /// </summary>
        private const string _dateTimeFormater = "yyyy-MM-dd HH:mm:ss";

        public JsonCreator() {
            JsonName = _defaultJsonName;
            TotalName = _defaultTotalName;
            SerializerSettings = CreateSettings();
        }
        
        public JsonCreator(string jsonName,string totalName) {
            JsonName = jsonName;
            TotalName = totalName;
            SerializerSettings = CreateSettings();
        }

        /// <summary>
        /// 生成 json
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public string Create<TData>(TData data, int total) {

            var settings = CreateSettings();
            //序列化数据=>json字符串
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            string initedJson = InitJson(jsonStr, total);

            return initedJson;
        }

        /// <summary>
        /// 包装整体的json
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private string InitJson(string jsonStr, int total) {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"{0}\":", JsonName);
            sb.Append(jsonStr);
            sb.Append(",");
            sb.AppendFormat("\"{0}\":", TotalName);
            sb.Append(total);
            sb.Append("}");

            string sb_jsonStr = sb.ToString();

            return sb_jsonStr;
        }
 
        /// <summary>
        /// 一些序列化设置
        /// </summary>
        /// <returns></returns>
        private JsonSerializerSettings CreateSettings() {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            //格式化时间
            settings.DateFormatString = _dateTimeFormater;
            return settings;
        }

        public JsonSerializerSettings SerializerSettings { get; set; }
 
        /// <summary>
        /// json名字 默认 data
        /// </summary>
        public string JsonName { get; set; }
        /// <summary>
        /// 总数据量的名字 默认 total 
        /// 用于分页
        /// </summary>
        public string TotalName { get; set; }
    }


}
