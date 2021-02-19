using Microsoft.EntityFrameworkCore;
using PaymentApi.Data.EntityConfigurations;
using PaymentApi.Entities;

namespace PaymentApi.Data
{
    public class PaymentsDbContext : DbContext
    {
        private readonly string connectionString;

        public PaymentsDbContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }

        public PaymentsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentLog> PaymentLog { get; set; }
    }
}
