using Car_Delivery_System.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Delivery_System.Models
{
    public class NVDOViewModel
    {
        public List<NVDO> listNVDO { get; set; }
    }

    public class NVDODetailViewModel
    {
        public NVDO curNVDO { get; set; }
        public NVSO curNVSO { get; set; }

        public List<Customer> listCus { get; set; }
        public Car matchedVehicle { get; set; }

        public List<Car> listCar { get; set; }
        public List<NVSO> listNVSO { get; set; }
    }

    //public class NVDO
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

    //    public int nvso { get; set; }
    //    public int vehicleId { get; set; }
    //    public DateTime deliveryDate { get; set; }
    //}
}