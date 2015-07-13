using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XK.Dal._Helper.UtilBase;

namespace XK.Dal._Helper {
    public class UpdateOP {
        /// <summary>
        /// 根据表名更新某条记录
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="dicFileVals">filed/val</param>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        public static bool Update(string table, Dictionary<string, dynamic> dicFileVals, string where) {
            StringBuilder sbK = new StringBuilder();
            SqlParameter[] parameters = new SqlParameter[dicFileVals.Count];
            int paramIndex = 0;
            foreach (KeyValuePair<string, dynamic> pair in dicFileVals) {
                sbK.AppendFormat("[{0}]=@{1},", pair.Key, pair.Key);
                parameters[paramIndex] = new SqlParameter(string.Format("@{0}", pair.Key), pair.Value);
                paramIndex += 1;
            }
            string sbKTrim = sbK.ToString().TrimEnd(',');
            string sql = string.Format(@"update [{0}] set {1} where {2}", table, sbKTrim, where);

            int retInt = DbHelperSQL.ExecuteSql(sql, parameters);
            return retInt > 0;
        }
 


    }
}
