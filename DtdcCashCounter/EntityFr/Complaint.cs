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
    
    public partial class Complaint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Complaint()
        {
            this.ReplyAdmins = new HashSet<ReplyAdmin>();
            this.ReplyAdmins1 = new HashSet<ReplyAdmin>();
        }
    
        public long Complaint_ID { get; set; }
        public string Consignment_No { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> Complaint_Date { get; set; }
        public string Company_id { get; set; }
        public string C_Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyAdmin> ReplyAdmins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReplyAdmin> ReplyAdmins1 { get; set; }
    }
}
