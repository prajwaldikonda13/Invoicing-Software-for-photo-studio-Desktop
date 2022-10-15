namespace VivaBillingDesktopApplication.Database
{
    public class Customer
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirmName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public long State { get; set; }
        public long Country { get; set; }
        public float Balance { get; set; }
        public string CustomerType { get; set; }
    }
}