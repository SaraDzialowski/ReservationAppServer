using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ReservationListItem:BasicReservationItem
    {
        public List<BasicActivityDayItem> ActivityDayList { get; set; }
        public bool IsGoToServer { get; set; }
       

    }
}
