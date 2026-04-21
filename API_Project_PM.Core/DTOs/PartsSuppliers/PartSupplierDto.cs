using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
