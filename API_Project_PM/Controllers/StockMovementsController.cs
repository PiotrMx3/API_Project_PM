using API_Project_PM.Models;
using API_Project_PM.Services.Parts;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementsController : ControllerBase
    {

        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementsController(IStockMovementRepository stockMovementRepository)
        {
            this._stockMovementRepository = stockMovementRepository;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockMovement>>> GetAllStockMovements()
        {
            IEnumerable<StockMovement> result = await _stockMovementRepository.GetAllStockMovements();

            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMovement?>> GetStockMovementById(int id)
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

            return CreatedAtAction(nameof(GetStockMovementById), new { id = item.Id }, item);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStockMovement(int id, StockMovement item)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed,
                "De geschiedenis van magazijnbewegingen is geblokkeerd voor bewerking om auditredenen. Voeg een corrigerende boeking toe");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteStockMovement(int id)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed,
                "Het is niet mogelijk om de geschiedenis van magazijnbewegingen te verwijderen. Maak een compenserende boeking (IN/OUT)");
        }

    }
}
