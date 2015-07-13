using System;
using System.Collections;
using System.IO;
using System.Threading;

namespace XK.Common {
   public class FileHelper {
       /// <summary>
       /// 写日志文件
       /// </summary>
       /// <param name="sPath">    年月  例  2011-04</param>
       /// <param name="sFileName">月日  例  04-22</param>
       /// <param name="content">时间+  内容</param>
       /// <returns></returns>
       public static bool WriteLog(string sPath, string sFileName, string content) {
           try {
               StreamWriter sr;
               if (!Directory.Exists(sPath)) {
                   Directory.CreateDirectory(sPath);
               }
               string v_filename = sPath + "\\" + sFileName;


               if (!File.Exists(v_filename)) //如果文件存在,则创建File.AppendText对象
               {
                   sr = File.CreateText(v_filename);
                   sr.Close();
               }
               using (
                   FileStream fs = new FileStream(v_filename, FileMode.Append, FileAccess.Write,
                       FileShare.Write)) {
                   using (sr = new StreamWriter(fs)) {

                       sr.WriteLine(DateTime.Now.ToString("hh:mm:ss") + "     " + content);
                       sr.Close();
                   }
                   fs.Close();
               }
               return true;

           }
           catch {
               return false;
           }
       }


       /// <summary>
       /// 读取文本文件内容,每行存入arrayList 并返回arrayList对象
       /// </summary>
       /// <param name="sFileName"></param>
       /// <returns>arrayList</returns>
       public static ArrayList ReadFileRow(string sFileName) {
           ArrayList alTxt = null;
           try {
               using (StreamReader sr = new StreamReader(sFileName)) {
                   alTxt = new ArrayList();

                   while (!sr.EndOfStream) {
                       string sLine = sr.ReadLine();
                       if (sLine != "") {
                           if (sLine != null) alTxt.Add(sLine.Trim());
                       }
                   }
                   sr.Close();
               }
           }
           catch (Exception) {
           }
           return alTxt;
       }


       /// <summary>
       /// 备份文件
       /// </summary>
       /// <param name="sourceFileName">源文件名</param>
       /// <param name="destFileName">目标文件名</param>
       /// <param name="overwrite">当目标文件存在时是否覆盖</param>
       /// <returns>操作是否成功</returns>
       public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite) {
           if (!File.Exists(sourceFileName))
               throw new FileNotFoundException(sourceFileName + "文件不存在！");

           if (!overwrite && File.Exists(destFileName))
               return false;

           try {
               File.Copy(sourceFileName, destFileName, true);
               return true;
           }
           catch (Exception e) {
               throw e;
           }
       }


       /// <summary>
       /// 备份文件,当目标文件存在时覆盖
       /// </summary>
       /// <param name="sourceFileName">源文件名</param>
       /// <param name="destFileName">目标文件名</param>
       /// <returns>操作是否成功</returns>
       public static bool BackupFile(string sourceFileName, string destFileName) {
           return BackupFile(sourceFileName, destFileName, true);
       }


