using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<ItemDiscount> GetItemDiscounts()
        {
            return _configuration.GetSection("ItemDiscounts").Get<IEnumerable<ItemDiscount>>();
        }

        public IEnumerable<WeekdaysDiscount> GetWeekdaysDiscounts()
        {
            return _configuration.GetSection("WeekdaysDiscounts").Get<IEnumerable<WeekdaysDiscount>>();
        }
    }
}
