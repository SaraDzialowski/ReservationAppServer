using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ResultItem<T>
    { 

        public List<T> DataList { get; set; }
        public int NumberOfRows { get; set; }
    }
}
