namespace VivaBillingDesktopApplication.Database
{
    public class EmailVerification
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public string VerificationStatus { get; set; }
    }
}