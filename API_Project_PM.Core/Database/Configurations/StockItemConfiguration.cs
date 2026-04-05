
using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Project_PM.Core.Database.Configurations
{
    internal class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {

            builder.ToTable("StockItems", t =>
            {
                t.HasCheckConstraint("CK_StockItems_Quantity", "[Quantity] >= 0");
                t.HasCheckConstraint("CK_StockItems_Quantity_Max", "[Quantity] <= 100000");
            });

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Quantity)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(si => new { si.PartId, si.LocationId })
                .IsUnique()
                .HasDatabaseName("IX_StockItems_PartId_LocationId");

            builder.HasOne(si => si.Part)
                .WithMany(p => p.StockItems)
                .HasForeignKey(si => si.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(si => si.Location)
                .WithMany()
                .HasForeignKey(si => si.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
