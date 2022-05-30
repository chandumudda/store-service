using System;

namespace WIPFLI.Infrastructure.Services
{
    public interface IWeekDaysDiscountService
    {
        decimal CalculateWeekdayDiscount(DateTime billDate, decimal billAmount);
    }
}
