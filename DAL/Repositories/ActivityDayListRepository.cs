using DAL.Models;
using DAL.Repositories.Interfaces;
using Newtonsoft.Json;
using reservation_app_server.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ActivityDayListRepository: IActivityDayListRepository
    {

        public List<ActivityDayListItem> GetActivityDayList()
        {
            List<ActivityDayListItem> activityDayListItems;
            using (StreamReader r = new StreamReader("../DAL/JsonFiles/getReservationsByActivityDayList.json"))
            {
                string json = r.ReadToEnd();
                activityDayListItems = JsonConvert.DeserializeObject<List<ActivityDayListItem>>(json);
            }

            return activityDayListItems;

        }
    }
}
