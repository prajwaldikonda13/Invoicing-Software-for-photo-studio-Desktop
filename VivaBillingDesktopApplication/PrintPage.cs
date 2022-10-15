using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace VivaBillingDesktopApplication
{

    public partial class PrintPage : Form
    {
        public ChromiumWebBrowser browser;
        public PrintPage()
        {
            InitializeComponent();
        }

        public PrintPage(string html)
        {

            InitializeComponent();
            try
            {
                if (!Cef.IsInitialized)
                    Cef.Initialize(new CefSettings());
                browser = new ChromiumWebBrowser("https://fakeurl.com") { Dock = DockStyle.Fill };
                Controls.Add(browser);
                browser.IsBrowserInitializedChanged += (s, e) =>
                {
                    if (e.IsBrowserInitialized)
                        browser.LoadHtml(html, "https://fakeurl.com");
                };
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