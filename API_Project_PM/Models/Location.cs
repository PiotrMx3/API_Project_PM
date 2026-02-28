namespace API_Project_PM.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Aisle { get; set; } = string.Empty;
        public string Rack { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public string Box { get; set; } = string.Empty;

    }
}
