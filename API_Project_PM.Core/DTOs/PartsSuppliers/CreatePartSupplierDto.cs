using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.DTOs.PartsSuppliers
{
    public class CreatePartSupplierDto
    {
        [Required]
        public int PartId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Range(0.01, 999999.99)]
        public decimal? SupplierPrice { get; set; }

        public bool IsPreferred { get; set; } = false;
    }
}
