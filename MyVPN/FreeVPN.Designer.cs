namespace MyVPN {
    partial class FreeVPN {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FreeVPN));
            this.btn_GO = new System.Windows.Forms.Button();
            this.btn_Discconnect = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox_Country = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_GO
            // 
            this.btn_GO.Location = new System.Drawing.Point(29, 32);
            this.btn_GO.Name = "btn_GO";
            this.btn_GO.Size = new System.Drawing.Size(125, 35);
            this.btn_GO.TabIndex = 0;
            this.btn_GO.Text = "我要翻墙";
            this.btn_GO.UseVisualStyleBackColor = true;
            this.btn_GO.Click += new System.EventHandler(this.btn_GO_Click);
            // 
            // btn_Discconnect
            // 
            this.btn_Discconnect.Location = new System.Drawing.Point(160, 32);
            this.btn_Discconnect.Name = "btn_Discconnect";
            this.btn_Discconnect.Size = new System.Drawing.Size(75, 35);
            this.btn_Discconnect.TabIndex = 1;
            this.btn_Discconnect.Text = "关闭翻墙";
            this.btn_Discconnect.UseVisualStyleBackColor = true;
            this.btn_Discconnect.Click += new System.EventHandler(this.btn_Discconnect_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(29, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(206, 112);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // comboBox_Country
            // 
            this.comboBox_Country.FormattingEnabled = true;
            this.comboBox_Country.Location = new System.Drawing.Point(29, 6);
            this.comboBox_Country.Name = "comboBox_Country";
            this.comboBox_Country.Size = new System.Drawing.Size(206, 20);
            this.comboBox_Country.TabIndex = 3;
            this.comboBox_Country.SelectedIndexChanged += new System.EventHandler(this.comboBox_Country_SelectedIndexChanged);
            // 
            // FreeVPN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 211);
            this.Controls.Add(this.comboBox_Country);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_Discconnect);
            this.Controls.Add(this.btn_GO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FreeVPN";
            this.Text = "FreeVPN";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_GO;
        private System.Windows.Forms.Button btn_Discconnect;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBox_Country;
    }
}