using BL.Services;
using BL.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using reservation_app_server.Models;
using System.Collections.Generic;

namespace reservation_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityDayController : ControllerBase
    {
        private IActivityDayListService _activityDayListService;
        public ActivityDayController(IActivityDayListService activityDayListService)
        {
            _activityDayListService = activityDayListService;
        }
        [HttpPost(Name = "GetActivityDayList")]
        public ResultItem<ActivityDayListItem> Post(FilterProperties filterProperties)
        {
            return _activityDayListService.GetActivityDayList(filterProperties,true);
        }
    }
}
