using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "De naam van de leverancier is verplicht")]
        [MaxLength(100, ErrorMessage = "De naam mag maximaal 100 karakters lang zijn")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het btw-nummer is verplicht")]
        [MaxLength(30, ErrorMessage = "Het btw-nummer mag maximaal 30 karakters lang zijn")]
        public string VatNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het e-mailadres is verplicht")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailadres")]
        [MaxLength(100, ErrorMessage = "Het e-mailadres mag maximaal 100 karakters lang zijn")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het land is verplicht")]
        [MaxLength(50, ErrorMessage = "Het land mag maximaal 50 karakters lang zijn")]
        public string Country { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "De betalingsvoorwaarden mogen maximaal 100 karakters lang zijn")]
        public string PaymentTerms { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "Het belastingtarief moet tussen 0 en 100 liggen")]
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; }

    }
}