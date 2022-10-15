using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class AddCustomer : Form
    {
        List<Customer> customerList;
        DataTable dt;
        List<Customer> customersToDeleteList;
        List<string> customerTypesList;
        DBConnection dBConnection;
        int editedRowIndex;

        public AddCustomer()
        {
            InitializeComponent();
            initVariables();
            BindGridViewData();
        }
        private void initVariables()
        {
            dt = new DataTable();
            customersToDeleteList = new List<Customer>();
            customerTypesList = new List<string>() { "CC", "RC" };
            editedRowIndex = -1;
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
                grid.Columns.Add("FirstName", "FirstName");
                grid.Columns.Add("LastName", "LastName");
                grid.Columns.Add("FirmName", "FirmName");
                grid.Columns.Add("EmailId", "EmailId");
                grid.Columns.Add("MobileNumber", "MobileNumber");
                grid.Columns.Add("Address", "Address");
                grid.Columns.Add("Balance", "Balance");

                DataGridViewComboBoxColumn stateColumn = new DataGridViewComboBoxColumn { HeaderText = "State", Name = "State", DataSource = dBConnection.states.ToList(), DisplayMember = "Name", ValueMember = "ID" };
                grid.Columns.Add(stateColumn);

                DataGridViewComboBoxColumn countryColumn = new DataGridViewComboBoxColumn { HeaderText = "Country", Name = "Country", DataSource = dBConnection.countries.ToList(), DisplayMember = "Name", ValueMember = "ID" };
                grid.Columns.Add(countryColumn);

                DataGridViewComboBoxColumn customerTypeColumn = new DataGridViewComboBoxColumn { HeaderText = "Customer Type", Name = "CustomerType", DataSource = customerTypesList };
                grid.Columns.Add(customerTypeColumn);
                foreach (var s in customerList)
                {
                    grid.Rows.Add(new object[] { s.ID, s.FirstName, s.LastName, s.FirmName, s.EmailId, s.MobileNumber, s.Address, s.Balance, s.State, s.Country, s.CustomerType });
                }
                grid.Rows.Add();
                grid.Columns["ID"].ReadOnly = true;
                grid.Columns["Balance"].ReadOnly = true;
                grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

        private void DeleteDatabaseEntry(Customer element, bool callBindGridView)
        {
            try
            {
                if (element == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Invoice temp = dBConnection.invoices.FirstOrDefault(m => m.customerID == element.ID);
                if(temp!=null)
                {
                    MessageBox.Show("Selected customer can't be deleted because it has entry in invoice table.Deleting this will lead to error in accounting data.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var entry = dBConnection.Entry(element);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.customers.Attach(element);
                dBConnection.customers.Remove(element);
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


        private bool checkFieldsAreValidAndNonEmpty(int rowIndex)
        {
            try
            {
                Customer customer = createCustomerObject(grid.CurrentCell.RowIndex);
                if (customer.FirstName == "" || customer.LastName == "" || customer.Address == "" || customer.State == 0 || customer.Country == 0 || customer.CustomerType == "")
                {
                    MessageBox.Show("First Name,Last Name,Address,State,Country and CustomerType are mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (!checkAllFieldsValidOrNot(customer))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return false;
            }
        }

        private bool checkAllFieldsValidOrNot(Customer customer)
        {
            try
            {
                Regex nonNumber = new Regex(@"[^\d]");
                Regex nonAlphabets = new Regex(@"[^a-zA-Z\s]");
                Regex alphabets = new Regex(@"[a-zA-Z\s]");
                Regex nonEmail = new Regex(@"[^a-zA-Z0-9@\.]");

                if (nonAlphabets.IsMatch(customer.FirstName) || nonAlphabets.IsMatch(customer.LastName))
                {
                    MessageBox.Show("First name and Last name should not contain digits.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (customer.FirmName != "" && !alphabets.IsMatch(customer.FirmName))
                {
                    MessageBox.Show("Firm must contain atleast one alphabet.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (customer.MobileNumber != "" && nonNumber.IsMatch(customer.MobileNumber))
                {
                    MessageBox.Show("Mobile number should not contain anything except digits.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (customer.MobileNumber != "" && customer.MobileNumber.Length != 10)
                {
                    MessageBox.Show("Mobile number should contain 10 digits only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (customer.EmailId != "" && (nonEmail.IsMatch(customer.EmailId) || !customer.EmailId.Contains("@") || !customer.EmailId.Contains(".") || customer.EmailId.Count(m => m == '@') > 1 || customer.EmailId.LastIndexOf('.') < customer.EmailId.LastIndexOf('@')))
                {
                    MessageBox.Show("Please enter a valid email id.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return false;
            }
            
        }

        private bool InsertEntryToDatabase(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return false;
                }
                if (IsDBEntryExists(customer, true))
                {
                    return false;
                }
                dBConnection.customers.Add(customer);
                dBConnection.SaveChanges();
                BindGridViewData();
                return true;
            }
            catch (Exception ex)
            {
                if (Program.showExceptionsInMessageBox)
                {
                    string exceptionMessage = "Data=" + ex.Data + "HelpLink=" + ex.HelpLink + "HResult=" + ex.HResult + "InnerException=" + ex.InnerException + "Message=" + ex.Message + "Source=" + ex.Source + "StackTrace=" + ex.StackTrace + "TargetSite=" + ex.TargetSite;
                    MessageBox.Show(exceptionMessage, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return false;
            }

        }

        private bool IsDBEntryExists(Customer customeObject, bool showMessageBox)
        {
            try
            {
                bool isExists = false;
                if (customeObject.MobileNumber == "" && customeObject.EmailId == "")
                {
                    if (dBConnection.customers.FirstOrDefault(m => m.FirstName == customeObject.FirstName && m.LastName == customeObject.LastName && m.ID != customeObject.ID) != null)
                    {
                        MessageBox.Show("The Customer with given name already exists.Please enter another name or Provide EmailId/Mobile number/Both.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isExists= true;
                    }
                }
                if (customeObject.MobileNumber != "" && customeObject.EmailId == "")
                {
                    if (dBConnection.customers.SingleOrDefault(m => m.MobileNumber == customeObject.MobileNumber && m.ID != customeObject.ID) != null)
                    {
                        MessageBox.Show("The Customer with given mobile number already exists.Please enter another Mobile number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isExists= true;
                    }
                }
                if (customeObject.EmailId != "" && customeObject.MobileNumber == "")
                {
                    if (dBConnection.customers.SingleOrDefault(m => m.EmailId == customeObject.EmailId && m.ID != customeObject.ID) != null)
                    {
                        MessageBox.Show("The Customer with given Email Id already exists.Please enter another Email Id.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isExists = true;
                    }
                }
                if (customeObject.EmailId != "" && customeObject.MobileNumber != "")
                {
                    bool flag1 = false;
                    bool flag2 = false;
                    string msg = "";
                    if (dBConnection.customers.SingleOrDefault(m => m.MobileNumber == customeObject.MobileNumber && m.ID != customeObject.ID) != null)
                    {
                        msg += "The Customer with given mobile number ";
                        flag1 = true;
                    }
                    if (dBConnection.customers.SingleOrDefault(m => m.EmailId == customeObject.EmailId && m.ID != customeObject.ID) != null)
                    {
                        if (flag1)
                            msg += "and Email Id ";
                        else
                            msg += "The Customer with given email id ";
                        flag2 = true;
                    }
                    msg += "already exists.Please enter another combination.";
                    if (flag1 || flag2)
                    {
                        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isExists = true;
                    }
                }

                if (customeObject.FirmName != "")
                {
                    if (dBConnection.customers.FirstOrDefault(m => m.FirmName == customeObject.FirmName && m.ID != customeObject.ID) != null)
                    {
                        MessageBox.Show("The Customer with given Firm name already exists.Please enter another Firm name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isExists = true;
                    }
                }
                return isExists;
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

        private void InsertEntryToDatabase(List<Customer> customers)
        {
            try
            {
                if (customers == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                List<Customer> notInsertedList = new List<Customer>();
                foreach (Customer customer in customers)
                {
                    if (!IsDBEntryExists(customer, false))
                    {
                        dBConnection.customers.Add(customer);
                    }
                    else
                        notInsertedList.Add(customer);
                }
                dBConnection.SaveChanges();
                string message = "Following records are not inserted to database because they are already in the database." + Environment.NewLine;
                foreach (Customer customer in notInsertedList)
                {
                    message += customer.FirstName + Environment.NewLine;
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

        private void UpdateDatabaseEntry(Customer oldElement, Customer newElement)
        {
            try
            {
                if (oldElement == null || newElement == null)
                {
                    MessageBox.Show("Error:The parameter is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGridViewData();
                    return;
                }
                newElement.ID = oldElement.ID;
                if (IsDBEntryExists(newElement, true))
                {
                    BindGridViewData();
                    return;
                }
                var entry = dBConnection.Entry(oldElement);
                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dBConnection.customers.Attach(oldElement);
                oldElement.Address = newElement.Address;
                oldElement.Country = newElement.Country;
                oldElement.CustomerType = newElement.CustomerType;
                oldElement.EmailId = newElement.EmailId;
                oldElement.FirmName = newElement.FirmName;
                oldElement.FirstName = newElement.FirstName;
                oldElement.LastName = newElement.LastName;
                oldElement.MobileNumber = newElement.MobileNumber;
                oldElement.State = newElement.State;
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

        private bool CheckIsNullOrEmpty(string customerName, bool showMessageBox)
        {
            try
            {
                if (string.IsNullOrEmpty(customerName) || string.IsNullOrWhiteSpace(customerName))
                {
                    if (showMessageBox)
                        MessageBox.Show("Please enter non empty Customer name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string keyString = e.KeyCode.ToString().ToLower();
                switch (keyString)
                {
                    case "delete":
                        if (grid.SelectedRows.Count <= 0)
                            return;
                        if (MessageBox.Show("Are you sure you want to delete selected record(s) ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                            return;
                        foreach (DataGridViewRow r in grid.SelectedRows)
                        {
                            if (r.Index >= 0 && r.Index <= customerList.Count - 1)
                                DeleteDatabaseEntry(customerList[r.Index], false);
                        }
                        BindGridViewData();
                        break;
                    case "return":
                        if (editedRowIndex < 0)
                            return;
                        if (!e.Shift)
                        {
                            editedRowIndex = -1;
                            return;
                        }
                        if (editedRowIndex == customerList.Count)
                        {
                            if (MessageBox.Show("Are you sure want to add new entry", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                            {
                                BindGridViewData();
                                editedRowIndex = -1;
                                return;
                            }
                            if (!checkFieldsAreValidAndNonEmpty(editedRowIndex))
                            {
                                editedRowIndex = -1;
                                return;
                            }
                            Customer customer = createCustomerObject(editedRowIndex);
                            if(InsertEntryToDatabase(customer))
                            if (MessageBox.Show("Customer added successfully. Do you want to send welcome message to this customer ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                                sendWelcomeMessage(customer);
                        }
                        else
                        {
                            if (MessageBox.Show("Are you sure want to update selected entry", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                            {
                                BindGridViewData();
                                editedRowIndex = -1;
                                return;
                            }
                            UpdateDatabaseEntry(customerList[editedRowIndex], createCustomerObject(editedRowIndex));
                        }
                        editedRowIndex = -1;
                        break;
                    default:
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

        private void sendWelcomeMessage(Customer customer)
        {
            try
            {
                string welcomeMessage = Program.WelcomeMessage;
                if (!string.IsNullOrWhiteSpace(customer.MobileNumber))
                {
                    VivaSMSServices.SendSMS(customer.MobileNumber, welcomeMessage);
                    MessageBox.Show("Welcome message was sent on mobile number " + customer.MobileNumber, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(customer.EmailId))
                {
                    VivaEmailServices.SendEmail(customer.EmailId, "Welcome to Viva Digital.", welcomeMessage);
                    MessageBox.Show("Welcome message was sent on email id " + customer.EmailId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("No mobile number or email ID is provided for given customer. No welcome message was sent.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private Customer createCustomerObject(int rowIndex)
        {
            try
            {
                string FirstName = "";
                string LastName = "";
                string FirmName = "";
                string EmailId = "";
                string MobileNumber = "";
                string Address = "";
                long State = 0;
                long Country = 0;
                float Balance = 0;
                string CustomerType = "";
                if ((grid["FirstName", rowIndex] as DataGridViewCell).Value != null)
                    FirstName = (grid["FirstName", rowIndex] as DataGridViewCell).Value.ToString();

                if ((grid["LastName", rowIndex] as DataGridViewCell).Value != null)
                    LastName = (grid["LastName", rowIndex] as DataGridViewCell).Value.ToString();

                if ((grid["FirmName", rowIndex] as DataGridViewCell).Value != null)
                    FirmName = (grid["FirmName", rowIndex] as DataGridViewCell).Value.ToString();

                if ((grid["EmailId", rowIndex] as DataGridViewCell).Value != null)
                    EmailId = (grid["EmailId", rowIndex] as DataGridViewCell).Value.ToString();

                if ((grid["MobileNumber", rowIndex] as DataGridViewCell).Value != null)
                    MobileNumber = (grid["MobileNumber", rowIndex] as DataGridViewCell).Value.ToString();

                if ((grid["Address", rowIndex] as DataGridViewCell).Value != null)
                    Address = (grid["Address", rowIndex] as DataGridViewCell).Value.ToString();

                if ((grid["State", rowIndex] as DataGridViewCell).Value != null)
                    State = Convert.ToInt64((grid["State", rowIndex] as DataGridViewComboBoxCell).Value);

                if ((grid["Country", rowIndex] as DataGridViewCell).Value != null)
                    Country = Convert.ToInt64((grid["Country", rowIndex] as DataGridViewComboBoxCell).Value);

                if ((grid["Balance", rowIndex] as DataGridViewCell).Value != null)
                    Balance = Convert.ToInt64((grid["Balance", rowIndex] as DataGridViewCell).Value);

                if ((grid["CustomerType", rowIndex] as DataGridViewCell).Value != null)
                    CustomerType = (grid["CustomerType", rowIndex] as DataGridViewCell).Value.ToString();


                FirstName = StaticFunctions.getValidString(FirstName, false, false);
                LastName = StaticFunctions.getValidString(LastName, false, false);
                FirmName = StaticFunctions.getValidString(FirmName, true, false);
                EmailId = StaticFunctions.getValidString(EmailId, false, true);
                MobileNumber = StaticFunctions.getValidString(MobileNumber, false, false);
                Address = StaticFunctions.getValidString(Address, true, false);

                Customer customer = new Customer { Address = Address, Balance = Balance, Country = Country, CustomerType = CustomerType, EmailId = EmailId, FirmName = FirmName, FirstName = FirstName, LastName = LastName, MobileNumber = MobileNumber, State = State };
                return customer;
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

        private void grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                editedRowIndex = (sender as DataGridViewComboBoxEditingControl).EditingControlRowIndex;
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
            editedRowIndex = e.RowIndex;
            
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewComboBoxCell dataGridViewComboBoxCell = grid.CurrentCell as DataGridViewComboBoxCell;
                if (dataGridViewComboBoxCell == null) return;
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