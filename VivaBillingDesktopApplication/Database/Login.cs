using System;

namespace VivaBillingDesktopApplication.Database
{
    public class Login
    {
        public long ID { get; set; }
        public string IP { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
    }
}