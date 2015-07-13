using System;
using System.Data;
using System.Data.SqlClient;
using XK.Dal._Helper.UtilBase;

namespace XK.Dal._Helper {
    public class SelectOP {
        /// <summary>
        /// 获取table数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="top">前几条</param>
        /// <param name="fields">要查询的字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>返回 DataTable</returns>
        public static DataTable QueryTable(string table, int top, string fields = "*", string where = "1=1") {
            string selectSql = string.Format("select top {3} {0} from [{1}] where {2}", fields, table, where, top);

            DataTable dt = DbHelperSQL.QueryDataTable(selectSql);
            return dt;
        }

         

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="top">前几条</param>
        /// <param name="fields">要查询的条件</param>
        /// <param name="where">查询条件</param>
        /// <returns>返回 SqlDataReader</returns>
        public static SqlDataReader QueryReader(string table, int top, string fields = "*", string where = "1=1") {
            string selectSql = string.Format("select top {3} {0} from [{1}] where {2}", fields, table, where, top);

            SqlDataReader reader = DbHelperSQL.ExecuteReader(selectSql);
            return reader;
        }
        /// <summary>
        /// 查询数据 SqlDataReader 分页
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页显示的数量</param>
        /// <param name="orderby">排序</param>
        /// <returns>返回 SqlDataReader</returns>
        public static SqlDataReader QueryReaderPager(string tableName, string @where, int pageIndex, int pageSize,
            string @orderby) {
            if (string.IsNullOrEmpty(@where)) {
                @where = "1=1";
            }
            string sqlBase = string.Format("select {0} from [{1}] where {2}", "*", tableName, @where);
            const string sqlPage = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

            int startPageIndex = (pageIndex - 1)*pageSize + 1;
            int endPageIndex = pageIndex*pageSize;
            string sql = string.Format(sqlPage, sqlBase, orderby, startPageIndex, endPageIndex);

            SqlDataReader reader = DbHelperSQL.ExecuteReader(sql);
            return reader;
        }
        /// <summary>
        /// 查询数据 DataTable 分页
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页显示的数量</param>
        /// <param name="orderby">排序</param>
        /// <returns>返回 DataTable</returns>
        public static DataTable QueryTablePager(string tableName, string @where, int pageIndex, int pageSize,
            string @orderby) {
            if (string.IsNullOrEmpty(@where)) {
                @where = "1=1";
            }
            string sqlBase = string.Format("select {0} from [{1}] where {2}", "*", tableName, @where);
            const string sqlPage = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

            int startPageIndex = (pageIndex - 1) * pageSize + 1;
            int endPageIndex = pageIndex * pageSize;
            string sql = string.Format(sqlPage, sqlBase, orderby, startPageIndex, endPageIndex);

            DataTable table = DbHelperSQL.QueryDataTable(sql);
            return table;
        }

        public static int GetTotalCount(string tableName, string @where="1=1") {
            string sqlCount = string.Format("select count(1) from [{0}] where {1}", tableName, where);
            int count = Convert.ToInt32(DbHelperSQL.GetSingle(sqlCount));
            return count;
        }

    }
}
