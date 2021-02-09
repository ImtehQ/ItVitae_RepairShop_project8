using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItVitae_RepairShop_project8.Models
{
    public class ViewModels
    {
        
    }
    public class UserModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserEmailConfirm { get; set; }
        public int Auth { get; set; }
        public string UserPassword { get; set; } 
    }
    public class EmployeeModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeEmailAddress { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public double EmployeePay { get; set; }
        public int Auth;
    }
    public class PartModel
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; }
        public double PartPrice { get; set; }
    }
    public class PartLinkModel
    {
        public int Id { get; set; }
        public int PartId { get; set; }
    }
    public class ImageModel
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string ImageData { get; set; }
    }
    public class ImageLinkModel
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int OrderId { get; set; }
    }
    public class OrderModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PartLinkId { get; set; }
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string OrderDiscription { get; set; }
        public int ImageID { get; set; }
        public double OrderPrice { get; set; }
        public int OrderStatus { get; set; }
    }
}