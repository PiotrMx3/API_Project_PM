
namespace API_Project_PM.Core.Models
{
    public class PartSupplier
    {
        public int PartId { get; set; }
        public int SupplierId { get; set; }
        public decimal? SupplierPrice { get; set; }
        public bool IsPreferred { get; set; } 

        // Navigation 
        public Part Part { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;



    }
}
