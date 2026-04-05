using API_Project_PM.Core.Enums;
using API_Project_PM.Core.Interfaces;

namespace API_Project_PM.Core.Models
{
    public class Location: ISoftDeletable
    {
        public int Id { get; set; }
        public string Zone { get; set; } = string.Empty;
        public string Rack { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public string Box { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public LocationType LocationType { get; set; }
    }
}
