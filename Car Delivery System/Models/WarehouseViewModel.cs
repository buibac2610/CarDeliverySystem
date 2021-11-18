using Car_Delivery_System.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_Delivery_System.Models
{
    public class WarehouseViewModel
    {
    }

    public class CarsViewModel
    {
        public List<Car> listCar { get; set; }
    }

    public class CarDetailViewModel
    {
        public Car currentCar { get; set; }
        public List<CarConfiguration> carConfigurations { get; set; }
        public List<CarModel> carModels { get; set; }
        public List<ExteriorColor> exteriorColors { get; set; }
        public List<InteriorColor> interiorColors { get; set; }
    }

    public class PartsViewModel
    {
        public List<CarPart> listPart { get; set; }
    }
    public class PartDetailViewModel
    {
        public CarPart currentPart { get; set; }
        public List<CarModel> carModels { get; set; }
    }


    #region dbdata
    //public class Car
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
    //    public string VIN { get; set; }
    //    public string engineNo { get; set; }
    //    public int state { get; set; }
    //    public int price { get; set; }
    //}

    //public class CarConfiguration
    //{
    //    public int Id { get; set; }
    //    public string name { get; set; }
    //    public int price { get; set; }

    //}

    //public class ExteriorColor
    //{
    //    public int Id { get; set; }
    //    public string name { get; set; }
    //    public int price { get; set; }
    //}

    //public class InteriorColor
    //{
    //    public int Id { get; set; }
    //    public string name { get; set; }
    //    public int price { get; set; }
    //}


    //public class CarModel
    //{
    //    public int Id { get; set; }
    //    public string name { get; set; }
    //    public int price { get; set; }
    //}

    //public class CarPart
    //{
    //    public int Id { get; set; }
    //    public string name { get; set; }
    //    public string carmodel { get; set; }
    //    public int carmodelCode { get; set; }
    //    public int qty { get; set; }
    //    public int unitprice { get; set; }
    //}
    #endregion
}