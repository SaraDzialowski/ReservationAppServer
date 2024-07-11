using DAL.Models;
using reservation_app_server.Models;
using System.Dynamic;

namespace BL.Services.Interfaces
{
    public interface IActivityDayListService
    {
        ResultItem<ActivityDayListItem> GetActivityDayList(FilterProperties filterProperties, bool flag);
        void SendEmail(MailInfo item);

    }
}
