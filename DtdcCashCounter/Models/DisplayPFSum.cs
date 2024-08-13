using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DtdcCashCounter.Models
{
    public class DisplayPFSum
    {
        public string PfCode { get; set; }
        public double ? Sum { get; set; }

        public string Branchname { get; set; }

        public Nullable<DateTime> fromdate { get; set; }
        public Nullable<DateTime> Todate { get; set; }
    }
}