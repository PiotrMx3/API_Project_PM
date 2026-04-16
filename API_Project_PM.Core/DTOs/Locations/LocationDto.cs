using API_Project_PM.Core.Enums;

namespace API_Project_PM.Core.DTOs.Locations
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Zone { get; set; } = string.Empty;
        public string Rack { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public string Box { get; set; } = string.Empty;
        public LocationType LocationType { get; set; }
        
    }
}
