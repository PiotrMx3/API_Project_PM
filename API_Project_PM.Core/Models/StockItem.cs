using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.Models
{
    public class StockItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Onderdeel ID (PartId) is verplicht")]
        public int PartId { get; set; }

        [Required(ErrorMessage = "Locatie ID is verplicht")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Het aantal is verplicht")]
        [Range(0, 100000, ErrorMessage = "Het aantal mag niet negatief zijn en moet tussen 0 en 100.000 liggen")]
        public int Quantity { get; set; }
    }
}
