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
        /// <summary>
        /// 初始化 数据插入实体
        /// </summary>
        /// <param name="table">数据库表名</param>
        /// <param name="dicKV">数据库字段和其对应的值</param>
        public InsertHelper(string table, Dictionary<string, dynamic> dicKV) : this(table) {
            foreach (KeyValuePair<string, dynamic> pair in dicKV) {
                AddParam(pair.Key, pair.Value);
            }
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
