using System;

namespace VivaBillingDesktopApplication.Database
{
    public class DailyCount
    {
        public long ID { get; set; }
        public DateTime dateTime { get; set; }
        public float Count { get; set; }
    }
}