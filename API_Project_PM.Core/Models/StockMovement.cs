using API_Project_PM.Core.Enums;

namespace API_Project_PM.Core.Models
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public Guid TransferGroupId { get; set; }

        // Navigation 
        public Part Part { get; set; } = null!;
        public Location Location { get; set; } = null!;
    }
}
