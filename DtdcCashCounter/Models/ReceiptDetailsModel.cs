using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DtdcCashCounter.Models
{
    public class ReceiptDetailsModel
    {
        public string Consignment_No { get; set; }
        public string Sender { get; set; }
        public string sender_phone { get; set; }
        public string ReciepentsPincode { get; set; }
        public DateTime? Datetime_Cons { get; set; }
        public string Destination { get; set; }
        public double Actual_Weight { get; set; }
        public double volumetric_Weight { get; set; }
        public int Shipment_Quantity { get; set; }
        public double Charges_Total { get; set; }
        public double Charges_Amount { get; set; }
    }
}