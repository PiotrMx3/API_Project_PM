using API_Project_PM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Project_PM.Core.DTOs.Locations
{
    public class LocationDto
    {
        public string Zone { get; set; } = string.Empty;
        public string Rack { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public string Box { get; set; } = string.Empty;
        public LocationType LocationType { get; set; }
        
    }
}
