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
    
    public partial class CarPart
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int carmodelCode { get; set; }
        public string carmodel { get; set; }
        public int qty { get; set; }
        public long unitprice { get; set; }
    }
}