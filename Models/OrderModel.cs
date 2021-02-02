using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItVitae_RepairShop_project8.Models
{
    public class OrderModel
    {
        public int Id;
        public int PartLinkId;
        public int UserId;
        public int EmployeeId;
        public string OrderDiscription;
        public int ImageID;
        public double OrderPrice;
        public int OrderStatus;
    }
}