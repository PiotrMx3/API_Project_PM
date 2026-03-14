using API_Project_PM.Models;
using API_Project_PM.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            IEnumerable<Category> result = await _categoryRepository.GetAllCategories();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Category?>> GetCategoryById(int id)
        {
            Category? result = await _categoryRepository.GetCategoryById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreateCategory(Category item)
        {
            if (item is null) return BadRequest();

            await _categoryRepository.CreateCategory(item);

            return CreatedAtAction(nameof(GetCategoryById), new {id = item.Id }, item);
        }


    }
}
