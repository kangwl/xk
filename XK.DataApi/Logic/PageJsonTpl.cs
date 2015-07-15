using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.DataApi.Logic {
    /// <summary>
    /// 输出带分页的json模板
    /// </summary>
    public class PageJsonTpl<T> : JsonTpl<T> {
        public int total { get; set; }
        public int pageindex { get; set; }
    }
}
