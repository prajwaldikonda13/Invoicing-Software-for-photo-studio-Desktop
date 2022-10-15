using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class MainPage : Form
    {
        string invoiceTableHtmlString;
        static Customer selectedCustomer;
        static Invoice selectedInvoice;
        List<Customer> customerList;
        private bool setJobsGridReadOnly;
        List<Job> jobsList;
        List<float> ccPriceList;
        List<float> rcPriceList;
        List<Product> productList;
        List<ProductType> productTypeList;
        List<Size> productSizeList;
        List<Product> tempProductList;
        List<ProductType> tempProductTypeList;
        List<Size> tempProductSizeList;
        List<Price> priceList;
        float totalPrice, totalQuantity;
        DBConnection dBConnection;
        private bool shiftEnterPressed;
        float balance , grandTotal , discount  , discountedPrice ,currentPaid ;
        bool isDtForInvoiceChanged ,isDtForPaymentChanged ,isForPrint = false;
        private bool isDataGridViewChangeInProgress;

        public MainPage()
        {
            InitializeComponent();
            initVariables();
            init();
        }
        private void initVariables()
        {
            invoiceTableHtmlString = "";
            selectedCustomer = null;
            selectedInvoice = null;
            customerList = null;
            setJobsGridReadOnly = false;
            jobsList = new List<Job>() { };
            ccPriceList = new List<float>() { 0 };
            rcPriceList = new List<float>() { 0 };
            productList = new List<Product>();
            productTypeList = new List<ProductType>();
            productSizeList = new List<Size>();
            tempProductList = new List<Product>();
            tempProductTypeList = new List<ProductType>();
            tempProductSizeList = new List<Size>();
            priceList = new List<Price>();
            totalPrice = totalQuantity = balance = grandTotal = discount = discountedPrice = currentPaid = 0;
            shiftEnterPressed = true;
            isDtForInvoiceChanged = isDtForPaymentChanged =isForPrint = false;
            isDataGridViewChangeInProgress = false;
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
        private void init()
        {
            try
            {
                dtpForInvoice.ValueChanged += (s, e) => isDtForInvoiceChanged = true;
                dtpForInvoiceWithPayment.ValueChanged += (s, e) => isDtForPaymentChanged = true; ;
                dtpForInvoice.Value = DateTime.Now;
                dtpForInvoiceWithPayment.Value = DateTime.Now;
                CustomerGrid.AutoSize = true;
                JobsGrid.AutoSize = true;
                JobsGrid.DataError += (s, e) =>
                {
                };
                CustomerGrid.DataError += (s, e) => { };
                CustomerNameDisplayPanel.Visible = false;
                productList = dBConnection.products.ToList();
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

        public MainPage(Invoice invoice, bool isForPrint)
        {
            InitializeComponent();
            initVariables();
            try
            {
                this.isForPrint = isForPrint;
                selectedInvoice = invoice;
                selectedCustomer = dBConnection.customers.SingleOrDefault(m => m.ID == invoice.customerID);

                jobsList = dBConnection.jobs.Where(m => m.InvoiceID == invoice.ID).ToList();
                if (isForPrint)
                {
                    Hide();
                    resetinvoiceTableHtmlString();
                    for (int i = 0; i < jobsList.Count; i++)
                    {
                        string productName = "";
                        string productTypeName = "";
                        string productSizeName = "";
                        if (productList.SingleOrDefault(m => m.ID == jobsList[i].ProductID) != null)
                            productName = productList.SingleOrDefault(m => m.ID == jobsList[i].ProductID).Name;
                        else
                        {
                            Product tempProduct;
                            long productID = jobsList[i].ProductID;
                            if ((tempProduct = dBConnection.products.SingleOrDefault(m => m.ID == productID)) != null)
                                productName = tempProduct.Name;
                        }

                        if (productTypeList.SingleOrDefault(m => m.ID == jobsList[i].ProductTypeID) != null)
                            productTypeName = productTypeList.SingleOrDefault(m => m.ID == jobsList[i].ProductTypeID).Name;
                        else
                        {
                            ProductType tempProductType;
                            long productTypeID = jobsList[i].ProductTypeID;
                            if ((tempProductType = dBConnection.productTypes.SingleOrDefault(m => m.ID == productTypeID)) != null)
                                productTypeName = tempProductType.Name;
                        }

                        if (productSizeList.SingleOrDefault(m => m.ID == jobsList[i].ProductSizeID) != null)
                            productSizeName = productSizeList.SingleOrDefault(m => m.ID == jobsList[i].ProductSizeID).Name;
                        else
                        {
                            Size tempProductSize;
                            long productSizeID = jobsList[i].ProductSizeID;
                            if ((tempProductSize = dBConnection.sizes.SingleOrDefault(m => m.ID == productSizeID)) != null)
                                productSizeName = tempProductSize.Name;
                        }

                        float total = jobsList[i].UnitPrice * jobsList[i].Quantity;

                        if (jobsList[i].Quantity > 0 && jobsList[i].UnitPrice > 0)
                        {
                            invoiceTableHtmlString += "<tr>";
                            invoiceTableHtmlString += "<td>" + (i + 1) + "</td>";
                            invoiceTableHtmlString += "<td>" + productName + "</td>";
                            invoiceTableHtmlString += "<td>" + productTypeName + "</td>";
                            invoiceTableHtmlString += "<td>" + productSizeName + "</td>";
                            invoiceTableHtmlString += "<td>" + jobsList[i].UnitPrice + "</td>";
                            invoiceTableHtmlString += "<td>" + jobsList[i].Quantity + "</td>";
                            invoiceTableHtmlString += "<td>" + total + "</td>";
                            invoiceTableHtmlString += "</tr>";
                        }
                        totalPrice += total;
                        totalQuantity += jobsList[i].Quantity;
                    }
                    addLastRowToInvoiceTableHtmlString();
                    if (selectedInvoice.Paid < 0)
                        InitializePrinting(true,false);
                    else
                        InitializePrinting(false,false);

                    return;
                }
                init();
                setJobsGridReadOnly = true;
                for (int i = 0; i <= jobsList.Count - 1; i++)
                {
                    rcPriceList[i] = jobsList[i].UnitPrice;
                    ccPriceList[i] = jobsList[i].UnitPrice;
                }
                lblCustomerName.Text = selectedCustomer.FirstName + " " + selectedCustomer.LastName + " " + selectedCustomer.FirmName + " " + selectedCustomer.MobileNumber + "," + selectedCustomer.CustomerType + ",Invoice ID:" + selectedInvoice.ID + "," + selectedInvoice.InvoiceDateTime.ToString();
                InputComboBox.Visible = false;
                CustomerNameDisplayPanel.Visible = true;
                lblChangeCustomerLabel.Visible = false;
                CustomerGrid.Visible = false;
                InputComboBox.Text = "";
                JobsGrid.Visible = true;
                bindJobsGridView();


                BottomPanel.Visible = true;
                JobsGrid.Focus();
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

        private void bindJobsGridView()
        {
            try
            {
                resetinvoiceTableHtmlString();
                totalPrice = 0;
                totalQuantity = 0;
                generateJobsGrid();
                if (shiftEnterPressed && !setJobsGridReadOnly)
                {
                    jobsList.Add(new Job { });
                    rcPriceList.Add(0);
                    ccPriceList.Add(0);
                    shiftEnterPressed = false;
                }
                for (int i = 0; i < jobsList.Count; i++)
                {
                    string productName = "";
                    string productTypeName = "";
                    string productSizeName = "";
                    if (productList.SingleOrDefault(m => m.ID == jobsList[i].ProductID) != null)
                        productName = productList.SingleOrDefault(m => m.ID == jobsList[i].ProductID).Name;
                    else
                    {
                        Product tempProduct;
                        long productID = jobsList[i].ProductID;
                        if ((tempProduct = dBConnection.products.SingleOrDefault(m => m.ID == productID)) != null)
                            productName = tempProduct.Name;
                    }

                    if (productTypeList.SingleOrDefault(m => m.ID == jobsList[i].ProductTypeID) != null)
                        productTypeName = productTypeList.SingleOrDefault(m => m.ID == jobsList[i].ProductTypeID).Name;
                    else
                    {
                        ProductType tempProductType;
                        long productTypeID = jobsList[i].ProductTypeID;
                        if ((tempProductType = dBConnection.productTypes.SingleOrDefault(m => m.ID == productTypeID)) != null)
                            productTypeName = tempProductType.Name;
                    }

                    if (productSizeList.SingleOrDefault(m => m.ID == jobsList[i].ProductSizeID) != null)
                        productSizeName = productSizeList.SingleOrDefault(m => m.ID == jobsList[i].ProductSizeID).Name;
                    else
                    {
                        Size tempProductSize;
                        long productSizeID = jobsList[i].ProductSizeID;
                        if ((tempProductSize = dBConnection.sizes.SingleOrDefault(m => m.ID == productSizeID)) != null)
                            productSizeName = tempProductSize.Name;
                    }
                    if (selectedCustomer.CustomerType.ToLower() == "rc")
                        jobsList[i].UnitPrice = rcPriceList[i];
                    else
                        jobsList[i].UnitPrice = ccPriceList[i];

                    float total = jobsList[i].UnitPrice * jobsList[i].Quantity;


                    JobsGrid.Rows.Add(new object[] { i + 1, productName, productTypeName, productSizeName, jobsList[i].UnitPrice, jobsList[i].Quantity, total });
                   if(jobsList[i].Quantity>=1 && jobsList[i].UnitPrice>=1)
                    {
                        invoiceTableHtmlString += "<tr>";
                        invoiceTableHtmlString += "<td>" + (i + 1) + "</td>";
                        invoiceTableHtmlString += "<td>" + productName + "</td>";
                        invoiceTableHtmlString += "<td>" + productTypeName + "</td>";
                        invoiceTableHtmlString += "<td>" + productSizeName + "</td>";
                        invoiceTableHtmlString += "<td>" + jobsList[i].UnitPrice + "</td>";
                        invoiceTableHtmlString += "<td>" + jobsList[i].Quantity + "</td>";
                        invoiceTableHtmlString += "<td>" + total + "</td>";
                        invoiceTableHtmlString += "</tr>";
                    }
                    setRowReadOnly(i);
                    totalPrice += total;
                    totalQuantity += jobsList[i].Quantity;
                    if (i == jobsList.Count - 1)
                    {
                        JobsGrid.Rows[i].ReadOnly = false;
                        JobsGrid.Rows[i].Cells["ItemNumber"].ReadOnly = true;
                        JobsGrid.Rows[i].Cells["UnitPrice"].ReadOnly = true;
                        JobsGrid.Rows[i].Cells["TotalPrice"].ReadOnly = true;
                        if (!setJobsGridReadOnly)
                        {
                            setProductComboBox(jobsList.Count - 1);
                            setProductTypeComboBox(jobsList.Count - 1);
                            setProductSizeComboBox(jobsList.Count - 1);

                        }
                    }
                }
                AddLastRowControls();
                if (setJobsGridReadOnly)
                {
                    JobsGrid.ReadOnly = true;
                    dtpForInvoice.Enabled = false;
                    btnInvoiceSave.Enabled = false;
                }
                addLastRowToInvoiceTableHtmlString();
                JobsGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                JobsGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                JobsGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                JobsGrid.Refresh();
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

        private void addLastRowToInvoiceTableHtmlString()
        {
            try
            {
                invoiceTableHtmlString += "<tr>";
                invoiceTableHtmlString += "<td>Total</td>";
                invoiceTableHtmlString += "<td></td>";
                invoiceTableHtmlString += "<td></td>";
                invoiceTableHtmlString += "<td></td>";
                invoiceTableHtmlString += "<td></td>";
                invoiceTableHtmlString += "<td>" + totalQuantity + "</td>";
                invoiceTableHtmlString += "<td>" + totalPrice + "</td>";
                invoiceTableHtmlString += "</tr>";
                invoiceTableHtmlString += "</tbody></table>";
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

        private void resetinvoiceTableHtmlString()
        {
            invoiceTableHtmlString = "<table class='table table-striped' cellspacing='0' rules='all' border='1' id='InvoiceGrid' style='border-collapse:collapse;font-size:20px;margin-top:1px;'><tbody><tr><th scope='col'>No.</th><th scope='col'>Product</th><th scope='col'>Type</th><th scope='col'>Size</th><th scope='col'>Unit Price</th><th scope='col'>Quantity</th><th scope='col'>Total</th></tr>";
        }

        private void generateJobsGrid()
        {
            try
            {
                if (isDataGridViewChangeInProgress) return;
                isDataGridViewChangeInProgress = true;
                JobsGrid.Rows.Clear();
                JobsGrid.Columns.Clear();
                tempProductList.Clear();
                tempProductTypeList.Clear();
                tempProductSizeList.Clear();
                JobsGrid.Columns.Add("ItemNumber", "No.");
                JobsGrid.Columns.Add("Product", "Product");
                JobsGrid.Columns.Add("ProductType", "ProductType");
                JobsGrid.Columns.Add("ProductSize", "Product Size");
                JobsGrid.Columns.Add("UnitPrice", "Unit Price");
                JobsGrid.Columns.Add("Quantity", "Quantity");
                JobsGrid.Columns.Add("TotalPrice", "TotalPrice");
                isDataGridViewChangeInProgress = false;
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
                JobsGrid.Rows[rowIndex].ReadOnly = true;
                JobsGrid.Rows[rowIndex].ReadOnly = true;
                JobsGrid.Rows[rowIndex].ReadOnly = true;
                JobsGrid.Rows[rowIndex].ReadOnly = true;
                JobsGrid.Rows[rowIndex].ReadOnly = true;
                JobsGrid.Rows[rowIndex].ReadOnly = true;
                JobsGrid.Rows[rowIndex].ReadOnly = true;
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

        private void AddLastRowControls()
        {
            try
            {
                JobsGrid.Rows.Add();
                JobsGrid.Rows[jobsList.Count].ReadOnly = true;
                JobsGrid["ItemNumber", jobsList.Count].Value = "Total";
                JobsGrid["Quantity", jobsList.Count].Value = totalQuantity;
                JobsGrid["TotalPrice", jobsList.Count].Value = totalPrice;
                InitializeValuesAndTextBoxes();
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

        private void setProductSizeComboBox(int rowIndex)
        {
            try
            {
                long productID = jobsList[rowIndex].ProductID;
                long productTypeID = jobsList[rowIndex].ProductTypeID;
                long productSizeID = jobsList[rowIndex].ProductSizeID;
                if (!(productID >= 1 && productTypeID >= 1))
                {
                    JobsGrid["ProductSize", rowIndex] = new DataGridViewComboBoxCell { DisplayMember = "Name", ValueMember = "ID" };
                }
                else
                {
                    List<Size> sizeList = new List<Size>();
                    foreach (Price price in priceList)
                    {
                        if (sizeList.SingleOrDefault(m => m.ID == (int)price.ProductSizeID) == null)
                            sizeList.Add(dBConnection.sizes.SingleOrDefault(m => m.ID == price.ProductSizeID));
                    }
                    if (sizeList.Count == 0)
                    {
                        JobsGrid["ProductSize", rowIndex] = new DataGridViewComboBoxCell { DisplayMember = "Name", ValueMember = "ID" };
                        return;
                    }
                    if (!(productSizeID >= 1))
                    {
                        JobsGrid["ProductSize", rowIndex] = new DataGridViewComboBoxCell { DataSource = productSizeList, DisplayMember = "Name", ValueMember = "ID" };
                    }
                    else
                    {
                        //priceList = priceList.Where(m => m.ProductSize == productSizeID).ToList();
                        JobsGrid["ProductSize", rowIndex] = new DataGridViewComboBoxCell { DataSource = productSizeList, DisplayMember = "Name", ValueMember = "ID" };
                        JobsGrid["ProductSize", rowIndex].Value = productSizeID;
                        //JobsGrid["UnitPrice", rowIndex].Value = unitP;
                        //JobsGrid["TotalPrice", rowIndex].Value = totalP;
                    }
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

        private void setProductTypeComboBox(int rowIndex)
        {
            try
            {
                long productID = jobsList[rowIndex].ProductID;
                long productTypeID = jobsList[rowIndex].ProductTypeID;
                if (!(productID >= 1))
                {
                    JobsGrid["ProductType", rowIndex] = new DataGridViewComboBoxCell { DisplayMember = "Name", ValueMember = "ID" };
                }
                else
                {
                    List<ProductType> typeList = new List<ProductType>();
                    foreach (Price price in priceList)
                    {
                        if (typeList.SingleOrDefault(m => m.ID == (int)price.ProductTypeID) == null)
                            typeList.Add(dBConnection.productTypes.SingleOrDefault(m => m.ID == price.ProductTypeID));
                    }
                    if (typeList.Count == 0)
                    {
                        JobsGrid["ProductType", rowIndex] = new DataGridViewComboBoxCell { DisplayMember = "Name", ValueMember = "ID" };
                        return;
                    }

                    if (!(productTypeID >= 1))
                    {
                        JobsGrid["ProductType", rowIndex] = new DataGridViewComboBoxCell { DataSource = productTypeList, DisplayMember = "Name", ValueMember = "ID" };
                        JobsGrid["ProductSize", rowIndex] = new DataGridViewComboBoxCell { DataSource = productTypeList, DisplayMember = "Name", ValueMember = "ID" };
                        return;
                    }
                    else
                    {
                        JobsGrid["ProductType", rowIndex] = new DataGridViewComboBoxCell { DataSource = productTypeList, DisplayMember = "Name", ValueMember = "ID" };
                        JobsGrid["ProductType", rowIndex].Value = productTypeID;
                    }
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

        private void setProductComboBox(int rowIndex)
        {
            try
            {
                long productID = jobsList[rowIndex].ProductID;
                long productTypeID = jobsList[rowIndex].ProductTypeID;
                long productSizeID = jobsList[rowIndex].ProductSizeID;

                if (productList.Count == 0)
                {
                    JobsGrid["Product", rowIndex] = new DataGridViewComboBoxCell { DisplayMember = "Name", ValueMember = "ID" };
                    return;
                }
                if (!(productID >= 1))
                {
                    JobsGrid["Product", rowIndex] = new DataGridViewComboBoxCell { DataSource = productList, DisplayMember = "Name", ValueMember = "ID" };
                }
                else
                {
                    JobsGrid["Product", rowIndex] = new DataGridViewComboBoxCell { DataSource = productList, DisplayMember = "Name", ValueMember = "ID" };
                    JobsGrid["Product", rowIndex].Value = productID;
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
                lblCustomerName.Text = selectedCustomer.FirstName + " " + selectedCustomer.LastName + " " + selectedCustomer.FirmName + " " + selectedCustomer.MobileNumber + "," + selectedCustomer.CustomerType;
                InputComboBox.Visible = false;
                CustomerNameDisplayPanel.Visible = true;
                lblChangeCustomerLabel.Visible = true;
                CustomerGrid.Visible = false;
                InputComboBox.Text = "";
                JobsGrid.Visible = true;
                bindJobsGridView();
                BottomPanel.Visible = true;
                JobsGrid.Focus();
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

        private void InitializeValuesAndTextBoxes()
        {
            try
            {
                if (!(totalPrice > 0))
                {
                    resetTextBoxes();
                    return;
                }
                grandTotal = totalPrice - selectedCustomer.Balance;
                if (grandTotal < 0)
                {
                    balance = -1 * grandTotal;
                    grandTotal = 0;
                }
                txtGrandTotal.Text = grandTotal + "";
                if (selectedCustomer.Balance >= 0)
                {
                    lblPrevDue.Text = "Prev Bal:";
                    txtPrevBal.Text = selectedCustomer.Balance + "";
                }
                else
                {
                    lblPrevDue.Text = "Prev Due:";
                    txtPrevBal.Text = -1 * selectedCustomer.Balance + "";
                }
                discountedPrice = grandTotal;
                txtFinalPrice.Text = discountedPrice + "";
                if (balance >= 0)
                {
                    DueLabel.Text = "Balance:";
                    txtCurrentDue.Text = balance + "";
                }
                else
                {
                    DueLabel.Text = "Due:";
                    txtCurrentDue.Text = -1 * balance + "";
                }
                txtPaid.Text = "0";

                setTextBoxes(null);
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

        private void setTextBoxes(object sender)
        {
            try
            {
                string tempStr;
                if (sender != null)
                {
                    TextBox textBox = (sender as TextBox);
                    if (!textBox.Focused) return;
                }
                grandTotal = totalPrice - selectedCustomer.Balance;
                if (grandTotal < 0)
                {
                    balance = -1 * grandTotal;
                    grandTotal = 0;
                }
                discountedPrice = 0;


                if (!StaticFunctions.IsNumber(txtDiscount.Text))
                {
                    if ((tempStr = txtDiscount.Text).Contains("%"))
                    {
                        if (tempStr.Count(m => m == '%') > 1)
                        {
                            discount = 0;
                        }
                        else
                        {
                            tempStr = tempStr.Replace("%", "");
                            discount = (float)Convert.ToDouble(tempStr) * grandTotal / 100;
                            txtDiscount.Text = discount + "";
                        }
                    }
                    else
                        discount = 0;
                }

                else
                {
                    discount = (float)Convert.ToDouble(txtDiscount.Text);
                }
                if (discount > grandTotal || discount < 0)
                {
                    discountedPrice = grandTotal;
                    MessageBox.Show("Discount can't be greater than grand total and can't be less than 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiscount.Text = "0";
                    discount = 0;
                }
                else
                {
                    discountedPrice = grandTotal - discount;
                    if (discount > 0)
                        txtDiscount.Text = discount + "";
                }




                if (!StaticFunctions.IsNumber(txtPaid.Text))
                {
                    currentPaid = 0;
                }
                else
                    currentPaid = (float)Convert.ToDecimal(txtPaid.Text);
                if (currentPaid < 0)
                {
                    currentPaid = 0;
                    MessageBox.Show("Paid value can't be negative.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPaid.Text = "0";
                }
                if (currentPaid >= discountedPrice)
                {
                    DueLabel.Text = "Balance:";
                    if (grandTotal != 0)
                        balance = currentPaid - discountedPrice;
                    else
                        balance = selectedCustomer.Balance + currentPaid - totalPrice;
                    txtCurrentDue.Text = balance + "";
                }
                else
                {
                    DueLabel.Text = "Due:";
                    txtCurrentDue.Text = discountedPrice - currentPaid + "";
                    balance = -1 * (discountedPrice - currentPaid);
                }
                txtFinalPrice.Text = discountedPrice + "";
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

        private void resetTextBoxes()
        {
            txtCurrentDue.Text = "";
            txtDiscount.Text = "";
            txtFinalPrice.Text = "";
            txtGrandTotal.Text = "";
            txtPaid.Text = "";
            txtPrevBal.Text = "";
        }

        private void lblChangeCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CustomerGrid.Visible = false;
            JobsGrid.Visible = false;
            CustomerNameDisplayPanel.Visible = false;
            InputComboBox.Visible = true;
            BottomPanel.Visible = false;
            BottomPanel.Visible = false;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox currentComboBox = sender as ComboBox;
                if (currentComboBox == null) return;
                if (!currentComboBox.ContainsFocus) return;
                if (currentComboBox.SelectedItem == null) return;
                int rowIndex = JobsGrid.CurrentCell.RowIndex;
                int columnIndex = JobsGrid.CurrentCell.ColumnIndex;
                if (columnIndex == 1)
                {
                    long productId = (currentComboBox.SelectedItem as Product).ID;
                    jobsList[rowIndex].ProductID = productId;
                    jobsList[rowIndex].ProductTypeID = 0;
                    jobsList[rowIndex].ProductSizeID = 0;

                }
                else if (columnIndex == 2)
                {
                    long productTypeId = (currentComboBox.SelectedItem as ProductType).ID;
                    jobsList[rowIndex].ProductTypeID = productTypeId;
                    jobsList[rowIndex].ProductSizeID = 0;

                }
                else if (columnIndex == 3)
                {
                    long productSizeId = (currentComboBox.SelectedItem as Size).ID;
                    jobsList[rowIndex].ProductSizeID = productSizeId;

                }
                bindJobsGridView();
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

        private void JobsGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewComboBoxCell dataGridViewComboBoxCell = JobsGrid.CurrentCell as DataGridViewComboBoxCell;
                if (dataGridViewComboBoxCell == null) return;
                JobsGrid.BeginEdit(true);
                if (JobsGrid.Visible)
                    (JobsGrid.EditingControl as ComboBox).DroppedDown = true;
                (JobsGrid.EditingControl as ComboBox).SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                (JobsGrid.EditingControl as ComboBox).SelectedIndexChanged += ComboBox_SelectedIndexChanged;
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
        private void JobsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            setValuesInJobListAndBindGridView(e.RowIndex);
        }

        private void setValuesInJobListAndBindGridView(int rowIndex)
        {
            try
            {
                if (!(rowIndex >= 0 && rowIndex <= jobsList.Count - 1))
                {
                    bindJobsGridView(); return;
                }
                jobsList[rowIndex].UnitPrice = 0;

                Job job = jobsList[rowIndex];
                if (!StaticFunctions.IsInteger(JobsGrid["Quantity", rowIndex].Value.ToString()))
                {
                    MessageBox.Show("Please enter a valid number in Quantity field", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    JobsGrid["Quantity", rowIndex].Value = 0;
                    return;
                }
                job.Quantity = Convert.ToInt64(JobsGrid["Quantity", rowIndex].Value.ToString());

                if (job.ProductID >= 1 && job.ProductSizeID >= 1 && job.ProductTypeID >= 1 && job.Quantity >= 1 && rowIndex == jobsList.Count - 1)
                {
                    Job temp = jobsList.FirstOrDefault(m => m.ProductID == job.ProductID && m.ProductTypeID == job.ProductTypeID && m.ProductSizeID == job.ProductSizeID);
                    int tempRowIndex = jobsList.IndexOf(temp);
                    if (tempRowIndex != rowIndex)
                    {
                        jobsList[tempRowIndex].Quantity += job.Quantity;
                        jobsList[rowIndex] = new Job { };
                        rcPriceList[rowIndex] = 0;
                        ccPriceList[rowIndex] = 0;

                        bindJobsGridView();
                        return;
                    }
                }

                if (job.ProductID >= 1)
                {
                    priceList = dBConnection.prices.Where(m => m.ProductID == job.ProductID).ToList();
                    productTypeList.Clear();
                    productSizeList.Clear();
                    foreach (Price price in priceList)
                    {
                        if (productTypeList.SingleOrDefault(m => m.ID == price.ProductTypeID) == null)
                            productTypeList.Add(dBConnection.productTypes.SingleOrDefault(m => m.ID == price.ProductTypeID));
                    }
                    
                }
                if (job.ProductTypeID >= 1)
                {
                    priceList = priceList.Where(m => m.ProductTypeID == job.ProductTypeID).ToList();
                    productSizeList.Clear();
                    foreach (Price price in priceList)
                    {
                        if (productSizeList.SingleOrDefault(m => m.ID == price.ProductSizeID) == null)
                            productSizeList.Add(dBConnection.sizes.SingleOrDefault(m => m.ID == price.ProductSizeID));
                    }
                    

                }
                if (job.ProductSizeID >= 1)
                {
                    priceList = priceList.Where(m => m.ProductSizeID == job.ProductSizeID).ToList();
                    if (selectedCustomer.CustomerType.ToLower() == "rc")
                        job.UnitPrice = priceList[0].RegularPrice;
                    else
                        job.UnitPrice = priceList[0].CounterPrice;


                    rcPriceList[rowIndex] = priceList[0].RegularPrice;
                    ccPriceList[rowIndex] = priceList[0].CounterPrice;
                    jobsList[rowIndex] = job;
                   

                }
                //bindJobsGridView();
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

        private void JobsGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (setJobsGridReadOnly)
                    return;
                switch (e.KeyCode.ToString().ToLower())
                {
                    case "return":
                        if (!e.Shift)
                            return;
                        if (jobsList[jobsList.Count - 1].Quantity == 0)
                        {
                            MessageBox.Show("New entry can't be added if current entry is not complete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        shiftEnterPressed = true;
                        bindJobsGridView();
                        break;
                    case "delete":
                        if (MessageBox.Show("Are you sure you want to delete selected record(s)", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                            return;
                        foreach (DataGridViewRow r in JobsGrid.SelectedRows)
                        {
                            if (!(r.Index >= 0 && r.Index <= jobsList.Count - 1))
                                continue;
                            jobsList.RemoveAt(r.Index);
                            rcPriceList.RemoveAt(r.Index);
                            ccPriceList.RemoveAt(r.Index);
                        }
                        if (jobsList.Count == 0)
                        {
                            jobsList.Add(new Job { UnitPrice = 0 });
                            rcPriceList.Clear();
                            ccPriceList.Clear();
                            rcPriceList.Add(0);
                            ccPriceList.Add(0);
                        }
                        setValuesInJobListAndBindGridView(jobsList.Count - 1);
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

        private void PrevBal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (totalPrice <= 0)
                    return;
                setTextBoxes(sender);
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

        private void btnInvoiceSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(totalPrice >= 1))
                {
                    MessageBox.Show("Please make the sure that total price is greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DateTime dtForInvoice = DateTime.Now;
                if (isDtForInvoiceChanged)
                {
                    dtForInvoice = dtpForInvoice.Value;
                    TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    dtForInvoice = dtForInvoice.Date + ts;
                }
                Invoice invoice = new Invoice { FinalPrice = totalPrice, customerID = selectedCustomer.ID, InvoiceDateTime = dtForInvoice, Paid = -100, workStatus = "NC" };
                dBConnection.invoices.Add(invoice);
                dBConnection.SaveChanges();

                selectedInvoice = dBConnection.invoices.SingleOrDefault(m => m.InvoiceDateTime == invoice.InvoiceDateTime && m.FinalPrice == invoice.FinalPrice && m.customerID == invoice.customerID && m.Paid == invoice.Paid);
                if (selectedInvoice == null)
                {
                    MessageBox.Show("Something went wrong.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                List<Job> jobsToAdd = new List<Job>();
                foreach (Job job in jobsList)
                {
                    if (job.Quantity >= 1 && job.UnitPrice >= 1)
                    {
                        job.InvoiceID = selectedInvoice.ID;
                        jobsToAdd.Add(job);
                    }
                }
                dBConnection.jobs.AddRange(jobsToAdd);
                dBConnection.SaveChanges();
                MessageBox.Show("Invoice saved successfully without payment.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (MessageBox.Show("Do you want to print job Cover for current order ?  If yes then make sure that you have entered a blank job cover in the printer page stack and press yes.", "For Invoice ID " + invoice.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                {
                    InitializePrinting(true,false);
                }
                resetFormView();
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
        string[] replaceWith;

        private void InitializePrinting(bool hidePaymentClasses,bool hideInvoiceClass)
        {
            try
            {
                string html = File.ReadAllText("htmlpage1.html");
                if (hidePaymentClasses)
                    html = Regex.Replace(html, "PAYMENTCLASS", "invisible");
                if (hideInvoiceClass)
                    html = Regex.Replace(html, "INVOICECLASS", "invisible");
                string[] toReplace = new string[] {"#INVOICEDATETIME#" ,
"#PAYMENTDATETIME#" ,
"#TXTCUSTOMERNAME#" ,
"#TXTCUSTOMERADDRESS#" ,
"#LBLPREVIOUS#" ,
"#TXTPREVIOUS#" ,
"#TXTGRANDTOTAL#" ,
"#TXTDISCOUNT#" ,
"#FINALPRICE#" ,
"#TXTPAID#" ,
"#TXTCURRENT#",
"#LBLCURRENT#",
"#TXTINVOICEID#",
"#INVOICETABLECODE#"
            };
                replaceWith = new string[14];
                ReplaceContentsInInvoiceHtmlFile();
                for (int i = 0; i < toReplace.Count(); i++)
                {
                    html = Regex.Replace(html, toReplace[i], replaceWith[i]);
                }
                File.WriteAllText("invoice.html", html);
                if (isForPrint)
                {
                    var f = new PrintPage(html);
                    f.FormClosed += (s, e) => Close();
                    f.Show();
                }
                else
                {
                    new PrintPage(html).Show();
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

        private void ReplaceContentsInInvoiceHtmlFile()
        {
            try
            {
                if (selectedInvoice.PrevBal >= 0)
                {
                    replaceWith[4] = "Prev Bal:";
                    replaceWith[5] = (selectedInvoice).PrevBal + "";
                }
                else
                {
                    replaceWith[4] = "Prev Due:";
                    replaceWith[5] = -1 * (selectedInvoice).PrevBal + "";
                }

                replaceWith[9] = selectedInvoice.Paid + "";
                if (selectedInvoice.CurrentBal >= 0)
                {
                    replaceWith[11] = "Balance:";
                    replaceWith[10] = "" + (selectedInvoice).CurrentBal;
                }
                else
                {
                    replaceWith[11] = "Due:";
                    replaceWith[10] = "" + -1 * (selectedInvoice).CurrentBal;
                }


                replaceWith[12] = selectedInvoice.ID + "";
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(selectedCustomer.FirmName))
                    sb.Append("," + selectedCustomer.FirmName);
                if (!string.IsNullOrWhiteSpace(selectedCustomer.EmailId))
                    sb.Append("," + selectedCustomer.EmailId);

                if (!string.IsNullOrWhiteSpace(selectedCustomer.MobileNumber))
                    sb.Append("," + selectedCustomer.MobileNumber);
                sb.Append("," + selectedCustomer.Address);
                sb.Append("," + dBConnection.states.SingleOrDefault(m => m.ID == selectedCustomer.State).Name);
                sb.Append("," + dBConnection.countries.SingleOrDefault(m => m.ID == selectedCustomer.Country).Name);
                if (sb[0] == ',') sb[0] = ' ';


                replaceWith[3] = sb.ToString();

                replaceWith[2] = selectedCustomer.FirstName + " " + selectedCustomer.LastName;
                replaceWith[0] = "INV DT : " + selectedInvoice.InvoiceDateTime.ToString();
                replaceWith[1] = "PAY DT : " + selectedInvoice.PaymentDateTime.ToString();

                replaceWith[9] = selectedInvoice.Paid + "";
                replaceWith[7] = selectedInvoice.Discount + "";
                replaceWith[8] = selectedInvoice.FinalPrice + "";
                replaceWith[6] = selectedInvoice.Discount + (selectedInvoice).FinalPrice + "";

                replaceWith[13] = invoiceTableHtmlString;
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



        private void btnPaymentSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(totalPrice >= 1))
                {
                    MessageBox.Show("Please make the sure that total price is greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!StaticFunctions.IsNumber(txtDiscount.Text))
                {
                    if (MessageBox.Show("Entered discount price is not valid and changed to 0.Do you want to save invoice with 0 discount ? If yes then click yes or click no to change discount price.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                        return;
                }
                if (!StaticFunctions.IsNumber(txtPaid.Text))
                {
                    if (MessageBox.Show("Entered paid amount is not valid and changed to 0.Do you want to save invoice with 0 paid amount ? If yes then click yes or click no to change paid amount.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() != "yes")
                        return;
                }

                DateTime dtForPayment = DateTime.Now;
                DateTime dtForInvoice = DateTime.Now;
                if (isDtForInvoiceChanged)
                {
                    dtForInvoice = dtpForInvoice.Value;
                    TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    dtForInvoice = dtForInvoice.Date + ts;
                }
                if (isDtForPaymentChanged)
                {
                    dtForPayment = dtpForInvoiceWithPayment.Value;
                    TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    dtForPayment = dtForPayment.Date + ts;
                }
                if (dtForInvoice > dtForPayment)
                {
                    MessageBox.Show("Invoice date should not be greater than Payment date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DailyCount todayCount = dBConnection.dailyCount.SingleOrDefault(m => DbFunctions.TruncateTime(m.dateTime) == dtForPayment.Date);
                if (todayCount == null)
                {
                    dBConnection.dailyCount.Add(new DailyCount { dateTime = DateTime.Now });
                    dBConnection.SaveChanges();
                    todayCount = dBConnection.dailyCount.SingleOrDefault(m => DbFunctions.TruncateTime(m.dateTime) == dtForPayment.Date);
                }
                Invoice invoice;
                if (!setJobsGridReadOnly)
                {

                    invoice = new Invoice { CurrentBal = balance, customerID = selectedCustomer.ID, Discount = discount, FinalPrice = discountedPrice, InvoiceDateTime = dtForInvoice, Paid = currentPaid, PaymentDateTime = dtForPayment, PrevBal = selectedCustomer.Balance, workStatus = "NC" };
                    dBConnection.invoices.Add(invoice);
                    dBConnection.SaveChanges();

                    Invoice temp = dBConnection.invoices.SingleOrDefault(m => m.CurrentBal == invoice.CurrentBal && m.customerID == invoice.customerID && m.Discount == invoice.Discount && m.FinalPrice == invoice.FinalPrice && m.InvoiceDateTime == invoice.InvoiceDateTime && m.Paid == invoice.Paid && m.PaymentDateTime == invoice.PaymentDateTime && m.PrevBal == invoice.PrevBal);
                    if (temp == null)
                    {
                        MessageBox.Show("Something went wrong.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    selectedInvoice = temp;
                    List<Job> jobsToAdd = new List<Job>();
                    foreach (Job job in jobsList)
                    {
                        if (job.Quantity >= 1 && job.UnitPrice >= 1)
                        {
                            job.InvoiceID = temp.ID;
                            jobsToAdd.Add(job);
                        }
                    }
                    dBConnection.jobs.AddRange(jobsToAdd);
                    dBConnection.SaveChanges();
                    MessageBox.Show("Invoice and payment saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBox.Show("Do you want to print job Cover with invoice and payment for this order ?  If yes then make sure that you have entered a blank job cover in the printer page stack and press yes.", "For Invoice ID " + invoice.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                    {
                        InitializePrinting(false,false);
                    }

                }
                else
                {
                    invoice = selectedInvoice;
                    var invoiceEntry = dBConnection.Entry(invoice);
                    if (invoiceEntry.State == EntityState.Detached)
                        dBConnection.invoices.Attach(invoice);
                    invoice.CurrentBal = balance;
                    invoice.Discount = discount;
                    invoice.FinalPrice = discountedPrice;
                    invoice.Paid = currentPaid;
                    invoice.PaymentDateTime = DateTime.Now;
                    invoice.PrevBal = selectedCustomer.Balance;
                    dBConnection.SaveChanges();
                    MessageBox.Show("Payment saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBox.Show("Do you want to print payment information on job cover for this order  ?  If yes then make sure that you have entered a job cover with ID " + invoice.ID + " in the printer page stack and press yes.", "For Invoice ID " + invoice.ID, MessageBoxButtons.YesNo, MessageBoxIcon.Information).ToString().ToLower() == "yes")
                    {
                        InitializePrinting(false,true);

                    }

                }


                var Customerentry = dBConnection.Entry(selectedCustomer);
                if (Customerentry.State == EntityState.Detached)
                    dBConnection.customers.Attach(selectedCustomer);
                selectedCustomer.Balance = balance;
                var DailyCountEntry = dBConnection.Entry(todayCount);
                if (DailyCountEntry.State == EntityState.Detached)
                    dBConnection.dailyCount.Attach(todayCount);
                float count = todayCount.Count;
                todayCount.Count = count + currentPaid;
                dBConnection.SaveChanges();
                resetTextBoxes();
                resetFormView();
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

        private void resetFormView()
        {
            selectedCustomer = null;
            selectedInvoice = null;
            customerList = null;
            setJobsGridReadOnly = false;
            jobsList = new List<Job>() { };
            ccPriceList = new List<float>() { 0 };
            rcPriceList = new List<float>() { 0 };
            productList = new List<Product>();
            productTypeList = new List<ProductType>();
            productSizeList = new List<Size>();
            tempProductList = new List<Product>();
            tempProductTypeList = new List<ProductType>();
            tempProductSizeList = new List<Size>();
            priceList = new List<Price>();
            totalPrice = 0;
            totalQuantity = 0;
            shiftEnterPressed = true;
            balance = 0;
            grandTotal = 0;
            discount = 0;
            discountedPrice = 0;
            currentPaid = 0;
            init();
            bindJobsGridView();
            InputComboBox.Visible = true;
            CustomerNameDisplayPanel.Visible = false;
            InputComboBox.Text = "";
            JobsGrid.Visible = false;
            BottomPanel.Visible = false;
            JobsGrid.Focus();
        }
        private void dtpForInvoice_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as DateTimePicker).Value.Date > DateTime.Now.Date)
                {
                    (sender as DateTimePicker).Value = DateTime.Now;
                    MessageBox.Show("Selected date should not be greater than today.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!(totalPrice >= 1))
            {
                MessageBox.Show("Please make the sure that total price is greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CopyForm copyForm = new CopyForm(selectedCustomer);
            copyForm.Show();
            Hide();
            copyForm.FormClosing += (s, e1) => {
                Show();
            };
        }
    }
}