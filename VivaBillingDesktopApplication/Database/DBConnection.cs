using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace VivaBillingDesktopApplication.Database
{
    class DBConnection:DbContext
    {
        public DbSet<State> states { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<PaymentMethod> paymentMethods { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Price> prices { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<EmailVerification> emailVerifications { get; set; }
        public DbSet<MobileVerification> mobileVerifications { get; set; }
        public DbSet<Login> logins { get; set; }
        public DbSet<Command> commands { get; set; }
        public DbSet<DailyCount> dailyCount { get; set; }
        public DbSet<MacAddress> macAddresses { get; set; }
        public DbSet<Expense> expenses { get; set; }
        public DBConnection() : base("Server=your_server;Database=your_database;Integrated Security=true;")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBConnection, DataContextConfiguration>());
            //System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<DBConnection>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<State>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Country>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Customer>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Size>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Product>().HasKey(m => new { m.ID });
            modelBuilder.Entity<PaymentMethod>().HasKey(m => new { m.ID });
            modelBuilder.Entity<ProductType>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Job>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Price>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Invoice>().HasKey(m => new { m.ID });
            modelBuilder.Entity<MobileVerification>().HasKey(m => new { m.ID });
            modelBuilder.Entity<EmailVerification>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Login>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Command>().HasKey(m => new { m.ID });
            modelBuilder.Entity<DailyCount>().HasKey(m => new { m.ID });
            modelBuilder.Entity<MacAddress>().HasKey(m => new { m.ID });
            modelBuilder.Entity<Expense>().HasKey(m => new { m.ID });
        }
    }
    internal sealed class DataContextConfiguration : DbMigrationsConfiguration<DBConnection>
    {
        public DataContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "DataContext";
        }
    }
}