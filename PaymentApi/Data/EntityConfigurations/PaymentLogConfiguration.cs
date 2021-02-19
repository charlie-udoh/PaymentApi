using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentApi.Entities;

namespace PaymentApi.Data.EntityConfigurations
{
    public class PaymentLogConfiguration : IEntityTypeConfiguration<PaymentLog>
    {
        public void Configure(EntityTypeBuilder<PaymentLog> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.PaymentId).IsRequired(true);
            builder.Property(s => s.Status).IsRequired(true).HasMaxLength(50);
        }
    }
}
