namespace VivaBillingDesktopApplication.Database
{
    public class Price
    {
        public long ID { get; set; }
        public long ProductID { get; set; }
        public long ProductTypeID { get; set; }
        public long ProductSizeID { get; set; }
        public float RegularPrice { get; set; }
        public float CounterPrice { get; set; }
    }
}