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
    public class NVSOController : BaseController
    {
        // GET: NVSO
        #region webpages
        public ActionResult ManageNVSO()
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
                var model = new NVSOViewModel();

                model.listNVSO = _db.NVSOes.Where(x => x.Id > 0).ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }

        public ActionResult NVSODetail(int id, string success = null)
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
                var model = new NVSODetailViewModel();
                model.curNVSO = _db.NVSOes.FirstOrDefault(x => x.Id == id);
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
        public async Task<ActionResult> EditNVSO(NVSODetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                var carConfigurations = _db.CarConfigurations.Where(x => x.Id == model.curNVSO.configurationCode).FirstOrDefault();
                var carModels = _db.CarModels.Where(x => x.Id == model.curNVSO.modelCode).FirstOrDefault();
                var interiorColors = _db.InteriorColors.Where(x => x.Id == model.curNVSO.interiorcolorCode).FirstOrDefault();
                var exteriorColors = _db.ExteriorColors.Where(x => x.Id == model.curNVSO.exteriorcolorCode).FirstOrDefault();
                long vehicleamount = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;

                var item = _db.NVSOes.FirstOrDefault(x => x.Id == model.curNVSO.Id);
                if (item != null && item.state == (int)Constants.OrderStatus.Open)
                {
                    item.configurationCode = model.curNVSO.configurationCode;
                    item.modelCode = model.curNVSO.modelCode;
                    item.interiorcolorCode = model.curNVSO.interiorcolorCode;
                    item.exteriorcolorCode = model.curNVSO.exteriorcolorCode;
                    item.configuration = carConfigurations.name;
                    item.model = carModels.name;
                    item.interiorcolor = interiorColors.name;
                    item.exteriorcolor = exteriorColors.name;
                    item.discount = model.curNVSO.discount;
                    item.totalamount = vehicleamount - model.curNVSO.discount;
                    item.vehicleamount = vehicleamount;
                    item.contractDate = model.curNVSO.contractDate;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("NVSODetail/"+model.curNVSO.Id, "NVSO");
        }

        [HttpPost]
        public async Task<string> ApproveNVSO(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "False";
                }
                var item = _db.NVSOes.FirstOrDefault(x => x.Id == id);
                if (item != null && item.state == (int)Constants.OrderStatus.OnApproval)
                {
                    item.state = (int)Constants.OrderStatus.Approved;
                    _db.SaveChanges();

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }

            return "Success";
        }

        [HttpPost]
        public async Task<string> DeleteNVSO(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var item = _db.NVSOes.Where(x => x.Id == id).FirstOrDefault();
                if (item.state !=(int) Constants.OrderStatus.Open)
                {
                    return "Cannot delete NVSO not Open";
                }
                var nvsq = _db.NVSQs.FirstOrDefault(x => x.Id == item.nvsq);
                nvsq.state =(int) Constants.OrderStatus.Open;
                _db.SaveChanges();

                _db.NVSOes.Remove(item);
                _db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<string> SendToApproval(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var item = _db.NVSOes.Where(x => x.Id == id).FirstOrDefault();
                if (item.state != (int)Constants.OrderStatus.Open)
                {
                    return "Cannot action with NVSO not Open";
                }
                item.state = (int)Constants.OrderStatus.OnApproval;
                _db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<string> CreateNVDO(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "/Account/Login";
                }
                var item = _db.NVSOes.FirstOrDefault(x => x.Id == id);
                if (item != null && item.state == (int)Constants.OrderStatus.Approved)
                {
                    var carConfigurations = _db.CarConfigurations.Where(x => x.Id == item.configurationCode).FirstOrDefault();
                    var carModels = _db.CarModels.Where(x => x.Id == item.modelCode).FirstOrDefault();
                    var interiorColors = _db.InteriorColors.Where(x => x.Id == item.interiorcolorCode).FirstOrDefault();
                    var exteriorColors = _db.ExteriorColors.Where(x => x.Id == item.exteriorcolorCode).FirstOrDefault();
                    var customer = _db.Customers.Where(x => x.Id == item.customerId).FirstOrDefault();
                    long vehicleamount = carConfigurations.price + carModels.price + interiorColors.price + exteriorColors.price;

                    var newNVDO = new NVDO();
                    newNVDO.configurationCode = item.configurationCode;
                    newNVDO.modelCode = item.modelCode;
                    newNVDO.interiorcolorCode = item.interiorcolorCode;
                    newNVDO.exteriorcolorCode = item.exteriorcolorCode;
                    newNVDO.customerId = item.customerId;
                    newNVDO.customerName = customer.name;
                    newNVDO.configuration = carConfigurations.name;
                    newNVDO.model = carModels.name;
                    newNVDO.interiorcolor = interiorColors.name;
                    newNVDO.exteriorcolor = exteriorColors.name;
                    newNVDO.deliveryDate = DateTime.Now;
                    newNVDO.nvso = id;
                    var check = _db.NVDOes.Add(newNVDO);
                    _db.SaveChanges();

                    item.state = (int)Constants.OrderStatus.Deliveried;
                    _db.SaveChanges();

                    var listVDO = _db.NVDOes.Where(x => x.Id > 0).ToList();
                    var lastVDO = listVDO[listVDO.Count - 1];
                    return "/NVDO/NVDODetail/" + lastVDO.Id;
                }
                return "/NVSO/ManageNVSO";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
        #endregion

    }
}