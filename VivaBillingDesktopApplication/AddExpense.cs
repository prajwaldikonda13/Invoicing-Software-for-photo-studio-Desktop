using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddExpense : Form
    {
        DBConnection dBConnection;
        bool isDateTimeChanged;
        public AddExpense()
        {
            InitializeComponent();
            initVariables();
            
        }
        private void initVariables()
        {
            dtp.Value = DateTime.Now;
            dtp.ValueChanged += (s, e) => isDateTimeChanged = true;
            isDateTimeChanged = false;
            try
            {
                dBConnection = new DBConnection();
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string itemName = StaticFunctions.getValidString(txtItemName.Text, true, false);
                string partyName = StaticFunctions.getValidString(txtPartyName.Text, true, false);
                string quantityText = StaticFunctions.getValidString(txtQuantity.Text, true, false);
                string description = StaticFunctions.getValidString(txtDescription.Text, true, false);
                string priceText = StaticFunctions.getValidString(txtPrice.Text, true, false);

                if (string.IsNullOrWhiteSpace(itemName) || string.IsNullOrWhiteSpace(quantityText) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(partyName))
                {
                    MessageBox.Show("Party name,Item name,quantity and price are mandatory", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!StaticFunctions.IsNumber(quantityText))
                {
                    MessageBox.Show("Please enter the valid quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!StaticFunctions.IsNumber(priceText))
                {
                    MessageBox.Show("Please enter the valid price.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DateTime dt = DateTime.Now;
                if (isDateTimeChanged)
                {
                    dt = dtp.Value;
                }

                DailyCount dailyCount = dBConnection.dailyCount.SingleOrDefault(m => DbFunctions.TruncateTime(m.dateTime) == DbFunctions.TruncateTime(dt));
                if (dailyCount == null)
                {
                    dBConnection.dailyCount.Add(new DailyCount { Count = 0, dateTime = dt });
                    dBConnection.SaveChanges();
                    dailyCount = dBConnection.dailyCount.SingleOrDefault(m => DbFunctions.TruncateTime(m.dateTime) == DbFunctions.TruncateTime(dt));
                }


                float price = (float)Convert.ToDecimal(priceText);
                float quantity = (float)Convert.ToDecimal(quantityText);
                dBConnection.expenses.Add(new Expense { PartyName = partyName, dailyCountID = dailyCount.ID, Description = description, Price = price, ItemName = itemName, Quantity = quantity });
                dBConnection.SaveChanges();
                MessageBox.Show("Expense for date " + dt.ToLongDateString() + " saved successfully.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resetAll();
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

        private void resetAll()
        {
            isDateTimeChanged = false;
            txtItemName.Text = "";
            txtPartyName.Text = "";
            txtQuantity.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
           try
            {
                if (dtp.Value.Date >DateTime.Now.Date)
                {
                    dtp.Value = DateTime.Now;
                    MessageBox.Show("Selected date should not be greater than today.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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