using System.Collections.Generic;
using WIPFLI.Models;
using WIPFLI.Models.Resource;

namespace WIPFLI.Infrastructure.Services
{
    public interface IItemDiscountService
    {
        List<CartItem> CalculateItemDiscount(CartResource resource);
    }
}
