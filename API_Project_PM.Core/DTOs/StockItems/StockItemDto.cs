namespace API_Project_PM.Core.DTOs.StockItems
{
    public class StockItemDto
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string PartLocation { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
