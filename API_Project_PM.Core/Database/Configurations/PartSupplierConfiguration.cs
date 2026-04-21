using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Project_PM.Core.Database.Configurations
{
    public class PartSupplierConfiguration : IEntityTypeConfiguration<PartSupplier>
    {
        public void Configure(EntityTypeBuilder<PartSupplier> builder)
        {

            builder.ToTable("PartSuppliers");

            // Composite PK
            builder.HasKey(ps => new { ps.PartId, ps.SupplierId });

            builder.Property(ps => ps.SupplierPrice)
                .HasColumnType("decimal(10,2)");

            builder.HasQueryFilter(ps => !ps.Part.IsDeleted);

            builder.Property(ps => ps.IsPreferred)
                .HasDefaultValue(false);

            builder.HasOne(ps => ps.Part)
                .WithMany(p => p.PartSuppliers)
                .HasForeignKey(ps => ps.PartId)

                // Can not remove part when PS existing
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ps => ps.Supplier)
                 .WithMany(s => s.PartSuppliers)
                 .HasForeignKey(ps => ps.SupplierId)
                 //Can not remove supplier when PS existing
                 .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
