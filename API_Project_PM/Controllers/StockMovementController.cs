using API_Project_PM.Models;
using API_Project_PM.Services.Parts;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementController : ControllerBase
    {

        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementController(IStockMovementRepository stockMovementRepository)
        {
            this._stockMovementRepository = stockMovementRepository;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockMovement>>> GetAllParts()
        {
            IEnumerable<StockMovement> result = await _stockMovementRepository.GetAllStockMovements();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMovement?>> GetPartById(int id)
        {
            StockMovement? result = await _stockMovementRepository.GetStockMovementById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreateStockMovement(StockMovement item)
        {
            if (item is null) return BadRequest();

            await _stockMovementRepository.CreateStockMovement(item);

            return CreatedAtAction(nameof(GetPartById), new { id = item.Id }, item);
        }

    }
}
