using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.DTOs.StockItems
{
    public class UpdateStockItemDto
    {
        [Required]
        [Range(1, 100000, ErrorMessage = "Aantal moet groter dan 0 en lager of gelijk aan 100000 zijn")]
        public int Quantity { get; set; }
    }
}
