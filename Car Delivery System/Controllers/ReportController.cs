using Car_Delivery_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Car_Delivery_System.CommonEntities;

namespace Car_Delivery_System.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index(string message = null)
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
                ViewBag.message = message;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorPage", "Home", ex.Message);
            }
  
        }

        [HttpPost]
        public async Task<ActionResult> ExportRP(ReportViewModel model)
        {

            string message = "";
            try
            {
                if (!validateLoggedInUser())
                {
                    return RedirectToAction("Login", "Account");
                }
                var listVSO = _db.NVSOes.Where(x => x.contractDate < model.endDate && x.contractDate > model.startDate).ToList();

                var export = new ExportToExcel();
                string filepath = "D:";//"D:\\ISD_Export\\
                bool exportResult = export.CreateExcelFile(listVSO,filepath);
                if (exportResult)
                {
                    message = "Exported successfully to the path " + filepath;
                }
                else
                {
                    message = "Export failed";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return RedirectToAction("Index", "Report", new { message = message });
        }

    }
}