using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class FindInvoices : Form
    {
        DBConnection dBConnection;
        List<Invoice> invoiceList;
        List<Customer> customerList;
        long ID;
        private Customer customer;
        string Customername;
        DateTime InvoiceDateTime;
        DateTime PaymentDateTime;
        float Total,Previous, Grand = 0,Discount,Final, Paid,Due,BalAfterPay,totalDue,totalPaid,totalDiscount;
        private int rightClickRowIndex;

        public FindInvoices()
        {
            InitializeComponent();
            initVariables();
            InvoicesGrid.AutoSize = true;
        }

        private void initVariables()
        {
            invoiceList = new List<Invoice>();
            customerList = new List<Customer>();
            ID = 0;
            Customername = "";
            InvoiceDateTime = DateTime.Now;
            PaymentDateTime = DateTime.Now;
            Total=Previous=Grand=Discount=Final=Paid=Due=BalAfterPay=totalDue=totalPaid=totalDiscount = 0;
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
                InvoicesGrid.Rows.Clear();
                InvoicesGrid.Columns.Clear();
                InvoicesGrid.Columns.Add("ID", "ID");
                InvoicesGrid.Columns.Add("CustomerName", "Customer Name");
                InvoicesGrid.Columns.Add("InvoiceDateTime", "Invoice Date & Time");
                InvoicesGrid.Columns.Add("PaymentDateTime", "Payment Date & Time");
                InvoicesGrid.Columns.Add("Total", "Total");
                InvoicesGrid.Columns.Add("PrevBal", "Prev Bal/Due");
                InvoicesGrid.Columns.Add("GrandTotal", "Grand Total");
                InvoicesGrid.Columns.Add("Discount", "Discount");
                InvoicesGrid.Columns.Add("FinalPrice", "Final Price");
                InvoicesGrid.Columns.Add("Paid", "Paid");
                InvoicesGrid.Columns.Add("Due", "Due");
                InvoicesGrid.Columns.Add("CurrentBal", "CurrentBal");
                InvoicesGrid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Work Status", Name = "WorkStatus" });
                for (int i = 0; i < invoiceList.Count; i++)
                {
                    long custId = invoiceList[i].customerID;
                    customer = customerList.SingleOrDefault(m => m.ID == custId);
                    ID = invoiceList[i].ID;
                    Customername = customer.FirstName + " " + customer.LastName + "," + customer.FirmName + "," + customer.CustomerType;
                    InvoiceDateTime = invoiceList[i].InvoiceDateTime;
                    PaymentDateTime = invoiceList[i].PaymentDateTime;
                    //Total=found...
                    Previous = invoiceList[i].PrevBal;
                    Grand = invoiceList[i].FinalPrice + invoiceList[i].Discount;
                    Discount = invoiceList[i].Discount;
                    Final = invoiceList[i].FinalPrice;
                    Paid = invoiceList[i].Paid;
                    //Due=found...
                    BalAfterPay = invoiceList[i].CurrentBal;

                    if (Grand != 0)
                        Total = Grand + invoiceList[i].PrevBal;//invoicesList[rowIndex].PrevBal + invoicesList[rowIndex].FinalPrice + "";
                    else
                        Total = invoiceList[i].Paid + invoiceList[i].PrevBal - invoiceList[i].CurrentBal;//invoicesList[rowIndex].PrevBal + invoicesList[rowIndex].FinalPrice + "";


                    float val = invoiceList[i].FinalPrice - invoiceList[i].Paid;
                    if (val < 0)
                        Due = 0;
                    else
                        Due = val;


                    if (Paid >= 0)
                    {
                        totalPaid += Paid;
                        totalDue += Due;
                        totalDiscount += Discount;
                    }

                    if (invoiceList[i].CurrentBal < 0)
                        BalAfterPay = 0;
                    else
                        BalAfterPay = invoiceList[i].CurrentBal;


                    if (invoiceList[i].PrevBal < 0)
                    {
                        Previous = -1 * invoiceList[i].PrevBal;
                        //InvoicesGrid["PrevBal", i].Style.ForeColor = Color.Red;
                        //PrevBal.ForeColor = Color.Red;
                    }
                    else
                    {
                        Previous = invoiceList[i].PrevBal;
                        //InvoicesGrid["PrevBal", i].Style.ForeColor = Color.Green;
                        // PrevBal.ForeColor = Color.Green;

                    }
                    bool status;
                    if (invoiceList[i].workStatus.ToLower() == "nc")
                        status = false;
                    else
                        status = true;
                    if (invoiceList[i].Paid >= 0)
                    {
                        InvoicesGrid.Rows.Add(new object[] { ID, Customername, InvoiceDateTime, PaymentDateTime, Total, Previous, Grand, Discount, Final, Paid, Due, BalAfterPay, status });
                    }
                    else
                    {
                        InvoicesGrid.Rows.Add(new object[] { ID, Customername, InvoiceDateTime, "", Total, "", "", "", "", "", "", "", status });
                    }

                    if (invoiceList[i].PrevBal < 0)
                    {
                        // Previous = -1 * invoiceList[i].PrevBal;
                        InvoicesGrid["PrevBal", i].Style.ForeColor = Color.Red;
                        //PrevBal.ForeColor = Color.Red;
                    }
                    else
                    {
                        // Previous = invoiceList[i].PrevBal;
                        InvoicesGrid["PrevBal", i].Style.ForeColor = Color.Green;
                        // PrevBal.ForeColor = Color.Green;

                    }
                    InvoicesGrid["Due", i].Style.ForeColor = Color.Red;
                    InvoicesGrid.Rows[i].Cells[12].ReadOnly = false;
                    setRowReadOnly(i);
                }
                lblTotalPaid.Text = "Total Paid:" + totalPaid;
                lblTotalDiscount.Text = "Total Discount:" + totalDiscount;
                lblTotalDue.Text = "Total Due:" + totalDue;
                totalPaid = 0;
                totalDiscount = 0;
                totalDue = 0;
                InvoicesGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                InvoicesGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                InvoicesGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                InvoicesGrid.Rows[rowIndex].Cells["ID"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["CustomerName"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["InvoiceDateTime"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["PaymentDateTime"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["Total"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["PrevBal"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["GrandTotal"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["Discount"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["FinalPrice"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["Paid"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["Due"].ReadOnly = true;
                InvoicesGrid.Rows[rowIndex].Cells["CurrentBal"].ReadOnly = true;
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
                invoiceList.Clear();
                string inputString = InputBox.Text;
                if (inputString.Length < 1 && !findByDate.Checked) return;
                DateTime startDateTime = dtpForStartDate.Value;
                DateTime endDateTime = dtpForEndDate.Value;
                if (findById.Checked)
                {
                    if (!StaticFunctions.IsInteger(inputString))
                    {
                        MessageBox.Show("Please enter the valid input ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    long id = Convert.ToInt64(inputString);
                    if (FilterByDate.Checked)
                        invoiceList = dBConnection.invoices.Where(m => m.ID == id && DbFunctions.TruncateTime(m.InvoiceDateTime) >= DbFunctions.TruncateTime(startDateTime) && DbFunctions.TruncateTime(m.InvoiceDateTime) <= DbFunctions.TruncateTime(endDateTime)).ToList();
                    else
                        invoiceList = dBConnection.invoices.Where(m => m.ID == id).ToList();

                    long custID = invoiceList[0].customerID;
                    customerList.Add(dBConnection.customers.SingleOrDefault(m => m.ID == custID));
                }
                else if (findByMobile.Checked)
                {
                    if (!StaticFunctions.IsInteger(inputString))
                    {
                        MessageBox.Show("Please enter the valid mobile number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    Customer customer = dBConnection.customers.SingleOrDefault(m => m.MobileNumber == inputString);
                    if (customer == null)
                    {
                        MessageBox.Show("Customer with given mobile number does not exist.Please provide another number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bindInvoicesGridView();
                        return;
                    }
                    customerList.Add(customer);
                    if (FilterByDate.Checked)

                        invoiceList = dBConnection.invoices.Where(m => m.customerID == customer.ID && DbFunctions.TruncateTime(m.InvoiceDateTime) >= DbFunctions.TruncateTime(startDateTime) && DbFunctions.TruncateTime(m.InvoiceDateTime) <= DbFunctions.TruncateTime(endDateTime)).ToList();
                    else
                        invoiceList = dBConnection.invoices.Where(m => m.customerID == customer.ID).ToList();

                }
                else if (findByEmailId.Checked)
                {
                    if (!StaticFunctions.IsValidEmail(inputString))
                    {
                        MessageBox.Show("Please enter the valid email ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bindInvoicesGridView();
                        return;
                    }
                    Customer customer = dBConnection.customers.SingleOrDefault(m => m.EmailId == inputString);
                    if (customer == null)
                    {
                        MessageBox.Show("Customer with given email ID does not exist.Please provide another email ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bindInvoicesGridView();
                        return;
                    }
                    customerList.Add(customer);
                    if (FilterByDate.Checked)

                        invoiceList = dBConnection.invoices.Where(m => m.customerID == customer.ID && DbFunctions.TruncateTime(m.InvoiceDateTime) >= DbFunctions.TruncateTime(startDateTime) && DbFunctions.TruncateTime(m.InvoiceDateTime) <= DbFunctions.TruncateTime(endDateTime)).ToList();
                    else
                        invoiceList = dBConnection.invoices.Where(m => m.customerID == customer.ID).ToList();

                }
                else if (findByDate.Checked)
                {

                    if (startDateTime > endDateTime)
                    {
                        MessageBox.Show("Start date should not be greater than end date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bindInvoicesGridView();
                        return;
                    }
                    invoiceList = dBConnection.invoices.Where(m => DbFunctions.TruncateTime(m.InvoiceDateTime) >= DbFunctions.TruncateTime(startDateTime) && DbFunctions.TruncateTime(m.InvoiceDateTime) <= DbFunctions.TruncateTime(endDateTime)).ToList();
                    foreach (Invoice inv in invoiceList)
                    {
                        if (customerList.SingleOrDefault(m => m.ID == inv.customerID) == null)
                            customerList.Add(dBConnection.customers.SingleOrDefault(m => m.ID == inv.customerID));
                    }
                }
                bindInvoicesGridView();
                if (!(invoiceList.Count > 0))
                    MessageBox.Show("Invoices not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!(e.RowIndex >= 0 && e.RowIndex <= invoiceList.Count - 1)) return;
                DataGridViewCheckBoxCell checkBoxCell;
                if ((checkBoxCell = InvoicesGrid.CurrentCell as DataGridViewCheckBoxCell) != null)
                {
                    if (checkBoxCell.Value == null) return;
                    if ((bool)checkBoxCell.Value == false)
                    {
                        Invoice inv = invoiceList[e.RowIndex];
                        if (MessageBox.Show("Do you want to set current invoice status as completed ?", "For Invoice Id:" + inv.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                        {
                            bindInvoicesGridView();
                            return;
                        }
                        var entry = dBConnection.Entry(inv);
                        if (entry.State == System.Data.Entity.EntityState.Detached)
                            dBConnection.invoices.Attach(invoiceList[e.RowIndex]);
                        inv.workStatus = "C";
                        dBConnection.SaveChanges();
                        bindInvoicesGridView();
                        if (MessageBox.Show("Invoice status is successfully changed to work completed. Do you want to notify customer about completion of work.", "Invoice ID:" + inv.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                            return;
                        Customer customer = dBConnection.customers.SingleOrDefault(m => m.ID == inv.customerID);
                        StringBuilder sb = new StringBuilder();
                        sb.Append("We are pleased to inform you that we have completed your printing work  with Invoice ID:");
                        sb.Append(inv.ID + ". ");
                        sb.Append("Please visit Viva Digital to receive your order.");
                        sb.Append("Amount:"+inv.FinalPrice+" Rs.");
                        //sb.Append("We are grateful for the pleasure of serving you and meeting your printing needs. ");
                        //sb.Append("If you have any questions about our service, we invite you to call us immediately at 0241 - 2977886 / 9270927886, and we will be happy to assist you. ");
                        //sb.Append("Thank you for joining Viva Digital.");
                        string subject = "Completion of work";
                        string workCompletionMessage = sb.ToString();
                        if (!string.IsNullOrWhiteSpace(customer.MobileNumber))
                        {
                            VivaSMSServices.SendSMS(customer.MobileNumber, workCompletionMessage);
                            MessageBox.Show("Work Completion Message was sent on mobile number " + customer.MobileNumber, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (!string.IsNullOrWhiteSpace(customer.EmailId))
                        {
                            VivaEmailServices.SendEmail(customer.EmailId, subject, workCompletionMessage);
                            MessageBox.Show("Work Completion Message was sent on email id " + customer.EmailId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        MessageBox.Show("No mobile number or email ID is provided for given customer. No welcome message was sent.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                    }
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


        private void InvoicesGridContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Equals(InvoicesGridContextMenu.Items[0]))
                {
                    if (invoiceList[rightClickRowIndex].Paid >= 0) return;
                    if (MessageBox.Show("Do you want to show invoice for payment ? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                        return;
                    var f = new MainPage(invoiceList[rightClickRowIndex], false);
                    f.Show();
                    Hide();
                    f.FormClosed += (s, ev) => { FindInvoicesAndBindGrid(); Show(); };
                }
                if (e.ClickedItem.Equals(InvoicesGridContextMenu.Items[1]))
                {
                    var f = new MainPage(invoiceList[rightClickRowIndex], true);
                    f.Show();
                    Hide();
                    f.FormClosed += (s, ev) => { Show(); };
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

        private void InvoicesGrid_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    rightClickRowIndex = InvoicesGrid.HitTest(e.X, e.Y).RowIndex;
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
                InvoicesGridContextMenu.Items[0].Enabled = true;
                if (invoiceList[rightClickRowIndex].Paid >= 0)
                    InvoicesGridContextMenu.Items[0].Enabled = false;
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
                    case "delete":
                        if (InvoicesGrid.SelectedRows.Count == 0) return;
                        if (MessageBox.Show("Are you sure want to delete selected invoice records", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                            return;
                        foreach (DataGridViewRow r in InvoicesGrid.SelectedRows)
                        {
                            if (invoiceList[r.Index].Paid < 0)
                            {
                                Invoice ele = invoiceList[r.Index];
                                var entry = dBConnection.Entry(ele);
                                if (entry.State == EntityState.Detached)
                                    dBConnection.invoices.Attach(ele);
                                invoiceList.RemoveAt(r.Index);
                                dBConnection.invoices.Remove(ele);
                            }
                            else
                            {
                                MessageBox.Show("Invoices with complete payment can't be deleted.Because it will lead to errors in the accounting data.", "For Invoice ID:" + invoiceList[r.Index].ID, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        dBConnection.SaveChanges();
                        bindInvoicesGridView();
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