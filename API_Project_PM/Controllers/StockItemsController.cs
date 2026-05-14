using API_Project_PM.Core.CustomException;
using API_Project_PM.Core.DTOs.StockItems;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.StockItems;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockItemsController : ControllerBase
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IMapper _mapper;



        public StockItemsController(IStockItemRepository stockItemRepository, IMapper mapper)
        {
            this._stockItemRepository = stockItemRepository;
            this._mapper = mapper;
        }



        [HttpDelete("part/{partId:int}/location/{locationId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteById(int partId, int locationId)
        {

            if (partId <= 0 || locationId <= 0) return BadRequest();

            try
            {
                bool result = await _stockItemRepository.DeleteAsync(partId, locationId);
                if (!result) return NotFound();

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


        [HttpGet("location/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockItemDto>>> GetAllByLocationId(int id)
        {
            if (id <= 0) return BadRequest();

            IEnumerable<StockItem> result = await _stockItemRepository.GetByLocationIdAsync(id);

            if (!result.Any()) return Ok(Array.Empty<StockItemDto>());

            IEnumerable<StockItemDto> repsone = _mapper.Map<IEnumerable<StockItemDto>>(result);

            return Ok(repsone);
        }


        [HttpGet("part/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockItemDto>>> GetAllByPartId(int id)
        {
            if (id <= 0) return BadRequest();

            IEnumerable<StockItem> result = await _stockItemRepository.GetByPartIdAsync(id);

            if (!result.Any()) return Ok(Array.Empty<StockItemDto>());

            IEnumerable<StockItemDto> repsone = _mapper.Map<IEnumerable<StockItemDto>>(result);

            return Ok(repsone);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StockItemDto>>> GetAllStockItems()
        {
            IEnumerable<StockItem> result = await _stockItemRepository.GetAllAsync();

            if (!result.Any()) return Ok(Array.Empty<StockItemDto>());

            IEnumerable<StockItemDto> repsone = _mapper.Map<IEnumerable<StockItemDto>>(result);

            return Ok(repsone);
        }

    }

}
