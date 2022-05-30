using Microsoft.AspNetCore.Mvc;
using WIPFLI.Infrastructure.Services;

namespace WIPFLI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_itemService.GetAllItems());
        }
    }
}
