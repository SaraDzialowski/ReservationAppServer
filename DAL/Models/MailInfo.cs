using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MailInfo
    {   
        public string To { get; set; }
        public string? Subject { get; set; }
        public string? body { get; set; }
        public string? FilePath { get; set; }
        public List<string>? Cc { get; set; }
        public string? FileName { get; set; } 
    }
}
