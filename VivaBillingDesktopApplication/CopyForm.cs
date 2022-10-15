using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class CopyForm : Form
    {
        DBConnection dBConnection;
        List<Customer> customerList;
        Array imagePaths;
        Customer selectedCustomer;
        private bool executeRenameOperation;
        string fileName;
        string year;
        string month;
        string date;
        string todayPath;
        string currentCustomerPath;
        public CopyForm()
        {
            InitializeComponent();
            initVariables();
            CustomerGrid.AutoSize = true;
        }
        public CopyForm(Customer selectedCustomer)
        {
            InitializeComponent();
            initVariables();
            CustomerGrid.AutoSize = true;
            this.selectedCustomer = selectedCustomer;
            this.executeRenameOperation = true;
            setSelectedCustomerData();
            lblChangeCustomerLabel.Visible = false;
        }
        private void initVariables()
        {
            customerList = null;
            executeRenameOperation = true;
            fileName = "";
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

        private void TempForm_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (executeRenameOperation)
                {
                    year = DateTime.Now.ToString("yyyy");
                    month = DateTime.Now.ToString("MMM");
                    date = DateTime.Now.ToString("dd");
                    todayPath = Path.Combine(Program.ImageCopyFolder, year, month, date);
                    if (!string.IsNullOrWhiteSpace(selectedCustomer.FirmName))
                        currentCustomerPath = Path.Combine(todayPath, selectedCustomer.FirmName);
                    else
                        currentCustomerPath = Path.Combine(todayPath, selectedCustomer.FirstName + " " + selectedCustomer.LastName);
                }

                if (!Directory.Exists(todayPath))
                    Directory.CreateDirectory(todayPath);
                if (!Directory.Exists(currentCustomerPath))
                {
                    Directory.CreateDirectory(currentCustomerPath);
                    executeRenameOperation = false;
                }
                else if (executeRenameOperation)
                {

                    executeRenameOperation = false;
                    bool isExists = true;
                    int temp = 2;
                    int i = 0;
                    while (isExists)
                    {
                        currentCustomerPath += "_" + temp;
                        //path = Path.Combine(rootFoldersList[cmbRoot.SelectedIndex].Path, validFolderName);
                        if (!Directory.Exists(currentCustomerPath))
                        {
                            Directory.CreateDirectory(currentCustomerPath);
                            i++;
                            isExists = false;
                        }
                        else
                        {
                            currentCustomerPath = currentCustomerPath.Substring(0, currentCustomerPath.LastIndexOf("_"));
                            temp++;
                        }
                    }
                }
                if (imagePaths != null)
                {
                    int i = 0;
                    foreach (string s in imagePaths)
                    {
                        fileName = Path.GetFileName(s);
                        File.Copy(s, Path.Combine(currentCustomerPath, fileName), false);
                        i++;
                    }
                    MessageBox.Show(i + " Images copied successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                imagePaths = null;
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

        private void TempForm_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Copy;
                imagePaths = GetFilenames(e);
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

        private void TempForm_DragLeave(object sender, EventArgs e)
        {

        }

        private void TempForm_DragOver(object sender, DragEventArgs e)
        {
        }
        private Array GetFilenames(DragEventArgs e)
        {
            try
            {
                Array data = null;
                if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    data = e.Data.GetData("FileDrop") as Array;
                    if (data != null)
                    {
                        foreach (string s in data)
                        {
                            string ext = Path.GetExtension(s).ToLower();
                            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp"))
                            {
                                MessageBox.Show("Please drop image files only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return null;
                            }
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return null;
            }

        }

        private void lblChangeCustomerLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dropPanel.Visible = false;
            CustomerGrid.Visible = false;
            //JobsGrid.Visible = false;
            CustomerNameDisplayPanel.Visible = false;
            InputComboBox.Visible = true;
            // BottomPanel.Visible = false;
            //BottomPanel.Visible = false;
        }

        private void InputComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CustomerGrid.Visible = false;
                if (InputComboBox.Text.Length <= 3)
                    return;
                string input = StaticFunctions.getValidString(InputComboBox.Text, false, false);
                if (StaticFunctions.IsInteger(input))
                    customerList = dBConnection.customers.Where(m => m.MobileNumber.StartsWith(input)).ToList();
                else
                {
                    customerList = dBConnection.customers.Where(m => m.FirstName.StartsWith(input) || m.LastName.StartsWith(input) || m.FirmName.StartsWith(input) || m.EmailId.StartsWith(input)).ToList();
                }

                CustomerGrid.Rows.Clear();
                CustomerGrid.Columns.Clear();
                CustomerGrid.Columns.Add("ID", "ID");
                CustomerGrid.Columns.Add("Name", "Name");
                foreach (var s in customerList)
                {
                    CustomerGrid.Rows.Add(new object[] { s.ID, s.FirstName + " " + s.LastName + " " + s.FirmName + " " + s.MobileNumber + "," + s.CustomerType });
                }
                if (customerList.Count > 0)
                {
                    CustomerGrid.Visible = true;
                }
                CustomerGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void CustomerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(e.RowIndex >= 0 && e.ColumnIndex >= 0))
                    return;
                if (MessageBox.Show("Do you want to select the entry as selected customer", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                    return;
                selectedCustomer = customerList[e.RowIndex];
                setSelectedCustomerData();
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

        private void setSelectedCustomerData()
        {
            lblCustomerName.Text = selectedCustomer.FirstName + " " + selectedCustomer.LastName + " " + selectedCustomer.FirmName + " " + selectedCustomer.MobileNumber + "," + selectedCustomer.CustomerType;
            InputComboBox.Visible = false;
            CustomerNameDisplayPanel.Visible = true;
            lblChangeCustomerLabel.Visible = true;
            CustomerGrid.Visible = false;
            dropPanel.Visible = true;
            StringBuilder sb = new StringBuilder();
            sb.Append("Please drop all images in this panel to copy for ");
            sb.Append(selectedCustomer.FirstName + " ");
            sb.Append(selectedCustomer.LastName + ",");
            if (!string.IsNullOrWhiteSpace(selectedCustomer.FirmName))
                sb.Append(selectedCustomer.FirmName + ",");
            if (!string.IsNullOrWhiteSpace(selectedCustomer.MobileNumber))
                sb.Append(selectedCustomer.MobileNumber + ",");
            if (!string.IsNullOrWhiteSpace(selectedCustomer.EmailId))
                sb.Append(selectedCustomer.EmailId + ",");
            if (sb[sb.Length - 1] == ',') sb[sb.Length - 1] = ' ';
            sb.Append(".");
            dropImageLabel.Text = sb.ToString();
            //Please drop all images in this panel to copy.
            InputComboBox.Text = "";
            //JobsGrid.Visible = true;
            //bindJobsGridView();
            //BottomPanel.Visible = true;
            //JobsGrid.Focus();
        }
    }
}