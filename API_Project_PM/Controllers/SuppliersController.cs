using API_Project_PM.Models;
using API_Project_PM.Services.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliersRepository _suppliersRepository;


        public SuppliersController(ISuppliersRepository suppliersRepository)
        {
            this._suppliersRepository = suppliersRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<Supplier>>> GetAllSuppliers()
        {
            IEnumerable<Supplier> result = await _suppliersRepository.GetAllSuppliers();
            if (!result.Any()) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Supplier?>> GetSupplierById(int id)
        {
            Supplier? result = await _suppliersRepository.GetSupplierById(id);

            if (result is null) return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> CreateSupplier(Supplier item)
        {
            if (item is null) return BadRequest();

            await _suppliersRepository.CreateSupplier(item);

            return CreatedAtAction(nameof(GetSupplierById), new { id = item.Id }, item);
        }

    }
}
