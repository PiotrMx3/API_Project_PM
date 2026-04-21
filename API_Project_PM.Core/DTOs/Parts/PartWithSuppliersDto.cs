using API_Project_PM.Core.DTOs.Suppliers;
using API_Project_PM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.Parts
{
    public class PartWithSuppliersDto
    {
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public bool IsSellItem { get; set; }
        public string AddInfo { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string? DefaultLocation { get; set; }
        public IEnumerable<SupplierDto> Suppliers { get; set; } = null!;

    }
}
