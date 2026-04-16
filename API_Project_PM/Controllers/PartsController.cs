using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Parts;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartRepository _partsRepository;

        public PartsController(IPartRepository partsRepository)
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

        [HttpGet("{id:int}")]
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreatePart(Part item)
        {
            if (item is null) return BadRequest();

            try
            {
                await _partsRepository.CreatePart(item);    

                return CreatedAtAction(nameof(GetPartById), new { id = item.Id }, item);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePart(int id, Part item)
        {

            if (item is null || id != item.Id) return BadRequest();

            Part? existing = await _partsRepository.GetPartById(id);
            if (existing is null) return NotFound();

            if(existing.Sku != item.Sku)
            {
                return BadRequest("Het SKU nummer kan niet worden bewerkt. Verwijder het artikel en maak een nieuw aan");
            }

            bool updated = await _partsRepository.UpdatePart(id, item);

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePart(int id)
        {

            bool existing = await _partsRepository.DeletePart(id);

            if (!existing) return NotFound();

            return NoContent();
        }


    }
}
