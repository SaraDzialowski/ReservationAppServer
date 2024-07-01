using DAL.Models;
using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IReservationsListService
    {
        ResultItem<ReservationListItem> FilterReservationsList(FilterProperties filterProperties);
    }
}
