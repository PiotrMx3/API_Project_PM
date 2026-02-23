using API_Project_PM.Models;
using API_Project_PM.Services.StockItems;
using Microsoft.AspNetCore.Mvc;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockItemsController : Controller
    {
        private readonly IStockItemsRepository _stockItemsRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockItem>>> GetAllStockParts()
        {

        }
    }
}
