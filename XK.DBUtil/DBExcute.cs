using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using XK.DBUtil.Helper;

namespace XK.DBUtil {
    /// <summary>
    /// 执行DB操作
    /// </summary>
    public class DBExcute {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static string connStr =
            System.Configuration.ConfigurationManager.ConnectionStrings["DBDefault"].ConnectionString;

        public static bool Insert(InsertHelper insertHelper) {
            using (SqlCommand sqlCmd = CreateSqlCmd(insertHelper.Sql, insertHelper.SqlParameters)) {
                int ret = sqlCmd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        /// <summary>
        /// 事务批量插入 
        /// 返回：已成功插入数据的当前索引
        /// 如果参数insertHelpers的数目和返回值相等，说明全部添加成功
        /// </summary>
        /// <param name="insertHelpers"></param>
        /// <returns>插入成功的个数</returns>
        public static int InsertTran(List<InsertHelper> insertHelpers) {
            //记录已成功插入数据的当前索引
            int successCount = 0;
            using (SqlCommand command = CreateSqlCmd()) {
                try {
                    //deal
                    command.Transaction = command.Connection.BeginTransaction();
                    foreach (InsertHelper insertHelper in insertHelpers) {
                        command.CommandText = insertHelper.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(insertHelper.SqlParameters);
                        bool success = command.ExecuteNonQuery() > 0;
                        if (!success) {
                            throw new Exception("数据插入失败");
                        }
                        successCount++;//记录当前成功的索引
                    }
                    command.Transaction.Commit();
                }
                catch (Exception) {
                    command.Transaction.Rollback();
                }
                return successCount;
            }
        }

        public static bool Update(UpdateHelper updateHelper) {
            using (SqlCommand sqlCmd = CreateSqlCmd(updateHelper.Sql, updateHelper.SqlParameters)) {
                int ret = sqlCmd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        /// <summary>
        /// 批量事务更新
        /// 返回：已成功处理的个数
        /// 如果参数 updateHelpers 的数目和返回值相等，说明全部更新成功
        /// </summary>
        /// <param name="updateHelpers">List UpdateHelper</param>
        /// <param name="showErrorIfNotExist">
        /// 默认false，即如果 ExecuteNonQuery 返回值是0（有可能待更新的目标数据不存在）不计入失败
        /// 如果true ，即如果 ExecuteNonQuery 返回值是0则计入失败
        /// </param>
        /// <returns></returns>
        public static int UpdateTran(List<UpdateHelper> updateHelpers, bool showErrorIfNotExist = false) {
            //记录已成功更新数据的当前索引
            int successCount = 0;
            using (SqlCommand command = CreateSqlCmd()) {
                try {
                    //deal
                    command.Transaction = command.Connection.BeginTransaction();
                    foreach (UpdateHelper updateHelper in updateHelpers) {
                        command.CommandText = updateHelper.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(updateHelper.SqlParameters);
                        bool success = command.ExecuteNonQuery() > 0;
                        if (showErrorIfNotExist) {
                            if (!success) {
                                throw new Exception("待更新的数据不存在");
                            }
                        }
                        successCount++; //记录当前成功的索引
                    }
                    command.Transaction.Commit();
                }
                catch (Exception) {
                    command.Transaction.Rollback();
                }
                return successCount;
            }
        }


        public static bool Delete(DeleteHelper deleteHelper) {
            using (SqlCommand sqlCmd = CreateSqlCmd(deleteHelper.Sql, deleteHelper.SqlParameters)) {
                int ret = sqlCmd.ExecuteNonQuery();
                return ret > 0;
            }
        }

        /// <summary>
        /// 批量事务删除
        /// 返回：已成功处理的个数
        /// 如果参数 deleteHelpers 和返回值个数相等，说明全部删除成功
        /// </summary>
        /// <param name="deleteHelpers"></param>
        /// <param name="showErrorIfNotExist">
        /// 默认false，即如果ExecuteNonQuery返回值是0（有可能待删除的目标数据不存在）不计入失败
        /// 如果true ，即如果ExecuteNonQuery返回值是0则计入失败
        /// </param>
        /// <returns></returns>
        public static int DeleteTran(List<DeleteHelper> deleteHelpers, bool showErrorIfNotExist = false) {
            //记录已成功删除数据的当前索引
            int successCount = 0;
            using (SqlCommand command = CreateSqlCmd()) {
                try {
                    //deal
                    command.Transaction = command.Connection.BeginTransaction();
                    foreach (DeleteHelper deleteHelper in deleteHelpers) {
                        command.CommandText = deleteHelper.Sql;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(deleteHelper.SqlParameters);
                        bool success = command.ExecuteNonQuery() > 0;
                        if (showErrorIfNotExist) {
                            if (!success) {
                                throw new Exception("待删除的数据不存在");
                            }
                        }
                        successCount++; //记录当前成功的索引
                    }
                    command.Transaction.Commit();
                }
                catch (Exception) {
                    //回滚操作
                    command.Transaction.Rollback();
                }
                return successCount;
            }
        }

        public static DataTable GetDataTable(SelectHelper selectHelper) {
            DataTable dt = new DataTable();
            using (SqlCommand sqlCmd = CreateSqlCmd(selectHelper.Sql, selectHelper.SqlParameters)) {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd)) {
                    dataAdapter.Fill(dt);
                }
                return dt;
            }
        }

        public static SqlDataReader GetDataReader(SelectHelper selectHelper) {
            SqlDataReader dataReader;
            using (SqlCommand sqlCmd = CreateSqlCmd(selectHelper.Sql, selectHelper.SqlParameters)) {
                dataReader = sqlCmd.ExecuteReader();
            }
            return dataReader;
        }

        public static int GetRecordCount(SelectHelper selectHelper) {
            using (SqlCommand sqlCmd = CreateSqlCmd(selectHelper.Sql, selectHelper.SqlParameters)) {
                object obj = sqlCmd.ExecuteScalar();
                return obj.ToInt();
            }
        }

        #region 基础方法

        private static SqlCommand CreateSqlCmd(string sqlText, SqlParameter[] sqlParameters) {
            SqlCommand sqlCmd = CreateSqlCmd();
            sqlCmd.CommandText = sqlText;
            sqlCmd.Parameters.AddRange(sqlParameters);
            return sqlCmd;
        }

        private static SqlCommand CreateSqlCmd() {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = new SqlConnection(connStr);
            sqlCmd.Connection.Open();

            return sqlCmd;
        }

        #endregion


    }
}
