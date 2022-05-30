using System.Collections.Generic;
using WIPFLI.Infrastructure.Repositories;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public IEnumerable<ItemDiscount> GetAllItemDiscounts()
        {
            return _discountRepository.GetItemDiscounts();
        }

        public IEnumerable<WeekdaysDiscount> GetAllWeekdaysDiscounts()
        {
            return _discountRepository.GetWeekdaysDiscounts();
        }
    }
}
