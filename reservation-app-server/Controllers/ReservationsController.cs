using BL.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using reservation_app_server.Models;

namespace reservation_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private IReservationsListService _reservationsListService;
        public ReservationsController(IReservationsListService reservationsListService)
        {
            _reservationsListService = reservationsListService;
        }
        [HttpGet(Name = "GetReservation")]
        public FullReservationItem Get(string id)
        {
            return null;
        }

        [HttpPost(Name = "GetReservationsList")]
        public ResultItem<ReservationListItem> Post(FilterProperties filterProperties)
        {
            return _reservationsListService.FilterReservationsList(filterProperties);
        }
    }
}
