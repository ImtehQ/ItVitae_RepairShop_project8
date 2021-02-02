using ItVitae_RepairShop_project8.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using static RepairShopData.Scripts.PartProcessor; 

namespace ItVitae_RepairShop_project8.Controllers
{

    /// Hele project is er wel maar je ziet het niet.
    /// Aan gezien het hele project een directe pcopy is van als die tutorials bijelkaar
    /// Da ik het niet allemaal schijven, het is er het werkt, aleen zie je het niet.
   

    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }








        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(PartModel partModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.successState = CreatePart(partModel.PartId, partModel.PartName, partModel.PartPrice);
            }
            else
            {
                ViewBag.message = "You did it wrong >:(";
            }

            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult PartsList()
        {
            List<PartModel> PartsList = new List<PartModel>();
            var data = LoadParts();
            foreach (var row in data)
            {
                PartsList.Add(new PartModel { PartId = row.PartId, PartName = row.PartName, PartPrice = row.PartPrice });
            }

            return View(PartsList);
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult AddPart()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPart(PartModel partModel)
        {
            if(ModelState.IsValid)
            {
                ViewBag.successState = CreatePart(partModel.PartId, partModel.PartName, partModel.PartPrice);
            }
            else
            {
                ViewBag.message = "You did it wrong >:(";
            }

            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(PartModel partModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.successState = CreatePart(partModel.PartId, partModel.PartName, partModel.PartPrice);
            }
            else
            {
                ViewBag.message = "You did it wrong >:(";
            }

            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
    }
}