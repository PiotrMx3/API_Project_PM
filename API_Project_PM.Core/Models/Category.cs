using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "De naam van de categorie is verplicht")]
        [MaxLength(50, ErrorMessage = "De naam van de categorie mag maximaal 50 karakters lang zijn")]
        public string Name { get; set; } = string.Empty;
    }
}
