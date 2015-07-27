using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.Common.help {
    /// <summary>
    /// 帮助生成where查询条件
    /// </summary>
    public class Where {
        public Where(){}
        public Where(string field, string sign, dynamic value) {
            Field = field;
            Sign = sign;
            Value = value;
        }

        public string Field { get; set; }
        public string Sign { get; set; }
        public dynamic Value { get; set; }
    }
}
