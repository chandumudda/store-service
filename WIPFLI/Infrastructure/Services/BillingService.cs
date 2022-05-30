using System.Collections.Generic;
using System.Linq;
using WIPFLI.Infrastructure.Extensions;
using WIPFLI.Models;
using WIPFLI.Models.Resource;
using WIPFLI.Models.Response;

namespace WIPFLI.Infrastructure.Services
{
    public class BillingService : IBillingService
    {
        private readonly IEnumerable<Item> _items;
        private readonly IItemDiscountService _itemDiscountService;
        private readonly IWeekDaysDiscountService _weekDaysDiscountService;
        private readonly IEnumerable<ItemDiscount> _itemDiscountData;
        public BillingService(IItemDiscountService itemDiscountService, IWeekDaysDiscountService weekDaysDiscountService, IItemService itemService, IDiscountService discountService)
        {
            _itemDiscountService = itemDiscountService;
            _weekDaysDiscountService = weekDaysDiscountService;
            _itemDiscountData = discountService.GetAllItemDiscounts();
            _items = itemService.GetAllItems();
        }

        public CartResponse CalculateDiscountAndGetFinalBill(CartResource resource)
        {
            var cartItems = _itemDiscountService.CalculateItemDiscount(resource);
            var totalPriceAfterDiscount = cartItems.Sum(x => x.PriceAfterDiscount);
            var totalWeekDayDiscount = _weekDaysDiscountService.CalculateWeekdayDiscount(resource.BillDate, totalPriceAfterDiscount);
            var resources = cartItems.ToItemsResponse(_itemDiscountData, _items);
            return new CartResponse()
            {
                Items = resources,
                WeekDayDiscount = totalWeekDayDiscount,
                TotalAmount = totalPriceAfterDiscount,
                NetPayable = totalPriceAfterDiscount - totalWeekDayDiscount
            };
        }
    }
}
