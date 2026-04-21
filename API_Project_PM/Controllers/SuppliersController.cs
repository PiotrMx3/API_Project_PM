using API_Project_PM.Core.DTOs.Suppliers;
using API_Project_PM.Core.Models;
using API_Project_PM.Core.Services.Suppliers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_PM.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _suppliersRepository;
        private readonly IMapper _mapper;



        public SuppliersController(ISupplierRepository suppliersRepository, IMapper mapper)
        {
            this._suppliersRepository = suppliersRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAllSuppliers()
        {
            IEnumerable<Supplier> result = await _suppliersRepository.GetAllAsync();

            if (!result.Any()) return Ok(Array.Empty<SupplierDto>());

            IEnumerable<SupplierDto> repsone = _mapper.Map<IEnumerable<SupplierDto>>(result);
            return Ok(repsone);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<SupplierWithPartsDto>> GetSupplierById(int id)
        {
            if (id <= 0) return BadRequest();

            Supplier? result = await _suppliersRepository.GetByIdAsync(id);

            if (result is null) return NotFound();

            SupplierWithPartsDto repsone = _mapper.Map<SupplierWithPartsDto>(result);

            return Ok(repsone);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreateSupplier(CreateSupplierDto item)
        {
            Supplier entity = _mapper.Map<Supplier>(item);

            try
            {
                Supplier created = await _suppliersRepository.CreateAsync(entity);

                return CreatedAtAction(nameof(GetSupplierById), new { id = created.Id }, item);
            }
            catch (DbUpdateException)
            {

                return Conflict(new { conflict = "Deze BTW nummer bestaat al" });
            }

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateSupplier(int id, UpdateSupplierDto item)
        {

            if (id <= 0) return BadRequest();

            try
            {
                Supplier entity = _mapper.Map<Supplier>(item);

                entity.Id = id;

                bool existing = await _suppliersRepository.UpdateAsync(entity);

                if (!existing) return NotFound();

                return NoContent();

            }
            catch (DbUpdateException)
            {

                return Conflict(new { conflict = "Deze Leverancier staat al" });
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            if (id <= 0) return BadRequest();


            try
            {
                bool existing = await _suppliersRepository.DeleteAsync(id);
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
