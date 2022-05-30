using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IConfiguration _configuration;

        public ItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Item> GetItems()
        {
            return _configuration.GetSection("Items").Get<IEnumerable<Item>>();
        }
    }
}
