using _2C2PTechExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace _2C2PTechExam.Entity
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private InvoiceContext context;

        private IFileValidation ifileValidate;

        public Repository(InvoiceContext context)
        {
            this.context = context;
        }

        public string RootFolder
        {
            set;
            get;
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public  string UploadFile(IFormFile file)
        {

           string ext = System.IO.Path.GetExtension(file.FileName);

            switch (ext)
            {
                case ".csv":
                    ifileValidate = new FileValidationCSV();
                    break;
                case ".xml":
                    ifileValidate = new FileValidationXML();
                    break;
                default:
                    ifileValidate = new FileValidation();
                    break;
            }

            string rtn = "";
            ifileValidate.RootFolder = RootFolder;

            
            if (ifileValidate.FileIsValid(file))
            {
                context.BulkInsert(ifileValidate.Invoices);

                rtn = "Success" + "|" + ifileValidate.LogFileName;
            }
            else
            {
                rtn = ifileValidate.ErrorMessage + "|" + ifileValidate.LogFileName; 
            }

             return rtn;

       }

        public IEnumerable<dynamic> GetInvoiceByCurrency(string currency)
        {


            var query = (from m in context.Invoices
                         where m.CurrenyCode == currency
                         select new
                         {
                             id = m.TransactionIdentificator,
                             payment = m.Amount + " " + m.CurrenyCode,
                             Status =
                              (
                                    m.Status == "Approved" ? "A" :
                                    m.Status == "Failed" ? "R" :
                                    m.Status == "Rejected" ? "R" :
                                    m.Status == "Finished" ? "D" : 
                                    m.Status == "Done" ? "D" : "X"
                                )
                             
                         }).ToList();


            return query;
        }

        public IEnumerable<dynamic> GetInvoiceByStatus(string status)
        {


            var query = (from m in context.Invoices
                         where m.Status == status
                         select new
                         {
                             id = m.TransactionIdentificator,
                             payment = m.Amount + " " + m.CurrenyCode,
                             Status =
                              (
                                    m.Status == "Approved" ? "A" :
                                    m.Status == "Failed" ? "R" :
                                    m.Status == "Rejected" ? "R" :
                                    m.Status == "Finished" ? "D" :
                                    m.Status == "Done" ? "D" : "X"
                                )

                         }).ToList();


            return query;
        }

        public IEnumerable<dynamic> GetInvoiceByDateRange(string dateFrom, string dateTo)
        {


            var query = (from m in context.Invoices
                         where m.TransactionDate >= DateTime.Parse(dateFrom) && m.TransactionDate <= DateTime.Parse(dateTo)
                         select new
                         {
                             id = m.TransactionIdentificator,
                             payment = m.Amount + " " + m.CurrenyCode,
                             Status =
                              (
                                    m.Status == "Approved" ? "A" :
                                    m.Status == "Failed" ? "R" :
                                    m.Status == "Rejected" ? "R" :
                                    m.Status == "Finished" ? "D" :
                                    m.Status == "Done" ? "D" : "X"
                                )

                         }).ToList();


            return query;
        }
    }
}
