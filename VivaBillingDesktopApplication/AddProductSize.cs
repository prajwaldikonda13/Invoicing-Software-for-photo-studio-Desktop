using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddProductSize : Form
    {
        List<Size> sizeList;
        DataTable dt;
        List<Size> sizesToDeleteList;
        DBConnection dBConnection;
        public AddProductSize()
        {
            InitializeComponent();
            initVariables();
            BindGridViewData();
        }
        private void initVariables()
        {
             dt = new DataTable();
            sizesToDeleteList = new List<Size>();
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
                sizeList = dBConnection.sizes.ToList();
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
                dt.Columns.Add("Size");
                foreach (var s in sizeList)
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

        private void DeleteDatabaseEntry(Size element, bool callBindGridView)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Price temp = dBConnection.prices.FirstOrDefault(m => m.ProductSizeID == element.ID);
                if (temp != null)
                {
                    MessageBox.Show("Selected size can't be deleted because it is assigned to a price in the database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.sizes.Attach(element);
                dBConnection.sizes.Remove(element);
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
                string sizeName;
                if (e.RowIndex == sizeList.Count)
                {
                    res = MessageBox.Show("Are you sure you want to add new entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res.ToString() != "Yes")
                    {
                        BindGridViewData();
                        return;
                    }
                    sizeName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", false, false);
                    if (CheckIsNullOrEmpty(sizeName, true))
                    {
                        BindGridViewData();
                        return;
                    }
                    InsertEntryToDatabase(new Size { Name = sizeName });
                    return;
                }
                res = MessageBox.Show("Are you sure you want to update selected entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res.ToString() != "Yes")
                {
                    BindGridViewData();
                    return;
                }
                sizeName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", false, false);
                if (CheckIsNullOrEmpty(sizeName, true))
                {
                    BindGridViewData();
                    return;
                }
                var element = sizeList[e.RowIndex];
                UpdateDatabaseEntry(element, sizeName);
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

        private void InsertEntryToDatabase(Size size)
        {
            try
            {
                if (size == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                if (IsDBEntryExists(size.Name, true))
                {
                    BindGridViewData();
                    return;
                }
                dBConnection.sizes.Add(size);
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

        private bool IsDBEntryExists(string sizeName, bool showMessageBox)
        {
            try
            {
                if (dBConnection.sizes.SingleOrDefault(m => m.Name == sizeName) != null)
                {
                    if (showMessageBox)
                        MessageBox.Show("Size with given name already exists.Please enter another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void InsertEntryToDatabase(List<Size> sizes)
        {
            try
            {
                if (sizes == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                List<Size> notInsertedList = new List<Size>();
                foreach (Size size in sizes)
                {
                    if (!IsDBEntryExists(size.Name, false))
                    {
                        dBConnection.sizes.Add(size);
                    }
                    else
                        notInsertedList.Add(size);
                }
                dBConnection.SaveChanges();
                string message = "Following records are not inserted to database because they are already in the database." + Environment.NewLine;
                foreach (Size size in notInsertedList)
                {
                    message += size.Name + Environment.NewLine;
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

        private void UpdateDatabaseEntry(Size element, string sizeName)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                sizeName = StaticFunctions.getValidString(sizeName, false, false);
                if (CheckIsNullOrEmpty(sizeName, true))
                {
                    BindGridViewData();
                    return;
                }

                if (IsDBEntryExists(sizeName, true))
                {
                    BindGridViewData();
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.sizes.Attach(element);
                element.Name = sizeName;
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

        private bool CheckIsNullOrEmpty(string sizeName, bool showMessageBox)
        {
            try
            {
                if (string.IsNullOrEmpty(sizeName) || string.IsNullOrWhiteSpace(sizeName))
                {
                    if (showMessageBox)
                        MessageBox.Show("Please enter non empty Size name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (r.Index >= 0 && r.Index <= sizeList.Count - 1)
                        DeleteDatabaseEntry(sizeList[r.Index], false);
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

        private void grid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
        }

    }
}