using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Het SKU nummer is verplicht")]
        [MaxLength(50, ErrorMessage = "Het SKU nummer mag maximaal 50 karakters lang zijn.")]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "De naam van het onderdeel is verplicht")]
        [MaxLength(100, ErrorMessage = "De naam mag maximaal 100 karakters lang zijn")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "De prijs is verplicht")]
        [Range(0.01, 100000, ErrorMessage = "De prijs moet groter zijn dan 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "De eenheid (unit) is verplicht")]
        [MaxLength(20, ErrorMessage = "De eenheid mag maximaal 20 karakters lang zijn")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Leverancier ID is verplicht")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Locatie ID is verplicht")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Categorie ID is verplicht")]
        public int CategoryId { get; set; }

        // Boolean is standaard false als deze niet wordt meegegeven
        // Daarom is hier check overbodig
        public bool isSellItem { get; set; }

        [MaxLength(500, ErrorMessage = "Aanvullende informatie mag maximaal 500 karakters lang zijn")]
        public string AddInfo { get; set; } = string.Empty;

    }
}
