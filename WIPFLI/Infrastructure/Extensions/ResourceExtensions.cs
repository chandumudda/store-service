using System.Collections.Generic;
using System.Linq;
using WIPFLI.Models;
using WIPFLI.Models.Response;

namespace WIPFLI.Infrastructure.Extensions
{
    public static class ResourceExtensions
    {
        public static List<ItemsResponse> ToItemsResponse(this List<CartItem> items, IEnumerable<ItemDiscount> itemDiscountData, IEnumerable<Item> masterItems)
        {
            return items.Select(item => new ItemsResponse()
                {
                    PriceAfterDiscount = item.PriceAfterDiscount,
                    UnitType = masterItems.FirstOrDefault(x => x.Id.Equals(item.ItemId)).UnitType,
                    DiscountType = itemDiscountData.FirstOrDefault(x => x.ItemId.Equals(item.ItemId)).DiscountType,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                    Name = masterItems.FirstOrDefault(x => x.Id.Equals(item.ItemId))?.Name
                })
                .ToList();
        }
    }
}
