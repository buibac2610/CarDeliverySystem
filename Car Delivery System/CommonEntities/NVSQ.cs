//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Car_Delivery_System.CommonEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class NVSQ
    {
        public int Id { get; set; }
        public int modelCode { get; set; }
        public int configurationCode { get; set; }
        public int exteriorcolorCode { get; set; }
        public int interiorcolorCode { get; set; }
        public string configuration { get; set; }
        public string exteriorcolor { get; set; }
        public string interiorcolor { get; set; }
        public string model { get; set; }
        public int state { get; set; }
        public int customerId { get; set; }
        public string customerName { get; set; }
        public int nvsq1 { get; set; }
        public long totalamount { get; set; }
        public long discount { get; set; }
        public long vehicleamount { get; set; }
    }
}
