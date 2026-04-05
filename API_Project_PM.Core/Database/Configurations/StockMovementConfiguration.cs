using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace API_Project_PM.Core.Database.Configurations
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements", t =>
                t.HasCheckConstraint("CK_StockMovements_Quantity", "Quantity >= 1"));


            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.MovementType)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(sm => sm.MovementDate)
                .IsRequired()
                .HasDefaultValueSql("(UTC_TIMESTAMP())");

            builder.Property(sm => sm.TransferGroupId)
                .HasDefaultValueSql("(UUID())");

            builder.HasIndex(sm => sm.PartId)
                .HasDatabaseName("IX_StockMovements_PartId");

            builder.HasIndex(sm => sm.MovementDate)
                .HasDatabaseName("IX_StockMovements_MovementDate");

            builder.HasQueryFilter(sm => !sm.Part.IsDeleted && !sm.Location.IsDeleted);

            builder.HasOne(sm => sm.Part)
                .WithMany()
                .HasForeignKey(sm => sm.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sm => sm.Location)
                .WithMany()
                .HasForeignKey(sm => sm.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
