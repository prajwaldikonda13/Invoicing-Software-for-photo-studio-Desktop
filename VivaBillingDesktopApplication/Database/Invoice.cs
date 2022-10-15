using System;

namespace VivaBillingDesktopApplication.Database
{
    public class Invoice
    {
        public long ID { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public float FinalPrice { get; set; }
        public float Paid { get; set; }
        public float Discount { get; set; }
        public float PrevBal { get; set; }
        public float CurrentBal { get; set; }
        public long customerID { get; set; }
        public string workStatus { get; set; }
    }
}