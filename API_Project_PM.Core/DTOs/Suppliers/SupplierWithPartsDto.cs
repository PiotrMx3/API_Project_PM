using API_Project_PM.Core.DTOs.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.Suppliers
{
    public class SupplierWithPartsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VatNumber { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PaymentTerms { get; set; } = string.Empty;
        public decimal TaxRate { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<PartDto> Parts { get; set; } = null!;
    }
}
