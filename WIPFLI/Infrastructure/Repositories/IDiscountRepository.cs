using System.Collections.Generic;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Repositories
{
    public interface IDiscountRepository
    {
        IEnumerable<WeekdaysDiscount> GetWeekdaysDiscounts();
        IEnumerable<ItemDiscount> GetItemDiscounts();
    }
}
