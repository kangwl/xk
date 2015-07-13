using System.Management;

namespace XK.Common {
   public class HardWareInfo {
       /// <summary>
       /// cpu序列号
       /// </summary>
       /// <returns></returns>
       public static string getID_CpuId() {
           string cpuInfo = "";//cpu序列号
           ManagementClass cimobject = new ManagementClass("Win32_Processor");
           ManagementObjectCollection moc = cimobject.GetInstances();
           foreach (var o in moc) {
               var mo = (ManagementObject) o;
               cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
           }
           return cpuInfo;
       }

       /// <summary>
       /// 硬盘ID号
       /// </summary>
       /// <returns></returns>
       public static string getID_HardDiskId() {
           string HDid = "";
           ManagementClass cimobject = new ManagementClass("Win32_DiskDrive");
           ManagementObjectCollection moc = cimobject.GetInstances();
           foreach (var o in moc) {
               var mo = (ManagementObject) o;
               HDid = (string)mo.Properties["Model"].Value;
           }
           return HDid;
       }

       /// <summary>
       /// 获取网卡MacAddress
       /// </summary>
       /// <returns></returns>
       public static string getID_NetCardId() {
           string NCid = "";
           ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
           ManagementObjectCollection moc = mc.GetInstances();
           foreach (var o in moc) {
               var mo = (ManagementObject) o;
               if ((bool)mo["IPEnabled"])
                   NCid = mo["MacAddress"].ToString();
               mo.Dispose();
           }
           return NCid;
       } 
   }
}
