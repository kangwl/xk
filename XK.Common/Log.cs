using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace XK.Common {
    public class Log {
        #region 自定义成员变量

        private readonly int m_maxfilesize;
        private readonly int m_maxfilecount;
        private readonly FileInfo m_fileinfo;
        #endregion

        public Log() {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log\\Log.txt");
            m_maxfilecount = 100;
            m_maxfilesize = 100;
            try {
                m_fileinfo = new FileInfo(path);
                if (m_fileinfo.Exists) {
                    if (m_fileinfo.Length / 0x400 > m_maxfilesize) {
                        Backup();
                        CreatLog();
                    }
                }
                else {
                    CreatLog();
                }
            }
            catch (Exception ex) {
                throw new Exception("初始化日志文件发生错误" + ex);
            }
        }

        public Log(string filepath) {
            m_maxfilecount = 100;
            m_maxfilesize = 100;
            try {
                m_fileinfo = new FileInfo(filepath);
                if (m_fileinfo.Exists) {
                    if (m_fileinfo.Length / 0x400 > m_maxfilesize) {
                        Backup();
                        CreatLog();
                    }
                }
                else {
                    CreatLog();
                }
            }
            catch (Exception ex) {
                throw new Exception("初始化日志文件发生错误" + ex);
            }
        }

        /// <summary>
        /// 备份日志文件
        /// </summary>
        private void Backup() {
            try {
                if (m_fileinfo.Directory != null) {
                    FileInfo[] files = m_fileinfo.Directory.GetFiles(m_fileinfo.Name + ".*");
                    int filecount = files.Length;
                    string bakfilename = m_fileinfo.Name + "." + filecount.ToString(CultureInfo.InvariantCulture);
                    if (filecount > m_maxfilecount) {
                        files[1].Delete();
                    }
                    m_fileinfo.CopyTo(m_fileinfo.Directory + "//" + bakfilename, true);
                }
                CreatLog();
            }
            catch (Exception ex) {
                throw new Exception("备份日志文件发生错误" + ex);
            }
        }


        /// <summary>
        /// 创建日志文件
        /// </summary>
        private void CreatLog() {
            TextWriter logwrite = null;
            ReaderWriterLock writelock = new ReaderWriterLock();
            try {
                writelock.AcquireWriterLock(-1);
                DirectoryInfo directoryInfo = m_fileinfo.Directory;
                if (directoryInfo != null && !directoryInfo.Exists) {
                    DirectoryInfo directory = m_fileinfo.Directory;
                    if (directory != null) directory.Create();
                }
                logwrite = TextWriter.Synchronized(m_fileinfo.CreateText());
                logwrite.WriteLine("#------------------------------------------------------");
                logwrite.WriteLine("#     SYSTEM LOG                                  ");
                logwrite.WriteLine("#                                                      ");
                logwrite.WriteLine("#     Create at " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "   ");
                logwrite.WriteLine("#                                                      ");
                logwrite.WriteLine("#------------------------------------------------------");
                logwrite.Close();
            }
            catch (Exception ex) {
                throw new Exception("创建系统日志文件出错!" + ex);
            }
            finally {
                writelock.ReleaseWriterLock();
                if (logwrite != null) {
                    logwrite.Close();
                }
            }
        }


        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="content"></param>
        public void WriteLog(string content) {
            ReaderWriterLock writelock = new ReaderWriterLock();
            TextWriter logwrite = null;
            try {
                writelock.AcquireWriterLock(-1);
                logwrite = TextWriter.Synchronized(m_fileinfo.AppendText());
                logwrite.Write("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + content + "\r\n");
                logwrite.Close();
            }
            catch (Exception ex) {
                throw new Exception("记录日志发生错误" + ex.Message);
            }
            finally {
                if (logwrite != null) {
                    logwrite.Close();
                }
                writelock.ReleaseWriterLock();
            }
        }

    }
}
