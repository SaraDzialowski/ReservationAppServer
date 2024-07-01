using DAL.Models;
using DAL.Repositories.Interfaces;
using Newtonsoft.Json;
using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReservationListRepository : IReservationListRepository
    {
        public List<ReservationListItem> GetReservationsList()
        {
            List<ReservationListItem> reservationListItems;
            using (StreamReader r = new StreamReader("../DAL/JsonFiles/getReservationsList.json"))
            {
                string json = r.ReadToEnd();
                reservationListItems = JsonConvert.DeserializeObject<List<ReservationListItem>>(json);
            }
            return reservationListItems;
        }
    }
}
