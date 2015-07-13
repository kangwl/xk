using System;
using System.Diagnostics;

namespace XK.Common {
    public static class Cmd {
        /// <summary>
        /// 调用cmd命令
        /// </summary>
        /// <param name="cmdArgs"></param>
        /// <returns></returns>
        public static string InvokeCmd(string cmdArgs) {
            string str = "";
            try {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                p.StandardInput.WriteLine(cmdArgs);
                p.StandardInput.WriteLine("exit");
                str = p.StandardOutput.ReadToEnd();
                p.Close();
            }
            catch (Exception) {
                str = "";
            }
            return str;
        }
    }
}
