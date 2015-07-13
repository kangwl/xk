using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.IO;

namespace XK.Common
{
    public class UploadFileParam
    {
        /// <summary>
        /// 总目录
        /// </summary>
        public const string FileDir = "Files\\";
        /// <summary>
        /// 新闻附件文件夹
        /// </summary>
        private const string NewsAttachmentDir = "NewsAttachments\\";
        /// <summary>
        /// 文件资料附件文件夹
        /// </summary>
        private const string DocumentAttachmentDir = "DocumentAttachments\\";
        /// <summary>
        /// 学院概况附件文件夹
        /// </summary>
        private const string SchoolAttachmentDir = "SchoolAttachments\\";
        /// <summary>
        /// 培训园地附件文件夹
        /// </summary>
        private const string GardenAttachmentDir = "GardenAttachments\\";
        /// <summary>
        /// 教师头像文件夹
        /// </summary>
        private const string TeacherImgDir = "TeacherImg\\";
        /// <summary>
        /// 培训班规划文件文件夹
        /// </summary>
        public const string TrainPlanDir = "TrainPlanFiles\\";
        /// <summary>
        /// 培训班文件文件夹
        /// </summary>
        public const string TrainClassDir = "TrainClassFiles\\";
        /// <summary>
        /// 课程频道图片文件夹名
        /// </summary>
        private const string CourseChannelImgDir = "CourseChannelImg\\";
        /// <summary>
        /// 课程图片文件夹名
        /// </summary>
        public const string CourseImgDir = "CourseImg\\";
        /// <summary>
        /// 培训班资料文件夹
        /// </summary>
        private const string TCDocDir = "TCDoc\\";

        /// <summary>
        /// 学术通知文件 2015.3.25
        /// </summary>
        public const string AcaNoticeFile = "AcaNoticeFile\\";
        /// <summary>
        /// 学术通知的辅助文件 AcaExtFile
        /// </summary>
        public const string AcaExtFile = "AcaExtFile\\";
        /// <summary>
        /// 学术通知文件的扩展 2015.3.25
        /// </summary>
        public static readonly List<string> AcaNoticeFile_Extension = new List<string> {
            ".doc",".docx",".xls",".xlsx",".ppt",".pptx",".pdf",".txt"
        };

        /// <summary>
        /// 学术论文文件
        /// </summary>
        public const string AcaArticleFile = "AcaArticleFile\\";
        /// <summary>
        /// 允许上传的学术论文类型
        /// </summary>
        public static readonly List<string> AcaArticleFile_Extension=new List<string> {
            ".doc",".docx"
        };



        /// <summary>
        /// 新闻附件允许格式
        /// </summary>
        public static readonly List<string> NewsAttachment_Extension = new List<string> {
            ".doc",".docx",".xls",".xlsx",".ppt",".pptx",".pdf",".txt"
        };
        /// <summary>
        /// 文件资料附件允许格式
        /// </summary>
        public static readonly List<string> DocumentAttachment_Extension = new List<string> {
            ".doc",".docx",".xls",".xlsx",".ppt",".pptx",".pdf",".txt"
        };
        /// <summary>
        /// 学院概况附件允许格式
        /// </summary>
        public static readonly List<string> SchoolAttachment_Extension = new List<string> {
            ".doc",".docx",".xls",".xlsx",".ppt",".pptx",".pdf",".txt"
        };

        /// <summary>
        /// 培训园地附件允许格式
        /// </summary>
        public static readonly List<string> GardenAttachment_Extension = new List<string> {
            ".doc",".docx",".xls",".xlsx",".ppt",".pptx",".pdf",".txt"
        };
        /// <summary>
        /// 允许的培训规划文件格式列表
        /// </summary>
        public static readonly List<string> TrainPlanFile_Extension = new List<string> {
            ".doc",".docx",".xls",".xlsx",".ppt",".pptx",".pdf",".txt",".zip",".rar"
        };
        /// <summary>
        /// 获取系统基目录
        /// </summary>
        public static string FileSaveBasePath
        {
            get
            {
                var basePath = AppDomain.CurrentDomain.BaseDirectory + FileDir;
                return basePath;
            }
        }
        /// <summary>
        /// 上传的新闻附件的地址
        /// </summary>
        public static string NewsAttachmentFilePath
        {
            get { return FileSaveBasePath + NewsAttachmentDir; }
        }
        /// <summary>
        /// 上传的文件资料附件的地址
        /// </summary>
        public static string DocumentAttachmentFilePath
        {
            get { return FileSaveBasePath + DocumentAttachmentDir; }
        }
        /// <summary>
        /// 上传的学院概况附件的地址
        /// </summary>
        public static string SchoolAttachmentFilePath
        {
            get { return FileSaveBasePath + SchoolAttachmentDir; }
        }
        /// <summary>
        /// 上传的培训园地附件地址
        /// </summary>
        public static string GardenAttachmentFilePath
        {
            get { return FileSaveBasePath + GardenAttachmentDir; }
        }
        /// <summary>
        /// 教师图片目录
        /// </summary>
        public static string TeacherImgPath
        {
            get { return FileSaveBasePath + TeacherImgDir; }
        }
        /// <summary>
        /// 上传的培训规划文件的地址
        /// </summary>
        public static string TrainPlanFilePath
        {
            get { return FileSaveBasePath + TrainPlanDir; }
        }
        /// <summary>
        /// 上传的培训班文件的地址
        /// </summary>
        public static string TrainClassFilePath {
            get { return FileSaveBasePath + TrainClassDir; }
        }
        /// <summary>
        /// 课程频道图片目录
        /// </summary>
        public static string CourseChannelImgPath
        {
            get { return FileSaveBasePath + CourseChannelImgDir; }
        }
        /// <summary>
        /// 课程图片目录
        /// </summary>
        public static string CourseImgPath
        {
            get { return FileSaveBasePath + CourseImgDir; }
        }
        /// <summary>
        /// 培训班资料目录
        /// </summary>
        public static string TCDocPath
        {
            get { return FileSaveBasePath + TCDocDir; }
        }

        /// <summary>
        /// 将Web站点下的绝对路径转换为虚拟路径
        /// 注：非Web站点下的则不转换
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="specifiedPath">绝对路径</param>
        /// <returns>虚拟路径, 型如: ~/</returns>
        public static string ConvertSpecifiedPathToRelativePath(string specifiedPath)
        {
            string virtualPath = HttpContext.Current.Request.ApplicationPath;
            string pathRooted = HostingEnvironment.MapPath(virtualPath);
            if (!Path.IsPathRooted(specifiedPath) || specifiedPath.IndexOf(pathRooted) == -1)
            {
                return specifiedPath;
            }
            if (pathRooted.Substring(pathRooted.Length - 1, 1) == "//")
            {
                specifiedPath = specifiedPath.Replace(pathRooted, "~/");
            }
            else
            {
                specifiedPath = specifiedPath.Replace(pathRooted, "/");
            }
            string relativePath = specifiedPath.Replace("//", "/").Replace("\\", "/");
            return relativePath;
        }
    }
}
