using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotRas;

namespace MyVPN {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            button1.Enabled = false;
        }

        private readonly VPN vpn = new VPN("Special VPN");

        private static System.Threading.Timer t = null;
        public void Run(int _minute) {
            if (t == null) {
                t = new System.Threading.Timer(delegate {
                    //AppendMsg("等待处理...");
                    CloseVPN();
                    DealVPN();
                }, null, 5 * 60 * 1000, _minute * 60 * 1000);
            }
        }
        private void btn_VPNStart_Click(object sender, EventArgs e) {
            try {
                btn_VPNStart.Enabled = false;
                Task.Factory.StartNew(() => {
                    AppendMsg("等待处理...");
                    DealVPN(); 
                });
                Run(8);
            }
            catch (Exception ex) {
                btn_VPNStart.Enabled = true;
                MessageBox.Show(ex.ToString());
            }

            Console.Read();
        }

        private void DealVPN() {
            vpn.SeverIP = ServerIPS.GetFastOne();
            vpn.Dialer.DialCompleted += Dialer_DialCompleted;
            vpn.Dialer.StateChanged += Dialer_StateChanged;
            vpn.Dial();
        }

        private void AppendMsg(string msg) {
            richTextBox1.AppendText(msg + Environment.NewLine);
        }

        private void Dialer_StateChanged(object sender, DotRas.StateChangedEventArgs e) {
            if (e.State == RasConnectionState.Disconnected) {
                //重连
                AppendMsg("重连中...");
                DealVPN();
            }
        }

        private void Dialer_DialCompleted(object sender, DotRas.DialCompletedEventArgs e) {
            button1.Enabled = true;
            AppendMsg("已成功连接！");
            //MessageBox.Show("已成功连接！");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
 
        }

        private void button1_Click(object sender, EventArgs e) {
            button1.Enabled = false;
            Task.Factory.StartNew(CloseVPN);
        }

        private void CloseVPN() {

            if (this.vpn.Dialer.IsBusy) {
                this.vpn.Dialer.DialAsyncCancel();
            }
            else {
                ReadOnlyCollection<RasConnection> connections = RasConnection.GetActiveConnections();

                foreach (RasConnection rasConnection in connections) {
                    if (rasConnection != null) {
                        rasConnection.HangUp();
                    }
                }
            }
            AppendMsg("已断开");
            btn_VPNStart.Enabled = true;
        }

    }
}
