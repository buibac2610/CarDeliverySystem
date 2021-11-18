using Car_Delivery_System.CommonEntities;
using Car_Delivery_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Car_Delivery_System.Controllers
{
    public class WarehouseController : BaseController
    {
        #region webpages
        public ActionResult ManageCars()
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isWarehouse())
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = new CarsViewModel();

                model.listCar = _db.Cars.Where(x => x.Id > 0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }

        public ActionResult CarDetail(int id, string success = null)
        {
            CarDetailViewModel model = new CarDetailViewModel();
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isWarehouse())
                {
                    return RedirectToAction("Index", "Home");
                }
                model.carModels = _db.CarModels.Where(x => x.Id > 0).ToList();
                model.carConfigurations = _db.CarConfigurations.Where(x => x.Id > 0).ToList();
                model.interiorColors = _db.InteriorColors.Where(x => x.Id > 0).ToList();
                model.exteriorColors = _db.ExteriorColors.Where(x => x.Id > 0).ToList();
                model.currentCar = _db.Cars.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }


            return View(model);
        }

        public ActionResult CarCreate()
        {
            CarDetailViewModel model = new CarDetailViewModel();
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isWarehouse())
                {
                    return RedirectToAction("Index", "Home");
                }
                model.carModels = _db.CarModels.Where(x => x.Id > 0).ToList();
                model.carConfigurations = _db.CarConfigurations.Where(x => x.Id > 0).ToList();
                model.interiorColors = _db.InteriorColors.Where(x => x.Id > 0).ToList();
                model.exteriorColors = _db.ExteriorColors.Where(x => x.Id > 0).ToList();

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }


            return View(model);

        }

        public ActionResult ManageParts()
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isWarehouse())
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = new PartsViewModel();

                model.listPart = _db.CarParts.Where(x => x.Id > 0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }
        public ActionResult PartCreate()
        {
            PartDetailViewModel model = new PartDetailViewModel();
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isWarehouse())
                {
                    return RedirectToAction("Index", "Home");
                }
                model.carModels = _db.CarModels.Where(x => x.Id > 0).ToList();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return View(model);

        }

        public ActionResult PartDetail(int id, string success = null)
        {
            PartDetailViewModel model = new PartDetailViewModel();
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isWarehouse())
                {
                    return RedirectToAction("Index", "Home");
                }
                model.carModels = _db.CarModels.Where(x => x.Id > 0).ToList();
                model.currentPart = _db.CarParts.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return View(model);
        }
        #endregion

        #region Web Methods
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CarDetail(CarDetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }

                if (model != null)
                {
                    var carConfigurations = _db.CarConfigurations.Where(x => x.Id == model.currentCar.configurationCode).FirstOrDefault();
                    var carModels = _db.CarModels.Where(x => x.Id == model.currentCar.modelCode).FirstOrDefault();
                    var interiorColors = _db.InteriorColors.Where(x => x.Id == model.currentCar.interiorcolorCode).FirstOrDefault();
                    var exteriorColors = _db.ExteriorColors.Where(x => x.Id == model.currentCar.exteriorcolorCode).FirstOrDefault();
                    long totalprice = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;

                    Car oldcar = _db.Cars.Where(x => x.Id == model.currentCar.Id).FirstOrDefault();
                    oldcar.configurationCode = model.currentCar.configurationCode;
                    oldcar.modelCode = model.currentCar.modelCode;
                    oldcar.interiorcolorCode = model.currentCar.interiorcolorCode;
                    oldcar.exteriorcolorCode = model.currentCar.exteriorcolorCode;
                    oldcar.VIN = model.currentCar.VIN;
                    oldcar.engineNo = model.currentCar.engineNo;
                    oldcar.price = totalprice;
                    oldcar.configuration = carConfigurations.name;
                    oldcar.model = carModels.name;
                    oldcar.interiorcolor = interiorColors.name;
                    oldcar.exteriorcolor = exteriorColors.name;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ManageCars", "Warehouse");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CarCreate(CarDetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }

                if (model != null)
                {
                    var carConfigurations = _db.CarConfigurations.Where(x => x.Id == model.currentCar.configurationCode).FirstOrDefault();
                    var carModels = _db.CarModels.Where(x => x.Id == model.currentCar.modelCode).FirstOrDefault();
                    var interiorColors = _db.InteriorColors.Where(x => x.Id == model.currentCar.interiorcolorCode).FirstOrDefault();
                    var exteriorColors = _db.ExteriorColors.Where(x => x.Id == model.currentCar.exteriorcolorCode).FirstOrDefault();

                    long totalprice = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;
                    Car newcar = new Car() { modelCode = model.currentCar.modelCode, model = carModels.name, configurationCode = model.currentCar.configurationCode, configuration = carConfigurations.name, exteriorcolorCode = model.currentCar.exteriorcolorCode, exteriorcolor = exteriorColors.name, interiorcolor = interiorColors.name, interiorcolorCode = model.currentCar.interiorcolorCode, VIN = model.currentCar.VIN, engineNo = model.currentCar.engineNo, state = (int)Constants.CarStatus.Available, price = totalprice };
                    _db.Cars.Add(newcar);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ManageCars", "Warehouse");
        }

        [HttpPost]
        public async Task<string> DeleteCar(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var car = _db.Cars.Where(x => x.Id == id).FirstOrDefault();
                _db.Cars.Remove(car);
                _db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> PartDetail(PartDetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (model != null)
                {
                    var carModels = _db.CarModels.Where(x => x.Id == model.currentPart.carmodelCode).FirstOrDefault();

                    CarPart oldcarpart = _db.CarParts.Where(x => x.Id == model.currentPart.Id).FirstOrDefault();
                    oldcarpart.carmodelCode = model.currentPart.carmodelCode;
                    oldcarpart.carmodel = carModels.name;
                    oldcarpart.name = model.currentPart.name;
                    oldcarpart.qty = model.currentPart.qty;
                    oldcarpart.unitprice = model.currentPart.unitprice;
                    _db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ManageParts", "Warehouse");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> PartCreate(PartDetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (model != null)
                {
                    var carModels = _db.CarModels.Where(x => x.Id == model.currentPart.carmodelCode).FirstOrDefault();

                    CarPart newcarpart = new CarPart() { carmodelCode = model.currentPart.carmodelCode, carmodel = carModels.name, name = model.currentPart.name, qty = model.currentPart.qty, unitprice = model.currentPart.unitprice };
                    _db.CarParts.Add(newcarpart);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ManageParts", "Warehouse");
        }

        [HttpPost]
        public async Task<string> DeletePart(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var item = _db.CarParts.Where(x => x.Id == id).FirstOrDefault();
                _db.CarParts.Remove(item);
                _db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}