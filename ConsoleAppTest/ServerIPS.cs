using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest {
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
                long millsecond = XK.Common.NetHelper.PingTime(ip, 1000 * 5);
                dicIP.Add(ip, millsecond);
            });

            //找出最快的
            var dicIPSort = dicIP.OrderBy(pair => pair.Key);
    
            return dicIPSort.FirstOrDefault().Key;
        }
    }
}
