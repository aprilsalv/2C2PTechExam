using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace _2C2PTechExam.Entity
{
    public interface IRepository<T> where T : class
    {
        string UploadFile(IFormFile entity);

        string RootFolder { set; get; }

        IEnumerable<dynamic> GetInvoiceByCurrency(string currency);

        IEnumerable<dynamic> GetInvoiceByStatus(string status);

        IEnumerable<dynamic> GetInvoiceByDateRange(string dateFrom, string dateTo);
    }
}
