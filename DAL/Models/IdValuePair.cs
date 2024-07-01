namespace reservation_app_server.Models
{
    public class IdValuePair
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public IdValuePair()
        {
            Id = string.Empty;
            Value = string.Empty;
        }

        public IdValuePair(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
