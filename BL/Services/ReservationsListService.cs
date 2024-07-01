using BL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ReservationsListService : IReservationsListService
    {
        private IReservationListRepository _reservationListRepository;
        private IActivityDayListService _activityDayListService;
        private IHelperService<ReservationListItem> _helperService;
        private string EntityName = "Reservation";
        public ReservationsListService(IReservationListRepository reservationListRepository, IActivityDayListService activityDayListService, IHelperService<ReservationListItem> helperService)
        {
            _reservationListRepository = reservationListRepository;
            _activityDayListService = activityDayListService;
            _helperService = helperService;

        }
        public ResultItem<ReservationListItem> FilterReservationsList(FilterProperties filterProperties)
        {
            List<ReservationListItem> reservationListItems = _reservationListRepository.GetReservationsList();
            if (filterProperties.FilterList.Count > 0 || filterProperties.SortList.Count > 0)
            {
                FilterProperties filterProperties1 = new();
                filterProperties1.FilterList = filterProperties.FilterList.Where(f => f.Alias[0] != "ActivityDay").ToList();
                filterProperties1.SortList = filterProperties.SortList;
                FilterProperties filterProperties2 = new();
                filterProperties2.FilterList = filterProperties.FilterList.Where(f => f.Alias[0] == "ActivityDay").ToList();
                filterProperties2.SortList = new List<Sort>();
                if (filterProperties2.FilterList.Count > 0)
                {
                    ResultItem<ActivityDayListItem> ri = _activityDayListService.GetActivityDayList(filterProperties2, false);
                    List<ReservationListItem> list = new();
                    foreach (var item in reservationListItems)
                    {
                        var a = ri.DataList.Find(a => a.Reservation.ReservationNum == item.ReservationNum);
                        if (a != null)
                        {
                            list.Add(item);
                        }
                    }
                    reservationListItems = list;
                }
                reservationListItems = _helperService.CasesHelper(reservationListItems, filterProperties1, EntityName);
                if (filterProperties1.FilterList.Any())
                {
                    if (filterProperties1.FilterList.Last().Type == "Any")
                    {
                        reservationListItems = _helperService.FilterAnyFieldReservation(reservationListItems, filterProperties1.FilterList.Last());
                    }
                }
            }


            ResultItem<ReservationListItem> result = new();
            result.DataList = (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage <= reservationListItems.Count ? reservationListItems.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + 1 <= reservationListItems.Count? reservationListItems.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, reservationListItems.Count - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                reservationListItems;

            result.NumberOfRows = reservationListItems.Count;
            return result;


        }
    }
}
