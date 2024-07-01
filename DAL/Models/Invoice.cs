using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Invoice:BasicItem
    {
        public string InvoiceDate { get; set; }
        public string InvoiceNum { get; set; }
        public IdValuePair Institution { get; set; }
        public IdValuePair PayingInstitution { get; set; }
        public IdValuePair MatchStatus { get; set; }
        public double Sum { get; set; }
        public IdValuePair Currency { get; set; }
        public string ExternalReference { get; set; }
        public IdValuePair Reservation { get; set; }
        public string EventCode { get; set; } 
        public string ProductNum { get; set; }
        public string CreatedBy { get; set; }
    }
}
