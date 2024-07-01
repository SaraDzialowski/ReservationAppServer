using BL.Services;
using BL.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace reservation_app_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpPost(Name = "GetInvoiceList")]
        public ResultItem<Invoice> Post(FilterProperties filterProperties)
        {
            return _invoiceService.GetInvoiceList(filterProperties);
        }
    }

        
 }
