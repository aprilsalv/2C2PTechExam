using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Entity
{

    public  class FileValidation: IFileValidation
    {
       

        public string ErrorMessage
        {
            set;
            get;
        }


        public List<Invoice> Invoices
        {
            set;
            get;
        }



        bool IFileValidation.FileIsValid(IFormFile file)
        {
            List<string> err = new List<string>();

           // ErrorMessage = "Wrong Format";

            err.Add("Wrong Format");

            if (!FileSizeIsValid(file))
            {
                err.Add("More than 1 mb file.");
            }

            ErrorMessage = String.Join(";", err);

            return false;
        }


        public bool FileSizeIsValid(IFormFile file)
        {
            return file.Length > 1048576 ? false : true; 
        }

    }

}
