using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.CustomExceptions;
using API_Project_PM.Core.DTOs.Categories;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;



        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            IEnumerable<Category> result = await _categoryRepository.GetAllAsync();

            if (!result.Any()) return Ok(Array.Empty<CategoryDto>());

            IEnumerable<CategoryDto> response = _mapper.Map<IEnumerable<CategoryDto>>(result);

            return Ok(response);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {

            if (id <= 0) return BadRequest();


            Category? result = await _categoryRepository.GetByIdAsync(id);

            if (result is null) return NotFound();

            CategoryDto response = _mapper.Map<CategoryDto>(result);

            return Ok(response);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCategory(CreateCategoryDto dto)
        {
            Category? entity = _mapper.Map<Category>(dto);

            try
            {
                Category created = await _categoryRepository.CreateAsync(entity);
                return CreatedAtAction(nameof(GetCategoryById), new { id = created.Id }, dto);
            }
            catch (ConflictException e)
            {
                return Conflict(new { conflict = e.Message });
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryDto dto)
        {

            if (id <= 0) return BadRequest();

            Category entity = _mapper.Map<Category>(dto);
            entity.Id = id;

            try
            {
                bool updated = await _categoryRepository.UpdateAsync(entity);

                if (!updated) return NotFound();

                return NoContent();

            }
            catch (ConflictException e)
            {
                return Conflict(new { conflict = e.Message });
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> DeleteCategory(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                bool deleted = await _categoryRepository.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (CannotDeleteException e)
            {
                return Conflict(new { conflict = e.Message });

            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
