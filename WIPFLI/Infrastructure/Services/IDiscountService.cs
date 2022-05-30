using System.Collections.Generic;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Services
{
    public interface IDiscountService
    {
        IEnumerable<WeekdaysDiscount> GetAllWeekdaysDiscounts();
        IEnumerable<ItemDiscount> GetAllItemDiscounts();
    }
}
