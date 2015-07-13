using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Data;

namespace XK.Common
{
    public class Utils
    {
        /// <summary>
        /// DataTable的深度拷贝,把SrcTable中的内容复制到DesTable中
        /// </summary>
        /// <param name="SrcTable">源</param>
        /// <param name="DesTable">目的</param>
        /// <returns></returns>
        public static DataTable TableCopy(DataTable SrcTable, out DataTable DesTable)
        {
            DesTable = new DataTable();
            foreach (DataColumn dc in SrcTable.Columns)
            {
                DesTable.Columns.Add(dc.ColumnName, dc.DataType);
            }
            foreach (DataRow dr in SrcTable.Rows)
            {
                DataRow Newdr = DesTable.NewRow();
                foreach (DataColumn newdc in SrcTable.Columns)
                {
                    Newdr[newdc.ColumnName] = dr[newdc.ColumnName];
                }
                DesTable.Rows.Add(Newdr);
            }
            return DesTable;
        }



        /// <summary> 
        /// 根据Url获得源文件内容 
        /// </summary> 
        /// <param name="url">合法的Url地址</param> 
        /// <returns></returns> 
        public static string GetSourceTextByUrl(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 20000;//20秒超时 
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StringBuilder sb = new StringBuilder();
                byte[] buffer = new byte[1024];
                int length = 1;
                while (length > 0)
                {
                    length = resStream.Read(buffer, 0, buffer.Length);
                    sb.Append(GetStringByByte(buffer));
                }
                resStream.Close();
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把字节变成字符串
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string GetStringByByte(byte[] temp)
        {
            Encoding encoding = Encoding.GetEncoding("GB2312");
            return encoding.GetString(temp);
        }



        /// <summary>
        /// 把字符串变成字节
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static byte[] GetStringByByte(string temp)
        {
            Encoding encoding = Encoding.GetEncoding("GB2312");
            return encoding.GetBytes(temp);
        }

        /// <summary>
        /// 从图片地址下载图片到本地磁盘
        /// </summary>
        /// <param name="ToLocalPath">图片本地磁盘地址</param>
        /// <param name="Url">图片网址</param>
        /// <returns></returns>
        public static bool SavePhotoFromUrl(string FileName, string Url)
        {
            bool Value = false;
            WebResponse response = null;
            Stream stream = null;

            try
            {
                WebRequest request = WebRequest.Create(Url);

                response = request.GetResponse();
                stream = response.GetResponseStream();

                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    Value = SaveBinaryFile(response, FileName);

                }

            }
            catch (Exception err)
            {
                string aa = err.ToString();
            }
            return Value;
        }

        /// <summary>
        /// Save a binary file to disk.
        /// </summary>
        /// <param name="response">The response used to save the file</param>
        // 将二进制文件保存到磁盘
        public static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            bool Value = true;
            byte[] buffer = new byte[1024];

            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = System.IO.File.Create(FileName);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }



        /// <summary>
        /// 得到字符串的子字符串。其中一个汉字的长度为2。
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Substring(string temp, int len)
        {
            Encoding encoding = Encoding.GetEncoding("GB2312");
            byte[] bs = encoding.GetBytes(temp);
            string AddString = "";
            if (bs.Length < len)
            {
                len = bs.Length;
            }
            else
            {
                AddString = "...";
            }
            byte[] NewByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                NewByte[i] = bs[i];
            }

            string str = encoding.GetString(NewByte) + AddString;
            return str.Replace("?", "");

        }




        /// <summary>
        /// 获取QQ当前状态（1：在线，0：不在线，-1：不存在）
        /// </summary>
        /// <param name="qq">qq号</param>
        /// <returns></returns>
        //public static Community.Model.Enums.qqStatus GetQQState(long qq)
        //{
        //    WebClient client = new WebClient();
        //    byte[] data;
        //    try { data = client.DownloadData("http://wpa.qq.com/pa?p=1:" + qq.ToString() + ":1"); }
        //    catch { data = new byte[0]; }
        //    switch (data.Length)
        //    {
        //        case 2329:
        //            return Community.Model.Enums.qqStatus.Online;
        //        case 2262:
        //            return Community.Model.Enums.qqStatus.NotOnline;
        //        case 0:
        //            return Community.Model.Enums.qqStatus.NotHave;
        //        default:
        //            return Community.Model.Enums.qqStatus.NotHave;
        //    }
        //}


        /// <summary>
        /// 去除html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string wipescript(string html)
        {
            if ("" + html == "")
                return "";
            Regex regex1 = new Regex(@"<script[\s\s]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[\s\s]*script *:", RegexOptions.IgnoreCase);
            Regex regex6 = new Regex(@" src *= *[\s\s]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" on[\s\s]*=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[\s\s]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\s]+</frameset *>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<a>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            return html;
        }


        #region
        ///移除HTML标签
        /**/
        ///   <summary>
        ///   移除HTML标签
        ///   </summary>
        ///   <param   name="HTMLStr">HTMLStr</param>
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }

        #endregion

        /// <summary>
        /// 去除html
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ReplaceHTML(object strHtml1)
        {
            string strHtml = "" + strHtml1;

            //删除脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //strHtml = Regex.Replace(strHtml, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            strHtml.Replace("<", "");
            strHtml.Replace(">", "");
            //strHtml.Replace("\r\n", "");
            strHtml = System.Web.HttpContext.Current.Server.HtmlEncode(strHtml).Trim();
            return strHtml;
        }

        /// <summary>
        /// 去除html,截断字符串
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ReplaceHTML(object strHtml1, int len)
        {
            string strHtml = "" + strHtml1;

            //删除脚本
            strHtml = Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            strHtml = Regex.Replace(strHtml, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //strHtml = Regex.Replace(strHtml, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"-->", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"<!--.*", "", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            strHtml = Regex.Replace(strHtml, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            strHtml.Replace("<", "");
            strHtml.Replace(">", "");

            string[] temp = strHtml.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string s in temp)
            {
                if (s.Trim().Length == 0)
                    continue;
                sb = sb.Append(Substring(s, len).Trim() + "<br/>");
            }

            return sb.ToString().Trim();
        }

        /// <summary> 
        /// 替换html中的特殊字符 
        /// </summary> 
        /// <param name="theString">需要进行替换的文本。</param> 
        /// <returns>替换完的文本。</returns> 
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace(">", ">");
            theString = theString.Replace("<", "<");
            theString = theString.Replace("&", "&amp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "'");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        /// <summary> 
        /// 恢复html中的特殊字符 
        /// </summary> 
        /// <param name="theString">需要恢复的文本。</param> 
        /// <returns>恢复好的文本。</returns> 
        public static string HtmlDiscode(string theString)
        {
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace(">", ">");
            theString = theString.Replace("<", "<");
            theString = theString.Replace("&amp;", "&");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("'", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }


        /// <summary>
        /// 过滤不安全字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SqlSafe(string str)
        {
            if (str == null)
                return "";
            string[] temp = { "'", "|", "&", ";", "$", "%", "@", '"'.ToString(), "\'", "\"", "<>", "()", "+", "\r", "\n", ",", "\\" };
            foreach (string s in temp)
            {
                str = str.Replace(s, "");
            }
            return str;
        }

        //截取字符串
        public static string GetCut(string str, int count)
        {
            if (str.Length > count)
                return str.Substring(0, count) + "...";
            else
                return str;
        }
        /// <summary>
        /// object
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string CutStr(object strObj, int count)
        {
            if (strObj == null || string.IsNullOrEmpty(strObj.ToString()))
            {
                return "";
            }
            return GetCut(strObj.ToString(), count);
        }


    }
}
