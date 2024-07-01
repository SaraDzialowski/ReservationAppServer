namespace reservation_app_server.Models
{
    public class BasicContact:BasicItem
    { 
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public CommunicationInfo communicationInfo { get; set; }

    }
}
