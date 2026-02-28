namespace API_Project_PM.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
        public int LocationId { get; set; }
        public bool isSellItem { get; set; }
        public string AddInfo { get; set; } = string.Empty;

    }
}
