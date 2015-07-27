using System.Data.SqlClient; 

namespace XK.DBUtil.Helper {
    public class SelectHelper {
        private const string sql = "select {0} from {1} where {2}";
        private const string sqlTop = "select top {0} {1} from {2} where {3}";
        private const string sqlTopOrderBy = "select top {0} {1} from {2} where {3} order by {4}";
        private const string sqlOrderBy = "select {0} from {1} where {2} order by {3}";

        public SelectHelper(string table, string fields, WhereHelper whereHelper) {
            Sql = string.Format(sql, fields, table, whereHelper.Where);
            WhereHelper = whereHelper;
        }


        public SelectHelper(string table, int top, string fields, WhereHelper whereHelper) {
            Sql = string.Format(sqlTop, top, fields, table, whereHelper.Where);
            WhereHelper = whereHelper;
        }

        public SelectHelper(string table, string fields, WhereHelper whereHelper, string orderBy) {

            Sql = string.Format(sqlOrderBy, fields, table, whereHelper.Where, orderBy);
            WhereHelper = whereHelper;
        }

        public SelectHelper(string table, int top, string fields, WhereHelper whereHelper,string orderBy) {
            Sql = string.Format(sqlTopOrderBy, top, fields, table, whereHelper.Where, orderBy);
            WhereHelper = whereHelper;
        }


        public SelectHelper(string table, string fields, WhereHelper whereHelper, int pageIndex, int pageSize,
            string orderBy) {
            string sqlBase = string.Format("select {0} from {1} where {2} ", fields, table, whereHelper.Where);
            const string sqlPageBase = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

            int startPageIndex = (pageIndex - 1)*pageSize + 1;
            int endPageIndex = pageIndex*pageSize;
            Sql = string.Format(sqlPageBase, sqlBase, orderBy, startPageIndex, endPageIndex);
            WhereHelper = whereHelper;
        }

        private WhereHelper WhereHelper { get; set; }
        public string Sql { get; set; }

        public SqlParameter[] SqlParameters {
            get { return WhereHelper.SqlParameters; }
        }

    }
}
