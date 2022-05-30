using Microsoft.AspNetCore.Mvc;
using WIPFLI.Infrastructure.Services;

namespace WIPFLI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        [Route("Items")]
        public IActionResult GetItemDiscounts()
        {
            return Ok(_discountService.GetAllItemDiscounts());
        }

        [HttpGet]
        [Route("WeekDays")]
        public IActionResult GetWeekDaysDiscount()
        {
            return Ok(_discountService.GetAllWeekdaysDiscounts());
        }
    }
}
