
using System.Collections.Generic;

namespace WIPFLI.Models.Response
{
    public class CartResponse
    {
        public List<ItemsResponse> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal WeekDayDiscount { get; set; }
        public decimal NetPayable { get; set; }
    }

    public class ItemsResponse
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public UnitType UnitType { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
