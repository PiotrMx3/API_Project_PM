using System.ComponentModel.DataAnnotations;

namespace API_Project_PM.Core.DTOs.Categories
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 50 tekens lang zijn")]
        public string Name { get; set; } = string.Empty;
    }
}
