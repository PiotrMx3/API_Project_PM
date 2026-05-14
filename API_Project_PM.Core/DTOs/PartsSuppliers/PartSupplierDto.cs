namespace API_Project_PM.Core.DTOs.PartsSuppliers
{
    public class PartSupplierDto
    {
        public string partName { get; set; } = string.Empty;
        public string supplierName { get; set; } = string.Empty;
        public decimal? SupplierPrice { get; set; }
        public bool IsPreferred { get; set; } = false;

    }
}
