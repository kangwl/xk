using System.Collections.Generic;
using System.Data.SqlClient;

namespace XK.DBUtil.Helper {
    /// <summary>
    /// insert helper
    /// </summary>
    public class InsertHelper {

        /// <summary>
        /// 初始化 数据插入实体
        /// </summary>
        /// <param name="table">数据库表名</param>
        public InsertHelper(string table) {
            Table = table;
        }

        private string Table { get; set; }

        private List<string> FieldNameList { get; set; }
        private List<string> FieldParamList { get; set; } 


        public void AddParam(string fieldName, dynamic fieldVal) {
            if (FieldNameList == null) {
                FieldNameList = new List<string>();
            }
            FieldNameList.Add(fieldName);
            if (FieldParamList == null) {
                FieldParamList = new List<string>();
            }

            FieldParamList.Add(string.Format("@{0}", fieldName));
            if (sqlParameterList == null) {
                sqlParameterList = new List<SqlParameter>();
            }
            sqlParameterList.Add(new SqlParameter(string.Format("@{0}", fieldName), fieldVal));
        }


        private List<SqlParameter> sqlParameterList { get; set; }

        public SqlParameter[] SqlParameters {
            get { return sqlParameterList.ToArray(); }
        }

        //insert into [user] (,,,)values (@l,@k,@n)
        public string Sql {
            get {
                const string sqlScm = "insert into {0} ({1}) values ({2})";
                string sql = string.Format(sqlScm, Table, string.Join(",", FieldNameList),
                    string.Join(",", FieldParamList));
                return sql;
            }
        }


    }
}
