using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using DotRas;

namespace MyVPN {
    public class VPN {

   
        public VPN(string entryName){
            UserName = "qiyuvpn";
            PassWord = "123456";
            SeverIP = "100.43.170.19";
            EntryName = entryName;
        }
        public VPN(string entryName,string userName,string passWord):this(entryName) {
            UserName = userName;
            PassWord = passWord;
        }

        /// <summary>
        /// vpn的名字
        /// exp：Kangwl VPN
        /// </summary>
        private string EntryName { get; set; }
        /// <summary>
        /// 100.43.170.19
        /// </summary>
        public string SeverIP { get; set; }

        /// <summary>
        /// qiyuvpn
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 123456
        /// </summary>
        public string PassWord { get; set; }

 


        private void InitEntry() { 

            string phonePath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers); 
            //检查是否存在
            if (File.Exists(phonePath) && RasEntry.Exists(EntryName, phonePath)) {
                return;
            }
            DotRas.RasPhoneBook phoneBook = new RasPhoneBook();

            RasDevice rasDevice = RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn);
            ReadOnlyCollection<RasDevice> rasDevices = RasDevice.GetDevices();
            phoneBook.Open(phonePath);
            RasEntry entry = RasEntry.CreateVpnEntry(EntryName, SeverIP, RasVpnStrategy.Default, rasDevices[0]);

            // Add the new entry to the phone book.
            phoneBook.Entries.Add(entry);  
        }

        public RasHandle handle = null;

        public RasDialer Dialer = new RasDialer();
        /// <summary>
        /// 连接vpn
        /// </summary>
        public void Dial(string serverIP="") {
            InitEntry();
            if (!string.IsNullOrWhiteSpace(serverIP)) {
                this.SeverIP = serverIP;
            }

            Dialer.EntryName = EntryName;
            Dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            // Set the credentials the dialer should use.
            Dialer.Credentials = new NetworkCredential(UserName, PassWord);

            // NOTE: The entry MUST be in the phone book before the connection can be dialed.
            // Begin dialing the connection; this will raise events from the dialer instance.
            this.handle = Dialer.DialAsync();
        }



    }
}
