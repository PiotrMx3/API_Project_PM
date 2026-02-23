using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Models
{
    public class StockItem
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
    }
}
