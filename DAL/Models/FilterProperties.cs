using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FilterProperties
    {
        public List<Filter> FilterList { get; set; }
        public List<Sort> SortList { get; set; }
        public int CurrentIndex { get; set; }
        public int NumberOfRowsInPage { get; set; }
    }
}
