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
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _invoiceRepository;
        private IHelperService<Invoice> _helperService;
        private string EntityName = "Invoice";
        public InvoiceService(IInvoiceRepository invoiceRepository, IHelperService<Invoice> helperService)
        {
            _invoiceRepository = invoiceRepository;
            _helperService = helperService;
        }
        public ResultItem<Invoice> GetInvoiceList(FilterProperties filterProperties)
        {
            List<Invoice> invoiceList = _invoiceRepository.GetInvoiceList();
            invoiceList = _helperService.CasesHelper(invoiceList, filterProperties, EntityName);
            ResultItem<Invoice> result = new();
            result.DataList = (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage <= invoiceList.Count ? invoiceList.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + filterProperties.NumberOfRowsInPage - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                          (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage + 1 <= invoiceList.Count ? invoiceList.GetRange((filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage, invoiceList.Count - (filterProperties.CurrentIndex) * filterProperties.NumberOfRowsInPage) :
                          invoiceList;
            result.NumberOfRows = invoiceList.Count;
            return result;
        }
    }
}
