namespace WIPFLI.Models
{
    public class CartItem : ItemDiscount
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
