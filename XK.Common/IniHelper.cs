using System.Runtime.InteropServices;
using System.Text;

namespace XK.Common {
   public class IniHelper {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32.dll")]
        public static extern int Beep(int dwFreq, int dwDuration);

        //写ini
        public static void iniFile_SetVal(string in_filename, string Section, string Key, string Value) {
            WritePrivateProfileString(Section, Key, Value, in_filename);
        }

        //获取INI
        public static string iniFile_GetVal(string in_filename, string Section, string Key) {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, in_filename);
            if (i == 0)
                return "";
            return temp.ToString();
        }
    }
}
