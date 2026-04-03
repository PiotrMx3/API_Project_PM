using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "De zone is verplicht")]
        [MaxLength(10, ErrorMessage = "De zone mag maximaal 10 karakters lang zijn")]
        public string Zone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Het rek (rack) is verplicht")]
        [MaxLength(10, ErrorMessage = "Het rek mag maximaal 10 karakters lang zijn")]
        public string Rack { get; set; } = string.Empty;

        [Required(ErrorMessage = "De plank (shelf) is verplicht")]
        [MaxLength(10, ErrorMessage = "De plank mag maximaal 10 karakters lang zijn")]
        public string Shelf { get; set; } = string.Empty;

        [Required(ErrorMessage = "De box is verplicht")]
        [MaxLength(20, ErrorMessage = "De box mag maximaal 20 karakters lang zijn")]
        public string Box { get; set; } = string.Empty;

    }
}
