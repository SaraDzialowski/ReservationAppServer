using DAL.Models;
using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IHelperService<T>
    {
        List<T> CasesHelper(List<T> data, FilterProperties filterProperties, string EntityName);
        List<T> FilterLookUpField(List<T> activityDayListItems, Filter filterItem, string EntityName);
        List<T> FilterDateField(List<T> activityDayListItems, Filter filterItem, string EntityName);
        List<T> FilterBooleanField(List<T> activityDayListItems, Filter filterItem, string EntityName);
        List<T> FilterTextField(List<T> activityDayListItems, Filter filterItem, string EntityName);
        List<T> FilterNumberField(List<T> activityDayListItems, Filter filterItem, string EntityName);
        List<ReservationListItem> FilterAnyFieldReservation(List<ReservationListItem> reservationListItems, Filter filterItem);
        List<ActivityDayListItem> FilterAnyFieldActivityDay(List<ActivityDayListItem> activityDayListItems, Filter filterItem);
        List<T> SortLookUpField(List<T> activityDayListItems, Sort s, string EntityName);
        List<T> SortDateField(List<T> activityDayListItems, Sort s, string EntityName);
        List<T> SortTextField(List<T> activityDayListItems, Sort s, string EntityName);
        List<T> SortNumberField(List<T> activityDayListItems, Sort s, string EntityName);
    }
}
