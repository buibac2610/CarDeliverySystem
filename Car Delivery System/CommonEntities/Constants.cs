using Car_Delivery_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Delivery_System.CommonEntities
{
    public class Constants
    {
        public enum OrderStatus
        {
            [Display(Name = "Open")]
            Open = 0,
            [Display(Name = "Converted")]
            Converted = 1,
            [Display(Name = "On Approval")]
            OnApproval = 2,
            [Display(Name = "Approved")]
            Approved = 3,
            [Display(Name = "Deliveried")]
            Deliveried = 4,
            [Display(Name = "Completed")]
            Completed = 5,

        };

        public enum CarStatus
        {
            [Display(Name = "Available")]
            Available = 1,
            [Display(Name = "Matched")]
            Matched = 2,
            [Display(Name = "Sold")]
            Sold = 3
        };


        public enum Roles
        {
            [Display(Name = "Sale Advisor")]
            SaleAdvisor = 1,
            [Display(Name = "Sale Manager")]
            SaleManager = 2,
            [Display(Name = "Warehouse Staff")]
            WarehouseStaff = 3,
            [Display(Name = "System Admin")]
            SystemAdmin = 4
        };

        public static List<CommonState> WarehouseState = new List<CommonState>() {
        new CommonState(){Id=1,name="Available "},
        new CommonState(){Id=2,name="Matched"},
        new CommonState(){Id=3,name="Sold"}
        };

        public static List<CommonState> OrderState = new List<CommonState>() {
        new CommonState(){Id=0,name="Open"},
        new CommonState(){Id=1,name="Converted"},
        new CommonState(){Id=2,name="On Approval"},
        new CommonState(){Id=3,name="Approved"},
        new CommonState(){Id=4,name="Deliveried"},
        new CommonState(){Id=5,name="Completed"},
        };

    }

    public class CommonState
    {
        public int Id { get; set; }
        public string name { get; set; }

    }
}