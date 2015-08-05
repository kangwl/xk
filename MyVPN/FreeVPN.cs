using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            string fastOne = ServerIPS.GetFastOne();

            return new VPNCore(vpnName, fastOne, userName, password);
        }

        private MyVPN.VPNCore GetVPNIns(string ip) {
  
            return new VPNCore(vpnName, ip, userName, password);
        }

        private MyVPN.VPNCore GetVPNInsExcept() {
            //找出延迟最低的服务地址  
            string fastOne = ServerIPS.GetNewFastIPByExceptIP(errIPs);

            return new VPNCore(vpnName, fastOne, userName, password);
        }
        private MyVPN.VPNCore GetVPNInsExcept(string ip) {
 
            return new VPNCore(vpnName, ip, userName, password);
        }

        private MyVPN.VPNCore vpnCore = null;

        public FreeVPN() {
            InitializeComponent(); 
            this.Text = text;
            CheckForIllegalCrossThreadCalls = false;
            richTextBox1.HideSelection = false;

        }
 
        
        public System.Threading.Timer Interval { get; set; }
        private Thread thread = null;
        private void btn_GO_Click(object sender, EventArgs e) {
            thread = new Thread(DealDial);
            thread.Start();
            //每8分钟断掉重连一次
            //Interval = new System.Threading.Timer(d => Reconnect(), null, 1 * 60 * 1000, 8 * 60 * 1000);
        }

        //失败重连
        private void ConnectException() {
            AppendMessage("连接失败,重试中...");
            ChangeTitle("连接重试...");
            ShowNotify("失败，重连中...", ToolTipIcon.Error);
            Thread.Sleep(3000);
            AddErrIPS(vpnCore.ServerIP);
            Reconnect();
        }
  
        //开始拨号
        private void DealDial() {
            try {
                AppendMessage("查找 IP ...");
                //找出延迟最低的服务地址  
                string fastOne = ServerIPS.GetFastOne();
                AppendMessage(string.Format("已找到 IP {0},连接中...", fastOne)); 
                ChangeTitle("连接中...");
                vpnCore = GetVPNIns(fastOne);//
                vpnCore.Dial();
                vpnCore.DialState = AppendMessage;
                vpnCore.DialError = AppendMessage;
                AppendMessage(string.Concat("已连接 ", vpnCore.ServerIP));
                ChangeTitle("已连接");
                
            }
            catch (Exception) {
               ConnectException();
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
                AppendMessage("查找 IP ...");
                string fastOne = ServerIPS.GetFastOne();//不用上次失败的IP地址
                AppendMessage(string.Format("已找到 IP {0},连接中...", fastOne)); 
                ChangeTitle("连接中...");
                vpnCore = GetVPNInsExcept(fastOne);
                vpnCore.Dial();
                vpnCore.DialState = AppendMessage;
                vpnCore.DialError = AppendMessage;
                errIPs.Clear();
                AppendMessage(string.Concat("已连接 ", vpnCore.ServerIP));
                ChangeTitle("已连接"); 
            }
            catch (Exception) {
                ConnectException();
            }
        }

        //关闭释放vpn
        private void CloseVPN() {
            if (vpnCore == null) return;
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
 

        //toolstrip

        private void FreeVPN_SizeChanged(object sender, EventArgs e) {
            if (this.WindowState == FormWindowState.Minimized) //判断是否最小化
            {
                this.ShowInTaskbar = false; //不显示在系统任务栏
                notifyIcon1.Visible = true; //托盘图标可见
                ShowNotify("我在这里运行。", ToolTipIcon.Info, 2);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e) {
           FormShow();
        }

        private void ShowNotify(string msg,ToolTipIcon icon=ToolTipIcon.Info, int timeout = 3) {
            notifyIcon1.BalloonTipIcon = icon;
            notifyIcon1.BalloonTipTitle = "提示";
            notifyIcon1.BalloonTipText = msg;
            notifyIcon1.ShowBalloonTip(3000);
        }

        private void Exit_ToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
         

        private void Show_ToolStripMenuItem_Click(object sender, EventArgs e) {
            FormShow();
        }

        private void FormShow() {
            this.ShowInTaskbar = true;  //显示在系统任务栏
            this.WindowState = FormWindowState.Normal;  //还原窗体
            notifyIcon1.Visible = false;  //托盘图标隐藏
        }

        private void FreeVPN_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult result = MessageBox.Show("确定退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes) {
                //关闭
                ShowNotify("退出中，请稍后", ToolTipIcon.Warning);
               // CloseVPN();
            }
            else {
                e.Cancel = true;
            }
        }

     

    }
}
