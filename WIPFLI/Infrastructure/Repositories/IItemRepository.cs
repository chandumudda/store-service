using System.Collections.Generic;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetItems();
    }
}
