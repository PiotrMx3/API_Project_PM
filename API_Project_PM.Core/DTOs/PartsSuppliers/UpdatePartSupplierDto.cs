using System.ComponentModel.DataAnnotations;


namespace API_Project_PM.Core.DTOs.PartsSuppliers
{
    public class UpdatePartSupplierDto
    {
        [Required]
        [Range(0.01, 999999.99)]
        public decimal SupplierPrice { get; set; }

        [Required]
        public bool IsPreferred { get; set; } = false;
    }
}
