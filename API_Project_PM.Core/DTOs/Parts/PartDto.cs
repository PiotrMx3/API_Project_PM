using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.Parts
{
    public class PartDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public bool IsSellItem { get; set; }
        public string AddInfo { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int? DefaultLocationId { get; set; }
    }
}
