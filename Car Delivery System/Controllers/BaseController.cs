using Car_Delivery_System.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Delivery_System.Controllers
{
    public class BaseController : Controller
    {
        public DMSEntities _db = new DMSEntities();

        public bool validateLoggedInUser()
        {
            if (Session["user"] != null & Session["email"] != null && Session["role"] != null)
            {
                return true;
            }
            return false;
        }

        public string getCurrentUser()
        {
            return Session["user"].ToString();
        }

        public string getCurrentEmail()
        {
            return Session["email"].ToString();
        }

        public string getCurrentRole()
        {
            return Session["role"].ToString();
        }

        public bool isSales()
        {
            try
            {
                if(Session["role"].ToString() == ((int)Constants.Roles.SaleAdvisor).ToString() || Session["role"].ToString() == ((int)Constants.Roles.SaleManager).ToString() || Session["role"].ToString() == ((int)Constants.Roles.SystemAdmin).ToString())
                {
                    return true;
                }
            }
            catch (Exception)
            { 
            }
            return false;
        }

        public bool isManager()
        {
            try
            {
                if (Session["role"].ToString() == ((int)Constants.Roles.SaleManager).ToString() || Session["role"].ToString() == ((int)Constants.Roles.SystemAdmin).ToString())
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool isWarehouse()
        {
            try
            {
                if (Session["role"].ToString() == ((int)Constants.Roles.WarehouseStaff).ToString() || Session["role"].ToString() == ((int)Constants.Roles.SystemAdmin).ToString())
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool isAdmin()
        {
            try
            {
                if (Session["role"].ToString() == ((int)Constants.Roles.SystemAdmin).ToString())
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}