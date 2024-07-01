namespace reservation_app_server.Models
{
    public class BasicBidItem:BasicItem
    { 
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
        public IdValuePair DiscountType { get; set; }
        public int TotalPrice { get; set; }
        public int Discount { get; set; }
        public int TotalPriceAfterDiscount { get; set; }
        public IdValuePair Product { get; set; }

    }
}
