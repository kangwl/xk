using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotRas;

namespace VPNTest {
    class Program {

        private static readonly VPN vpn = new VPN("Special VPN");

        private static void Main(string[] args) {
            try {

                Console.WriteLine("等待处理...");
                DealVPN();

            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }

            Console.Read();
        }

        private static void DealVPN() { 
            vpn.SeverIP = ServerIPS.GetFastOne();
            vpn.Dialer.DialCompleted += Dialer_DialCompleted;
            vpn.Dialer.StateChanged += Dialer_StateChanged;
            vpn.Dial();
        }

        static void Dialer_StateChanged(object sender, DotRas.StateChangedEventArgs e) {
            if (e.State == RasConnectionState.Disconnected) {
                //重连
                Console.WriteLine("重连中...");
                DealVPN();
            }
        }

        static void Dialer_DialCompleted(object sender, DotRas.DialCompletedEventArgs e) {
            Console.WriteLine("已成功连接！");
        }
 

        
    }
}
