using System.Windows.Forms;

namespace VivaBillingDesktopApplication
{
    public partial class ExceptionForm : Form
    {
        private string exceptionMessage;

        public ExceptionForm()
        {
            InitializeComponent();
        }

        public ExceptionForm(string exceptionMessage)
        {
            Text = "Startup Exception";
            this.exceptionMessage = exceptionMessage;
            InitializeComponent();
            MessageBox.Show(exceptionMessage, "Startup Exception");
        }
    }
}
