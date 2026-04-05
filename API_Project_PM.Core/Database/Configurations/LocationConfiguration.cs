using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Project_PM.Core.Database.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Zone)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(l => l.Rack)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(l => l.Shelf)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(l => l.Box)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(l => l.LocationType)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasIndex(l => new { l.Zone, l.Rack, l.Shelf, l.Box })
                .IsUnique()
                .HasDatabaseName("IX_Locations_Zone_Rack_Shelf_Box");

            builder.Property(l => l.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(l => l.DeletedAt)
                .IsRequired(false);
            // Filter on delet items
            builder.HasQueryFilter(l => !l.IsDeleted);

        }
    }
}
