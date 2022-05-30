namespace WIPFLI.Models
{
    public class ItemDiscount : Item
    {
        public string ItemId { get; set; }
        public decimal MinQuantity { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
