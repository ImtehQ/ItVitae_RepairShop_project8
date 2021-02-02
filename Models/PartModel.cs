using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItVitae_RepairShop_project8.Models
{
    public class PartModel
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public double PartPrice { get; set; }
    }
}