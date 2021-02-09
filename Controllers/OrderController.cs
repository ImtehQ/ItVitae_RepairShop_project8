using ItVitae_RepairShop_project8.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using RepairShopData.Scripts;
using System.Net;

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
        public ActionResult AddImage(PartModel partModel)
        {
            if (ModelState.IsValid)
            {
               // ViewBag.successState = CreatePart(partModel.PartId, partModel.PartName, partModel.PartPrice);
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
        public ActionResult OrderList()
        {
            List<OrderModel> OrdersList = new List<OrderModel>();
            var data = OrderProcessor.Load();
            foreach (var row in data)
            {
                OrdersList.Add(new OrderModel { OrderId = row.OrderId, PartLinkId = row.PartLinkId, UserId = row.UserId, EmployeeId = row.EmployeeId, OrderDiscription = row.OrderDiscription, ImageID = row.ImageID, OrderPrice = row.OrderPrice, OrderStatus = row.OrderStatus });
            }

            return View(OrdersList);
        }
        public ActionResult CreateOrder()
        {
            var x1 = DataProcessors.Create<OrderModel>(new OrderModel { Id = 6 }, "Orders");
            var x2 = DataProcessors.Update<OrderModel>(new OrderModel { Id = 6 }, "Orders", "Id");
            var x3 = DataProcessors.ListAll<OrderModel>(new OrderModel { Id = 6 }, "Orders");
            var x4 = DataProcessors.ListOne<OrderModel>(new OrderModel { Id = 6 }, "Orders", "Id");

            return View();
        }
        [HttpPost]
        public ActionResult CreateOrder(OrderModel orderModel)
        {
            OrderProcessor.Create(orderModel.PartLinkId, orderModel.UserId, orderModel.EmployeeId, orderModel.OrderDiscription, orderModel.ImageID, orderModel.OrderPrice, orderModel.OrderStatus);
            return View();
        }
        public ActionResult EditOrder(int orderId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditOrder(OrderModel orderModel)
        {
            OrderProcessor.Update(orderModel.Id, orderModel.OrderId, orderModel.PartLinkId, orderModel.UserId, orderModel.EmployeeId, orderModel.OrderDiscription, orderModel.ImageID, orderModel.OrderPrice, orderModel.OrderStatus);
            return View();
        }
        public ActionResult OrderDetails(int orderId)
        {
            var data = OrderProcessor.Load();
            var row = (OrderModel)data.Select(o => o.OrderId == orderId);
            var orderModelDetails = new OrderModel { 
                OrderId = row.OrderId, 
                PartLinkId = row.PartLinkId, 
                UserId = row.UserId, 
                EmployeeId = row.EmployeeId, 
                OrderDiscription = row.OrderDiscription, 
                ImageID = row.ImageID, 
                OrderPrice = row.OrderPrice, 
                OrderStatus = row.OrderStatus };


            return View(orderModelDetails);
        }
        //---------------------------------------------------------------------------------------------
        //=============================================================================================
        //---------------------------------------------------------------------------------------------
        public ActionResult PartsList()
        {
            List<PartModel> PartsList = new List<PartModel>();

            var data = PartProcessor.Load();
            var x = data.Where(d => d.Id > 0);


            foreach (var row in data)
            {
                PartsList.Add(new PartModel { PartId = row.PartId, PartName = row.PartName, PartPrice = row.PartPrice });
            }

            return View(PartsList);
        }
        public ActionResult CreatePart()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePart(PartModel partModel)
        {
            if(ModelState.IsValid)
            {
                ViewBag.successState = PartProcessor.Create(partModel.PartId, partModel.PartName, partModel.PartPrice);
            }
            else
            {
                ViewBag.message = "You did it wrong >:(";
            }

            return View();
        }

        public ActionResult EditPart(int Id)
        {
            //Cant return object of type PartModel, omdat front and back is niet het zelfde.
            //data uit de database is van een andere type en kan je niet uitzelzen, zonder een klote manier 
            //Hele front en back los makeen om deze manier is alles maar niet veel makelijker. ik geef ik.

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPart(PartModel partModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.successState = PartProcessor.Update(partModel.Id, partModel.PartId, partModel.PartName, partModel.PartPrice);
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
        public ActionResult RegisterUser()
        {
            ViewBag.message = "Page not added yet!";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.successState = UserProcessor.Create(userModel.UserId, userModel.UserName, userModel.UserEmail, userModel.Auth, true);
            }
            else
            {
                ViewBag.message = "You did it wrong >:(";
            }

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
            if (ModelState.IsValid)
            {
                ViewBag.successState = UserProcessor.Create(userModel.UserId, userModel.UserName, userModel.UserEmail, userModel.Auth, false);
            }
            else
            {
                ViewBag.message = "You did it wrong >:(";
            }

            return View();
        }
        public ActionResult EditUser()
        {

            return View();
        }
        [HttpPost]
        public ActionResult EditUser(UserModel userModel)
        {
            UserProcessor.Update(userModel.Id, userModel.UserId, userModel.UserName, userModel.UserEmail, userModel.Auth, false);
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUser(UserModel userModel)
        {
            UserProcessor.Remove(userModel.Id);
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
            EmployeeProcessor.Create(employeeModel.EmployeeId, employeeModel.EmployeeEmailAddress, employeeModel.EmployeeFirstName, employeeModel.EmployeeLastName, employeeModel.EmployeePay, employeeModel.Auth);
            return View();
        }
        public ActionResult EditEmployee()
        {
            //EmployeeProcessor.Load();
            return View();
        }
        [HttpPost]
        public ActionResult EditEmployee(EmployeeModel employeeModel)
        {
            EmployeeProcessor.Update(employeeModel.Id, employeeModel.EmployeeId, employeeModel.EmployeeEmailAddress, employeeModel.EmployeeFirstName, employeeModel.EmployeeLastName, employeeModel.EmployeePay, employeeModel.Auth);
            return View();
        }
        public ActionResult EmployeesList()
        {
            List<EmployeeModel> EmployeesList = new List<EmployeeModel>();
            var data = EmployeeProcessor.Load();
            foreach (var row in data)
            {
                EmployeesList.Add(new EmployeeModel { EmployeeId = row.EmployeeId, EmployeeEmailAddress = row.EmployeeEmailAddress, EmployeeFirstName = row.EmployeeFirstName, EmployeeLastName = row.EmployeeLastName, EmployeePay = row.EmployeePay, Auth = row.Auth });
            }

            return View(EmployeesList);
        }
    }
}