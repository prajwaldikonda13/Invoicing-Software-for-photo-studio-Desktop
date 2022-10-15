using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddCountry : Form
    {
        List<Country> countryList;
        DataTable dt;
        List<Country> countriesToDeleteList;
        DBConnection dBConnection;
        public AddCountry()
        {
            InitializeComponent();
            initVariables();
            BindGridViewData();
        }

        private void initVariables()
        {
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
            dt = new DataTable();
            countriesToDeleteList = new List<Country>(); 
        }

        private void BindGridViewData()
        {
            try
            {
                countryList = dBConnection.countries.ToList();
                AddListEntriesToDataTable();
                grid.DataSource = dt;
                grid.Columns[0].ReadOnly = true;
                grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                if(Program.showExceptionsInMessageBox)
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
                dt.Columns.Add("Country");
                foreach (var s in countryList)
                {
                    dt.Rows.Add(new object[] { s.ID, s.Name });
                }
            }
            catch (Exception ex)
            {
                if(Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
           
        }


        private void DeleteDatabaseEntry(Country element, bool callBindGridView)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Customer temp = dBConnection.customers.FirstOrDefault(m => m.Country == element.ID);
                if (temp != null)
                {
                    MessageBox.Show("Selected country can't be deleted because it is assigned to a customer in the database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.countries.Attach(element);
                dBConnection.countries.Remove(element);
                dBConnection.SaveChanges();
                if (callBindGridView)
                    BindGridViewData();
            }
            catch (Exception ex)
            {
               if(Program.showExceptionsInMessageBox)
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
                string countryName;
                if (e.RowIndex == countryList.Count)
                {
                    res = MessageBox.Show("Are you sure you want to add new entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res.ToString() != "Yes")
                    {
                        BindGridViewData();
                        return;
                    }
                    countryName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", false, false);
                    if (CheckIsNullOrEmpty(countryName, true))
                    {
                        BindGridViewData();
                        return;
                    }
                    InsertEntryToDatabase(new Country { Name = countryName });
                    return;
                }
                res = MessageBox.Show("Are you sure you want to update selected entry ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res.ToString() != "Yes")
                {
                    BindGridViewData();
                    return;
                }
                countryName = StaticFunctions.getValidString(grid.CurrentCell.Value + "", false, false);
                if (CheckIsNullOrEmpty(countryName, true))
                {
                    BindGridViewData();
                    return;
                }
                var element = countryList[e.RowIndex];
                UpdateDatabaseEntry(element, countryName);
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

        private void InsertEntryToDatabase(Country country)
        {
            try
            {
                if (country == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                if (IsDBEntryExists(country.Name, true))
                {
                    BindGridViewData();
                    return;
                }
                dBConnection.countries.Add(country);
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

        private bool IsDBEntryExists(string countryName, bool showMessageBox)
        {
            try
            {
                if (dBConnection.countries.SingleOrDefault(m => m.Name == countryName) != null)
                {
                    if (showMessageBox)
                        MessageBox.Show("Country with given name already exists.Please enter another name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void InsertEntryToDatabase(List<Country> countries)
        {
            try
            {
                if (countries == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                List<Country> notInsertedList = new List<Country>();
                foreach (Country country in countries)
                {
                    if (!IsDBEntryExists(country.Name, false))
                    {
                        dBConnection.countries.Add(country);
                    }
                    else
                        notInsertedList.Add(country);
                }
                dBConnection.SaveChanges();
                string message = "Following records are not inserted to database because they are already in the database." + Environment.NewLine;
                foreach (Country country in notInsertedList)
                {
                    message += country.Name + Environment.NewLine;
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

        private void UpdateDatabaseEntry(Country element, string countryName)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                countryName = StaticFunctions.getValidString(countryName, false, false);
                if (CheckIsNullOrEmpty(countryName, true))
                {
                    BindGridViewData();
                    return;
                }

                if (IsDBEntryExists(countryName, true))
                {
                    BindGridViewData();
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.countries.Attach(element);
                element.Name = countryName;
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

        private bool CheckIsNullOrEmpty(string countryName, bool showMessageBox)
        {
            try
            {
                if (string.IsNullOrEmpty(countryName) || string.IsNullOrWhiteSpace(countryName))
                {
                    if (showMessageBox)
                        MessageBox.Show("Please enter non empty Country name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (r.Index >= 0 && r.Index <= countryList.Count - 1)
                        DeleteDatabaseEntry(countryList[r.Index], false);
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