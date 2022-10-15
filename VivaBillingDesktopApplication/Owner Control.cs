using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class Owner_Control : Form
    {
        List<Customer> customerList;
        DBConnection dBConnection;
        public Owner_Control()
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
        }
        private void BindGridViewData()
        {
            try
            {
                customerList = dBConnection.customers.ToList();
                grid.Rows.Clear();
                grid.Columns.Clear();
                grid.Columns.Add("ID", "ID");
                grid.Columns.Add("Name", "Name");
                grid.Columns.Add("Balance", "Balance");
                foreach (var s in customerList)
                {
                    grid.Rows.Add(new object[] { s.ID, s.FirstName+" "+s.LastName+","+s.FirmName,s.Balance });
                }
                grid.Rows.Add();
                grid.Columns[0].ReadOnly = true;
                grid.Columns[1].ReadOnly = true;


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

        private void grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure to update balance of selected customer?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
            {
                BindGridViewData();
                return;
            }
            var element = customerList[e.RowIndex];
            var entry = dBConnection.Entry(element);
            if (entry.State == System.Data.Entity.EntityState.Detached)
                dBConnection.customers.Attach(element);
            element.Balance =(float) Convert.ToDecimal(grid[2, e.RowIndex].Value);
            dBConnection.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "your_password")
            {
                passwordPanel.Visible = false;
                mainPanel.Visible = true;
            }
            else
                MessageBox.Show("Invalid password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
