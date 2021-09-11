using Microsoft.EntityFrameworkCore;
using _2C2PTechExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        /// <summary>
        /// Create employee
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UploadFile(IFormFile entity)
        {

            List<Invoice> invoiceList;          

            string ext = System.IO.Path.GetExtension(entity.FileName);

            switch (ext)
            {
                case "csv":
                    ifileValidate = new FileValidationCSV();
                break;

                case "xml":
                    ifileValidate = new FileValidationXML();
                    break;

                default:
                    ifileValidate = new FileValidationCSV();
                    break;
            }


            invoiceList = await ifileValidate.Validate(entity);


            //context.BulkInsert(invoiceList);
           

            //await context.SaveChangesAsync();
        }


    }
}
