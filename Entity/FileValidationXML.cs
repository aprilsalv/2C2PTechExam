using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Entity
{
    public class FileValidationXML: FileValidation, IFileValidation
    {

        public List<Invoice> Invoices
        {
            set;
            get;
        }
        public bool FileIsValid(IFormFile file)
        {

            List<Invoice> invoiceList = new List<Invoice>();
            Invoices = invoiceList;

            //await return invoiceList;

            return true;
        }
    }
}
