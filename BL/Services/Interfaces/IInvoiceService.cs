﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IInvoiceService
    {
        ResultItem<Invoice> GetInvoiceList(FilterProperties filterProperties);
        
    }
}
