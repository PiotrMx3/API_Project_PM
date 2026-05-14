namespace API_Project_PM.Core.DTOs.StockMovements
{
    public class StockMovementDto
    {
        public int Id { get; set; }
        public string Part { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public Guid TransferGroupId { get; set; }
    }
}
