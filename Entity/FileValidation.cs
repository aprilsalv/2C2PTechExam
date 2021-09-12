using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace _2C2PTechExam.Entity
{

    public  class FileValidation: IFileValidation
    {
       

        /// <summary>
        /// reference Error message
        /// </summary>
        public string ErrorMessage
        {
            set;
            get;
        }

        /// <summary>
        /// reference for the Logs
        /// </summary>
        public List<string> Logs
        {
            set;
            get;
        }
      
        /// <summary>
        /// reference for the list of invoices
        /// </summary>
        public List<Invoice> Invoices
        {
            set;
            get;
        }
        
        /// <summary>
        /// reference for the root folder
        /// </summary>
        public string RootFolder { set; get; }
      
        /// <summary>
        /// A reference for the log
        /// </summary>
        public string LogFileName { set; get; }
        
        
        /// <summary>
        /// Should be .csv and .xml 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool IFileValidation.FileIsValid(IFormFile file)
        {
            List<string> err = new List<string>();

            err.Add("Unknown format. ");

            if (!FileSizeIsValid(file))
            {
                err.Add("More than 1 mb file.");
            }

            ErrorMessage = String.Join(" ", err);

            return false;
        }

        /// <summary>
        /// Check if file is more than 1 Mb
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool FileSizeIsValid(IFormFile file)
        {
            return file.Length > 1048576 ? false : true; 
        }

        /// <summary>
        /// Check if currency is existing in the xml lookup
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if date is valid
        /// </summary>
        /// <param name="sDate"></param>
        /// <returns></returns>
        public virtual bool DateIsValid(string sDate)
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


        /// <summary>
        /// To save the log in Log folde
        /// </summary>
        public void StoreTheLogs()
        {

            var fileName = Path.Combine(RootFolder, @"Log\", DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".txt");

            LogFileName = fileName;

            using (var writer = File.CreateText(fileName))
            {
                foreach (var line in Logs)
                {
                    writer.WriteLine(line);
                }
            }

        }


    }

}
