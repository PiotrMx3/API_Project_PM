using API_Project_PM.Models;
using API_Project_PM.Services.Locations;
using API_Project_PM.Services.Parts;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartsRepository _partsRepository;

        public PartsController(IPartsRepository partsRepository)
        {
            this._partsRepository = partsRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Part>>> GetAllParts()
        {
            IEnumerable<Part> result = await _partsRepository.GetAllParts();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Part?>> GetPartById(int id)
        {
            Part? result = await _partsRepository.GetPartById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreatePart(Part item)
        {
            if (item is null) return BadRequest();

            await _partsRepository.CreatePart(item);

            return CreatedAtAction(nameof(GetPartById), new { id = item.Id }, item);
        }

    }
}
