using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VivaBillingDesktopApplication
{
    static class Program
    {
        public static string[] dbVariables = new string[4];
        public static bool showExceptionsInMessageBox = false;
        public static bool showStartupExceptionsInMessageBox = false;
        public static string SMSConfigCountry;
        public static string SMSConfigSender;
        public static string SMSConfigAuthKey;
        public static string WelcomeMessage;
        public static string EmailConfigFromEmailID;
        public static string EmailConfigUserName;
        public static string EmailConfigPassword;
        public static string EmailConfigDisplayName;
        public static string EmailConfigSMTPHost;
        public static string EmailConfigSMTPPort;
        public static string ImageCopyFolder;
        private static XmlDocument xmlDocument = new XmlDocument();

        //SMSConfigWelcomeMessage Value=""></WelcomeMessage>

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.FirstChanceException += (s, e) =>{
                StreamWriter sr = new StreamWriter("StartupException.txt");
                sr.Write("");
                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine+"Message=" + e.Exception.Message);
                sb.Append(Environment.NewLine + "InnerException=" + e.Exception.InnerException);
                sb.Append(Environment.NewLine + "Source=" + e.Exception.Source);
                sb.Append(Environment.NewLine+"Data=" + e.Exception.Data);
                sb.Append(Environment.NewLine + "StackTrace=" + e.Exception.StackTrace);
                sb.Append(Environment.NewLine+"HelpLink=" + e.Exception.HelpLink);
                sb.Append(Environment.NewLine+"HResult=" + e.Exception.HResult);
                sb.Append(Environment.NewLine+"TargetSite=" + e.Exception.TargetSite);
                string exceptionMessage = sb.ToString();
                sr.WriteLine(exceptionMessage);
                sr.Close();
                sr.Dispose();
                if(showStartupExceptionsInMessageBox)
                new ExceptionForm(exceptionMessage);
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            getConfigurationValues();
            Application.Run(new StartForm());
        }

        private static void getConfigurationValues()
        {
            xmlDocument.Load("Configuration.xml");
            getDatabaseConfigVariables();
            getSMSConfigVariables();
            getEmailConfigVariables();
            getExceptionConfigVariables();
            getGeneralConfigVariables();
            ImageCopyFolder = xmlDocument.SelectSingleNode("Root/Configuration/FolderConfigs/ImageCopyFolder").Attributes["Value"].InnerText; ;
        }

        private static void getEmailConfigVariables()
        {
            EmailConfigFromEmailID = xmlDocument.SelectSingleNode("Root/Configuration/EmailConfig/FromEmailID").Attributes["Value"].InnerText; ;
            EmailConfigUserName = xmlDocument.SelectSingleNode("Root/Configuration/EmailConfig/EmailUserName").Attributes["Value"].InnerText; ;
            EmailConfigPassword = xmlDocument.SelectSingleNode("Root/Configuration/EmailConfig/EmailPassword").Attributes["Value"].InnerText; ;
            EmailConfigDisplayName = xmlDocument.SelectSingleNode("Root/Configuration/EmailConfig/EmailDisplayName").Attributes["Value"].InnerText; ;
            EmailConfigSMTPHost = xmlDocument.SelectSingleNode("Root/Configuration/EmailConfig/SMTPHost").Attributes["Value"].InnerText; ;
            EmailConfigSMTPPort = xmlDocument.SelectSingleNode("Root/Configuration/EmailConfig/SMTPPort").Attributes["Value"].InnerText; ;
        }

        private static void getGeneralConfigVariables()
        {
            WelcomeMessage = xmlDocument.SelectSingleNode("Root/Configuration/Strings/WelcomeMessage").Attributes["Value"].InnerText;
        }

        private static void getExceptionConfigVariables()
        {
            showExceptionsInMessageBox = Convert.ToBoolean(xmlDocument.SelectSingleNode("Root/Configuration/ExceptionConfig/ShowExceptionsInMessageBox").Attributes["Value"].InnerText);
            showStartupExceptionsInMessageBox = Convert.ToBoolean(xmlDocument.SelectSingleNode("Root/Configuration/ExceptionConfig/ShowStartupExceptionsInMessageBox").Attributes["Value"].InnerText);
        }

        private static void getSMSConfigVariables()
        {
            SMSConfigCountry = xmlDocument.SelectSingleNode("Root/Configuration/SMSConfig/Country").Attributes["Value"].InnerText;
            SMSConfigSender = xmlDocument.SelectSingleNode("Root/Configuration/SMSConfig/Sender").Attributes["Value"].InnerText;
            SMSConfigAuthKey = xmlDocument.SelectSingleNode("Root/Configuration/SMSConfig/AuthKey").Attributes["Value"].InnerText;
        }

        private static void getDatabaseConfigVariables()
        {
            dbVariables[0] = xmlDocument.SelectSingleNode("Root/Configuration/DatabaseConfig/Server").Attributes["Value"].InnerText;
            dbVariables[1] = xmlDocument.SelectSingleNode("Root/Configuration/DatabaseConfig/Database").Attributes["Value"].InnerText;
            dbVariables[2] = xmlDocument.SelectSingleNode("Root/Configuration/DatabaseConfig/UserID").Attributes["Value"].InnerText;
            dbVariables[3] = xmlDocument.SelectSingleNode("Root/Configuration/DatabaseConfig/Password").Attributes["Value"].InnerText;
        }
    }
}