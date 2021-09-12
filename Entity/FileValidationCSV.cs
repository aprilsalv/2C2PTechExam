using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            var s =  ReadAsStringAsync(file);
            
            return s.Result;
        }

        private async Task<bool> ReadAsStringAsync(IFormFile file)
        {
            List<string> result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(await reader.ReadLineAsync());
            }

            return ValidateRecord(result, file);

        }


        private string[] CsvParser(string csvrow)
        {
            const string obscureCharacter = "ᖳ";
            if (csvrow.Contains(obscureCharacter)) throw new Exception("Error: csv row may not contain the " + obscureCharacter + " character");

            var unicodeSeparatedString = "";

            var quotesArray = csvrow.Split('"');  // Split string on double quote character
            if (quotesArray.Length > 1)
            {
                for (var i = 0; i < quotesArray.Length; i++)
                {
                    // CSV must use double quotes to represent a quote inside a quoted cell
                    // Quotes must be paired up
                    // Test if a comma lays outside a pair of quotes.  If so, replace the comma with an obscure unicode character
                    if (Math.Round(Math.Round((decimal)i / 2) * 2) == i)
                    {
                        var s = quotesArray[i].Trim();
                        switch (s)
                        {
                            case ",":
                                quotesArray[i] = obscureCharacter;  // Change quoted comma seperated string to quoted "obscure character" seperated string
                                break;
                        }
                    }
                    // Build string and Replace quotes where quotes were expected.
                    unicodeSeparatedString += (i > 0 ? "\"" : "") + quotesArray[i].Trim();
                }
            }
            else
            {
                // String does not have any pairs of double quotes.  It should be safe to just replace the commas with the obscure character
                unicodeSeparatedString = csvrow.Replace(",", obscureCharacter);
            }

            var csvRowArray = unicodeSeparatedString.Split(obscureCharacter[0]);

            for (var i = 0; i < csvRowArray.Length; i++)
            {
                var s = csvRowArray[i].Trim();
                if (s.StartsWith("\"") && s.EndsWith("\""))
                {
                    csvRowArray[i] = s.Length > 2 ? s.Substring(1, s.Length - 2) : "";  // Remove start and end quotes.
                }
            }

            return csvRowArray;
        }


        private bool ValidateRecord(List<string> records, IFormFile file)
        {

            Invoices = new List<Invoice>();
            List<string> log = new List<string>();
            string[] fields;
            bool recordIsValid = true;
            bool OkToImport = records.Count > 0 ? true : false ;

            foreach (string record in records)
            {
                recordIsValid = true;

                fields = CsvParser(record);

                //check if record has 5 columns
                if (fields.Length != 5)
                {
                    recordIsValid = false;
                    
                    log.Add(record + " [ Record is invalid.]");
                }

                // record is valid
                StringBuilder strB;
                if (recordIsValid)
                {
                    strB = new StringBuilder();

                    //1. Check the Invoice
                    //1.1 Check the max lenght
                    if (fields[0].Length > 50)
                    {
                        recordIsValid = false;
                        strB.AppendLine("Transaction ID has more than 50 charactres.");
                    }

                    //2. Check the amount
                    decimal dAmount;
                    if (!decimal.TryParse(fields[1], out dAmount))
                    {
                        //valid 
                        recordIsValid = false;
                        strB.AppendLine("Amount is not correct format.");
                    }

                    //3. Check the amount

                    if (!CurrencyIsValid(fields[2]))
                    {
                        //valid 
                        recordIsValid = false;
                        strB.AppendLine("Currency is not valid.");
                    }


                    //4. Check the amount
                    if (!DateIsValid(fields[3]))
                    {
                        //valid 
                        recordIsValid = false;
                        strB.AppendLine("Date is not valid.");
                    }

                    List<String> firstlist = new List<String>() { "Approved", "Failed", "Finished" };

                    if (!firstlist.Contains(fields[4]))
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
                        TransactionIdentificator = fields[0],
                        Amount = decimal.Parse(fields[1]),
                        CurrenyCode = fields[2],
                        TransactionDate = DateTime.Parse(fields[3]),
                        Status = fields[4]

                    });


                }


            }

            Logs = log;


            
                StoreTheLogs();
            

            return OkToImport;

        }


    }
}
