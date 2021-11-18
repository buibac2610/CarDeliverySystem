using Car_Delivery_System.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Car_Delivery_System.Controllers
{
    public class ShowroomController : BaseController
    {
        // GET: Showroom
        public ActionResult ShowroomInformation()
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isManager())
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = _db.ShowroomInformations.FirstOrDefault(x => x.Id > 0);
                if (model == null)
                {
                    model = new ShowroomInformation();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            
        }


        [HttpPost]
        public async Task<ActionResult> UpdateSR(ShowroomInformation model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (model.Id ==0)
                {
                    var SR = new ShowroomInformation();
                    SR.accountno = model.accountno;
                    SR.address = model.address;
                    SR.bank = model.bank;
                    SR.companyname = model.companyname;
                    SR.representative = model.representative;
                    _db.ShowroomInformations.Add(SR);
                    _db.SaveChanges();
                }
                else
                {
                    var SR = _db.ShowroomInformations.FirstOrDefault(x => x.Id > 0);
                    SR.accountno = model.accountno;
                    SR.address = model.address;
                    SR.bank = model.bank;
                    SR.companyname = model.companyname;
                    SR.representative = model.representative;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ShowroomInformation", "Showroom");
        }
        
    }
}