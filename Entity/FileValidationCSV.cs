﻿using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Entity
{
    public class FileValidationCSV : FileValidation,IFileValidation
    {


        public List<Invoice> Invoices
        {
            set;
            get;
        }


        public bool FileIsValid(IFormFile file)
        {
            List<Invoice> invoiceList = new List<Invoice>();

            List<string> err = new List<string>();

            //await return invoiceList;

            if (!FileSizeIsValid(file))
            {
                err.Add("More than 1 mb file.");
            }

            ErrorMessage = String.Join(";", err);

            Invoices = invoiceList;

            return false;
        }
    }
}
