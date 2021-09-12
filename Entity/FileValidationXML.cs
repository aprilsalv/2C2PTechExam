using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            List<string> err = new List<string>();

            //await return invoiceList;

            if (!FileSizeIsValid(file))
            {
                err.Add("More than 1 mb file.");
            }

            ErrorMessage = String.Join(";", err);

            var s = ReadAsStringAsync(file);

            return s.Result;
        }

        private async Task<bool> ReadAsStringAsync(IFormFile file)
        {

            XElement xml = XElement.Load(file.OpenReadStream());


            var invoiceList = (from message in xml.Elements("Transaction")
                                select new
                                {
                                    TransactionIdentificator = message.Attribute("id").Value,
                                    Amount = message.Element("PaymentDetails").Element("Amount").Value,
                                    CurrencyCode = message.Element("PaymentDetails").Element("CurrencyCode").Value,
                                    TransactionDate = message.Element("TransactionDate").Value,
                                    Status = message.Element("Status").Value

                                }).ToList();



            foreach (dynamic item in invoiceList)
            {
                //string name = item.Nasddd;
                //int id = item.Id;
            }

            return ValidateRecord(invoiceList);

        }

        public override bool DateIsValid(string sDate)
        {

            DateTime dt;
            if (DateTime.TryParseExact(sDate, "yyyy-MM-ddTHH:mm:ss",
                                       CultureInfo.InvariantCulture, DateTimeStyles.None,
                                       out dt))
            {
                string text = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                // Use text
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool ValidateRecord(IEnumerable<dynamic> invoiceList)
        {

            Invoices = new List<Invoice>();
            List<string> log = new List<string>();
            string[] fields;
            bool recordIsValid = true;
            bool OkToImport = invoiceList.Count<dynamic>() > 0 ? true : false;



            foreach (dynamic record in invoiceList)
            {
                recordIsValid = true;

               

                //check if record has 5 columns
                // record is valid
                StringBuilder strB;
                if (recordIsValid)
                {
                    strB = new StringBuilder();

                    //1. Check the Invoice
                    //1.1 Check the max lenght
                    string id = record.TransactionIdentificator;
                    if (id.Length > 50)
                    {
                        recordIsValid = false;
                        strB.AppendLine("Transaction ID has more than 50 charactres.");
                    }

                    //2. Check the amount
                    decimal dAmount;
                    if (!decimal.TryParse(record.Amount, out dAmount))
                    {
                        //valid 
                        recordIsValid = false;
                        strB.AppendLine("Amount is not correct format.");
                    }

                    //3. Check the amount

                    if (!CurrencyIsValid(record.CurrencyCode))
                    {
                        //valid 
                        recordIsValid = false;
                        strB.AppendLine("Currency is not valid.");
                    }


                    //4. Check the amount
                    if (!DateIsValid(record.TransactionDate))
                    {
                        //valid 
                        recordIsValid = false;
                        strB.AppendLine("Date is not valid.");
                    }

                    List<String> firstlist = new List<String>() { "Approved", "Rejected", "Done" };

                    if (!firstlist.Contains(record.Status))
                    {
                        recordIsValid = false;
                        strB.AppendLine("Status is not valid.");
                    }

                    if (!recordIsValid)
                    {
                        log.Add(record + " [ " + strB.ToString() + " ]");
                    }
                    else
                    {
                        log.Add(record + " [ Record is Ok.]");
                    }

                }

                if (!recordIsValid)
                {
                    OkToImport = false;
                }
                else
                {
                    Invoices.Add(new Invoice()
                    {
                        TransactionIdentificator = record.TransactionIdentificator,
                        Amount = decimal.Parse(record.Amount),
                        CurrenyCode = record.CurrencyCode,
                        TransactionDate = DateTime.Parse(record.TransactionDate),
                        Status = record.Status

                    });


                }


            }





            Logs = log;


                StoreTheLogs();
            

            return OkToImport;

        }


    }
}
