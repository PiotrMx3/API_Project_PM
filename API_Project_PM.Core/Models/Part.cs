using API_Project_PM.Core.Interfaces;

namespace API_Project_PM.Core.Models
{
    public class Part : ISoftDeletable
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public bool IsSellItem { get; set; }
        public string AddInfo { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int? DefaultLocationId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Navigation for EF for .Include() 
        public Category? Category { get; set; }

        // Deafult location for Part after purchase
        public Location? DefaultLocation { get; set; }

        public ICollection<PartSupplier> PartSuppliers { get; set; } = new List<PartSupplier>();
        public ICollection<StockItem> StockItems { get; set; } = new List<StockItem>();
    }
}
