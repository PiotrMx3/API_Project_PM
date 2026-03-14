using API_Project_PM.Models;
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
            IEnumerable<StockMovement> result = await _stockMovementRepository.GetAll();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMovement?>> GetAllParts(int id)
        {
            StockMovement? result = await _stockMovementRepository.GetById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

    }
}
