using System.Data.SqlClient; 

namespace XK.DBUtil.Helper {
    public class DeleteHelper {

        //delete from [users] where id=2
        private const string sqlScm = "delete from {0} where {1}";

        public DeleteHelper(string table, WhereHelper whereHelper) {
            Table = table;
            this.WhereHelper = whereHelper;
        }

        private string Table { get; set; }

        private WhereHelper WhereHelper { get; set; }

        public string Sql {
            get {
                string sql = string.Format(sqlScm, Table, WhereHelper.Where);
                return sql;
            }
        }

        public SqlParameter[] SqlParameters {
            get { return WhereHelper.SqlParameters; }
        }

    }
}
