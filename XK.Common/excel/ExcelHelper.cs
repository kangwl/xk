using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;

namespace XK.Common.excel {
    public partial class ExcelHelper {
        /// <summary>
        /// 将整个excel文档导入数据库
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="dbConnStr"></param>
        /// <param name="tableName"></param>
        /// <param name="dicMap">key:excel中的列名，value:数据库表中的列名</param>
        public static void InsertDBFromExcelData(string excelFile, string dbConnStr, string tableName,
            Dictionary<string, string> dicMap) {

            List<SqlBulkCopyColumnMapping> columnMappings = new List<SqlBulkCopyColumnMapping>();
            foreach (KeyValuePair<string, string> pair in dicMap) {
                columnMappings.Add(new SqlBulkCopyColumnMapping(pair.Key, pair.Value));
            }
            InsertDBFromExcelData(excelFile, dbConnStr, tableName, columnMappings.ToArray());
        }

        /// <summary>
        /// excel数据到数据库
        /// </summary>
        /// <param name="excelFile">excel文件全路径</param>
        /// <param name="dbConnStr">数据库连接字符串</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="mappings">列映射</param>
        private static void InsertDBFromExcelData(string excelFile, string dbConnStr, string tableName,
            SqlBulkCopyColumnMapping[] mappings) {

            string connStr = GetExcelConnStr(excelFile);
            OleDbConnection cnnxls = new OleDbConnection(connStr);

            var allSheetNames = GetAllWorkSheets(excelFile);
            foreach (string sheetName in allSheetNames) {

                OleDbDataAdapter myDa = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheetName.Replace("$", "")), cnnxls);
                DataTable dt = new DataTable();
                myDa.Fill(dt);
                if (dt.Rows.Count > 0) {
                    using (SqlBulkCopy copy = new SqlBulkCopy(dbConnStr)) //与目标服务器连接
                    {
                        copy.BulkCopyTimeout = 5000;
                        copy.DestinationTableName = tableName; //导入到数据库的表名
                        if (mappings != null) {
                            foreach (var item in mappings) {
                                copy.ColumnMappings.Add(item);
                            }
                        }
                        copy.WriteToServer(dt);
                    }
                }
            }
        }
        /// <summary>
        /// 将指定的excel的SheetName写入数据库
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="sheetName"></param>
        /// <param name="dicMap"></param>
        /// <param name="connStr"></param>
        /// <param name="toTableName"></param>
        public static void InsertDBFromSheetName(string excelFile,string sheetName,Dictionary<string, string> dicMap,string connStr,string toTableName) {
            DataTable excelDT = ExcelDataSource(excelFile, sheetName);
            SqlBulkCopyHelper copyHelper = new SqlBulkCopyHelper(connStr, toTableName, dicMap);
            copyHelper.StartWrite(excelDT);
        }

        /// <summary>
        /// 根据sheetname获取数据
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="sheetname"></param>
        /// <returns></returns>
        public static DataTable ExcelDataSource(string excelFile, string sheetname = "Sheet1") {
            try {

                string strConn = GetExcelConnStr(excelFile);
                if (sheetname.Trim().LastIndexOf('$') == -1) {
                    sheetname = sheetname + "$";
                }
                using (OleDbConnection conn = new OleDbConnection(strConn)) {
                    using (OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + sheetname + "]", strConn)) {
                        DataTable dt = new DataTable();
                        oada.Fill(dt);
                        return dt;
                    }
                }

            }
            catch (Exception) {
                return null;
            }
        }

        /// <summary>
        /// 得到Execl中所有的工作表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllWorkSheets(string excelFile) {
            string conn = GetExcelConnStr(excelFile);
            List<string> WorkSheets = new List<string>();
            using (OleDbConnection oledbconnection = new OleDbConnection(conn)) {
                oledbconnection.Open();
                DataTable dt = oledbconnection.GetSchema("Tables");
                foreach (DataRow dr in dt.Rows) {
                    WorkSheets.Add(dr["TABLE_NAME"].ToString().Trim());
                }
            }
            return WorkSheets;
        }

        /// <summary>
        /// 根据excel文件全路径获取excel的连接字符串
        /// </summary>
        /// <param name="excelFile"></param>
        /// <returns></returns>
        public static string GetExcelConnStr(string excelFile) {

            string strConn =
                string.Format(
                    "Provider = Microsoft.Ace.OleDb.12.0 ; Data Source = '{0}';Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'",
                    excelFile);

            //if (ExistsRegedit() == 1) {
            //    strConn =
            //        string.Format(
            //            "Provider=Microsoft.Jet.Oledb.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'",
            //            excelFile);
            //}

            return strConn;
        }

        //检查office的版本
        public static int ExistsRegedit() {
            int ifused = 0;
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey akey = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Excel\InstallRoot\"); //查询2003
            RegistryKey akey07 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Excel\InstallRoot\"); //查询2007
            RegistryKey akeytwo = rk.OpenSubKey(@"SOFTWARE\Kingsoft\Office\6.0\common\"); //查询wps
            //检查本机是否安装Office2003
            if (akey != null) {
                string file03 = akey.GetValue("Path").ToString();
                if (File.Exists(file03 + "Excel.exe")) {
                    ifused = 1;
                }
            }

            //检查本机是否安装Office2007

            //if (akey07 != null)
            //{
            //    string file07 = akey.GetValue("Path").ToString();
            //    if (File.Exists(file07 + "Excel.exe"))
            //    {
            //        ifused += 2;
            //    }
            //}
            ////检查本机是否安装wps
            //if (akeytwo != null)
            //{
            //    string filewps = akeytwo.GetValue("InstallRoot").ToString();
            //    if (File.Exists(filewps + @"\office6\et.exe"))
            //    {
            //        ifused += 4;
            //    }
            //}
            return ifused;
        }
    }
}
