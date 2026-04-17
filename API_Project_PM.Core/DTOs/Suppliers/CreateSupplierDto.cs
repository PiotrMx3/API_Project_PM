using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.DTOs.Suppliers
{
    public class CreateSupplierDto
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "De naam moet tussen de 5 en 100 tekens lang zijn")]
        public string Name { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "De vat nummer mag max 30 tekens lang zijn")]
        public string VatNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "De e-mail mag max 100 tekens lang zijn")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "De land mag max 50 tekens lang zijn")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "De betalingsvoorwaarden moeten tussen de 5 en 100 tekens lang zijn")]
        public string PaymentTerms { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 99.99)]
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; }
    }
}
