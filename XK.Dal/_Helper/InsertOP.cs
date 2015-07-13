using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XK.Dal._Helper.UtilBase;

namespace XK.Dal._Helper {
    public class InsertOP {
        
        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="dicFileVals">field/val</param>
        /// <returns>true 成功/false 失败</returns>
        public static bool Insert(string table, Dictionary<string, dynamic> dicFileVals) {
            StringBuilder sbK = new StringBuilder();
            StringBuilder sbValParam = new StringBuilder();
            SqlParameter[] parameters = new SqlParameter[dicFileVals.Count];
            int paramIndex = 0;
            foreach (KeyValuePair<string, dynamic> pair in dicFileVals) {
                sbK.AppendFormat("[{0}],", pair.Key);
                sbValParam.AppendFormat("@{0},", pair.Key);
                parameters[paramIndex] = new SqlParameter(string.Format("@{0}", pair.Key), pair.Value);
                paramIndex += 1;
            }
            string sbKTrim = sbK.ToString().TrimEnd(',');
            string sbVTrim = sbValParam.ToString().TrimEnd(',');

            string sql = string.Format("insert into [{0}] ({1}) values ({2})", table, sbKTrim, sbVTrim);
            int retInt = DbHelperSQL.ExecuteSql(sql, parameters);
            return retInt > 0;
        }

   
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="listDicFileVals">字段及其对应值的列表</param>
        /// <returns>返回listErr，记录发生错误的索引；成功则不记录。</returns>
        public static List<int> InsertBatch(string table, List<Dictionary<string, dynamic>> listDicFileVals) {
            List<int> listErr = new List<int>();//用于记录添加失败的索引
            int index = 0;
            foreach (Dictionary<string, dynamic> dicFileVals in listDicFileVals) {
                StringBuilder sbK = new StringBuilder();
                StringBuilder sbValParam = new StringBuilder();
                SqlParameter[] parameters = new SqlParameter[dicFileVals.Count];
                int paramIndex = 0;
                foreach (KeyValuePair<string, dynamic> pair in dicFileVals) {
                    sbK.AppendFormat("[{0}],", pair.Key);
                    sbValParam.AppendFormat("@{0},", pair.Key);
                    parameters[paramIndex] = new SqlParameter(string.Format("@{0}", pair.Key), pair.Value);
                    paramIndex += 1;
                }
                string sbKTrim = sbK.ToString().TrimEnd(',');
                string sbVTrim = sbValParam.ToString().TrimEnd(',');

                string sql = string.Format("insert into [{0}] ({1}) values ({2})", table, sbKTrim, sbVTrim);
                int retInt = DbHelperSQL.ExecuteSql(sql, parameters);
                bool success = retInt > 0;
                if (!success) {
                    listErr.Add(index);
                }
                index += 1;
            }
            return listErr;
        }
    }
}
