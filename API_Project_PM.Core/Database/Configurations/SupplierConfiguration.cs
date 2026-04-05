using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Project_PM.Core.Database.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.VatNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(s => s.VatNumber)
                .IsUnique()
                .HasFilter("[VatNumber] IS NOT NULL")
                .HasDatabaseName("IX_Suppliers_VatNumber");

            builder.Property(s => s.ContactEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Country)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.PaymentTerms)
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder.Property(s => s.TaxRate)
                .HasColumnType("decimal(4,2)");

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

        }
    }
}
