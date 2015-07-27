using System.Data;
using System.Data.SqlClient;
using XK.DBUtil.Helper;

namespace XK.DBUtil {
    /// <summary>
    /// 执行DB操作
    /// </summary>
    public class DBExcute {

        protected static string connStr =
            System.Configuration.ConfigurationManager.ConnectionStrings["DBDefault"].ConnectionString;

        public static bool Insert(InsertHelper insertHelper) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = insertHelper.Sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(connStr);
                sqlCommand.Parameters.AddRange(insertHelper.SqlParameters);
                sqlCommand.Connection.Open();
                int ret = sqlCommand.ExecuteNonQuery();
                return ret > 0;
            }
        }

        public static bool Update(UpdateHelper updateHelper) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = updateHelper.Sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(connStr);
                sqlCommand.Parameters.AddRange(updateHelper.SqlParameters);
                sqlCommand.Connection.Open();
                int ret = sqlCommand.ExecuteNonQuery();
                return ret > 0;
            }
        }

        public static bool Delete(DeleteHelper deleteHelper) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = deleteHelper.Sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(connStr);
                sqlCommand.Parameters.AddRange(deleteHelper.SqlParameters);
                sqlCommand.Connection.Open();
                int ret = sqlCommand.ExecuteNonQuery();
                return ret > 0;
            }
        }

        public static DataTable GetDataTable(SelectHelper selectHelper) {
            DataTable dt = new DataTable();
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = selectHelper.Sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(connStr);
                sqlCommand.Parameters.AddRange(selectHelper.SqlParameters);
                sqlCommand.Connection.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand)) {
                    dataAdapter.Fill(dt);
                }
                return dt;
            }
        }

        public static SqlDataReader GetDataReader(SelectHelper selectHelper) {
            SqlDataReader dataReader;
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = selectHelper.Sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(connStr);
                sqlCommand.Parameters.AddRange(selectHelper.SqlParameters);
                sqlCommand.Connection.Open();
                dataReader = sqlCommand.ExecuteReader();
            }
            return dataReader;
        }

        public static int GetRecordCount(SelectHelper selectHelper) {
            using (SqlCommand sqlCommand = new SqlCommand()) {
                sqlCommand.CommandText = selectHelper.Sql;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = new SqlConnection(connStr);
                sqlCommand.Parameters.AddRange(selectHelper.SqlParameters);
                sqlCommand.Connection.Open();
                object obj = sqlCommand.ExecuteScalar();
                return obj.ToInt();
            }
        }


    }
}
