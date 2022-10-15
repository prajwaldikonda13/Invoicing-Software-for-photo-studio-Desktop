using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddProduct : Form
    {
        List<Product> productList;
        DataTable dt;
        List<Product> productsToDeleteList;
        DBConnection dBConnection;
        public AddProduct()
        {
            InitializeComponent();
            initVariables();
            BindGridViewData();
        }
        private void initVariables()
        {
            dt = new DataTable();
            productsToDeleteList = new List<Product>();
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
                productList = dBConnection.products.ToList();
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
                dt.Columns.Add("Product");
                foreach (var s in productList)
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

        private void DeleteDatabaseEntry(Product element, bool callBindGridView)
        {
            try
            {
                Price temp = dBConnection.prices.FirstOrDefault(m => m.ProductID == element.ID);
                if (temp != null)
                {
                    MessageBox.Show("Selected product can't be deleted because it is assigned to a price combination in the database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.products.Attach(element);
                dBConnection.products.Remove(element);
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
                string productName;
                if (e.RowIndex == productList.Count)
                {
                    res = MessageBox.Show("Are you sure you want to add new entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res.ToString() != "Yes")
                    {
                        BindGridViewData();
                        return;
                    }
                    productName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", true, false);
                    if (CheckIsNullOrEmpty(productName, true))
                    {
                        BindGridViewData();
                        return;
                    }
                    InsertEntryToDatabase(new Product { Name = productName });
                    return;
                }
                res = MessageBox.Show("Are you sure you want to update selected entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res.ToString() != "Yes")
                {
                    BindGridViewData();
                    return;
                }
                productName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", true, false);
                if (CheckIsNullOrEmpty(productName, true))
                {
                    BindGridViewData();
                    return;
                }
                var element = productList[e.RowIndex];
                UpdateDatabaseEntry(element, productName);
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

        private void InsertEntryToDatabase(Product product)
        {
            try
            {
                if (product == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                if (IsDBEntryExists(product.Name, true))
                {
                    BindGridViewData();
                    return;
                }
                dBConnection.products.Add(product);
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

        private bool IsDBEntryExists(string productName, bool showMessageBox)
        {
            try
            {
                if (dBConnection.products.SingleOrDefault(m => m.Name == productName) != null)
                {
                    if (showMessageBox)
                        MessageBox.Show("Product with given name already exists.Please enter another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void InsertEntryToDatabase(List<Product> products)
        {
            try
            {
                if (products == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                List<Product> notInsertedList = new List<Product>();
                foreach (Product product in products)
                {
                    if (!IsDBEntryExists(product.Name, false))
                    {
                        dBConnection.products.Add(product);
                    }
                    else
                        notInsertedList.Add(product);
                }
                dBConnection.SaveChanges();
                string message = "Following records are not inserted to database because they are already in the database." + Environment.NewLine;
                foreach (Product product in notInsertedList)
                {
                    message += product.Name + Environment.NewLine;
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

        private void UpdateDatabaseEntry(Product element, string productName)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                productName = StaticFunctions.getValidString(productName, true, false);
                if (CheckIsNullOrEmpty(productName, true))
                {
                    BindGridViewData();
                    return;
                }

                if (IsDBEntryExists(productName, true))
                {
                    BindGridViewData();
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.products.Attach(element);
                element.Name = productName;
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

        private bool CheckIsNullOrEmpty(string productName, bool showMessageBox)
        {
            try
            {
                if (string.IsNullOrEmpty(productName) || string.IsNullOrWhiteSpace(productName))
                {
                    if (showMessageBox)
                        MessageBox.Show("Please enter non empty Product name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (r.Index >= 0 && r.Index <= productList.Count - 1)
                        DeleteDatabaseEntry(productList[r.Index], false);
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