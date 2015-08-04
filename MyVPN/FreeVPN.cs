using System;
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
            return new VPNCore(vpnName, FastServerTask().Result, userName, password);
        }

        private MyVPN.VPNCore vpnCore = null;

        public FreeVPN() {
            InitializeComponent();
            this.Text = text;
            CheckForIllegalCrossThreadCalls = false;
            richTextBox1.HideSelection = false;
            FastServerTask();//用任务加速首次启动速度
        }

        //找出延迟最低的服务地址
        public string ServerIP {
            get { return FastServerTask().Result; }
        }

        public Task<string> FastServerTask() {
            return Task<string>.Factory.StartNew(ServerIPS.GetFastOne);
        }

        // public string FastServerIP { get; set; }

        public System.Threading.Timer Interval { get; set; }
        private Thread thread = null;
        private void btn_GO_Click(object sender, EventArgs e) {
            thread = new Thread(() => {
                DealDial();
                //每8分钟断掉重连一次
                Interval = new System.Threading.Timer(d => Reconnect(), null, 1*60*1000, 3*60*1000);
            });
            thread.Start();
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
                Reconnect();
            }
        }
        //重连
        private void Reconnect() {
            Task.Factory.StartNew(() => {
                CloseVPN(); //关掉
                DealDial(); //连接
            });
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
    }
}
