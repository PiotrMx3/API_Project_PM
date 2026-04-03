using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.Models
{
    public class StockMovement
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Artikel ID is vereist")]
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Locatie ID is vereist")]
        public int LocationId { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Aantal in een eenmalige operatie moet tussen 1 en 10.000 zijn")]
        public int Quantity { get; set; }

        [Required]
        [RegularExpression("^(IN|OUT)$", ErrorMessage = "Transactiebeweging moet IN of OUT zijn")]
        public string MovementType { get; set; } = string.Empty;

        [Required]
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
    }
}
