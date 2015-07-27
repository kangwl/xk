using System.Collections.Generic;
using System.Data.SqlClient; 

namespace XK.DBUtil.Helper {

    public class UpdateHelper {

        private const string sql = "update {0} set {1} where {2}";

        private string Table { get; set; }
        private List<string> FieldList { get; set; }

        private WhereHelper WhereHelper { get; set; }
        private List<SqlParameter> sqlParameterList { get; set; }


        public UpdateHelper(string table, WhereHelper whereHelper) {
            Table = table;
            this.WhereHelper = whereHelper;
        }

        public UpdateHelper(string table, WhereHelper whereHelper, Dictionary<string, dynamic> dicKV)
            : this(table, whereHelper) {
            AddUpdateItem(dicKV);
        }

        public string Sql {
            get { return string.Format(sql, Table, string.Join(",", FieldList), WhereHelper.Where); }
        }

        public SqlParameter[] SqlParameters {
            get { 
                sqlParameterList.AddRange(WhereHelper.SqlParameters);
                return sqlParameterList.ToArray();
            }
        }

        public void AddUpdateItem(Dictionary<string, dynamic> dicKV) {
            if (FieldList == null) {
                FieldList = new List<string>();
            }
            foreach (KeyValuePair<string, dynamic> pair in dicKV) {
                FieldList.Add(string.Format("{0}=@{0}", pair.Key));

                if (sqlParameterList == null) {
                    sqlParameterList = new List<SqlParameter>();
                }

                sqlParameterList.Add(new SqlParameter(string.Format("@{0}", pair.Key), pair.Value));
            }
        }

        public void AddUpdateItem(string field,string value) {
            if (FieldList == null) {
                FieldList = new List<string>();
            }
            FieldList.Add(string.Format("{0}=@{0}", field));

            if (sqlParameterList == null) {
                sqlParameterList = new List<SqlParameter>();
            }

            sqlParameterList.Add(new SqlParameter(string.Format("@{0}", field), value));

        }

    }
}
