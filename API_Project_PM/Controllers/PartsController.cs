using API_Project_PM.Core.DTOs.Parts;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Parts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IPartRepository _partsRepository;
        private readonly IMapper _mapper;


        public PartsController(IPartRepository partsRepository, IMapper mapper)
        {
            this._partsRepository = partsRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PartDto>>> GetAllParts()
        {
            IEnumerable<Part> result = await _partsRepository.GetAllAsync();

            if (!result.Any()) return Ok(Array.Empty<PartDto>());

            IEnumerable<PartDto> repsone = _mapper.Map<IEnumerable<PartDto>>(result);

            return Ok(repsone);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PartDto?>> GetPartById(int id)
        {
            if (id <= 0) return BadRequest();

            Part? result = await _partsRepository.GetByIdAsync(id);

            if (result is null) return NotFound();

            PartDto response = _mapper.Map<PartDto>(result);

            return Ok(response);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreatePart(CreatePartDto item)
        {
            Part entity = _mapper.Map<Part>(item);

            try
            {
                Part created = await _partsRepository.CreateAsync(entity);

                return CreatedAtAction(nameof(GetPartById), new { id = created.Id }, item);

            }
            catch (DbUpdateException)
            {
                return Conflict(new { conflict = "Deze Sku bestaat all" });
            }

        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePart(int id, UpdatePartDto item)
        {
            if (id <= 0) return BadRequest();

            Part entity = _mapper.Map<Part>(item);

            entity.Id = id;

            try
            {
                bool updated = await _partsRepository.UpdateAsync(entity);

                if (!updated) return NotFound();

                return NoContent();
            }
            catch (DbUpdateException)
            {

                return Conflict(new { conflict = "Deze Sku bestaat al" });
            }


        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePart(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                bool existing = await _partsRepository.DeleteAsync(id);

                if (!existing) return NotFound();

                return NoContent();

            }
            catch (InvalidOperationException e)
            {

                return Conflict(new { conflict = e.Message });
            }
        }


    }
}
