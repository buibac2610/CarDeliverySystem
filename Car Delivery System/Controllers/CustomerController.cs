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
    public class CustomerController : BaseController
    {
        #region webpages
        public ActionResult ManageCustomer()
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
                var model = new CustomerViewModel();
                
                model.listCustomer = _db.Customers.Where(x=>x.Id>0).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }

        }

        public ActionResult CustomerDetail(int id, string success = null)
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
                var model = _db.Customers.FirstOrDefault(x => x.Id == id);

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
           
        }

        public ActionResult CustomerCreate()
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
                var model = new Customer();

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
        public async Task<ActionResult> CreateCustomer(Customer model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                var newitem = new Customer();
                newitem.address = model.address;
                newitem.email = model.email;
                newitem.identificationNo = model.identificationNo;
                newitem.name = model.name;
                newitem.phonenumber = model.phonenumber;
                _db.Customers.Add(newitem);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ManageCustomer", "Customer");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomer(Customer model)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                var newitem = _db.Customers.FirstOrDefault(x=>x.Id ==model.Id);
                newitem.address = model.address;
                newitem.email = model.email;
                newitem.identificationNo = model.identificationNo;
                newitem.name = model.name;
                newitem.phonenumber = model.phonenumber;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
            return RedirectToAction("ManageCustomer", "Customer");
        }

        [HttpPost]
        public async Task<string> DeleteCustomer(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }

                var item = _db.Customers.Where(x => x.Id == id).FirstOrDefault();
                _db.Customers.Remove(item);
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