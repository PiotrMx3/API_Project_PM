using API_Project_PM.Core.DTOs.PartsSuppliers;
using API_Project_PM.Core.DTOs.Suppliers;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Categories;
using API_Project_PM.Core.Services.PartsSuppliers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartSupplierController : ControllerBase
    {
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly IMapper _mapper;



        public PartSupplierController(IPartSupplierRepository partSupplierRepository, IMapper mapper)
        {
            this._partSupplierRepository = partSupplierRepository;
            this._mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreatePartSupplier(CreatePartSupplierDto item)
        {
            if (item.PartId <= 0 || item.SupplierId <= 0) return BadRequest();

            PartSupplier newItem = _mapper.Map<PartSupplier>(item);

            try
            {
                PartSupplier? created = await _partSupplierRepository.CreateAsync(newItem);

                return CreatedAtAction(nameof(GetPartSupplierById), new { partId = created.PartId, supplierId = created.SupplierId }, item);
            }
            catch (DbUpdateException)
            {

                return Conflict(new { conflict = "ID combinatie is niet correct" });
            }
        }



        [HttpGet("part/{partId:int}/supplier/{supplierId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PartSupplierDto>> GetPartSupplierById(int partId, int supplierId)
        {
            if (partId <= 0 || supplierId <= 0) return BadRequest();

            PartSupplier? result = await _partSupplierRepository.GetById(partId, supplierId);

            if (result is null) return NotFound();

            PartSupplierDto respone = _mapper.Map<PartSupplierDto>(result);

            return Ok(respone);

        }


        [HttpDelete("part/{partId:int}/supplier/{supplierId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePartSupplier(int partId, int supplierId)
        {
            if (partId <= 0 || supplierId <= 0) return BadRequest();

            bool result = await _partSupplierRepository.DeleteAsync(partId, supplierId);

            if (!result) return NotFound();

            return NoContent();
        }


        [HttpPut("part/{partId:int}/supplier/{supplierId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartSupplier(int partId, int supplierId, UpdatePartSupplierDto item)
        {
            if (partId <= 0 || supplierId <= 0) return BadRequest();

            try
            {
                PartSupplier entity = _mapper.Map<PartSupplier>(item);
                entity.PartId = partId;
                entity.SupplierId = supplierId;

                bool response = await _partSupplierRepository.UpdetAsync(entity);
                if (!response) return NotFound();

                return NoContent();

            }
            catch (DbUpdateException)
            {

                return Conflict(new { conflict = "ID combinatie is niet correct" });
            }
        }

    }
}
