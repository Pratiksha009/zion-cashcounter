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
    
    public partial class Ratem
    {
        public int Rete_Id { get; set; }
        public Nullable<double> slab1 { get; set; }
        public Nullable<double> slab2 { get; set; }
        public Nullable<double> slab3 { get; set; }
        public Nullable<double> slab4 { get; set; }
        public Nullable<int> Sector_Id { get; set; }
        public Nullable<double> Uptosl1 { get; set; }
        public Nullable<double> Uptosl2 { get; set; }
        public Nullable<double> Uptosl3 { get; set; }
        public Nullable<double> Uptosl4 { get; set; }
        public string Company_id { get; set; }
        public Nullable<int> NoOfSlab { get; set; }
        public Nullable<bool> CashCounter { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Company Company1 { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
