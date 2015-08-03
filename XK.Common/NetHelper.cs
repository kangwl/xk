using System;
using System.Net;
using System.Net.NetworkInformation;

namespace XK.Common {
   public class NetHelper {


       /// <summary>
       /// ping IP地址 timeout 局域网用200,广域网用2000
       /// </summary>
       /// <param name="ip">IP地址</param>
       /// <param name="timeout">超时 毫秒</param>
       /// <returns></returns>
       public static bool Ping(string ip, int timeout) {
           IPAddress ipadd;
           if (!IPAddress.TryParse(ip, out ipadd)) {
               return false;
           }
           Ping pingSender = new Ping();
           PingReply reply = pingSender.Send(ip, timeout, new Byte[] { Convert.ToByte(1) });
           if (reply != null && reply.Status == IPStatus.Success)
               return true;
           return false;
       }

       public static long PingTime(string ip, int timeout=5000) {
           IPAddress ipadd;
           if (!IPAddress.TryParse(ip, out ipadd)) {
               return timeout;
           }
           Ping pingSender = new Ping();
           PingReply reply = pingSender.Send(ip, timeout, new Byte[] { Convert.ToByte(1) });
           if (reply != null && reply.Status == IPStatus.Success)
               return reply.RoundtripTime;
           return timeout;
       }
       
   }
}
