using ItVitae_RepairShop_project8.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using ItVitae_RepairShop_project8.RepairShopData.Scripts;

namespace ItVitae_RepairShop_project8.Controllers
{
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
        public ActionResult AddImage(ImageModel imageModel)
        {
            DataProcessors.Create<ImageModel>(imageModel, "Images");
            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult OrderList()
        {
            return View(DataProcessors.ListEverything<OrderModel>("Orders"));
        }
        public ActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrder(OrderModel orderModel)
        {
            DataProcessors.Create(orderModel, "Orders");
            return View();
        }
        public ActionResult EditOrder(int OrderId)
        {
            OrderModel o = DataProcessors.OneById<OrderModel>(OrderId, "Orders");
            List<OrderStatus> orderStatuses = DataProcessors.ListEverything<OrderStatus>("orderStatuses");
            List<SelectListItem> Statuseslist = new List<SelectListItem>();
            foreach (var item in orderStatuses)
            {
                Statuseslist.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }

            ViewBag.statusDropDown = Statuseslist;

            o.OrderStatusLabel = (StatusType)o.OrderStatus; //Small patch
            return View(o);
        }
        [HttpPost]
        public ActionResult EditOrder(OrderModel orderModel)
        {
            orderModel.OrderStatus = (int)orderModel.OrderStatusLabel;
            DataProcessors.Update(orderModel, "Orders", new List<string>{"OrderStatusLabel"});

            return RedirectToAction("OrderDetails", orderModel);
        }
        public ActionResult OrderDetails(OrderModel orderModel)
        {
            return View(DataProcessors.Details(orderModel, "Orders"));
        }
        public ActionResult DeleteOrder(OrderModel orderModel)
        {
            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult PartsList()
        {
            return View(DataProcessors.ListEverything<PartModel>("ComputerParts"));
        }
        public ActionResult CreatePart()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePart(PartModel partModel)
        {
            DataProcessors.Create<PartModel>(partModel, "ComputerParts");
            return View();
        }

        public ActionResult EditPart(int PartId)
        {
            return View(DataProcessors.OneById<PartModel>(PartId, "ComputerParts"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPart(PartModel partModel)
        {
            DataProcessors.Update(partModel, "ComputerParts");
            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserModel userModel)
        {
            //Add fancy stuff here
            DataProcessors.Create<UserModel>(userModel, "Users");
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UserModel userModel)
        {
            DataProcessors.Create<UserModel>(userModel, "Users");
            return View();
        }
        public ActionResult EditUser(int UserId)
        {
            var x = DataProcessors.OneById<UserModel>(UserId, "Users");
            return View(x);
        }
        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            DataProcessors.Update(userModel, "Users");
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(UserModel userModel)
        {
            DataProcessors.Delete("Users", userModel.Id.ToString());
            return View();
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(EmployeeModel employeeModel)
        {
            DataProcessors.Create<EmployeeModel>(employeeModel, "Employees");
            return View();
        }
        public ActionResult EditEmployee(int EmployeeId)
        {
            return View(DataProcessors.OneById<EmployeeModel>(EmployeeId, "Employees"));
        }
        [HttpPost]
        public ActionResult EditEmployee(EmployeeModel employeeModel)
        {
            DataProcessors.Update(employeeModel, "Employees");
            return View();
        }
        public ActionResult EmployeesList()
        {
            return View(DataProcessors.ListEverything<EmployeeModel>("Employees"));
        }
    }
}