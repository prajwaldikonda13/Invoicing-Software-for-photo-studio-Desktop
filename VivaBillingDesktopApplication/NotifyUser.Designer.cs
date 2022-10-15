namespace VivaBillingDesktopApplication
{
    partial class NotifyUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.sendSms = new System.Windows.Forms.RadioButton();
            this.broadcastSMS = new System.Windows.Forms.RadioButton();
            this.sendEmail = new System.Windows.Forms.RadioButton();
            this.broadcastEmail = new System.Windows.Forms.RadioButton();
            this.selectFromFile = new System.Windows.Forms.RadioButton();
            this.btnSend = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyBgWorker = new System.ComponentModel.BackgroundWorker();
            this.fd = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.sendSms);
            this.flowLayoutPanel1.Controls.Add(this.broadcastSMS);
            this.flowLayoutPanel1.Controls.Add(this.sendEmail);
            this.flowLayoutPanel1.Controls.Add(this.broadcastEmail);
            this.flowLayoutPanel1.Controls.Add(this.selectFromFile);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1403, 27);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // sendSms
            // 
            this.sendSms.AutoSize = true;
            this.sendSms.Checked = true;
            this.sendSms.Location = new System.Drawing.Point(3, 3);
            this.sendSms.Name = "sendSms";
            this.sendSms.Size = new System.Drawing.Size(95, 21);
            this.sendSms.TabIndex = 0;
            this.sendSms.TabStop = true;
            this.sendSms.Text = "Send SMS";
            this.sendSms.UseVisualStyleBackColor = true;
            this.sendSms.CheckedChanged += new System.EventHandler(this.sendSms_CheckedChanged);
            // 
            // broadcastSMS
            // 
            this.broadcastSMS.AutoSize = true;
            this.broadcastSMS.Location = new System.Drawing.Point(104, 3);
            this.broadcastSMS.Name = "broadcastSMS";
            this.broadcastSMS.Size = new System.Drawing.Size(126, 21);
            this.broadcastSMS.TabIndex = 2;
            this.broadcastSMS.Text = "Broadcast SMS";
            this.broadcastSMS.UseVisualStyleBackColor = true;
            this.broadcastSMS.CheckedChanged += new System.EventHandler(this.sendSms_CheckedChanged);
            // 
            // sendEmail
            // 
            this.sendEmail.AutoSize = true;
            this.sendEmail.Location = new System.Drawing.Point(236, 3);
            this.sendEmail.Name = "sendEmail";
            this.sendEmail.Size = new System.Drawing.Size(100, 21);
            this.sendEmail.TabIndex = 1;
            this.sendEmail.Text = "Send Email";
            this.sendEmail.UseVisualStyleBackColor = true;
            // 
            // broadcastEmail
            // 
            this.broadcastEmail.AutoSize = true;
            this.broadcastEmail.Location = new System.Drawing.Point(342, 3);
            this.broadcastEmail.Name = "broadcastEmail";
            this.broadcastEmail.Size = new System.Drawing.Size(131, 21);
            this.broadcastEmail.TabIndex = 3;
            this.broadcastEmail.Text = "Broadcast Email";
            this.broadcastEmail.UseVisualStyleBackColor = true;
            this.broadcastEmail.CheckedChanged += new System.EventHandler(this.sendSms_CheckedChanged);
            // 
            // selectFromFile
            // 
            this.selectFromFile.AutoSize = true;
            this.selectFromFile.Location = new System.Drawing.Point(479, 3);
            this.selectFromFile.Name = "selectFromFile";
            this.selectFromFile.Size = new System.Drawing.Size(307, 21);
            this.selectFromFile.TabIndex = 4;
            this.selectFromFile.Text = "Select Emails and mobile numbers From File";
            this.selectFromFile.UseVisualStyleBackColor = true;
            this.selectFromFile.CheckedChanged += new System.EventHandler(this.sendSms_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSend.Location = new System.Drawing.Point(0, 427);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(1403, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send SMS";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtInput);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1403, 42);
            this.panel1.TabIndex = 3;
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(0, 17);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(1403, 22);
            this.txtInput.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Receipents:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtSubject);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1403, 42);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // txtSubject
            // 
            this.txtSubject.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSubject.Location = new System.Drawing.Point(0, 17);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(1403, 22);
            this.txtSubject.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Subject:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtBody);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 111);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1403, 316);
            this.panel3.TabIndex = 5;
            // 
            // txtBody
            // 
            this.txtBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBody.Location = new System.Drawing.Point(0, 17);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(1403, 299);
            this.txtBody.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Message Body";
            // 
            // notifyBgWorker
            // 
            // 
            // fd
            // 
            this.fd.FileName = "openFileDialog1";
            // 
            // NotifyUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 450);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "NotifyUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NotifyUser";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton sendSms;
        private System.Windows.Forms.RadioButton sendEmail;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtBody;
        private System.ComponentModel.BackgroundWorker notifyBgWorker;
        private System.Windows.Forms.RadioButton broadcastSMS;
        private System.Windows.Forms.RadioButton broadcastEmail;
        private System.Windows.Forms.RadioButton selectFromFile;
        private System.Windows.Forms.OpenFileDialog fd;
    }
}