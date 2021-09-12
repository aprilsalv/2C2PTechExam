using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2C2PTechExam.Entity
{

    public  class FileValidation: IFileValidation
    {
       

        public string ErrorMessage
        {
            set;
            get;
        }

        public List<string> Logs
        {
            set;
            get;
        }
        public List<Invoice> Invoices
        {
            set;
            get;
        }
        public string RootFolder { set; get; }

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

        public bool CurrencyIsValid(string currency)
        {

            XmlDocument xml = new XmlDocument();

            var fileName = Path.Combine(RootFolder, @"Asset\CountryCurrencyCode.xml");

        
            if (File.Exists(fileName))
            {
                xml.Load(fileName);
            }
                       
            XmlNodeList xnList = xml.SelectNodes("/ISO_4217/CcyTbl/CcyNtry[Ccy='" + currency + "']");

            if (xnList.Count > 0)
            {
                return true;
            }

            return false;

        }


        public bool DateIsValid(string sDate)
        {

            DateTime dt;
            if (DateTime.TryParseExact(sDate, "dd/MM/yyyy HH:mm:ss",
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



    }

}
