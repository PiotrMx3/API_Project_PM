using API_Project_PM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.Locations
{
    public class CreateLocationDto
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "De zone moet tussen de 3 en 10 tekens lang zijn")]
        public string Zone { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "De rek moet tussen de 3 en 10 tekens lang zijn")]
        public string Rack { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "De plank moet tussen de 3 en 10 tekens lang zijn")]
        public string Shelf { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "De doos moet tussen de 3 en 20 tekens lang zijn")]
        public string Box { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(LocationType))]
        public LocationType LocationType { get; set; }

    }
}
