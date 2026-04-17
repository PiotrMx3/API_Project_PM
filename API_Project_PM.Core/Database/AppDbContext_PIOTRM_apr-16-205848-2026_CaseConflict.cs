using API_Project_PM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Core.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Part> Parts => Set<Part>();
        public DbSet<StockItem> StockItems => Set<StockItem>();
        public DbSet<PartSupplier> PartSuppliers => Set<PartSupplier>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    
    }
}
