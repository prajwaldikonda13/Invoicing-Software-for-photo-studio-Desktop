using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddPrice : Form
    {
        List<Price> priceList;
        DataTable dt;
        List<Price> pricesToDeleteList;
        DBConnection dBConnection;

        public AddPrice()
        {
            InitializeComponent();
            initVariables();
            BindGridViewData();
        }
        private void initVariables()
        {
            pricesToDeleteList = new List<Price>();
            dt = new DataTable();
            grid.DataError += Grid_DataError;
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
        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private void BindGridViewData()
        {
            try
            {
                priceList = dBConnection.prices.ToList();
                grid.Rows.Clear();
                grid.Columns.Clear();
                grid.Columns.Add("ID", "ID");
                grid.Columns.Add("Product", "Product");
                grid.Columns.Add("ProductType", "Product Type");
                grid.Columns.Add("ProductSize", "Product Size");
                grid.Columns.Add("CounterPrice", "Counter Price");
                grid.Columns.Add("RegularPrice", "RegularPrice");
                string name;
                string type;
                string size;
                foreach (var s in priceList)
                {
                    name = dBConnection.products.SingleOrDefault(m => m.ID == s.ProductID).Name;
                    type = dBConnection.productTypes.SingleOrDefault(m => m.ID == s.ProductTypeID).Name;
                    size = dBConnection.sizes.SingleOrDefault(m => m.ID == s.ProductSizeID).Name;
                    grid.Rows.Add(new object[] { s.ID, name, type, size, s.CounterPrice, s.RegularPrice });
                }
                grid.Rows.Add();
                grid.Columns[0].ReadOnly = true;
                grid.Columns[1].ReadOnly = true;
                grid.Columns[2].ReadOnly = true;
                grid.Columns[3].ReadOnly = true;
                grid.Rows[grid.RowCount - 1].Cells[1] = new DataGridViewComboBoxCell { DataSource = dBConnection.products.ToList(), DisplayMember = "Name", ValueMember = "ID" };
                grid.Rows[grid.RowCount - 1].Cells[2] = new DataGridViewComboBoxCell { DataSource = dBConnection.productTypes.ToList(), DisplayMember = "Name", ValueMember = "ID" };
                grid.Rows[grid.RowCount - 1].Cells[3] = new DataGridViewComboBoxCell { DataSource = dBConnection.sizes.ToList(), DisplayMember = "Name", ValueMember = "ID" };


                grid.Rows[grid.RowCount - 1].Cells[1].ReadOnly =false;
                grid.Rows[grid.RowCount - 1].Cells[2].ReadOnly = false;
                grid.Rows[grid.RowCount - 1].Cells[3].ReadOnly = false;


                grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

        private void DeleteDatabaseEntry(Price element, bool callBindGridView)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Job temp = dBConnection.jobs.FirstOrDefault(m => m.ProductID == element.ProductID && m.ProductTypeID==element.ProductTypeID && m.ProductSizeID==element.ProductSizeID);
                if (temp != null)
                {
                    MessageBox.Show("Selected price can't be deleted because it is assigned for job(s)  in invoice in the database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.prices.Attach(element);
                dBConnection.prices.Remove(element);
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
           

        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                DialogResult res;
                float counterPrice;
                float regularPrice;
                if (e.RowIndex == priceList.Count)
                {
                    DataGridViewComboBoxCell productCell = (DataGridViewComboBoxCell)grid.Rows[e.RowIndex].Cells[1];
                    DataGridViewComboBoxCell productTypeCell = (DataGridViewComboBoxCell)grid.Rows[e.RowIndex].Cells[2];
                    DataGridViewComboBoxCell productSizeCell = (DataGridViewComboBoxCell)grid.Rows[e.RowIndex].Cells[3];
                    DataGridViewTextBoxCell counterPriceCell = (DataGridViewTextBoxCell)grid.Rows[e.RowIndex].Cells[4];
                    DataGridViewTextBoxCell regularPriceCell = (DataGridViewTextBoxCell)grid.Rows[e.RowIndex].Cells[5];
                    if (productCell == null || productTypeCell == null || productSizeCell == null)
                        return;
                    if (productCell.Value == null || productTypeCell.Value == null || productSizeCell.Value == null)
                        return;
                    string productCellValue = productCell.Value.ToString();
                    string productTypeCellValue = productTypeCell.Value.ToString();
                    string productSizeCellValue = productSizeCell.Value.ToString();
                    if(regularPriceCell.Value != null)
                    {
                        if (counterPriceCell.Value == null)
                            MessageBox.Show("Please enter non empty Counter Price.","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    if (counterPriceCell.Value == null || regularPriceCell.Value == null)
                    {
                        return;
                    }
                    string counterPriceCellString = counterPriceCell.Value.ToString();
                    string regularPriceCellString = regularPriceCell.Value.ToString();


                    res = MessageBox.Show("Are you sure you want to add new entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res.ToString() != "Yes")
                    {
                        BindGridViewData();
                        return;
                    }
                    counterPrice = (float)Convert.ToDecimal(grid[4, e.RowIndex].Value);
                    regularPrice = (float)Convert.ToDecimal(grid[5, e.RowIndex].Value);
                    InsertEntryToDatabase(new Price { CounterPrice = counterPrice, RegularPrice = regularPrice, ProductID = Convert.ToInt64(grid[1, e.RowIndex].Value), ProductTypeID = Convert.ToInt64(grid[2, e.RowIndex].Value), ProductSizeID = Convert.ToInt64(grid[3, e.RowIndex].Value) });
                    return;
                }
                res = MessageBox.Show("Are you sure you want to update selected entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res.ToString() != "Yes")
                {
                    BindGridViewData();
                    return;
                }
                counterPrice = (float)Convert.ToDecimal(grid[4, e.RowIndex].Value);
                regularPrice = (float)Convert.ToDecimal(grid[5, e.RowIndex].Value);
                if (CheckIsNullOrEmpty(grid[4, e.RowIndex].Value.ToString(), false) || CheckIsNullOrEmpty(grid[5, e.RowIndex].Value.ToString(), false))
                {
                    MessageBox.Show("Please enter non empty Regular Price and Customer Price");
                    BindGridViewData();
                    return;
                }
                var element = priceList[e.RowIndex];
                UpdateDatabaseEntry(element, counterPrice, regularPrice);
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

        private void UpdateDatabaseEntry(Price element, float counterPrice, float regularPrice)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                if (CheckIsNullOrEmpty(counterPrice + "", false) || CheckIsNullOrEmpty(regularPrice + "", false))
                {
                    MessageBox.Show("Please enter non empty Regular Price and Customer Price");
                    BindGridViewData();
                    return;
                }

                var entry = dBConnection.Entry(element);
                if (entry == null) return;
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.prices.Attach(element);
                element.CounterPrice = counterPrice;
                element.RegularPrice = regularPrice;
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

        private void InsertEntryToDatabase(Price price)
        {
            try
            {
                if (price == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                if (IsDBEntryExists(price, true))
                {
                    BindGridViewData();
                    return;
                }
                dBConnection.prices.Add(price);
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

        private bool IsDBEntryExists(Price PriceObject, bool showMessageBox)
        {
            try
            {
                if (dBConnection.prices.SingleOrDefault(m => m.ProductID==PriceObject.ProductID && m.ProductSizeID==PriceObject.ProductSizeID && m.ProductTypeID==PriceObject.ProductTypeID) != null)
                {
                    if (showMessageBox)
                        MessageBox.Show("Price with given combination already exists.Please enter another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void InsertEntryToDatabase(List<Price> prices)
        {
            try
            {
                if (prices == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                List<Price> notInsertedList = new List<Price>();
                foreach (Price price in prices)
                {
                    if (!IsDBEntryExists(price, false))
                    {
                        dBConnection.prices.Add(price);
                    }
                    else
                        notInsertedList.Add(price);
                }
                dBConnection.SaveChanges();
                string message = "Following records are not inserted to database because they are already in the database." + Environment.NewLine;
                foreach (Price price in notInsertedList)
                {
                    message += price.CounterPrice + "" + Environment.NewLine;
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

        private void UpdateDatabaseEntry(Price element, string price)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                price = StaticFunctions.getValidString(price, false, false);
                if (CheckIsNullOrEmpty(price, true))
                {
                    BindGridViewData();
                    return;
                }

                if (IsDBEntryExists(element, true))
                {
                    BindGridViewData();
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.prices.Attach(element);
                element.CounterPrice = (float)Convert.ToDecimal(price);
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

        private bool CheckIsNullOrEmpty(string price, bool showMessageBox)
        {
            try
            {
                if (string.IsNullOrEmpty(price) || string.IsNullOrWhiteSpace(price))
                {
                    if (showMessageBox)
                        MessageBox.Show("Please enter non empty Price name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (r.Index >= 0 && r.Index <= priceList.Count - 1)
                        DeleteDatabaseEntry(priceList[r.Index], false);
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
        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewComboBoxCell currentComboBoxCell = grid.CurrentCell as DataGridViewComboBoxCell;
                if (currentComboBoxCell == null) return;
                grid.BeginEdit(true);
                (grid.EditingControl as ComboBox).DroppedDown = true;
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