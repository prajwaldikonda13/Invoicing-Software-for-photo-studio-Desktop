namespace VivaBillingDesktopApplication.Database
{
    public class Job
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public long ProductTypeID { get; set; }
        public long ProductSizeID { get; set; }
        public float UnitPrice { get; set; }
        public float Quantity { get; set; }
        //public float TotalPrice { get; set; }
        public long InvoiceID { get; set; }
        public string Status { get; set; }
    }
}