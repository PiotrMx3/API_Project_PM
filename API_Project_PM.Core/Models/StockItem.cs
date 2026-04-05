namespace API_Project_PM.Core.Models
{
    public class StockItem
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }

        // Navigation 

        public Part Part { get; set; } = null!;
        public Location Location { get; set; } = null!;

    }
}
