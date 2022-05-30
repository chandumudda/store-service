using System;
using System.Collections.Generic;
using System.Linq;
using WIPFLI.Models;
using WIPFLI.Models.Resource;

namespace WIPFLI.Infrastructure.Services
{
    public class Billing : IItemDiscountService, IWeekDaysDiscountService
    {
        private readonly IDiscountService _discountService;

        public Billing(IDiscountService discountService, IItemService itemService)
        {
            _discountService = discountService;
        }
        public virtual List<CartItem> CalculateItemDiscount(CartResource resource)
        {
            var cartItems = new List<CartItem>();
            resource.Items.ForEach(item =>
            {
                cartItems.Add(GetItemDiscount(item));
            });
            return cartItems;
        }

        public virtual decimal CalculateWeekdayDiscount(DateTime billDate, decimal billAmount)
        {
            var discountInfo = _discountService.GetAllWeekdaysDiscounts().FirstOrDefault(x => x.Day.Equals(billDate.DayOfWeek.ToString()));
            if (discountInfo == null)
                return 0;
            return discountInfo.DiscountInPercentage > 0
                ? (billAmount * discountInfo.DiscountInPercentage) / 100
                : 0;
        }

        private CartItem GetItemDiscount(InputItems item)
        {
            var cartItem = new CartItem()
            {
                ItemId = item.ItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.UnitPrice * item.Quantity,
                PriceAfterDiscount = item.UnitPrice * item.Quantity
            };

            var discountInfo = _discountService.GetAllItemDiscounts().FirstOrDefault(x => x.ItemId.Equals(item.ItemId));
            if (discountInfo == null)
                return cartItem;

            switch (discountInfo.DiscountType)
            {
                case DiscountType.Flat:
                    {
                        cartItem.PriceAfterDiscount = discountInfo.DiscountValue > 0
                            ? cartItem.TotalPrice - (cartItem.TotalPrice * discountInfo.DiscountValue / 100)
                            : cartItem.TotalPrice;
                        break;
                    }
                case DiscountType.Unit:
                    {
                        cartItem = GetFlatDiscount(cartItem, discountInfo);
                        break;
                    }
                case DiscountType.NoDiscount:
                    {
                        cartItem.PriceAfterDiscount = cartItem.TotalPrice;
                        break;
                    }
                default:
                    {
                        cartItem.PriceAfterDiscount = cartItem.TotalPrice;
                        break;
                    }
            }

            return cartItem;
        }

        private static CartItem GetFlatDiscount(CartItem item, ItemDiscount discountInfo)
        {
            if (item.Quantity < discountInfo.MinQuantity)
            {
                item.PriceAfterDiscount = item.TotalPrice;
                return item;
            }

            var modValue = item.Quantity % discountInfo.MinQuantity;

            switch (modValue)
            {
                case 0:
                    item.Quantity += Convert.ToInt32((item.Quantity / discountInfo.MinQuantity) *
                                                     discountInfo.DiscountValue);
                    item.PriceAfterDiscount = item.TotalPrice;
                    break;
                case 1:
                    item.PriceAfterDiscount = item.TotalPrice - item.UnitPrice;
                    break;
                default:
                    {
                        item.PriceAfterDiscount = item.TotalPrice - item.UnitPrice;
                        break;
                    }
            }

            return item;
        }
    }
}
