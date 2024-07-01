using DAL.Models;
using DAL.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public List<Invoice> GetInvoiceList()
        {
            List<Invoice> invoiceList;
            using (StreamReader r = new StreamReader("../DAL/JsonFiles/getInvoiceList.json"))
            {
                string json = r.ReadToEnd();
                invoiceList = JsonConvert.DeserializeObject<List<Invoice>>(json);
            }
            return invoiceList;
        }
    }
}
