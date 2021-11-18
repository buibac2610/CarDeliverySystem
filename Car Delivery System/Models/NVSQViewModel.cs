using Car_Delivery_System.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Delivery_System.Models
{

    public class NVSQViewModel
    {
        public List<NVSQ> listNVSQ { get; set; }
    }

    public class NVSQDetailViewModel
    {
        public NVSQ curNVSQ { get; set; }

        public List<Customer> listCus { get; set; }
        public List<CarConfiguration> carConfigurations { get; set; }
        public List<CarModel> carModels { get; set; }
        public List<ExteriorColor> exteriorColors { get; set; }
        public List<InteriorColor> interiorColors { get; set; }
    }

    //public class NVSQ
    //{
    //    public int Id { get; set; }
    //    public int modelCode { get; set; }
    //    public int configurationCode { get; set; }
    //    public int exteriorcolorCode { get; set; }
    //    public int interiorcolorCode { get; set; }
    //    public string configuration { get; set; }
    //    public string exteriorcolor { get; set; }
    //    public string interiorcolor { get; set; }
    //    public string model { get; set; }
    //    public int state { get; set; }
    //    public int customerId { get; set; }
    //    public string customerName { get; set; }

    //    public float totalamount { get; set; }
    //    public float discount { get; set; }
    //    public float vehicleamount { get; set; }
    //}
}