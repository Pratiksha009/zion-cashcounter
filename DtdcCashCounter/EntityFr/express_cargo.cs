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
    
    public partial class express_cargo
    {
        public int Exp_Id { get; set; }
        public Nullable<double> Exslab1 { get; set; }
        public Nullable<double> Exslab2 { get; set; }
        public Nullable<double> Upto { get; set; }
        public string Company_id { get; set; }
        public Nullable<int> Sector_Id { get; set; }
        public Nullable<bool> CashCounterExpr { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual Sector Sector1 { get; set; }
    }
}
