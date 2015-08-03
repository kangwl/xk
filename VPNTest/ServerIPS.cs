using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace VPNTest {
    public class ServerIPS {
        public static string GetFastOne() {
            List<string> ips = new List<string> {
                "98.126.50.92",
                "100.43.170.19",
                "98.126.46.227",
                "98.126.63.251",
                "174.139.1.11",
                "174.139.176.35",
                "174.139.248.124",
                "103.228.92.203",
                "103.228.92.204",
                "103.228.92.205",
                "103.228.92.210",
                "103.228.92.211"
            };
            Dictionary<string, long> dicIP = new Dictionary<string, long>();

            ips.ForEach(ip => {
                long millsecond = PingTime(ip, 1000*5);
                dicIP.Add(ip, millsecond);
            });

            //找出最快的
            var dicIPSort = dicIP.OrderBy(pair => pair.Key);

            return dicIPSort.FirstOrDefault().Key;
        }

        public static long PingTime(string ip, int timeout = 5000) {
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
