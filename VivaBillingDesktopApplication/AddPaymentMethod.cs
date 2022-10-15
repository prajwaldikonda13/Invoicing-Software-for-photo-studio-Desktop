using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddPaymentMethod : Form
    {
        List<PaymentMethod> paymentMethodList;
        DataTable dt;
        List<PaymentMethod> paymentMethodsToDeleteList;
        DBConnection dBConnection;
        public AddPaymentMethod()
        {
            InitializeComponent();
            initVariables();
            BindGridViewData();
        }
        private void initVariables()
        {
            dt = new DataTable();
            paymentMethodsToDeleteList = new List<PaymentMethod>();
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
        private void BindGridViewData()
        {
            try
            {
                paymentMethodList = dBConnection.paymentMethods.ToList();
                AddListEntriesToDataTable();
                grid.DataSource = dt;
                grid.Columns[0].ReadOnly = true;
                grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

        private void AddListEntriesToDataTable()
        {
            try
            {
                dt.Clear();
                dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Payment Method");
                foreach (var s in paymentMethodList)
                {
                    dt.Rows.Add(new object[] { s.ID, s.Name });
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

        private void DeleteDatabaseEntry(PaymentMethod element, bool callBindGridView)
        {
            try
            {
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.paymentMethods.Attach(element);
                dBConnection.paymentMethods.Remove(element);
                dBConnection.SaveChanges();
                if (callBindGridView)
                    BindGridViewData();
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (element == null)
            {
                MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DialogResult res;
                string paymentMethodName;
                if (e.RowIndex == paymentMethodList.Count)
                {
                    res = MessageBox.Show("Are you sure you want to add new entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res.ToString() != "Yes")
                    {
                        BindGridViewData();
                        return;
                    }
                    paymentMethodName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", true, false);
                    if (CheckIsNullOrEmpty(paymentMethodName, true))
                    {
                        BindGridViewData();
                        return;
                    }
                    InsertEntryToDatabase(new PaymentMethod { Name = paymentMethodName });
                    return;
                }
                res = MessageBox.Show("Are you sure you want to update selected entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res.ToString() != "Yes")
                {
                    BindGridViewData();
                    return;
                }
                paymentMethodName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", true, false);
                if (CheckIsNullOrEmpty(paymentMethodName, true))
                {
                    BindGridViewData();
                    return;
                }
                var element = paymentMethodList[e.RowIndex];
                UpdateDatabaseEntry(element, paymentMethodName);
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

        private void InsertEntryToDatabase(PaymentMethod paymentMethod)
        {
            try
            {
                if (paymentMethod == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                if (IsDBEntryExists(paymentMethod.Name, true))
                {
                    BindGridViewData();
                    return;
                }
                dBConnection.paymentMethods.Add(paymentMethod);
                dBConnection.SaveChanges();
                BindGridViewData();
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

        private bool IsDBEntryExists(string paymentMethodName, bool showMessageBox)
        {
            try
            {
                if (dBConnection.paymentMethods.SingleOrDefault(m => m.Name == paymentMethodName) != null)
                {
                    if (showMessageBox)
                        MessageBox.Show("Payment Method with given name already exists.Please enter another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }

        }

        private void InsertEntryToDatabase(List<PaymentMethod> paymentMethods)
        {
            try
            {
                if (paymentMethods == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                List<PaymentMethod> notInsertedList = new List<PaymentMethod>();
                foreach (PaymentMethod paymentMethod in paymentMethods)
                {
                    if (!IsDBEntryExists(paymentMethod.Name, false))
                    {
                        dBConnection.paymentMethods.Add(paymentMethod);
                    }
                    else
                        notInsertedList.Add(paymentMethod);
                }
                dBConnection.SaveChanges();
                string message = "Following records are not inserted to database because they are already in the database." + Environment.NewLine;
                foreach (PaymentMethod paymentMethod in notInsertedList)
                {
                    message += paymentMethod.Name + Environment.NewLine;
                }
                BindGridViewData();
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

        private void UpdateDatabaseEntry(PaymentMethod element, string paymentMethodName)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                paymentMethodName = StaticFunctions.getValidString(paymentMethodName, true, false);
                if (CheckIsNullOrEmpty(paymentMethodName, true))
                {
                    BindGridViewData();
                    return;
                }

                if (IsDBEntryExists(paymentMethodName, true))
                {
                    BindGridViewData();
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.paymentMethods.Attach(element);
                element.Name = paymentMethodName;
                dBConnection.SaveChanges();
                BindGridViewData();
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

        private bool CheckIsNullOrEmpty(string paymentMethodName, bool showMessageBox)
        {
            try
            {
                if (string.IsNullOrEmpty(paymentMethodName) || string.IsNullOrWhiteSpace(paymentMethodName))
                {
                    if (showMessageBox)
                        MessageBox.Show("Please enter non empty Payment Method name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return true;
            }

        }

        private void grid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (grid.SelectedRows.Count <= 0)
                {
                    return;
                }
                if (!(e.KeyCode.ToString().ToLower() == "delete"))
                    return;
                if (MessageBox.Show("Are you sure you want to delete selected record(s) ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                {
                    return;
                }
                foreach (DataGridViewRow r in grid.SelectedRows)
                {
                    if (r.Index >= 0 && r.Index <= paymentMethodList.Count - 1)
                        DeleteDatabaseEntry(paymentMethodList[r.Index], false);
                }
                BindGridViewData();
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

        private void grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
        }

    }
}