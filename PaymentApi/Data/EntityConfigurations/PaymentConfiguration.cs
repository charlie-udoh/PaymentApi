using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentApi.Entities;

namespace PaymentApi.Data.EntityConfigurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CreditCardNumber).IsRequired(true).HasMaxLength(50);
            builder.Property(s => s.CardHolder).IsRequired(true).HasMaxLength(100);
            builder.Property(s => s.ExpirationDate).IsRequired(true);
            builder.Property(s => s.SecurityCode).HasMaxLength(3);
            builder.Property(s => s.Amount).IsRequired(true).HasPrecision(16, 4);
            builder.HasOne(s=>s.PaymentLog).WithOne(ad => ad.Payment).HasForeignKey<PaymentLog>(ad => ad.PaymentId);
        }
    }
}
