using API_Project_PM.Models;
using API_Project_PM.Services;
using API_Project_PM.Services.Locations;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsRepository _locationsRepository;
        public LocationsController(ILocationsRepository locationRepository)
        {
            this._locationsRepository = locationRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            IEnumerable<Location> result = await _locationsRepository.GetAllLocations();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Location?>> GetLocationById(int id)
        {
            Location? result = await _locationsRepository.GetLocationById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreateLocation(Location item)
        {
            if (item is null) return BadRequest();

            await _locationsRepository.CreateLocation(item);

            return CreatedAtAction(nameof(GetLocationById), new { id = item.Id }, item);
        }
    }
}
