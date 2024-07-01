using BL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using reservation_app_server.Models;

namespace BL.Services
{


    public class ActivityDayListService : IActivityDayListService
    {
        private IActivityDayListRepository _activityDayListRepository;
        private IHelperService<ActivityDayListItem> _helperService;
        private string EntityName = "ActivityDay";
        public ActivityDayListService(IActivityDayListRepository activityDayListRepository, IHelperService<ActivityDayListItem> helperService)
        {
            _activityDayListRepository = activityDayListRepository;
            _helperService = helperService;
        }
        public ResultItem<ActivityDayListItem> GetActivityDayList(FilterProperties filterProperties, bool flag)
        {
            List<ActivityDayListItem> activityDayListItems = _activityDayListRepository.GetActivityDayList();
            activityDayListItems = _helperService.CasesHelper(activityDayListItems, filterProperties, EntityName);

            if (filterProperties.FilterList.Count() > 0 && filterProperties.FilterList.Last()?.Type == "Any")
            {
                activityDayListItems = _helperService.FilterAnyFieldActivityDay(activityDayListItems, filterProperties.FilterList.Last());
            }
            ResultItem<ActivityDayListItem> result = new();
            result.DataList = (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage <= activityDayListItems.Count ? activityDayListItems.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + 1 <= activityDayListItems.Count ? activityDayListItems.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, activityDayListItems.Count - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                activityDayListItems;
            result.NumberOfRows = activityDayListItems.Count;
            return result;
        }

    }
}
