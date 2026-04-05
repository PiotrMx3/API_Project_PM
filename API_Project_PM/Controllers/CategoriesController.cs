using API_Project_PM.Core.Categories;
using API_Project_PM.Core.Database;
using API_Project_PM.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;


        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            IEnumerable<Category> result = await _categoryRepository.GetAllAsync();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category?>> GetCategoryById(int id)
        {
            Category? result = await _categoryRepository.GetByIdAsync(id);

            if (result is null) return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCategory(Category item)
        {
            try
            {
                var created = await _categoryRepository.CreateAsync(item);
                return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, created);
            }
            catch (DbUpdateException)
            {
                return Conflict(new {conflict = "Categorie met deze naam bestaat al" } );
            }
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCategory(int id, Category item)
        {

            if (item is null || id != item.Id) return BadRequest();

            bool updated = await _categoryRepository.UpdateAsync(id, item);

            if (!updated) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var deleted = await _categoryRepository.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return Conflict("Kan categorie niet verwijderen: er zijn onderdelen gekoppeld");
            }
        }
    }
}
