using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class NotifyUser : Form
    {
        DBConnection dBConnection;
        public NotifyUser()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            try
            {
                dBConnection = new DBConnection();
            }
            catch(Exception ex)
            {

            }
        }

        private void sendSms_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            if (sendSms.Checked)
            {
                btnSend.Text = "Send SMS";
                panel1.Visible = true;
            }
            else if (broadcastSMS.Checked)
            {
                btnSend.Text = "Broadcast SMS";
            }
            else if(sendEmail.Checked)
            {
                btnSend.Text = "Send Email";
                panel1.Visible = true;
                panel2.Visible = true;
            }
            else if (broadcastEmail.Checked)
            {
                btnSend.Text = "Broadcast Email";
                panel2.Visible = true;
            }
            else if (selectFromFile.Checked)
            {
                btnSend.Text = "Send to all in file";
                panel2.Visible = true;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //notifyBgWorker.RunWorkerAsync();
            Thread newThread = new Thread(new ThreadStart(backgroundWorker1_DoWork));
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }

        private void resetTextBoxes()
        {
            txtBody.Text = "";
            txtInput.Text = "";
            txtSubject.Text = "";
        }

        private void backgroundWorker1_DoWork()
        {
            try
            {
                Regex mobileRegex = new Regex("[^0-9,]");
                Regex emailRegex = new Regex(@"[^a-zA-Z0-9_@\.,]");
                string To = StaticFunctions.getValidString(txtInput.Text, false, false);
                string Subject = StaticFunctions.getValidString(txtSubject.Text, true, false);
                string Body = StaticFunctions.getValidString(txtBody.Text, true, false);
                if(Body.Length<=0)
                {
                    MessageBox.Show("Body of message can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (To.Length >= 1)
                {
                    if (To[To.Length - 1] == ',') To = To.Remove(To.Length - 1);
                    if (To[0] == ',') To = To.Remove(0, 1);
                }
                if (sendSms.Checked)
                {
                    if (mobileRegex.IsMatch(To))
                    {
                        MessageBox.Show("Input should not contain anything except mobile number and comma(,)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //btnSend.Enabled = false;
                    foreach (string number in To.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(number) && number.Length == 10)
                            VivaSMSServices.SendSMS(number, Body);
                    }
                    if (MessageBox.Show("Message sent to specified reciepents.Do you want to clear all fields?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                        resetTextBoxes();
                    return;
                }
                if (sendEmail.Checked)
                {
                    if (emailRegex.IsMatch(To))
                    {
                        MessageBox.Show("Input should not contain anything except email id and comma(,)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    foreach (string emailID in To.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(emailID) && StaticFunctions.IsValidEmail(emailID))
                            VivaEmailServices.SendEmail(emailID, Subject, Body);
                    }
                    if (MessageBox.Show("Email sent to specified reciepents.Do you want to clear all fields?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                        resetTextBoxes();
                    return;
                }
                if (broadcastSMS.Checked)
                {
                    foreach (Customer customer in dBConnection.customers)
                    {
                        if (!string.IsNullOrWhiteSpace(customer.MobileNumber))
                            VivaSMSServices.SendSMS(customer.MobileNumber, Body);
                    }
                    if (MessageBox.Show("Message sent to specified reciepents.Do you want to clear all fields?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                        resetTextBoxes();
                    return;
                }
                if (broadcastEmail.Checked)
                {
                    foreach (Customer customer in dBConnection.customers)
                    {
                        if (!string.IsNullOrWhiteSpace(customer.EmailId))
                            VivaEmailServices.SendEmail(customer.EmailId, Subject, Body);
                    }
                    if (MessageBox.Show("Email sent to specified reciepents.Do you want to clear all fields?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                        resetTextBoxes();
                    return;
                }
                if (selectFromFile.Checked)
                {
                    string filePath = "";
                    string str = "";
                    if (fd.ShowDialog().ToString().ToLower() != "ok") return;
                    filePath = fd.FileName;
                    StreamReader sr = new StreamReader(filePath);
                    while ((str = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(str))
                            continue;
                        if (StaticFunctions.IsValidMobile(str))
                            VivaSMSServices.SendSMS(str, Body);
                        else if (StaticFunctions.IsValidEmail(str))
                            VivaEmailServices.SendEmail(str, Subject, Body);
                    }
                    if (MessageBox.Show("Email sent to specified reciepents.Do you want to clear all fields?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                        resetTextBoxes();
                    return;
                }
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}