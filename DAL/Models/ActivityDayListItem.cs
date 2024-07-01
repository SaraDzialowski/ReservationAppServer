namespace reservation_app_server.Models
{
    public class ActivityDayListItem:BasicActivityDayItem
    {
        public bool IsGoToServer { get; set; }
        public BasicBidItem[] BidItemShortList { get; set; }
        public ShortScheduleEventItem[] ServiceProviderShortList { get; set; }
        public string[] StartTimeMuseumEventItemList { get; set; }
        public string[] InvoiceList { get; set; }
        public int numOfActivityDaysForParent { get; set; }
        public IdValuePair Product { get; set; }
        public IdValuePair Resource { get; set; }
        
    }
}
