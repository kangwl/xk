using System.Security.Principal;

namespace XK.Common {
    /// <summary>
    /// OS
    /// </summary>
    public static class CheckOSAdmin {
        /// <summary>
        /// 检查系统是否是以管理员身份运行
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin() {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null) {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator)) {
                    return true;
                }
                // MessageBox.Show("请以管理员身份运行!");
                return false;
            }
            return false;
        }
    }
}
