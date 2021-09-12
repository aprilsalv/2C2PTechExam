using _2C2PTechExam.Entity;
using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _2C2PTechExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IRepository<Invoice> repository;
        public InvoiceController (IRepository<Invoice> repository)
        {

            this.repository = repository;
     
        }


        // GET api/<InvoiceController>/ByCurrency/{currency}
        [Route("ByCurrency/{currency}")]
        public string ByCurrency(string currency)       {
 
            var resultTuple =  repository.GetInvoiceByCurrency(currency);

           

            return JsonConvert.SerializeObject(resultTuple);
        }


        // GET api/<InvoiceController>/ByStatus/{status}
        [Route("ByStatus/{status}")]
        public string ByStatus(string status)
        {

            var resultTuple = repository.GetInvoiceByStatus(status);



            return JsonConvert.SerializeObject(resultTuple);
        }


        // GET api/<InvoiceController>/ByDateRange/{dateFrom}/{dateTo}
        [Route("ByDateRange/{dateFrom}/{dateTo}")]
        public string ByDateRange(string dateFrom, string dateTo)
        {

            var resultTuple = repository.GetInvoiceByDateRange(dateFrom, dateTo);

            return JsonConvert.SerializeObject(resultTuple);
        }

    }
}
