using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotRas;

namespace VPNManage {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            InitCom();
        }

        private void InitCom() {
            var conn = DotRas.RasConnection.GetActiveConnections();
            foreach (RasConnection connection in conn) {
                comboBox1.Items.Add(connection.EntryId);
            }

        }

        private void button1_Click(object sender, EventArgs e) {
               var conn = DotRas.RasConnection.GetActiveConnections();
            var id = (Guid) comboBox1.SelectedItem;
            conn.First(c => c.EntryId == id).HangUp();
        }
    }
}
