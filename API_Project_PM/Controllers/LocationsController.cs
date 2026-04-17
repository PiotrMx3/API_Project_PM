using API_Project_PM.Core.DTOs.Locations;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Locations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository location, IMapper mapper)
        {
            this._locationRepository = location;
            this._mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations()
        {
            IEnumerable<Location> result = await _locationRepository.GetAllAsync();

            //if (!result.Any()) return Ok(new List<LocationDto>[] { });

            if (!result.Any()) return Ok(Array.Empty<LocationDto>());

            IEnumerable<LocationDto> repsone = _mapper.Map<List<LocationDto>>(result);

            return Ok(repsone);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LocationDto>> GetLocationById(int id)
        {
            if (id <= 0) return BadRequest();

            Location? result = await _locationRepository.GetByIdAsync(id);

            if (result is null) return NotFound();

            LocationDto response = _mapper.Map<LocationDto>(result);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateLocation(CreateLocationDto item)
        {
            Location entity = _mapper.Map<Location>(item);

            try
            {
                Location created = await _locationRepository.CreateAsync(entity);

                return CreatedAtAction(nameof(GetLocationById), new { id = created.Id }, item);
            }
            catch (DbUpdateException)
            {
                return Conflict(new { conflict = "Locatie bestaat al" });
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateLocation(int id, UpdateLocationDto item)
        {

            if (id <= 0) return BadRequest();

            try
            {
                var entity = _mapper.Map<Location>(item);

                entity.Id = id;

                bool existing = await _locationRepository.UpdateAsync(entity);

                if (!existing) return NotFound();

                return NoContent();
            }
            catch (DbUpdateException)
            {

                return Conflict(new { conflict = "Locatie bestaat al" });

            }

        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLocation(int id)
        {

            if (id <= 0) return BadRequest();

            try
            {
                bool existing = await _locationRepository.DeleteAsync(id);
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
