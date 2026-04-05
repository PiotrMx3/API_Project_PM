using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Project_PM.Core.Database.Configurations
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("Parts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Sku)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.Sku)
                .IsUnique()
                .HasDatabaseName("IX_Parts_Sku");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.AddInfo)
                .HasMaxLength(500)
                .HasDefaultValue("");

            builder.Property(p => p.IsSellItem)
                .HasDefaultValue(false);

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);
            
            builder
                .Property(p => p.DeletedAt)
                .IsRequired(false);
            
            builder.HasQueryFilter(p => !p.IsDeleted);


            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.DefaultLocation)
                .WithMany()
                .HasForeignKey(p => p.DefaultLocationId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
