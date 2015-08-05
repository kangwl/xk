using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using DotRas;

namespace MyVPN {
    public class VPNCore {

        public VPNCore(string vpnName,string serverIP,string userName,string password) {
            this.EntryName = vpnName;
            this.ServerIP = serverIP;
            this.UserName = userName;
            this.PassWord = password;

        }
        /// <summary>
        /// 拨号完成
        /// </summary>
        public Action<DialCompletedEventArgs> DialComplete;//对外调用
        void dialer_DialCompleted(object sender, DialCompletedEventArgs e) {
            DialComplete(e);
        }
        /// <summary>
        /// 出错时
        /// </summary>
        public Action<string> DialError; //对外调用
        void dialer_Error(object sender, ErrorEventArgs e) {
            DialError(e.ToString());
        }
        /// <summary>
        /// 拨号状态
        /// </summary>
        public Action<string> DialState; //对外调用
        void dialer_StateChanged(object sender, StateChangedEventArgs e) {
            DialState(e.State.ToString());
        }

        private string EntryName { get; set; }

        public string ServerIP { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

        private RasPhoneBook phoneBook = new RasPhoneBook();

        private void InitEntry() {

            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            // This opens the phonebook so it can be used. Different overloads here will determine where the phonebook is opened/created.
            this.phoneBook.Open(phoneBookPath);
            // 如果已经该名称的PPPOE已经存在，则更新这个PPPOE服务器地址
            if (phoneBook.Entries.Contains(EntryName)) {
                // phoneBook.Entries[EntryName].Options.RemoteDefaultGateway
                phoneBook.Entries[EntryName].PhoneNumber = ServerIP;
                phoneBook.Entries[EntryName].Update();

                return;
            }
            //if (File.Exists(phoneBookPath) && RasEntry.Exists(EntryName, phoneBookPath)) {
            //    return;
            //}

            #region new

            ReadOnlyCollection<RasDevice> readOnlyCollection = RasDevice.GetDevices();
            RasDevice device = readOnlyCollection.First(o => o.DeviceType == RasDeviceType.Vpn);
            RasEntry entry = RasEntry.CreateVpnEntry(EntryName, ServerIP, RasVpnStrategy.Default, device);
      
            #endregion

            // Create the entry that will be used by the dialer to dial the connection. Entries can be created manually, however the static methods on
            // the RasEntry class shown below contain default information matching that what is set by Windows for each platform.
            //RasEntry entry = RasEntry.CreateVpnEntry(EntryName, ServerIP, RasVpnStrategy.Default,
            //    RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));

            // Add the new entry to the phone book.
            this.phoneBook.Entries.Add(entry);
        }
 

        private RasDialer dialer = new RasDialer();
        /// <summary>
        /// Holds a value containing the handle used by the connection that was dialed.
        /// </summary>
        private RasHandle handle = null;
        /// <summary>
        /// start
        /// </summary>
        public void Dial() {
            InitEntry(); 
            this.dialer.Options.DisableConnectedUI = true;
           
            dialer.StateChanged += dialer_StateChanged;
            dialer.Error += dialer_Error;
            dialer.DialCompleted += dialer_DialCompleted;

            // This button will be used to dial the connection.
            this.dialer.EntryName = EntryName;
            this.dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);


            // Set the credentials the dialer should use.
            this.dialer.Credentials = new NetworkCredential(UserName, PassWord);

            this.dialer.AllowUseStoredCredentials = true;
            this.dialer.AutoUpdateCredentials = RasUpdateCredential.AllUsers;
            // NOTE: The entry MUST be in the phone book before the connection can be dialed.
            // Begin dialing the connection; this will raise events from the dialer instance.
            this.handle = this.dialer.Dial();
            while (handle.IsInvalid) {
                Thread.Sleep(10000);
                ServerIP = ServerIPS.GetFastOne();
                handle = dialer.Dial();
            }
            if (!handle.IsInvalid) {
                //_log.Info("RasDialer Success! " + Convert.ToString(DateTime.Now));
            }
        }

        public void UpdateConn() {
            ReadOnlyCollection<RasConnection> rasConnections = RasConnection.GetActiveConnections();
            foreach (RasConnection connection in rasConnections) {
                connection.UpdateConnection(1, IPAddress.Loopback, IPAddress.Parse(ServerIP));
            }
        }

        public Action<string> CloseAcrion;

        public void Disconnect() {
            string message = "已关闭";

 
            try {

                if (this.dialer.IsBusy) {
                    // The connection attempt has not been completed, cancel the attempt.
                    this.dialer.DialAsyncCancel();
                    
                }
                else {
                    if (this.handle != null) {
                        // The connection attempt has completed, attempt to find the connection in the active connections.
                        RasConnection connection = RasConnection.GetActiveConnectionByHandle(this.handle);
                        if (connection != null) {
                            // The connection has been found, disconnect it.
                            connection.HangUp();
                        }
                    }
                }
                if (this.dialer != null) {
                    this.dialer.Dispose();
                }
                if (this.phoneBook != null) {
                    this.phoneBook.Dispose();
                }
                if (this.handle != null) {
                    this.handle.Dispose();
                }
            }
            catch (Exception ex) {
                message = "关闭时：" + ex.ToString();
            }
            CloseAcrion(message);
        }


    }
}
