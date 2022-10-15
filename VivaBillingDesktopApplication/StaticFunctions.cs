using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VivaBillingDesktopApplication
{
    public class StaticFunctions
    {
        public static string ConvertToTitleCase(string str)
        {
            str = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
            return str;
        }
        public static string getValidString(string str, bool allowOneSpace, bool isEmail)
        {
            if (allowOneSpace)
                str = Regex.Replace(str, @"\s+", " ").ToLower().Trim();
            else
                str = Regex.Replace(str, @"\s+", "").ToLower().Trim();
            if (!isEmail)
                str = ConvertToTitleCase(str);
            return str;

        }
        public static bool IsNumber(string str)
        {
            Regex regex = new Regex(@"[^0-9\.]");
            if (regex.IsMatch(str))
                return false;

            if (str.Count(m => m == '.') > 1)
                return false;
            if (str.IndexOf('.') == 0)
                return false;
            if (str.LastIndexOf('.') == str.Length-1)
                return false;
            return true;
        }
        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"[^0-9]");
            if (regex.IsMatch(str))
                return false;
            return true;
        }


        public static void addActionButtonsToGridView(DataGridView grid)
        {
            //DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            //Editlink.UseColumnTextForLinkValue = true;
            //Editlink.HeaderText = "Edit";
            //Editlink.DataPropertyName = "lnkColumn";
            //Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            //Editlink.Text = "Edit";
            //grid.Columns.Add(Editlink);

            //DataGridViewLinkColumn UpdateLink = new DataGridViewLinkColumn();
            //UpdateLink.UseColumnTextForLinkValue = true;
            //UpdateLink.HeaderText = "Update";
            //UpdateLink.DataPropertyName = "lnkColumn";
            //UpdateLink.LinkBehavior = LinkBehavior.SystemDefault;
            //UpdateLink.Text = "Update";
            //grid.Columns.Add(UpdateLink);

            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "Delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            grid.Columns.Add(Deletelink);

            
        }

        public static string getValidEmail(string str)
        {
            return Regex.Replace(str, @"\s+", "").ToLower().Trim();
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidMobile(string mobile)
        {
            try
            {
                if (mobile.Length != 10)
                    return false;
                Regex regex = new Regex("[^0-9]");
                if (regex.IsMatch(mobile))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}