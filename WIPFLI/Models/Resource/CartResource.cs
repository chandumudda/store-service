using System;
using System.Collections.Generic;

namespace WIPFLI.Models.Resource
{
    public class CartResource
    {
        public DateTime BillDate { get; set; }
        public List<InputItems> Items { get; set; }
    }

    public class InputItems
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
