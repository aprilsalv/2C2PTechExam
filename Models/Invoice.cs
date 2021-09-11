using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Models
{
    public class Invoice
    {
        public string TransactionIdentificator { get; set; }

        public decimal Amount { get; set; }
      
        public string CurrenyCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Status { get; set; }

    }
}
