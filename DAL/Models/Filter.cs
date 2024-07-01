namespace reservation_app_server.Models
{
    public class Filter
    {
        public string FieldName { get; set; }
        public string[] Values { get; set; }
        public string Type { get; set; }
        public string Operator { get; set; }
        public string[] Alias { get; set; }
        public string ParentAlias { get; set; }
        public string FilterType { get; set; }

    }
}
