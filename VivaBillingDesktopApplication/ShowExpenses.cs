using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class ShowExpenses : Form
    {
        DBConnection dBConnection;
        List<Expense> expenseList;
        List<Customer> customerList;
        float totalDue, totalPaid, totalDiscount;
        private int rightClickRowIndex;

        public ShowExpenses()
        {
            InitializeComponent();
            initVariables();
            ExpenseGrid.AutoSize = true;
            expenseList = dBConnection.expenses.ToList();
            FindInvoicesAndBindGrid();
        }
        private void initVariables()
        {
            expenseList = new List<Expense>();
            customerList = new List<Customer>();
            totalDue = totalPaid =totalDiscount = 0;
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
        private void bindInvoicesGridView()
        {
            try
            {
                ExpenseGrid.Rows.Clear();
                ExpenseGrid.Columns.Clear();
                ExpenseGrid.Columns.Add("ID", "ID");
                ExpenseGrid.Columns.Add("ItemName", "ItemName");
                ExpenseGrid.Columns.Add("Quantity", "Quantity");
                ExpenseGrid.Columns.Add("Description", "Description");
                ExpenseGrid.Columns.Add("Price", "Price");
                ExpenseGrid.Columns.Add("PartyName", "PartyName");
                ExpenseGrid.Columns.Add("DateTime", "Date and Time");
                for (int i = 0; i < expenseList.Count; i++)
                {
                    totalPaid += expenseList[i].Price;
                    long dailyCountId = expenseList[i].dailyCountID;
                    DateTime dailyCountDateTime = dBConnection.dailyCount.SingleOrDefault(m => m.ID == dailyCountId).dateTime;
                    ExpenseGrid.Rows.Add(new object[] { expenseList[i].ID, expenseList[i].ItemName, expenseList[i].Quantity, expenseList[i].Description, expenseList[i].Price, expenseList[i].PartyName, dailyCountDateTime });
                    setRowReadOnly(i);
                }
                lblTotalPaid.Text = "Total Expense:" + totalPaid;
                //lblTotalDiscount.Text = "Total Discount:" + totalDiscount;
                //lblTotalDue.Text = "Total Due:" + totalDue;
                totalPaid = 0;
                totalDiscount = 0;
                totalDue = 0;
                ExpenseGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ExpenseGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ExpenseGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void setRowReadOnly(int rowIndex)
        {
            try
            {
                ExpenseGrid.Rows[rowIndex].Cells["ID"].ReadOnly = true;
                ExpenseGrid.Rows[rowIndex].Cells["DateTime"].ReadOnly = true;
                ExpenseGrid.Rows[rowIndex].Cells["ItemName"].ReadOnly = true;
                ExpenseGrid.Rows[rowIndex].Cells["PartyName"].ReadOnly = true;
                ExpenseGrid.Rows[rowIndex].Cells["Quantity"].ReadOnly = true;
                ExpenseGrid.Rows[rowIndex].Cells["Description"].ReadOnly = true;
                ExpenseGrid.Rows[rowIndex].Cells["Price"].ReadOnly = true;
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

        private void InputBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode.ToString().ToLower())
                {
                    case "return":
                        if (!e.Shift)
                            return;
                        FindInvoicesAndBindGrid();
                        break;

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

        private void FindInvoicesAndBindGrid()
        {
            try
            {
                customerList.Clear();
                expenseList.Clear();
                //string inputString = InputBox.Text;
                //if (inputString.Length < 1 && !findByDate.Checked) return;
                DateTime startDateTime = dtpForStartDate.Value;
                DateTime endDateTime = dtpForEndDate.Value;
                if (FilterByDate.Checked)
                {
                    if (startDateTime > endDateTime)
                    {
                        MessageBox.Show("Start date should not be greater than end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bindInvoicesGridView();
                        return;
                    }
                    if (textBox1.Text.Length<=5)
                    {
                        List<DailyCount> dailyCountsList = dBConnection.dailyCount.Where(m => DbFunctions.TruncateTime(m.dateTime) >= DbFunctions.TruncateTime(startDateTime) && DbFunctions.TruncateTime(m.dateTime) <= DbFunctions.TruncateTime(endDateTime)).ToList();
                        expenseList = new List<Expense>();
                        foreach (DailyCount dc in dailyCountsList)
                        {
                            expenseList.AddRange(dBConnection.expenses.Where(m => m.dailyCountID == dc.ID));
                        }
                    }
                    else
                    {
                        List<DailyCount> dailyCountsList = dBConnection.dailyCount.Where(m => DbFunctions.TruncateTime(m.dateTime) >= DbFunctions.TruncateTime(startDateTime) && DbFunctions.TruncateTime(m.dateTime) <= DbFunctions.TruncateTime(endDateTime)).ToList();
                        expenseList = new List<Expense>();
                        foreach (DailyCount dc in dailyCountsList)
                        {
                            expenseList.AddRange(dBConnection.expenses.Where(m => m.dailyCountID == dc.ID && m.PartyName.Contains(textBox1.Text)));
                        }
                    }
                }
                else
                {
                    expenseList = dBConnection.expenses.Where(m => m.PartyName.Contains(textBox1.Text)).ToList();
                }
                bindInvoicesGridView();
                if (!(expenseList.Count > 0))
                    MessageBox.Show("Expenses not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void InvoicesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (!(e.RowIndex >= 0 && e.RowIndex <= expenseList.Count - 1)) return;
                //DataGridViewCheckBoxCell checkBoxCell;
                //if ((checkBoxCell = ExpenseGrid.CurrentCell as DataGridViewCheckBoxCell) != null)
                //{
                //    if (checkBoxCell.Value == null) return;
                //    if ((bool)checkBoxCell.Value == false)
                //    {
                //        Invoice inv = expenseList[e.RowIndex];
                //        if (MessageBox.Show("Do you want to set current invoice status as completed ?", "For Invoice Id:" + inv.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                //        {
                //            bindInvoicesGridView();
                //            return;
                //        }
                //        var entry = dBConnection.Entry(inv);
                //        if (entry.State == EntityState.Detached)
                //            dBConnection.invoices.Attach(expenseList[e.RowIndex]);
                //        inv.workStatus = "C";
                //        dBConnection.SaveChanges();
                //        bindInvoicesGridView();
                //        if (MessageBox.Show("Invoice status is successfully changed to work completed. Do you want to notify customer about completion of work.", "Invoice ID:" + inv.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                //            return;
                //        Customer customer = dBConnection.customers.SingleOrDefault(m => m.ID == inv.customerID);
                //        StringBuilder sb = new StringBuilder();
                //        sb.Append("We are pleased to inform you that we have been able to complete your printing work  with Invoice ID:");
                //        sb.Append(inv.ID + " ");
                //        sb.Append("at the Viva Digital in the desired time. ");
                //        sb.Append("We are grateful for the pleasure of serving you and meeting your printing needs. ");
                //        sb.Append("If you have any questions about our service, we invite you to call us immediately at 0241 - 2977886 / 9270927886, and we will be happy to assist you. ");
                //        sb.Append("Thank you for joining Viva Digital.");
                //        string subject = "Completion of work";
                //        string workCompletionMessage = sb.ToString();
                //        if (!string.IsNullOrWhiteSpace(customer.MobileNumber))
                //        {
                //            VivaSMSServices.SendSMS(customer.MobileNumber, workCompletionMessage);
                //            MessageBox.Show("Work Completion Message was sent on mobile number " + customer.MobileNumber, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //        if (!string.IsNullOrWhiteSpace(customer.EmailId))
                //        {
                //            VivaEmailServices.SendEmail(customer.EmailId, subject, workCompletionMessage);
                //            MessageBox.Show("Work Completion Message was sent on email id " + customer.EmailId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //        MessageBox.Show("No mobile number or email ID is provided for given customer. No welcome message was sent.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    else
                //    {

                //    }
                //    return;
                //}

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


        private void InvoicesGridContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                //if (e.ClickedItem.Equals(InvoicesGridContextMenu.Items[0]))
                //{
                //    if (expenseList[rightClickRowIndex].Paid >= 0) return;
                //    if (MessageBox.Show("Do you want to show invoice for payment ? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                //        return;
                //    var f = new MainPage(expenseList[rightClickRowIndex], false);
                //    f.Show();
                //    Hide();
                //    f.FormClosed += (s, ev) => { FindInvoicesAndBindGrid(); Show(); };
                //}
                //if (e.ClickedItem.Equals(InvoicesGridContextMenu.Items[1]))
                //{
                //    var f = new MainPage(expenseList[rightClickRowIndex], true);
                //    f.Show();
                //    Hide();
                //    f.FormClosed += (s, ev) => { Show(); };
                //}
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

        private void InvoicesGrid_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    rightClickRowIndex = ExpenseGrid.HitTest(e.X, e.Y).RowIndex;
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

        private void InvoicesGridContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //InvoicesGridContextMenu.Items[0].Enabled = true;
                //if (expenseList[rightClickRowIndex].Paid >= 0)
                //    InvoicesGridContextMenu.Items[0].Enabled = false;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            if (!(rightClickRowIndex >= 0))
            {
                e.Cancel = true;
                return;
            }

        }

        private void findById_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as RadioButton).Checked)
                    FindInvoicesAndBindGrid();
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

        private void InvoicesGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode.ToString().ToLower())
                {
                    //case "delete":
                    //    if (ExpenseGrid.SelectedRows.Count == 0) return;
                    //    if (MessageBox.Show("Are you sure want to delete selected invoice records", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                    //        return;
                    //    foreach (DataGridViewRow r in ExpenseGrid.SelectedRows)
                    //    {
                    //        if (expenseList[r.Index].Paid < 0)
                    //        {
                    //            Invoice ele = expenseList[r.Index];
                    //            var entry = dBConnection.Entry(ele);
                    //            if (entry.State == EntityState.Detached)
                    //                dBConnection.invoices.Attach(ele);
                    //            expenseList.RemoveAt(r.Index);
                    //            dBConnection.invoices.Remove(ele);
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Invoices with complete payment can't be deleted.Because it will lead to errors in the accounting data.", "For Invoice ID:" + expenseList[r.Index].ID, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        }
                    //    }
                    //    dBConnection.SaveChanges();
                    //    bindInvoicesGridView();
                    //    break;
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

        private void FilterByDate_CheckedChanged(object sender, EventArgs e)
        {
           FindInvoicesAndBindGrid();
        }

        private void dtpForStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as DateTimePicker).Value.Date > DateTime.Now.Date)
                {
                    (sender as DateTimePicker).Value = DateTime.Now;
                    MessageBox.Show("Selected date should not be greater than today.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (dtpForStartDate.Value.Date > dtpForEndDate.Value.Date)
                    MessageBox.Show("Start date should not be greater than end date.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    FindInvoicesAndBindGrid();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>=6)
                FindInvoicesAndBindGrid();
        }
    }
}