namespace reservation_app_server.Models
{
    public class BasicActivityDayItem:BasicItem
    {
        public string  StartDate { get; set; }///
        public string EndDate { get; set; }///
        public string StartTime { get; set; }///
        public string EndTime { get; set; }///
        public IdValuePair Location { get; set; }
        public bool IsBhadimCity { get; set; }
        public bool IsNeedMuseum { get; set; }
        public bool IsNeedBigQuestions { get; set; }
        public bool IsNeedCampus { get; set; }
        public bool IsActivityDayLocked { get; set; }
        public string ActivityName { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfVisitors { get; set; }
        public int ActualNumberOfVisitors { get; set; }
        public BasicReservationItem Reservation { get; set; }
        public IdValuePair ActivityDayType { get; set; }
        public bool DidGroupArrive { get; set; }
        public IdValuePair Department { get; set; }
        public IdValuePair ActivityDayStatus { get; set; }
        public IdValuePair paymentStatus { get; set; }
        public IdValuePair DivisionRouter { get; set; }
        public string City { get; set; }
        public int NumOfGuideNeeded { get; set; }
        public int NumOfGuideScheduled { get; set; }
        public int NumOfLecturerNeeded { get; set; }
        public int numOfLecturerScheduled { get; set; }
        public int NumOfTestimonyNeeded { get; set; }
        public int NumOfTestimonyScheduled { get; set; }
        public int NumOfCantorNeeded { get; set; }
        public int NumOfCantorScheduled { get; set; }
        public int NumOfActorNeeded { get; set; }
        public int NumOfActorScheduled { get; set; }




    }
}
