//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DtdcCashCounter.EntityFr
{
    using System;
    using System.Collections.Generic;
    
    public partial class NEFT
    {
        public int Neft_id { get; set; }
        public Nullable<double> NeftAmount { get; set; }
        public Nullable<System.DateTime> neftdate { get; set; }
        public string tempneftdate { get; set; }
        public string Invoiceno { get; set; }
        public string Transaction_Id { get; set; }
        public Nullable<double> N_Tds_Amount { get; set; }
        public Nullable<double> N_Total_Amount { get; set; }
    }
}
