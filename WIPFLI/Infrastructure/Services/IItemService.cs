using System.Collections.Generic;
using WIPFLI.Models;

namespace WIPFLI.Infrastructure.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetAllItems();
    }
}
