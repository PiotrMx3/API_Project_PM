using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.DTOs.Parts
{
    public class UpdatePartDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "De Naam mag max 100 tekens lang zijn")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(00000000.01, 99999999.99)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "De eenheid mag max 20 tekens lang zijn")]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public bool IsSellItem { get; set; }

        [StringLength(500, ErrorMessage = "De commentar mag max 500 tekens lang zijn")]
        public string AddInfo { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        public int? DefaultLocationId { get; set; }
    }
}
