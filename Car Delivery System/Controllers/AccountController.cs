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
    public class AccountController : BaseController
    {
        #region webpages
        public ActionResult Login(string returnUrl)
        {
            if (Session["user"] != null & Session["email"] != null && Session["role"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["role"] = null;
            Session["email"] = null;
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            if (Session["user"] != null & Session["email"] != null && Session["role"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            if (!validateLoggedInUser())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult ManagerUser()
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isAdmin())
                {
                    return RedirectToAction("Index", "Home");
                }

                var model = new UserManagementViewModel();
                string cur_email = getCurrentEmail();
                var listuser = _db.DMSUsers.Where(x => x.email != cur_email).ToList();

                model.ListUser = listuser;
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }

        }

        public ActionResult UserDetail(int id, string success = null)
        {
 
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                if (!isAdmin())
                {
                    return RedirectToAction("Index", "Home");
                }
                var user = _db.DMSUsers.FirstOrDefault(x=>x.Id == id);
                var role = _db.Roles.FirstOrDefault(x => x.Id == user.role);
                var rolelist = _db.Roles.Where(x => x.Id > 0).ToList();

                var model = new UserDetailViewModel();
                model.Role = role;
                model.RoleList = rolelist;
                model.User = user;

                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }

        #endregion

        #region Web Methods
        // POST: Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (model == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //save user vao session
                string pass = HashingText.Hash(model.Password);
                var usercheck = _db.DMSUsers.Where(x => x.email == model.Email && x.password == pass).FirstOrDefault();
                if (usercheck != null)
                {
                    Session["user"] = usercheck.name;
                    Session["role"] = usercheck.role;
                    Session["email"] = usercheck.email;
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (model != null && !string.IsNullOrWhiteSpace(model.Name) && !string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.Password))
                {
                    var check = _db.DMSUsers.Where(x => x.email == model.Email).FirstOrDefault();
                    if (check == null)
                    {
                        DMSUser user = new DMSUser() { email = model.Email, name = model.Name, role = (int)Constants.Roles.SaleAdvisor, password = HashingText.Hash(model.Password) };
                        _db.DMSUsers.Add(user);
                        _db.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        return RedirectToAction("Register", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ResetPasswordViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var email = getCurrentEmail();
                    var checkuser = _db.DMSUsers.Where(x => x.email == email).FirstOrDefault();
                    if (checkuser != null)
                    {
                        checkuser.password = HashingText.Hash(model.Password);
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> UserDetail(UserDetailViewModel model)
        {
            try
            {
                var _user = _db.DMSUsers.Where(x => x.Id == model.User.Id).FirstOrDefault();
                _user.role = model.User.role;
                _user.name = model.User.name;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("ManagerUser", "Account");
        }

        [HttpPost]
        public async Task<string> DeleteUser(int id)
        {
            try
            {
                if (!validateLoggedInUser())
                {
                    return "false";
                }
                var user = _db.DMSUsers.Where(x => x.Id == id).FirstOrDefault();
                _db.DMSUsers.Remove(user);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Success";
        }
        #endregion
    }
}