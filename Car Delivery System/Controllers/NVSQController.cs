using Car_Delivery_System.CommonEntities;
using Car_Delivery_System.Controllers;
using Car_Delivery_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NVSQ_Delivery_System.Controllers
{
    /// <summary>
    /// New Vehicle Sale Quote
    /// </summary>
    public class NVSQController : BaseController
    {
        // GET: NVSQ
        #region webpages
        public ActionResult ManageNVSQ()
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isSales())
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = new NVSQViewModel();

                model.listNVSQ = _db.NVSQs.Where(x => x.Id > 0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }

        public ActionResult NVSQDetail(int id, string success = null)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isSales())
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = new NVSQDetailViewModel();
                model.curNVSQ = _db.NVSQs.FirstOrDefault(x => x.Id == id);
                model.carModels = _db.CarModels.Where(x => x.Id > 0).ToList();
                model.carConfigurations = _db.CarConfigurations.Where(x => x.Id > 0).ToList();
                model.interiorColors = _db.InteriorColors.Where(x => x.Id > 0).ToList();
                model.exteriorColors = _db.ExteriorColors.Where(x => x.Id > 0).ToList();

                model.listCus = _db.Customers.Where(x => x.Id > 0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }

        public ActionResult NVSQCreate()
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isSales())
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = new NVSQDetailViewModel();
                model.carModels = _db.CarModels.Where(x => x.Id > 0).ToList();
                model.carConfigurations = _db.CarConfigurations.Where(x => x.Id > 0).ToList();
                model.interiorColors = _db.InteriorColors.Where(x => x.Id > 0).ToList();
                model.exteriorColors = _db.ExteriorColors.Where(x => x.Id > 0).ToList();

                model.listCus = _db.Customers.Where(x => x.Id > 0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }
        #endregion

        #region Web Methods
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNVSQ(NVSQDetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                var carConfigurations = _db.CarConfigurations.Where(x => x.Id == model.curNVSQ.configurationCode).FirstOrDefault();
                var carModels = _db.CarModels.Where(x => x.Id == model.curNVSQ.modelCode).FirstOrDefault();
                var interiorColors = _db.InteriorColors.Where(x => x.Id == model.curNVSQ.interiorcolorCode).FirstOrDefault();
                var exteriorColors = _db.ExteriorColors.Where(x => x.Id == model.curNVSQ.exteriorcolorCode).FirstOrDefault();
                var customer = _db.Customers.Where(x => x.Id == model.curNVSQ.customerId).FirstOrDefault();
                long vehicleamount = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;

                var item = new NVSQ();
                item.configurationCode = model.curNVSQ.configurationCode;
                item.modelCode = model.curNVSQ.modelCode;
                item.interiorcolorCode = model.curNVSQ.interiorcolorCode;
                item.exteriorcolorCode = model.curNVSQ.exteriorcolorCode;
                item.customerId = model.curNVSQ.customerId;
                item.customerName = customer.name;
                item.configuration = carConfigurations.name;
                item.model = carModels.name;
                item.interiorcolor = interiorColors.name;
                item.exteriorcolor = exteriorColors.name;
                item.discount = model.curNVSQ.discount;
                item.totalamount = vehicleamount - model.curNVSQ.discount;
                item.vehicleamount = vehicleamount;
                _db.NVSQs.Add(item);
                _db.SaveChanges();

                var listVSQ = _db.NVSQs.Where(x => x.Id > 0).ToList();
                var lastVSQ = listVSQ[listVSQ.Count - 1];
                return RedirectToAction("NVSQDetail/" + lastVSQ.Id, "NVSQ");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }


        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditNVSQ(NVSQDetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }

                var carConfigurations = _db.CarConfigurations.Where(x => x.Id == model.curNVSQ.configurationCode).FirstOrDefault();
                var carModels = _db.CarModels.Where(x => x.Id == model.curNVSQ.modelCode).FirstOrDefault();
                var interiorColors = _db.InteriorColors.Where(x => x.Id == model.curNVSQ.interiorcolorCode).FirstOrDefault();
                var exteriorColors = _db.ExteriorColors.Where(x => x.Id == model.curNVSQ.exteriorcolorCode).FirstOrDefault();
                var customer = _db.Customers.Where(x => x.Id == model.curNVSQ.customerId).FirstOrDefault();
                long vehicleamount = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;

                var item = _db.NVSQs.FirstOrDefault(x => x.Id == model.curNVSQ.Id);
                if (item != null && item.state == (int)Constants.OrderStatus.Open)
                {
                    item.configurationCode = model.curNVSQ.configurationCode;
                    item.modelCode = model.curNVSQ.modelCode;
                    item.interiorcolorCode = model.curNVSQ.interiorcolorCode;
                    item.exteriorcolorCode = model.curNVSQ.exteriorcolorCode;
                    item.customerId = model.curNVSQ.customerId;
                    item.customerName = customer.name;
                    item.configuration = carConfigurations.name;
                    item.model = carModels.name;
                    item.interiorcolor = interiorColors.name;
                    item.exteriorcolor = exteriorColors.name;
                    item.discount = model.curNVSQ.discount;
                    item.totalamount = vehicleamount - model.curNVSQ.discount;
                    item.vehicleamount = vehicleamount;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home");
            }

            return RedirectToAction("NVSQDetail/" + model.curNVSQ.Id, "NVSQ");
        }

        [HttpPost]
        public async Task<string> ConvertToNVSO(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "/Account/Login";
                }
                var item = _db.NVSQs.FirstOrDefault(x => x.Id == id);
                if (item != null && item.state == (int)Constants.OrderStatus.Open)
                {
                    var carConfigurations = _db.CarConfigurations.Where(x => x.Id == item.configurationCode).FirstOrDefault();
                    var carModels = _db.CarModels.Where(x => x.Id == item.modelCode).FirstOrDefault();
                    var interiorColors = _db.InteriorColors.Where(x => x.Id == item.interiorcolorCode).FirstOrDefault();
                    var exteriorColors = _db.ExteriorColors.Where(x => x.Id == item.exteriorcolorCode).FirstOrDefault();
                    var customer = _db.Customers.Where(x => x.Id == item.customerId).FirstOrDefault();
                    long vehicleamount = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;

                    var newNVSO = new NVSO();
                    newNVSO.configurationCode = item.configurationCode;
                    newNVSO.modelCode = item.modelCode;
                    newNVSO.interiorcolorCode = item.interiorcolorCode;
                    newNVSO.exteriorcolorCode = item.exteriorcolorCode;
                    newNVSO.customerId = item.customerId;
                    newNVSO.customerName = customer.name;
                    newNVSO.configuration = carConfigurations.name;
                    newNVSO.model = carModels.name;
                    newNVSO.interiorcolor = interiorColors.name;
                    newNVSO.exteriorcolor = exteriorColors.name;
                    newNVSO.discount = item.discount;
                    newNVSO.totalamount = vehicleamount - item.discount;
                    newNVSO.vehicleamount = vehicleamount;
                    newNVSO.contractDate = DateTime.Now;
                    newNVSO.nvsq = id;
                    _db.NVSOes.Add(newNVSO);
                    _db.SaveChanges();

                    item.state = (int)Constants.OrderStatus.Converted;
                    _db.SaveChanges();

                    var listVSO = _db.NVSOes.Where(x => x.Id > 0).ToList();
                    var lastVSO = listVSO[listVSO.Count - 1];
                    return "/NVSO/NVSODetail/" + lastVSO.Id;
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }

            return "/NVSQ/NVSQDetail/" + id;
        }

        [HttpPost]
        public async Task<string> DeleteNVSQ(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var item = _db.NVSQs.Where(x => x.Id == id).FirstOrDefault();
                if (item.state != (int)Constants.OrderStatus.Open)
                {
                    return "Cannot delete NVSQ with state not Open";
                }
                _db.NVSQs.Remove(item);
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