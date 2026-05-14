using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.DTOs.StockMovements;
using API_Project_PM.Core.Enums;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.StockItems;
using API_Project_PM.Core.Services.StockMovements;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementsController : ControllerBase
    {

        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMapper _mapper;


        public StockMovementsController(IStockMovementRepository stockMovementRepository, IStockItemRepository stockItemRepository, IMapper mapper)
        {
            this._stockMovementRepository = stockMovementRepository;
            this._stockItemRepository = stockItemRepository;
            this._mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockMovementDto>>> GetAllStockMovements()
        {
            IEnumerable<StockMovement> result = await _stockMovementRepository.GetAllStockMovements();
            if (!result.Any()) return Ok(Array.Empty<StockMovementDto>());

            IEnumerable<StockMovementDto> response = _mapper.Map<List<StockMovementDto>>(result);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMovementDto?>> GetStockMovementById(int id)
        {
            StockMovement? result = await _stockMovementRepository.GetStockMovementById(id);
            if (result is null) return NotFound();

            StockMovementDto response = _mapper.Map<StockMovementDto>(result);

            return Ok(response);
        }


        [HttpPut("movetype/{typeMove:int}/part/{partId:int}/location/{locationId:int}/quantity/{quantity:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpsertStockItem(int partId, int locationId, int quantity, MovementType typeMove)
        {
            if (partId <= 0 || locationId <= 0) return BadRequest();

            try
            {

                bool result = await _stockItemRepository.UpsertAsync(partId, locationId, quantity, typeMove);
                if (!result) return NotFound();

                await _stockMovementRepository.CreateStockMovement(new StockMovement
                {
                    PartId = partId,
                    LocationId = locationId,
                    Quantity = quantity,
                    MovementType = typeMove,
                    TransferGroupId = Guid.NewGuid()
                });

                return NoContent();
            }
            catch (ConflictException e)
            {
                return Conflict(new { conflict = e.Message });
            }
            catch (NotFoundException e)
            {
                return NotFound(new { notFound = e.Message });
            }
            catch (DbUpdateException)
            {
                throw;
            }
        } 


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStockMovement(int id)
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
