namespace API_Project_PM.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VatNumber { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PaymentTerms { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; }

    }
}
