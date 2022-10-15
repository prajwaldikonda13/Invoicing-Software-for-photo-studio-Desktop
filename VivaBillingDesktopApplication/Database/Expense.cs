using System;

namespace VivaBillingDesktopApplication.Database
{
    public class Expense
    {
        public long ID { get; set; }
        public long dailyCountID { get; set; }
        public string ItemName { get; set; }
        public string PartyName { get; set; }
        public float Quantity { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}