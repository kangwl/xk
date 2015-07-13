using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XK.Common.excel
{
    public partial class ExcelHelper {
        /// <summary>
        /// Dictionary 方式
        /// key:field value:headerText
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="dicKV">key:field value:headerText</param>
        /// <param name="tableName"></param>
        public static void ExportToExcel(DataTable dataList, Dictionary<string, string> dicKV, string tableName) {
            var fields = dicKV.Keys.ToArray();
            var headTexts = dicKV.Values.ToArray();
            ExportToExcel(dataList, fields, headTexts, tableName);
        }
        /// <summary>
        /// list方式
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="fields"></param>
        /// <param name="headTexts"></param>
        /// <param name="tableName"></param>
        public static void ExportToExcel(DataTable dataList, List<string> fields, List<string> headTexts,
            string tableName) {
            ExportToExcel(dataList, fields.ToArray(), headTexts.ToArray(), tableName);
        }
        /// <summary>
        /// 数组方式
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="fields"></param>
        /// <param name="headTexts"></param>
        /// <param name="title"></param>
        /// <param name="TableName"></param>
        public static void ExportToExcel(DataTable dataList, string[] fields, string[] headTexts, string title, string TableName)
        {
            GridView gvw = new GridView();
            int ColCount, i;
            //如果筛选的字段和对应的列头名称个数相对的情况下只导出指定的字段
            if (fields.Length != 0 && fields.Length == headTexts.Length)
            {
                ColCount = fields.Length;
                gvw.AutoGenerateColumns = false;

                for (i = 0; i < ColCount; i++)
                {
                    BoundField bf = new BoundField();
                    bf.DataField = fields[i];
                    bf.HeaderText = headTexts[i];
                    gvw.Columns.Add(bf);
                }
            } else
            {
                gvw.AutoGenerateColumns = true;
            }
            gvw.DataSource = dataList;
            SetStype(gvw);
          //  gvw.Columns[]
            gvw.DataBind();
            ExportToExcel(gvw, title, TableName);
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="headTexts">字段对应显示的名称</param>
        /// <param name="TableName">导出Excel的名称</param>
        /// <param name="dataList"></param>
        /// <param name="fields">要导出的字段</param>
        public static void ExportToExcel(DataTable dataList, string[] fields, string[] headTexts, string TableName)
        {
            ExportToExcel(dataList, fields, headTexts, string.Empty, TableName);
        }


        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="gvw"></param>
        private static void SetStype(GridView gvw)
        {
            gvw.Font.Name = "Verdana";
            gvw.BorderStyle = BorderStyle.Solid;
            gvw.HeaderStyle.BackColor = System.Drawing.Color.LightCyan;
            gvw.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            gvw.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvw.HeaderStyle.Wrap = false;
            gvw.HeaderStyle.Font.Bold = true;
            gvw.HeaderStyle.Font.Size = 10;
            gvw.RowStyle.Font.Size = 10;
        }

        /// <summary>
        /// 导出GridView中的数据到Excel
        /// </summary>
        /// <param name="gvw"></param>
        /// <param name="title"></param>
        /// <param name="TableName"></param>
        private static void ExportToExcel(GridView gvw, string title, string TableName) {

            //int coun = ExistsRegedit();
            //string fileName = string.Format("DataInfo{0:yyyy-MM-dd_HH_mm}.xls", DateTime.Now);
            //if (coun >0)
            //{
            //    fileName = string.Format("DataInfo{0:yyyy-MM-dd_HH_mm}.xls", DateTime.Now);
            //    //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF7;
            //}
            //else
            //{
            //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //    //Page.RegisterStartupScript("mess", "<script>alert('该机器没有安装任何office软件');</script>");
            //    //return;
            //}

            for (int i = 0; i < gvw.Rows.Count; i++) {
                for (int j = 0; j < gvw.HeaderRow.Cells.Count; j++) {
                    //这里给指定的列编辑格式，将数字输出为文本，防止数字溢出  
                    gvw.Rows[i].Cells[j].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                    //nowrap="nowrap" 不换行
                    gvw.Rows[i].Cells[j].Attributes.Add("nowrap", "nowrap");

                }
            }
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            //fileName = string.Format("DataInfo{0:yyyy-MM-dd_HH_mm}.xls", DateTime.Now);
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(TableName) + HttpUtility.UrlEncode(title) + ".xls");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");


            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter tw = new StringWriter();
 
            HtmlTextWriter hw = new HtmlTextWriter(tw);
       
            gvw.RenderControl(hw);
            if (!string.IsNullOrEmpty(title)) {
                HttpContext.Current.Response.Write("<b><center><font size=3 face=Verdana color=#0000FF>" + title +
                                                   "</font></center></b>");
            }
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.Close();
            HttpContext.Current.Response.End();
            gvw.Dispose();
            tw.Dispose();
            hw.Dispose();
        }



    }
}
