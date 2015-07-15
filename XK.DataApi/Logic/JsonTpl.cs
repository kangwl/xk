using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.DataApi.Logic {
    /// <summary>
    /// 输出的json模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonTpl<T> {
        public ApiInfo info { get; set; } 
        public T data { get; set; }
    }


}
