using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XK.Common.help {
    /// <summary>
    /// 帮助生成where查询条件
    /// 查询全部：new WhereItem("1", "=", 1)
    /// </summary>
    public class WhereItem {
        /// <summary>
        /// 查询全部：new WhereItem("1", "=", 1)
        /// </summary>
        public WhereItem(){}
        /// <summary>
        /// 查询全部：new WhereItem("1", "=", 1)
        /// </summary>
        /// <param name="field">对应数据库字段</param>
        /// <param name="sign">连接field和value之间的符号</param>
        /// <param name="value">对应field的值</param>
        public WhereItem(string field, string sign, dynamic value) {
            Field = field;
            Sign = sign;
            Value = value;
        }

        public string Field { get; set; }
        public string Sign { get; set; }
        public dynamic Value { get; set; }
    }
}
