using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Entity
{
    public class FileValidationCSV : IFileValidation
    {
        public async Task<List<Invoice>> Validate(IFormFile file)
        {
            List<Invoice> invoiceList = new List<Invoice>();

            //await return invoiceList;

            return invoiceList;
        }
    }
}
