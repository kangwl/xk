using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace MyVPN {
    public partial class FreeVPN : Form {

        private const string text = "VPN";

        /// <summary>
        /// 要显示的vpn名
        /// </summary>
        private const string vpnName = "MyVPN";
        private const string userName = "qiyuvpn";
        private const string password = "123456";

        private MyVPN.VPNCore GetVPNIns() {
            //找出延迟最低的服务地址
            object country = comboBox_Country.SelectedItem;
            ServerIPS.Country c = (ServerIPS.Country) country;
            string fastOne = ServerIPS.GetFastOne(c);
            
            return new VPNCore(vpnName, fastOne, userName, password);
        }

        private MyVPN.VPNCore GetVPNInsExcept() {
            //找出延迟最低的服务地址
            object country = comboBox_Country.SelectedItem;
            ServerIPS.Country c = (ServerIPS.Country)country;
            string fastOne = ServerIPS.GetNewFastIPByExceptIP(errIPs, c);

            return new VPNCore(vpnName, fastOne, userName, password);
        }

        private MyVPN.VPNCore vpnCore = null;

        public FreeVPN() {
            InitializeComponent();
            comboBox_Country.Visible = false;
            InitCountry();
            this.Text = text;
            CheckForIllegalCrossThreadCalls = false;
            richTextBox1.HideSelection = false; 
        }

        private void InitCountry() {
            comboBox_Country.Items.Add(ServerIPS.Country.All);
            comboBox_Country.Items.Add(ServerIPS.Country.America);
            comboBox_Country.Items.Add(ServerIPS.Country.Japan);
            comboBox_Country.Items.Add(ServerIPS.Country.HongKong);
            comboBox_Country.SelectedIndex = 0;
        }
        

        public System.Threading.Timer Interval { get; set; }
        private Thread thread = null;
        private void btn_GO_Click(object sender, EventArgs e) {
            thread = new Thread(DealDial);
            thread.Start();
            //每8分钟断掉重连一次
            Interval = new System.Threading.Timer(d => Reconnect(), null, 7 * 60 * 1000, 8 * 60 * 1000);
        }

        //开始拨号
        private void DealDial() {
            try {
                 
                AppendMessage("连接中...");
                ChangeTitle("连接中...");
                vpnCore = GetVPNIns();//
                vpnCore.Dial();
                vpnCore.DialState = AppendMessage;
                vpnCore.DialError = AppendMessage;

                AppendMessage("已连接");
                ChangeTitle("已连接");

                #region 当拨号为异步时 
                //vpnCore.DialComplete = completeArg => {
                //    if (completeArg.Connected) {
                //        //success
                //        AppendMessage("已连接");
                //    }
                //    else {
                //        AppendMessage("连接失败,重试中...");
                //        CloseVPN();
                //        vpnCore.Dial();
                //    }
                //};

                #endregion

            }
            catch (Exception) {
                AppendMessage("连接失败,重试中...");
                ChangeTitle("连接重试...");
                Thread.Sleep(3000);
                AddErrIPS(vpnCore.ServerIP);
                Reconnect();
            }
        }
        //重连
        private void Reconnect() {
            Task.Factory.StartNew(() => {
                CloseVPN(); //关掉
                ReDealDial(); //连接
            });
        }

        private List<string> errIPs = new List<string>();

        private void AddErrIPS(string ip) {
            if (!errIPs.Contains(ip)) {
                errIPs.Add(ip);
            }
        }

        private void ReDealDial() {
            try {

                AppendMessage("连接中...");
                ChangeTitle("连接中...");
                vpnCore = GetVPNInsExcept();//
                vpnCore.Dial();
                vpnCore.DialState = AppendMessage;
                vpnCore.DialError = AppendMessage;
                errIPs.Clear();
                AppendMessage("已连接");
                ChangeTitle("已连接");

            }
            catch (Exception) {
                AppendMessage("连接失败,重试中...");
                ChangeTitle("连接重试...");
                Thread.Sleep(3000);
                AddErrIPS(vpnCore.ServerIP);
                Reconnect();
            }
        }
        //关闭释放vpn
        private void CloseVPN() {
            AppendMessage("关闭中，请等待...");
            ChangeTitle("关闭中...");
            vpnCore.CloseAcrion = AppendMessage;
            vpnCore.Disconnect();
            ChangeTitle("已关闭");
        }

        private void btn_Discconnect_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(CloseVPN);
        }

        //改变winform显示的标题
        private void ChangeTitle(string message) {
            this.Text = string.Format("{0}-{1}", text, message);
        }
        //显示日志
        private void AppendMessage(string message) {
            richTextBox1.AppendText(message + Environment.NewLine);
            richTextBox1.Focus();
        }

        private void comboBox_Country_SelectedIndexChanged(object sender, EventArgs e) {
           // MessageBox.Show("ss");
        }
    }
}
