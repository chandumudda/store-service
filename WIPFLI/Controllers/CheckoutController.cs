using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using WIPFLI.Infrastructure.Services;
using WIPFLI.Models.Resource;
using WIPFLI.Models.Response;

namespace WIPFLI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : Controller
    {
        private readonly IBillingService _billingService;

        public CheckoutController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(CartResponse))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody]CartResource resource)
        {
            if (resource == null || resource.Items.Count < 1)
                return BadRequest("Cart can not be empty!.");

            if (resource.Items.Any(x => x.ItemId.Equals("0") 
                                        || x.ItemId.Equals("string")) 
                                        || resource.Items.Any(x => x.UnitPrice.Equals(0)) 
                                        || resource.Items.Any(x => x.Quantity.Equals(0)))
                return BadRequest("Invalid input provided.");

            return Ok(_billingService.CalculateDiscountAndGetFinalBill(resource));
        }
    }
}
