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
    public class NVDOController : BaseController
    {
        // GET: NVDO
        #region webpages
        public ActionResult ManageNVDO()
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
                var model = new NVDOViewModel();

                model.listNVDO = _db.NVDOes.Where(x=>x.Id>0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }

        }

        public ActionResult NVDODetail(int id, string success = null)
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
                var model = new NVDODetailViewModel();

                model.curNVDO = _db.NVDOes.FirstOrDefault(x=>x.Id == id);
                model.curNVSO = _db.NVSOes.FirstOrDefault(x => x.Id == model.curNVDO.nvso);
                model.listCus = _db.Customers.Where(x => x.Id > 0).ToList();
                model.listCar = _db.Cars.Where(x => x.state ==(int)Constants.CarStatus.Available 
                && x.modelCode == model.curNVSO.modelCode && x.configurationCode == model.curNVSO.configurationCode && x.exteriorcolorCode == model.curNVSO.exteriorcolorCode && x.interiorcolorCode == model.curNVSO.interiorcolorCode).ToList(); ;
                model.matchedVehicle = model.listCar.Where(x => x.Id == model.curNVDO.vehicleId).FirstOrDefault();
                model.listNVSO = _db.NVSOes.Where(x => x.state ==(int) Constants.OrderStatus.Approved).ToList();
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
        public async Task<ActionResult> EditNVDO(NVDODetailViewModel model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                var oldvdo = _db.NVDOes.FirstOrDefault(x => x.Id == model.curNVDO.Id);
                oldvdo.vehicleId = model.curNVDO.vehicleId;
                oldvdo.deliveryDate = model.curNVDO.deliveryDate;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("NVDODetail/"+model.curNVDO.Id, "NVDO");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<string> ReleaseNVDO(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "False";
                }

                var oldvdo = _db.NVDOes.FirstOrDefault(x => x.Id == id);
                if (oldvdo.vehicleId != null && oldvdo.vehicleId > 0)
                {
                    oldvdo.state = (int)Constants.OrderStatus.Completed;

                    var oldvso = _db.NVSOes.FirstOrDefault(x => x.Id == oldvdo.nvso);
                    oldvso.state = (int)Constants.OrderStatus.Completed;


                    var oldvsq = _db.NVSQs.FirstOrDefault(x => x.Id == oldvso.nvsq);
                    oldvsq.state = (int)Constants.OrderStatus.Completed;

                    var car = _db.Cars.FirstOrDefault(x => x.Id == oldvdo.vehicleId);
                    car.state = (int)Constants.CarStatus.Sold;
                    _db.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Please assign vehicle for this NVDO first";
                }
                
            }
            catch (Exception ex)
            {
                return  ex.Message;
            }

        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<string> DeleteNVDO(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var item = _db.NVDOes.Where(x => x.Id == id).FirstOrDefault();
                if (item.state == (int)Constants.OrderStatus.Open)
                {
                    var oldvso = _db.NVSOes.FirstOrDefault(x => x.Id == item.nvso);
                    oldvso.state = (int)Constants.OrderStatus.Approved;


                    _db.NVDOes.Remove(item);
                    _db.SaveChanges(); 



                    return "Success";
                }
                else
                {
                    return "Cannot delete NVDO not Open";
                }
               
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

    }
}