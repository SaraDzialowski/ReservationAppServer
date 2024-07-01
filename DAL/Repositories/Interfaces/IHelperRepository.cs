using DAL.Models;
using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHelperRepository<T>
    {
        List<T> CasesHelper(List<T> data, FilterProperties filterProperties);
        List<T> FilterLookUpField(List<T> activityDayListItems,Filter filterItem);
        List<T> FilterDateField(List<T> activityDayListItems, Filter filterItem);
        List<T> FilterBooleanField(List<T> activityDayListItems, Filter filterItem);
        List<T> FilterTextField(List<T> activityDayListItems, Filter filterItem);
        List<T> FilterNumberField(List<T> activityDayListItems, Filter filterItem);
        List<T> SortLookUpField(List<T> activityDayListItems, Sort s);
        List<T> SortDateField(List<T> activityDayListItems, Sort s);
        List<T> SortTextField(List<T> activityDayListItems, Sort s);
        List<T> SortNumberField(List<T> activityDayListItems, Sort s);
    }
}
