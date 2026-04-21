using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.PartsSuppliers
{
    public class UpdatePartSupplierDto
    {
        [Required]
        [Range(0.01, 999999.99)]
        public decimal SupplierPrice { get; set; }

        [Required]
        public bool IsPreferred { get; set; } = false;
    }
}
