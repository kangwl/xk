using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Vinny.DBHelper.Core
{
    /// <summary>
    /// 生成sqlserver的where条件
    /// 并生成SqlParameter[]
    /// select * from [user] where name='kwl' and age=12 order by id desc
    /// </summary>
    public class WhereHelper {
        public WhereHelper() {
            whereList = new List<string>();
            sqlParameterList = new List<SqlParameter>();
        }

        private List<string> whereList { get; set; }

        /// <summary>
        /// 生成的where
        /// EXP:a=@a and b>@b
        /// </summary>
        public string Where {
            get { return string.Join(" and ", whereList); }
        }

        private List<SqlParameter> sqlParameterList { get; set; }

        /// <summary>
        /// 根据where生成的sql参数和对应的值
        /// </summary>
        public SqlParameter[] SqlParameters {
            get { return sqlParameterList.ToArray(); }
        }

        public void AddWhere(string key, string sign, dynamic value) {
            string where = string.Format("{0}{1}@{2}", key, sign, key);
            whereList.Add(where);
            sqlParameterList.Add(new SqlParameter(string.Format("@{0}", key), value));
        }
    }
}