       /// <summary>
       /// 恢复文件
       /// </summary>
       /// <param name="backupFileName">备份文件名</param>
       /// <param name="targetFileName">要恢复的文件名</param>
       /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
       /// <returns>操作是否成功</returns>
       public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName) {
           try {
               if (!File.Exists(backupFileName))
                   throw new FileNotFoundException(backupFileName + "文件不存在！");

               if (backupTargetFileName != null) {
                   if (!File.Exists(targetFileName))
                       throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                   File.Copy(targetFileName, backupTargetFileName, true);
               }
               File.Delete(targetFileName);
               File.Copy(backupFileName, targetFileName);
           }
           catch (Exception e) {
               throw e;
           }
           return true;
       }

       public static bool RestoreFile(string backupFileName, string targetFileName) {
           return RestoreFile(backupFileName, targetFileName, null);
       }
       /// <summary>
       /// 获取文件MD5值
       /// </summary>
       /// <param name="pathName">文件路径</param>
       /// <returns></returns>
       public static string GetMd5Hash(string pathName) {
           string strResult;
           System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
           try {
               FileStream oFileStream = new FileStream(pathName, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
               byte[] arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
               oFileStream.Close();
               //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
               string strHashData = BitConverter.ToString(arrbytHashValue);
               //替换-
               strHashData = strHashData.Replace("-", "");
               strResult = strHashData;
           }
           catch (Exception ex) {
               strResult = "";
           }
           return strResult;
       }
       /// <summary>
       /// 根据流写入文件
       /// </summary>
       /// <param name="stream"></param>
       /// <param name="filePath">EXP:[d:\aaa.jpg]</param>
       public static void WriteFile(Stream stream, string filePath)
       {
           using (stream)
           {
               const int bufferSize = 2048 * 5;
               byte[] bytes = new byte[bufferSize];
               int readCount = 0;

               readCount = stream.Read(bytes, 0, bufferSize);

               using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
               {
                   while (readCount > 0)
                   {
                       fileStream.Write(bytes, 0, readCount);
                       fileStream.Flush();
                       readCount = stream.Read(bytes, 0, bufferSize);
                   }

               }
           }
       }

       public static long GetFileLeng(string filePath)
       {
           using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {

               return fileStream.Length;
           }
       }


       public static void CopyFile(string filePath,string destFilePath) {
           int bufferSize = 102400;//100k
           byte[] buffer = new byte[bufferSize];
           using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) {

               var fileLen = fileStream.Length;
               using (FileStream writeStream = new FileStream(destFilePath, FileMode.Create)) {
                   while (fileLen > 0) {
                       int readBytes = fileStream.Read(buffer, 0, bufferSize);//从当前流读取指定字节
                       writeStream.Write(buffer, 0, readBytes);
                       writeStream.Flush();
                       fileLen = fileLen - readBytes;//剩余的字节数
                   }
               }
           }
       }

       public static void CopyFilePos(string filePath, string destFilePath)
       {
           const int bufferSize = 102400; //100k
           byte[] buffer = new byte[bufferSize];
           using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
           {
               var fileLen = fileStream.Length;
               var readTotal = 0;
               using (FileStream writeStream = new FileStream(destFilePath, FileMode.Create)) {

                   while (readTotal < fileLen) {

                       var readCount = fileStream.Read(buffer, 0, bufferSize);

                       writeStream.Write(buffer,0,readCount);
                       writeStream.Flush();
                       readTotal += readCount;
                   }
               }
           }
       }
       /// <summary>
       /// 限速下载 100k/s
       /// </summary>
       /// <param name="filePath"></param>
       /// <param name="destFilePath"></param>
       public static void DownControl(string filePath, string destFilePath) {
           const int bufferSize = 102400; //100k
           byte[] buffer = new byte[bufferSize];
           FileStream fileStream = new FileStream(filePath, FileMode.Open,FileAccess.Read,FileShare.Read);
           var fileLen = fileStream.Length;
           var readTotal = 0;
           FileStream writeStream = new FileStream(destFilePath, FileMode.Create,FileAccess.Write);

           System.Threading.Timer timer = new Timer(call => {
               if (readTotal < fileLen) {
                   var readCount = fileStream.Read(buffer, 0, bufferSize);

                   writeStream.Write(buffer, 0, readCount);
                   writeStream.Flush();
                   readTotal += readCount;
               }
               else {
                   fileStream.Close();
                   writeStream.Close();
               }
           }, null, 0, 1000);
       }
       /// <summary>
       /// 限速下载 较快 最高下载速度500k/s
       /// </summary>
       /// <param name="filePath"></param>
       /// <param name="destFilePath"></param>
       public static void DownControlVip(string filePath, string destFilePath)
       {
           const int bufferSize = 102400; //100k
           byte[] buffer = new byte[bufferSize];
           FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
           var fileLen = fileStream.Length;
           var readTotal = 0;
           FileStream writeStream = new FileStream(destFilePath, FileMode.Create, FileAccess.Write);

           System.Threading.Timer timer = new Timer(call => {
               if (readTotal < fileLen)
               {
                   var readCount = fileStream.Read(buffer, 0, bufferSize);

                   writeStream.Write(buffer, 0, readCount);
                   writeStream.Flush();
                   readTotal += readCount;
               } else
               {
                   fileStream.Close();
                   writeStream.Close();
               }
           }, null, 0, 200);
       }
   }
}
