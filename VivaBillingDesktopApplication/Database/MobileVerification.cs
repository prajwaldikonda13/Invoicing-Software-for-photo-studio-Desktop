namespace VivaBillingDesktopApplication.Database
{
    public class MobileVerification
    {
        public long ID { get; set; }
        public string Mobile { get; set; }
        public string OTP { get; set; }
        public string VerificationStatus { get; set; }
    }
}