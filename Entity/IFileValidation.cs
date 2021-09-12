using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Entity
{
    public interface IFileValidation
    {
        bool FileIsValid(IFormFile file);

        string ErrorMessage { set; get; }

        string LogFileName { set; get; }

        List<Invoice> Invoices { set; get; }

        string RootFolder { set; get; }
    }
}
