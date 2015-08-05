using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest {
    public class ServerIPS {

        //all
        public static string GetFastOne() {

            return GetFastOne(GetAllIPs());
        }

        private static List<string> GetAllIPs() {
            List<string> ips = new List<string>();
            ips.AddRange(JapanList());
            ips.AddRange(AmericanList());
            ips.AddRange(HongKongList());
            return ips;
        } 

        public static string GetFastOne(List<string> ips) {
            IEnumerable<string> sortedIPs = Sort(ips);

            return sortedIPs.First();
        }

        private static List<string> Sort(List<string> ips) {
            Dictionary<string, long> dicIP = new Dictionary<string, long>();
            ips.ForEach(ip => {
                long millsecond = XK.Common.NetHelper.PingTime(ip, 1000 * 5);
                dicIP.Add(ip, millsecond);
            });

            //找出最快的
            var dicIPSort = dicIP.OrderBy(pair => pair.Value);

            List<string> ipSorted = dicIPSort.Select(d => d.Key).ToList();
            return ipSorted;
        }

        public static List<string> GetFastSortedIPs(Country country) {
            List<string> ips = new List<string>();
            switch (country) {
                    case Country.America:
                    ips = Sort(AmericanList());
                    break;
                    case Country.HongKong:
                   ips= Sort(HongKongList());
                    break;
                    case Country.Japan:
                    ips = Sort(JapanList());
                    break;
                    case Country.All:
                    ips = Sort(GetAllIPs());
                    break;
            }
            return ips;
        }

        public static string GetNewFastIPByExceptIP(string ip, Country country) {
            List<string> ips = GetFastSortedIPs(country);
            ips.Remove(ip);
            return ips.First();
        }

        public enum Country {
            Japan,
            America,
            HongKong,
            All
        }

        public string GetAmericanFastOne() {
            return GetFastOne(AmericanList());
        }

        public string GetJapanFastOne() {
            return GetFastOne(JapanList());
        }

        public string GetHongKongFastOne() {
            return GetFastOne(HongKongList());
        }

        public static List<string> AmericanList() {
            List<string> ipList = new List<string> {
                "98.126.50.92",
                "100.43.170.19", 
                "98.126.63.251",
                "174.139.1.11",
                "174.139.176.35",
                "174.139.248.124"
            };
            return ipList;
        }

        public static List<string> HongKongList() {
            List<string> ipList=new List<string> {
                "103.228.92.203",
                "103.228.92.204",
                "103.228.92.205",
                "103.228.92.211"
            };
            return ipList;
        }

        public static List<string> JapanList() {
            List<string> ipList = new List<string> {
                "106.186.17.73",
                "106.187.46.130",
                "106.185.26.225",
                "106.185.34.197"
            };
            return ipList;
        }

    }
}
