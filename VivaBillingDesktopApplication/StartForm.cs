using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VivaBillingDesktopApplication.Database;

namespace VivaBillingDesktopApplication
{
    public partial class StartForm : Form
    {
        float expenseCount = 0;
        float cashCount = 0;

        DBConnection dBConnection = new DBConnection();
        private Font selectedFont;

        public StartForm()
        {
            InitializeComponent();
            initVariables();
            Timer timer = new Timer();

            VisibleChanged += (s, e) =>
            {
                if (Visible)
                    setTodayCount();
            };
            timer.Tick += (s, e) => { if (Visible) lblTimer.Text = DateTime.Now.ToString(); };
            timer.Start();
        }
        private void initVariables()
        {
            expenseCount = cashCount = 0;
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
        private void setTodayCount()
        {
            try
            {
                DailyCount todayCount = dBConnection.dailyCount.SingleOrDefault(m => DbFunctions.TruncateTime(m.dateTime) == DbFunctions.TruncateTime(DateTime.Now));
                if (todayCount == null)
                {
                    dBConnection.dailyCount.Add(new DailyCount { Count = 0, dateTime = DateTime.Now });
                    dBConnection.SaveChanges();
                    todayCount = dBConnection.dailyCount.SingleOrDefault(m => DbFunctions.TruncateTime(m.dateTime) == DbFunctions.TruncateTime(DateTime.Now));
                }
                var entry = dBConnection.Entry(todayCount);
                entry.Reload();
                cashCount = todayCount.Count;
                List<Expense> todaysExpenseList = dBConnection.expenses.Where(m => m.dailyCountID == todayCount.ID).ToList();
                if (todaysExpenseList.Count == 0)
                {
                    lblTodaysExpense.Text = "Today's expense count:0";
                }
                else
                {
                    foreach (Expense expense in todaysExpenseList)
                    {
                        expenseCount += expense.Price;
                    }
                    lblTodaysExpense.Text = "Today's expense :" + expenseCount;
                }
                lblTodaysCount.Text = "Today's count:" + todayCount.Count;
                lblCashCount.Text = "Cash count:" + (cashCount - expenseCount);
                cashCount = 0;
                expenseCount = 0;
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

        private void mainToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var form = new MainPage();
            setConfigAndShowNextPage(form);

        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddProduct();
            setConfigAndShowNextPage(form);

        }

        private void productTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var form = new AddProductType();
            setConfigAndShowNextPage(form);

        }

        private void productSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddProductSize();
            setConfigAndShowNextPage(form);

        }

        private void priceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var form = new AddPrice();
            setConfigAndShowNextPage(form);

        }

        private void stateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var form = new AddState();
            setConfigAndShowNextPage(form);

        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddCountry();
            setConfigAndShowNextPage(form);

        }

        private void paymentMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddPaymentMethod();
            setConfigAndShowNextPage(form);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new FindInvoices();
            setConfigAndShowNextPage(form);

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new AddCustomer();
            setConfigAndShowNextPage(form);

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var form = new AddExpense();
            setConfigAndShowNextPage(form);

        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (fontDialog1.ShowDialog().ToString().ToLower() != "ok") return;
                selectedFont = fontDialog1.Font;
                Font = selectedFont;
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

        private void notifyCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new NotifyUser();
            setConfigAndShowNextPage(form);
        }

        private void testPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ShowExpenses();
            setConfigAndShowNextPage(form);
        }

        private void setConfigAndShowNextPage(Form form)
        {
            try
            {
                Hide();
                form.Show();
                if (selectedFont != null)
                    form.Font = selectedFont;
                form.FormClosed += (s, e1) => Show();
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

        private void copyPicturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var form = new CopyForm();
            //setConfigAndShowNextPage(form);
        }

        private void updateCustomerBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Owner_Control();
            setConfigAndShowNextPage(form);
        }
    }
}