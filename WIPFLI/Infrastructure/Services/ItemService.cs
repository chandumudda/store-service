using System.Collections.Generic;
using WIPFLI.Infrastructure.Repositories;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public IEnumerable<Item> GetAllItems()
        {
            return _itemRepository.GetItems();
        }
    }
}
